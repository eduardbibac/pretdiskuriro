using HtmlAgilityPack;
using System.Formats.Asn1;
using System.Globalization;

//var MarketPalces = new List<string>()
//{

//}

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

var products = doc.DocumentNode.SelectNodes("//div[contains(@class, 'card-v2')]");
foreach (var product in products)
{
    //259&#44;99 Lei
    var buf = product.SelectNodes("//p[@class='product-new-price']")[0].InnerText;
    var price = buf.Split("&#44;")[0];
    var ddecimal = buf.Split("&#44;")[1].Split(" ")[0];
    Console.WriteLine($"{price}.{ddecimal}");
    break;
}
//foreach (var item in pageCount)
//{
//    var x = item.InnerHtml.Split(" ");
//    Console.WriteLine();
//}
//foreach (var item in pageCount)
//{
//    //titles.Add(new Row { Title = item.InnerText });
//    Console.WriteLine(item.InnerText);
//}


//var titles = new List<Row>();



//using (var writer = new StreamWriter("your_path_here/example.csv"))
//using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
//{ csv.WriteRecords(titles); }



//public class Row { public string Title { get; set; } }
//public class MarketPlace
//{
//    public List<string> URIsCategoryLinksToScrape;
//    public uint TrustTier;
//    public string name;


//}