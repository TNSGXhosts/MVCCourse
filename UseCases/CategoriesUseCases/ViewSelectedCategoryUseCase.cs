using CoreBusiness;

using UseCases.DataStorePluginInterfaces;

namespace UseCases.CategoriesUseCases;

public class ViewSelectedCategoryUseCase(ICategoryRepository categoryRepository) : IViewSelectedCategoryUseCase
{
    public Category? Execute(int categoryId)
    {
        return categoryRepository.GetCategoryById(categoryId);
    }
}