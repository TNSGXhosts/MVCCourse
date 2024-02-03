using CoreBusiness;

using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases;

public class EditProductUseCase(IProductRepository productRepository) : IEditProductUseCase
{
    public void Execute(int productId, Product product)
    {
        productRepository.UpdateProduct(product.ProductId, product);
    }
}