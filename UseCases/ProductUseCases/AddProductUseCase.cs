using CoreBusiness;

using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases;

public class AddProductUseCase(IProductRepository productRepository) : IAddProductUseCase
{
    public void Execute(Product product)
    {
        productRepository.AddProduct(product);
    }
}