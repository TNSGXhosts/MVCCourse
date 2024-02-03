using CoreBusiness;

using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases;

public class ViewProductsUseCase(IProductRepository productRepository) : IViewProductsUseCase
{
    public IEnumerable<Product> Execute(bool loadCategories)
    {
        return productRepository.GetProducts(loadCategories);
    }
}