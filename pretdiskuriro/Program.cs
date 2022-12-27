//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddRazorPages();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapRazorPages();

//app.Run();

using DbModels;
using pretdiskuriro.Data;

var marketProducts = new List<MarketProduct>();
var emagMarket = Repository.GetMarketByName(MarketName.EMAG);
var product1 = new Product
{
    Title = "Testing1"
};
var marketProduct1 = new MarketProduct { Market = emagMarket, Product = product1, Prices=new List<DailyPrice>() };
marketProduct1.Prices.Add(new DailyPrice { Price = 150, MarketProduct=marketProduct1 });


marketProducts.Add(marketProduct1);
//MainScraper.Run();
Repository.MergeNewProducts(marketProducts);