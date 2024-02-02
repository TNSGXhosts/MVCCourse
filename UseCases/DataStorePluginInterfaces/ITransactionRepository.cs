using CoreBusiness;

namespace UseCases.DataStorePluginInterfaces;

public interface ITransactionRepository
{
    IEnumerable<Transaction> GetByDayAndCashier(DateTime date, string cashierName);
    IEnumerable<Transaction> Search(string cashierName, DateTime startDate, DateTime endDate);
    void Add(string cashierName, int productId, string productName, double price, int beforeQty, int soldQty);
}