using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases;

public class ViewProductsInCategoryUseCase(IProductRepository productRepository) : IViewProductsInCategoryUseCase
{
    public IEnumerable<Product> Execute(int categoryId)
    {
        return productRepository.GetProductsByCategoryId(categoryId);
    }
}