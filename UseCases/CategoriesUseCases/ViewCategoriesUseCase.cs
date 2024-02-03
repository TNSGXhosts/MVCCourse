using CoreBusiness;

using UseCases.DataStorePluginInterfaces;

namespace UseCases.CategoriesUseCases;

public class ViewCategoriesUseCase(ICategoryRepository categoryRepository) : IViewCategoriesUseCase
{
    public IEnumerable<Category> Execute()
    {
        return categoryRepository.GetCategories();
    }
}