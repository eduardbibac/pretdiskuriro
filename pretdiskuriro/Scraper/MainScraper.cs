using DbModels;
using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using pretdiskuriro.Data;
using pretdiskuriro.Scraper;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;
using System.Web;

public static class MarketName
{
    public static string EMAG = "emag";
}
public class MainScraper
{
    public static void Run()
    {
        List<MarketProduct> emagPoructs = Emag.RunScraper();
        Repository.MergeNewProducts(emagPoructs);
    }
}