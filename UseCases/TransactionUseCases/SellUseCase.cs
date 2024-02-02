using UseCases.DataStorePluginInterfaces;

namespace UseCases.TransactionsUseCases;

public class SellUseCase(
    IProductRepository productRepository,
    ITransactionRepository transactionRepository
) : ISellUseCase
{
    public void Execute(int productId, int quantity)
    {
        var prod = productRepository.GetProductById(productId);
            if (prod != null)
            {
                transactionRepository.Add(
                    "Cashier 1",
                    prod.ProductId,
                    prod.Name,
                    prod.Price.HasValue ? prod.Price.Value : 0,
                    prod.Quantity.HasValue ? prod.Quantity.Value : 0,
                    quantity
                );
                 
                prod.Quantity -= quantity;
                productRepository.UpdateProduct(productId, prod);
            } 
    }
}