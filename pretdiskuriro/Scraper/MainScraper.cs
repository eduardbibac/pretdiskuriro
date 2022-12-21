using DbModels;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;
using System.Web;

public class MainScraper
{
    public static void Run()
    {
        HtmlWeb web = new HtmlWeb();

        // foreach market in marketplaces
        // foreach category in categoriestoscrape
        HtmlDocument doc = web.Load("https://www.emag.ro/hard_disk-uri");
        // Get the last page number
        var pageCount = int.Parse(
            doc.DocumentNode
            .SelectNodes("//div[contains(@class, 'js-listing-pagination')]")[0]
            .InnerText.Split(" ")[2]);

        Console.WriteLine(pageCount);
        var products = new List<Product>();

        var card_grid = doc.DocumentNode.SelectSingleNode("//div[@id='card_grid']");
        var nodesnodeProduct = card_grid.SelectNodes(".//div[contains(@class, 'card-v2-wrapper')]");
        foreach (var nodeProduct in nodesnodeProduct)
        {
            var buf = HttpUtility.HtmlDecode(nodeProduct.SelectSingleNode(".//p[@class='product-new-price']").InnerText)
                .Split(",");
            var intPrice = buf[0].Replace(".", "");
            var decimalPrice = buf[1].Split(" ")[0];

            var title = nodeProduct.SelectSingleNode(".//a[contains(@class, 'card-v2-title ')]").InnerText;
            // TODO: 512GBs...
            var c = title.IndexOf("TB");
            var capacity = '0';
            if (c != -1)
            {
                capacity = title[c - 1];
            }

            var price = float.Parse($"{intPrice}.{decimalPrice}");


            var product = new Product
            {
                Title = title,
                Prices = new List<DailyPrice>()
            };
            product.Prices.Add(new DailyPrice { Price = price });

            products.Add(product);
        }

        // AddProducts(products);
        // MergeNewProducts(products);


        // TODO: time limiter ? don't run the scraper if 12 hours didn't pass
        // TODO: Pirce tracker (we need a price history)
        // TODO: match newly scraped products with exisitng products in DB, by productNo or something
    }

   
}