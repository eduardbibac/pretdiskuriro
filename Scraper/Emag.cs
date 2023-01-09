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

        private static int[] GetRandomDelaysBetween(int n, int l, int r)
        {
            int[] delays = new int[n];
            var rnd = new Random();
            for(int i =0; i<n; i++)
            {
                delays[i] = rnd.Next(l, r);
            }

            return delays;
        }
        public static List<Product> RunScraper()
        {
            var web = new HtmlWeb();

            // foreach market in marketplaces
            // foreach category in categoriestoscrape
            HtmlAgilityPack.HtmlDocument doc = web.Load("https://www.emag.ro/hard_disk-uri");
            // Get the last page number
            var productsOnPage = int.Parse(
                doc.DocumentNode
                .SelectNodes("//div[contains(@class, 'js-listing-pagination')]/strong")[0]
                .InnerText.Split(" ")[2]);

            var totalProducts = int.Parse(
                doc.DocumentNode
                .SelectNodes("//div[contains(@class, 'js-listing-pagination')]/strong")[1].InnerText);

            int pages = (int)Math.Ceiling((double)totalProducts / productsOnPage);
            var delays = GetRandomDelaysBetween(pages, 1, 6);
            var products = new List<Product>();

            for (int i = 1; i <= pages; i++)
            {
                if(i>1)
                {
                    int procent = (int)((float)i / pages * 100 * 0.8);
                    MainScraper.worker.ReportProgress(procent);
                    Thread.Sleep(delays[i-1] * 1000);
                    doc = web.Load($"https://www.emag.ro/hard_disk-uri/p{i}/c");
                }
                // TODO: FOREACH PAGE
                // TODO: FOR MORE CATEGORIES
                // TODO: RANDOM RATE LIMITER, DO IT IN ONE PASS IN AN ARRAY, UTILITY FUNCTION
                // TODO: CAL ETA
                // TODO: ASYNC?
                var card_grid = doc.DocumentNode.SelectSingleNode("//div[@id='card_grid']");
                var nodesnodeProduct = card_grid.SelectNodes(".//div[contains(@class, 'card-v2-wrapper')]");
                foreach (var nodeProduct in nodesnodeProduct)
                {
                    try
                    {
                        var nodePrice = nodeProduct.SelectSingleNode(".//p[@class='product-new-price']");
                        
                        string[] buf;
                        if (nodePrice != null)
                            buf = HttpUtility.HtmlDecode(nodePrice.InnerText).Split(",");
                        else continue;
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
                    } catch (Exception) { continue; }
                }

            }

            return products;
        }
    }
}
