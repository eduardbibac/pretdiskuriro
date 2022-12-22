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

// MainScraper.Run();

var products = new List<Product>();
var product = new Product
{
    Title = "Testing",
    Prices = new List<DailyPrice>()
};
product.Prices.Add(new DailyPrice { Price = 120 });
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
Repository.MergeNewProducts(products);