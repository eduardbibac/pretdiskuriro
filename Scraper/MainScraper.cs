using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Globalization;
using System.Web;
using WinPretDiskuri.Data;
using WinPretDiskuri.Scraper;

public static class MarketName
{
    public static string EMAG = "emag";
}
public class MainScraper
{
    public static BackgroundWorker worker;
    public static void Run()
    {
        List<Product> emagPoructs = Emag.RunScraper();
        
        worker.ReportProgress(80);
        Repository.MergeNewProducts(emagPoructs, MarketName.EMAG);
        worker.ReportProgress(100);
    }
}