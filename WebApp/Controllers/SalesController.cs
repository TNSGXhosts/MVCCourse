using Microsoft.AspNetCore.Mvc;

using UseCases.CategoriesUseCases;
using UseCases.ProductsUseCases;
using UseCases.TransactionsUseCases;

using WebApp.ViewModel;

namespace WebApp.Controllers;

public class SalesController(
    IViewCategoriesUseCase viewCategoriesUseCase,
    IViewSelectedProductUseCase viewSelectedProductUseCase,
    ISellUseCase sellUseCase
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
        if (ModelState.IsValid)
        {
            sellUseCase.Execute(salesViewModel.SelectedProductId, salesViewModel.QuantityToSell);
        }

        var product = viewSelectedProductUseCase.Execute(salesViewModel.SelectedProductId);
        salesViewModel.SelectedCategoryId = product?.CategoryId ?? 0;
        salesViewModel.Categories = viewCategoriesUseCase.Execute();

        return View("Index", salesViewModel);
    }
}