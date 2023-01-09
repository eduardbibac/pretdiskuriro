using WinPretDiskuri.Data;

namespace WinPretDiskuri
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }

        static void MockProducts()
        {
            var products = new List<Product>();
            var product = new Product
            {
                Title = "Testing",
                Prices = new List<DailyPrice>()
            };
            product.Prices.Add(new DailyPrice { Price = 140 });
            var product2 = new Product
            {
                Title = "Testing2",
                Prices = new List<DailyPrice>()
            };
            product2.Prices.Add(new DailyPrice { Price = 200 });
            var product3 = new Product
            {
                Title = "Testing3",
                Prices = new List<DailyPrice>()
            };
            product3.Prices.Add(new DailyPrice { Price = 400 });

            products.Add(product);
            products.Add(product2);
            products.Add(product3);
            //MainScraper.Run();
            Repository.MergeNewProducts(products, MarketName.EMAG);
        }
    }
}

