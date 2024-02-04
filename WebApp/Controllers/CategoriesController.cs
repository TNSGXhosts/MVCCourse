using Microsoft.AspNetCore.Mvc;

using CoreBusiness;

using UseCases.CategoriesUseCases;

using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize(Policy = "Inventory")]
    public class CategoriesController(
        IViewCategoriesUseCase viewCategoriesUseCase,
        IViewSelectedCategoryUseCase viewSelectedCategoryUseCase,
        IAddCategoryUseCase addCategoryUseCase,
        IEditCategoryUseCase editCategoryUseCase,
        IDeleteCategoryUseCase deleteCategoryUseCase) : Controller
    {
        public IActionResult Index()
        {
            var categories = viewCategoriesUseCase.Execute();
            return View(categories);
        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Action = "edit";

            var category = viewSelectedCategoryUseCase.Execute(id ?? 0);

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                editCategoryUseCase.Execute(category.CategoryId, category);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "edit";

            return View(category);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";

            return View();
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                addCategoryUseCase.Execute(category);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "add";

            return View(category);
        }

        public IActionResult Delete(int categoryId)
        {
            deleteCategoryUseCase.Execute(categoryId);
            return RedirectToAction(nameof(Index));
        }
    }
}
