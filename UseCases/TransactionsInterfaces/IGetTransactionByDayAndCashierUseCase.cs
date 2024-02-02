using CoreBusiness;

namespace UseCases.TransactionsUseCases;

public interface IGetTransactionsByDayAndCashierUseCase
{
    IEnumerable<Transaction> Execute(DateTime date, string cashierName);
}