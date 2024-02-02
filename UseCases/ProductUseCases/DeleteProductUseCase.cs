using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases;

public class DeleteProductUseCase(IProductRepository productRepository) : IDeleteProductUseCase
{
    public void Execute(int productId)
    {
        productRepository.DeleteProduct(productId);
    }
}