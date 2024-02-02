using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.TransactionsUseCases;

public class GetTransactionByDayAndCashierUseCase(ITransactionRepository transactionRepository) : IGetTransactionsByDayAndCashierUseCase
{
    public IEnumerable<Transaction> Execute(DateTime date, string cashierName)
    {
        return transactionRepository.GetByDayAndCashier(date, cashierName);
    }
}