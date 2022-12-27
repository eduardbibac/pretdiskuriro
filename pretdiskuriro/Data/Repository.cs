using DbModels;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

namespace pretdiskuriro.Data
{
    public class Repository
    {
        private static DataContext _db = new DataContext();

        public static void AddProducts(List<Product> products)
        {
            // var all = from row in context.Products select row;

            _db.Products.AddRange(products);
            _db.SaveChanges();
        }

        public static Market GetMarketByName(string marketName)
        {
            // Include relation tables 
            var q = from market in _db.Markets
                    where market.Name == marketName
                    select market;
                
            return q.First();
        }

        // TODO: Availabilty dates
        private static void AddNewProducts(List<MarketProduct> scrapes)
        {
            // This function does:
            // OR: Adds the items that were previously not in the database
            // OR: Ads newly listed goods from the retailer
            // OR: Ads the items that have been scraped for the first time


            // get items not contained in inner join
            // inner join = existing
            /* Note that this is a "client evaluation" linq by adding .AsEnumerable()
                https://learn.microsoft.com/en-us/ef/core/querying/client-eval
                becasue we mixed in local data with SQL data
            */
            var querryForNewScrapes = from newScrape in scrapes
                                where !(from product in _db.Products.AsEnumerable()
                                        join scrape in scrapes on product.Title equals scrape.Product.Title
                                        select scrape.Product.Title).Contains(newScrape.Product.Title)
                                select newScrape;

            var newScrapes = querryForNewScrapes.ToList();
            _db.MarketProducts.AddRange(newScrapes);
            _db.SaveChanges();

            //// Set market
            //var market = GetMarketByName(marketName);
            //market.Products.AddRange(newScrapes);
        }

        private static void UpdateExistingProductsPrice(List<MarketProduct> scrapes)
        {
            // Join DailyPrices and Products to get the latest price for each product in the database
            // Then join the result to the newly scraped products on the title
            // AKA Update the products where the price has changed and archive older price
            var productsWithPrice = from price in _db.DailyPrices.AsEnumerable()
                                    join marketProduct in _db.MarketProducts on price.MarketProductId equals marketProduct.Id
                                    where price.EndDate == null
                                    // up TODO: GetProductsWithCurrentPrice() -> how would I make this in a reusable function??hm?
                                    //https://stackoverflow.com/questions/7712951/reusing-a-join-in-linq
                                    join scrape in scrapes on marketProduct.Product.Title equals scrape.Product.Title
                                    where  price.Price != scrape.Prices[0].Price
                                    select new { marketProduct, price, newPrice = scrape.Prices[0].Price };

            // For each product with a changed price, add a new DailyPrice with the new price and mark the old price as ended
            foreach (var p in productsWithPrice) 
            {
                p.marketProduct.Prices.Add(new DailyPrice { Price = p.newPrice, EndDate=null});
                p.price.EndDate = DateTime.Now;
            }
            _db.SaveChanges();
        }

        public static void MergeNewProducts(List<MarketProduct> scrapes)
        {
            AddNewProducts(scrapes);
            UpdateExistingProductsPrice(scrapes);

            // TODO: Eliminate Delisted Products
        }
    }
}
