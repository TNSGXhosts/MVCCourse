using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModel;

namespace WebApp.Controllers;

public class ProductController : Controller
{
    public IActionResult Index()
    {
        var products = ProductRepository.GetProducts(true);
        return View(products);
    }

    public IActionResult Edit(int? productId)
    {
        ViewBag.Action = "edit";

        var productViewModel = new ProductViewModel(){
            Categories = CategoriesRepository.GetCategories(),
            Product = ProductRepository.GetProductById(productId.HasValue ? productId.Value : 0, true) ?? new Product()
        };

        return View(productViewModel);
    }

    [HttpPost]
    public IActionResult Edit(ProductViewModel productViewModel)
    {
        if (ModelState.IsValid)
        {
            ProductRepository.UpdateProduct(productViewModel.Product.ProductId, productViewModel.Product);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Action = "edit";
        productViewModel.Categories = CategoriesRepository.GetCategories();

        return View(productViewModel);
    }

    public IActionResult Add()
    {
        ViewBag.Action = "add";

        var productViewModel = new ProductViewModel()
        {
            Categories = CategoriesRepository.GetCategories()
        };

        return View(productViewModel);
    }

    [HttpPost]
    public IActionResult Add(ProductViewModel productViewModel)
    {
        if (ModelState.IsValid)
        {
            ProductRepository.AddProduct(productViewModel.Product);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Action = "add";
        productViewModel.Categories = CategoriesRepository.GetCategories();

        return View(productViewModel);
    }

    public IActionResult Delete(int productId)
    {
        ProductRepository.DeleteProduct(productId);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult ProductsByCategoryPartial(int categoryId)
    {
        var products = ProductRepository.GetProductsByCategoryId(categoryId); 

        return PartialView("_Products", products);
    } 
}