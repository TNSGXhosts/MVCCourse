using CoreBusiness;

using Microsoft.EntityFrameworkCore;

using Plugins.DataStore.InMemory;
using Plugins.DataStore.SQL;

using UseCases.CategoriesUseCases;
using UseCases.DataStorePluginInterfaces;
using UseCases.ProductsUseCases;
using UseCases.TransactionsUseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<MarketContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("MarketManagement"),
            sqlServerOptionsAction: sqlOptions => sqlOptions.MigrationsAssembly("Plugins.DataStore.SQL")));

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

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
