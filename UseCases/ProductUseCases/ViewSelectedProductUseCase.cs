using CoreBusiness;

using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases;

public class ViewSelectedProductUseCase(IProductRepository productRepository) : IViewSelectedProductUseCase
{
    public Product? Execute(int productId, bool loadCategory = false)
    {
        return productRepository.GetProductById(productId, loadCategory);
    }
}