using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.CategoriesUseCases;

public class AddCategoryUseCase(ICategoryRepository categoryRepository) : IAddCategoryUseCase
{
    public void Execute(Category category)
    {
        categoryRepository.AddCategory(category);
    }
}