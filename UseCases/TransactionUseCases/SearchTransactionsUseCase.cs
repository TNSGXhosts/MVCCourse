using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.TransactionsUseCases;

public class SearchTransactionsUseCase(ITransactionRepository transactionRepository) : ISearchTransactionsUseCase
{
    public IEnumerable<Transaction> Execute(string cashierName, DateTime startDate, DateTime endDate)
    {
        return transactionRepository.Search(cashierName, startDate, endDate);
    }
}