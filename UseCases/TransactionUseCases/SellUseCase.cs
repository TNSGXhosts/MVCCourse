using UseCases.DataStorePluginInterfaces;

namespace UseCases.TransactionsUseCases;

public class SellUseCase(
    IProductRepository productRepository,
    ITransactionRepository transactionRepository
) : ISellUseCase
{
    public void Execute(string cashierName, int productId, int quantity)
    {
        var prod = productRepository.GetProductById(productId);
        if (prod != null)
        {
            transactionRepository.Add(
                cashierName,
                prod.ProductId,
                prod.Name,
                prod.Price ?? 0,
                prod.Quantity ?? 0,
                quantity
            );

            prod.Quantity -= quantity;
            productRepository.UpdateProduct(productId, prod);
        }
    }
}