using UseCases.DataStorePluginInterfaces;

namespace UseCases.CategoriesUseCases;

public class DeleteCategoryUseCase(ICategoryRepository categoryRepository) : IDeleteCategoryUseCase
{
    public void Execute(int categoryId)
    {
        categoryRepository.DeleteCategory(categoryId);
    }
}