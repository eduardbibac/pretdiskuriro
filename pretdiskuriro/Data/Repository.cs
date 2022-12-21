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
                var all = from row in context.Products select row;

                context.Products.AddRange(products);

                context.SaveChanges();
            }
        }

        private static void AddNewProducts(List<Product> scrapes)
        {
            using (var context = new DataContext())
            {
                // get items not existing in inner join
                var newScrapes = from newScrape in scrapes
                                 where !(from product in context.Products.AsEnumerable()
                                         join scrape in scrapes on product.Title equals newScrape.Title
                                         select scrape.Title).Contains(newScrape.Title)
                                 select newScrape;


                foreach (var row in newScrapes)
                {
                    context.Products.Add(new Product { Title = row.Title, Prices = row.Prices });
                }

                context.SaveChanges();
            }
        }

        public static void MergeNewProducts(List<Product> scrapes)
        {
            AddNewProducts(scrapes);
            //UpdateExistingProductsPrice();

            //var newTableRows = from product in context.Products
            //                   join newScraped in newScrapes on product.Title equals newScraped.Title into np
            //                   from xd in np.DefaultIfEmpty()
            //                   select new { product, xd };

            //var all = from product in context.Products select product;
            //all.ExecuteDelete();

        }
    }
}
