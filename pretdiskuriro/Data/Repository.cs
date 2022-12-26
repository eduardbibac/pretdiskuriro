using DbModels;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

namespace pretdiskuriro.Data
{
    public class Repository
    {

        public static void AddProducts(List<Product> products)
        {
            using (var context = new DataContext())
            {
                // var all = from row in context.Products select row;

                context.Products.AddRange(products);
                context.SaveChanges();
            }
        }

        private static Market GetMarketByName(string marketName)
        {
            using (var context = new DataContext())
            {
                var markets = context.Markets.Include(o => o.Products).ToList();
                var q = from market in markets
                        where market.Name == marketName
                        select market;
                
                return q.First();
            }
        }

        // TODO: Availabilty dates
        private static void AddNewProducts(List<Product> scrapes, string marketName)
        {
            // This function does:
            // OR: Adds the items that were previously not in the database
            // OR: Ads newly listed goods from the retailer
            // OR: Ads the items that have been scraped for the first time
            using (var context = new DataContext())
            {


                // get items not contained in inner join
                // inner join = existing
                /* Note that this is a "client evaluation" linq by adding .AsEnumerable()
                   https://learn.microsoft.com/en-us/ef/core/querying/client-eval
                   becasue we mixed in local data with SQL data
                */
                var newScrapes = from newScrape in scrapes
                                 where !(from product in context.Products.AsEnumerable()
                                         join scrape in scrapes on product.Title equals scrape.Title
                                        select scrape.Title).Contains(newScrape.Title)
                                 select newScrape;



                
                // Set market
                var market = GetMarketByName(marketName);
                //foreach (var prod in newScrapes)
                //{
                //    prod.Markets.Add(market);
                //}
                market.Products.AddRange(newScrapes);
                context.SaveChanges();
            }
        }

        private static void UpdateExistingProductsPrice(List<Product> scrapes)
        {

            using (var context = new DataContext())
            {
                // Join DailyPrices and Products to get the latest price for each product in the database
                // Then join the result to the newly scraped products on the title
                // AKA Update the products where the price has changed and archive older price
                var productsWithPrice = from price in context.DailyPrices.AsEnumerable()
                                        join product in context.Products on price.ProductId equals product.Id
                                        where price.EndDate == null
                                        // up TODO: GetProductsWithCurrentPrice() -> how would I make this in a reusable function??hm?
                                        //https://stackoverflow.com/questions/7712951/reusing-a-join-in-linq
                                        join scrape in scrapes on product.Title equals scrape.Title
                                        where  price.Price != scrape.Prices[0].Price
                                        select new { product, price, newPrice = scrape.Prices[0].Price };

                // For each product with a changed price, add a new DailyPrice with the new price and mark the old price as ended
                foreach (var p in productsWithPrice) 
                {
                    p.product.Prices.Add(new DailyPrice { Price = p.newPrice, EndDate=null});
                    p.price.EndDate = DateTime.Now;
                }
                context.SaveChanges();
            }
        }

        public static void MergeNewProducts(List<Product> scrapes, string marketName)
        {
            AddNewProducts(scrapes, marketName);
            UpdateExistingProductsPrice(scrapes);

            // TODO: Eliminate Delisted Products
        }
    }
}
