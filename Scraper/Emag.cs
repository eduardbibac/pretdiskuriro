using HtmlAgilityPack;
using System.Web;
using WinPretDiskuri.Data;

namespace WinPretDiskuri.Scraper
{
    public class Emag
    {
        private static int FindNumberToLeftOfIndex(string title, int index)
        {
            int i = index - 1;
            bool whiteflag = false;
            string value = "";
            while (true)
            {
                var isNumber = title[i] >= '0' && title[i] <= '9';

                if (isNumber)
                {
                    whiteflag = true;
                    value = title[i] + value;
                }
                else if (whiteflag == true) { break; }
                i--;
            }
            return int.Parse(value);
        }
        public static float GetCapacityInTB(string title)
        {
            var c = title.ToLower().IndexOf("tb");
            int multip = 1;
            if (c == -1)
            {
                multip = 1024;
                c = title.ToLower().IndexOf("gb");
                if (c == -1)
                {
                    return 0;
                }   
            }
            var x = FindNumberToLeftOfIndex(title, c);
            float capacity = (float)x / multip;
            return capacity;
        }
        public static List<Product> RunScraper()
        {
            var web = new HtmlWeb();

            // foreach market in marketplaces
            // foreach category in categoriestoscrape
            HtmlAgilityPack.HtmlDocument doc = web.Load("https://www.emag.ro/hard_disk-uri");
            // Get the last page number
            var pageCount = int.Parse(
                doc.DocumentNode
                .SelectNodes("//div[contains(@class, 'js-listing-pagination')]")[0]
                .InnerText.Split(" ")[2]);

            var products = new List<Product>();

            // TODO: FOREACH PAGE
            // TODO: FOR MORE CATEGORIES
            // TODO: RANDOM RATE LIMITER, DO IT IN ONE PASS IN AN ARRAY, UTILITY FUNCTION
            // TODO: CAL ETA
            // TODO: ASYNC?
            var card_grid = doc.DocumentNode.SelectSingleNode("//div[@id='card_grid']");
            var nodesnodeProduct = card_grid.SelectNodes(".//div[contains(@class, 'card-v2-wrapper')]");
            foreach (var nodeProduct in nodesnodeProduct)
            {
                var buf = HttpUtility.HtmlDecode(nodeProduct.SelectSingleNode(".//p[@class='product-new-price']").InnerText)
                    .Split(",");
                var intPrice = buf[0].Replace(".", "");
                var decimalPrice = buf[1].Split(" ")[0];

                var title = nodeProduct.SelectSingleNode(".//a[contains(@class, 'card-v2-title ')]").InnerText;
                
                var capacity = GetCapacityInTB(title);

                var price = float.Parse($"{intPrice}.{decimalPrice}");


                var product = new Product
                {
                    Title = title,
                    Prices = new List<DailyPrice>(),
                    CapacityInTB = capacity,
                };
                product.Prices.Add(new DailyPrice { Price = price });

                products.Add(product);
            }


            return products;
        }
    }
}
