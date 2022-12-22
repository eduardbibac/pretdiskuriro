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

        private static void AddNewProducts(List<Product> scrapes)
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

                context.Products.AddRange(newScrapes);
                context.SaveChanges();
            }
        }

        private static void UpdateExistingProductsPrice(List<Product> scrapes)
        {

            using (var context = new DataContext())
            {
                // We do a join to get the latest price(where it's null) for each item in db 
                //  then we join that to the newly scraped items on TITLE and select the ones where the price is different
                // AKA Update the items where the price changed and move older price to history
                var productsWithPrice = from price in context.DailyPrices.AsEnumerable()
                                        join product in context.Products on price.ProductId equals product.Id
                                        where price.EndDate == null
                                        // up TODO: GetProductsWithCurrentPrice() -> how would I make this in a reusable function??hm?
                                        join scrape in scrapes on product.Title equals scrape.Title
                                        where  price.Price != scrape.Prices[0].Price
                                        select new { product, price, newPrice = scrape.Prices[0].Price };

                // Add new DailyPrice and mark older one as ended (by date)
                foreach (var p in productsWithPrice) 
                {
                    p.product.Prices.Add(new DailyPrice { Price = p.newPrice, EndDate=null});
                    p.price.EndDate = DateTime.Now;
                }
                context.SaveChanges();
            }
        }

        public static void MergeNewProducts(List<Product> scrapes)
        {
            AddNewProducts(scrapes);
            UpdateExistingProductsPrice(scrapes);

            // TODO: Eliminate Delisted Products
        }
    }
}
