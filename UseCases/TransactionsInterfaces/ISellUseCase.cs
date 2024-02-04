namespace UseCases.TransactionsUseCases;

public interface ISellUseCase
{
    void Execute(string cashierName, int productId, int quantity);
}