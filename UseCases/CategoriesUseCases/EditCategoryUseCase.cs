using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.CategoriesUseCases;

public class EditCategoryUseCase(ICategoryRepository categoryRepository) : IEditCategoryUseCase
{
    public void Execute(int categoryId, Category category)
    {
        categoryRepository.UpdateCategory(categoryId, category);
    }
}