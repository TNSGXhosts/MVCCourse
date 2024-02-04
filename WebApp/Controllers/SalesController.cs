using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UseCases.CategoriesUseCases;
using UseCases.ProductsUseCases;
using UseCases.TransactionsUseCases;

using WebApp.ViewModel;

namespace WebApp.Controllers;

[Authorize(Policy = "Cashiers")]
public class SalesController(
    IViewCategoriesUseCase viewCategoriesUseCase,
    IViewSelectedProductUseCase viewSelectedProductUseCase,
    ISellUseCase sellUseCase,
    IViewProductsInCategoryUseCase viewProductsInCategoryUseCase
    ) : Controller
{
    public IActionResult Index()
    {
        var salesViewModel = new SalesViewModel()
        {
            Categories = viewCategoriesUseCase.Execute()
        };
        return View(salesViewModel);
    }

    public IActionResult GetSalesProductPartial(int productId)
    {
        var product = viewSelectedProductUseCase.Execute(productId);
        return PartialView("_SellProduct", product);
    }

    public IActionResult Sell(SalesViewModel salesViewModel)
    {
        var userName = User?.Identity?.Name;
        if (ModelState.IsValid && userName != null)
        {
            sellUseCase.Execute(userName, salesViewModel.SelectedProductId, salesViewModel.QuantityToSell);
        }

        var product = viewSelectedProductUseCase.Execute(salesViewModel.SelectedProductId);
        salesViewModel.SelectedCategoryId = product?.CategoryId ?? 0;
        salesViewModel.Categories = viewCategoriesUseCase.Execute();

        return View("Index", salesViewModel);
    }

    public IActionResult ProductsByCategoryPartial(int categoryId)
    {
        var products = viewProductsInCategoryUseCase.Execute(categoryId);

        return PartialView("_Products", products);
    }
}