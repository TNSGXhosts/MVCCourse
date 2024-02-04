using CoreBusiness;

using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;

using UseCases.CategoriesUseCases;
using UseCases.ProductsUseCases;

using WebApp.ViewModel;

namespace WebApp.Controllers;

[Authorize(Policy = "Inventory")]
public class ProductController(
    IAddProductUseCase addProductUseCase,
    IDeleteProductUseCase deleteProductUseCase,
    IEditProductUseCase editProductUseCase,
    IViewProductsUseCase viewProductsUseCase,
    IViewSelectedProductUseCase viewSelectedProductUseCase,
    IViewCategoriesUseCase viewCategoriesUseCase) : Controller
{
    public IActionResult Index()
    {
        var products = viewProductsUseCase.Execute(true);
        return View(products);
    }

    public IActionResult Edit(int? productId)
    {
        ViewBag.Action = "edit";

        var productViewModel = new ProductViewModel()
        {
            Categories = viewCategoriesUseCase.Execute(),
            Product = viewSelectedProductUseCase.Execute(productId ?? 0, true) ?? new Product()
        };

        return View(productViewModel);
    }

    [HttpPost]
    public IActionResult Edit(ProductViewModel productViewModel)
    {
        if (ModelState.IsValid)
        {
            editProductUseCase.Execute(productViewModel.Product.ProductId, productViewModel.Product);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Action = "edit";
        productViewModel.Categories = viewCategoriesUseCase.Execute();

        return View(productViewModel);
    }

    public IActionResult Add()
    {
        ViewBag.Action = "add";

        var productViewModel = new ProductViewModel()
        {
            Categories = viewCategoriesUseCase.Execute()
        };

        return View(productViewModel);
    }

    [HttpPost]
    public IActionResult Add(ProductViewModel productViewModel)
    {
        if (ModelState.IsValid)
        {
            addProductUseCase.Execute(productViewModel.Product);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Action = "add";
        productViewModel.Categories = viewCategoriesUseCase.Execute();

        return View(productViewModel);
    }

    public IActionResult Delete(int productId)
    {
        deleteProductUseCase.Execute(productId);
        return RedirectToAction(nameof(Index));
    }
}