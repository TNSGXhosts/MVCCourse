using CoreBusiness;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using Plugins.DataStore.InMemory;
using Plugins.DataStore.SQL;

using UseCases.CategoriesUseCases;
using UseCases.DataStorePluginInterfaces;
using UseCases.ProductsUseCases;
using UseCases.TransactionsUseCases;

using WebApp.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<MarketContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("MarketManagement"),
            sqlServerOptionsAction: sqlOptions => sqlOptions.MigrationsAssembly("Plugins.DataStore.SQL")));

builder.Services
    .AddDbContext<AccountContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("MarketManagement"),
            sqlServerOptionsAction: sqlOptions => sqlOptions.MigrationsAssembly("WebApp")));

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

if (builder.Environment.IsEnvironment("QA"))
{
    builder.Services.AddSingleton<ICategoryRepository, CategoriesInMemoryRepository>();
    builder.Services.AddSingleton<IProductRepository, ProductsInMemoryRepository>();
    builder.Services.AddSingleton<ITransactionRepository, TransactionsInMemoryRepository>();
}
else
{
    builder.Services.AddTransient<ICategoryRepository, CategorySQLRepository>();
    builder.Services.AddTransient<IProductRepository, ProductSQLRepository>();
    builder.Services.AddTransient<ITransactionRepository, TransactionSQLRepository>();
}

builder.Services.AddTransient<IAddCategoryUseCase, AddCategoryUseCase>();
builder.Services.AddTransient<IDeleteCategoryUseCase, DeleteCategoryUseCase>();
builder.Services.AddTransient<IEditCategoryUseCase, EditCategoryUseCase>();
builder.Services.AddTransient<IViewCategoriesUseCase, ViewCategoriesUseCase>();
builder.Services.AddTransient<IViewSelectedCategoryUseCase, ViewSelectedCategoryUseCase>();

builder.Services.AddTransient<IAddProductUseCase, AddProductUseCase>();
builder.Services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();
builder.Services.AddTransient<IEditProductUseCase, EditProductUseCase>();
builder.Services.AddTransient<IViewProductsInCategoryUseCase, ViewProductsInCategoryUseCase>();
builder.Services.AddTransient<IViewProductsUseCase, ViewProductsUseCase>();
builder.Services.AddTransient<IViewSelectedProductUseCase, ViewSelectedProductUseCase>();

builder.Services.AddTransient<IGetTransactionsByDayAndCashierUseCase, GetTransactionByDayAndCashierUseCase>();
builder.Services.AddTransient<ISearchTransactionsUseCase, SearchTransactionsUseCase>();
builder.Services.AddTransient<ISellUseCase, SellUseCase>();

builder.Services.AddScoped<ICategoryMapperConfig, CategoryMapperConfig>();
builder.Services.AddScoped<IProductMapperConfig, ProductMapperConfig>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Inventory", p => p.RequireClaim("Position", "Inventory"));
    options.AddPolicy("Cashiers", p => p.RequireClaim("Position", "Cashier"));
});
builder.Services.AddAuthentication();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AccountContext>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
