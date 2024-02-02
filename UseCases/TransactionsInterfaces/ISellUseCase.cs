namespace UseCases.TransactionsUseCases;

public interface ISellUseCase
{
    void Execute(int productId, int quantity);
}