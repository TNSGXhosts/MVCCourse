using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModel;

namespace WebApp.Controllers;

public class SalesController : Controller
{
    public IActionResult Index()
    {
        var salesViewModel = new SalesViewModel()
        {
            Categories = CategoriesRepository.GetCategories()
        };
        return View(salesViewModel);
    }

    public IActionResult GetSalesProductPartial(int productId)
    {
        var product = ProductRepository.GetProductById(productId);
        return PartialView("_SellProduct", product);
    }

    public IActionResult Sell(SalesViewModel salesViewModel)
    {
        if (ModelState.IsValid)
        {
            var prod = ProductRepository.GetProductById(salesViewModel.SelectedProductId);
            if (prod != null)
            {
                TransactionRepository.Add(
                    "Cashier 1",
                    prod.ProductId,
                    prod.Name,
                    prod.Price.HasValue ? prod.Price.Value : 0,
                    prod.Quantity.HasValue ? prod.Quantity.Value : 0,
                    salesViewModel.QuantityToSell
                );
                 
                prod.Quantity -= salesViewModel.QuantityToSell;
                ProductRepository.UpdateProduct(salesViewModel.SelectedProductId, prod);
            } 
        }

        var product = ProductRepository.GetProductById(salesViewModel.SelectedProductId);
        salesViewModel.SelectedCategoryId = product?.CategoryId ?? 0;
        salesViewModel.Categories = CategoriesRepository.GetCategories();

        return View("Index", salesViewModel);
    }
}