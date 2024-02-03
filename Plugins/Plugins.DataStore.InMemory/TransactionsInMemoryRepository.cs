using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory;

public class TransactionsInMemoryRepository : ITransactionRepository
{
    private static readonly ICollection<Transaction> s_transactions = new List<Transaction>();

    public IEnumerable<Transaction> GetByDayAndCashier(DateTime date, string cashierName)
    {
        if (string.IsNullOrWhiteSpace(cashierName))
        {
            return s_transactions.Where(t => t.TimeStamp.Date == date.Date);
        }
        else
        {
            return s_transactions.Where(t => t.TimeStamp.Date == date.Date && t.CashierName.ToLower().Contains(cashierName.ToLower()));
        }
    }

    public IEnumerable<Transaction> Search(string cashierName, DateTime startDate, DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(cashierName))
        {
            return s_transactions.Where(t => t.TimeStamp.Date >= startDate.Date && t.TimeStamp.Date <= endDate.Date.AddDays(1).Date);
        }
        else
        {
            return s_transactions.Where(t => t.CashierName.ToLower().Contains(cashierName.ToLower()) && t.TimeStamp.Date >= startDate.Date && t.TimeStamp.Date <= endDate.Date.AddDays(1).Date);
        }
    }

    public void Add(string cashierName, int productId, string productName, double price, int beforeQty, int soldQty)
    {
        var transaction = new Transaction
        {
            ProductId = productId,
            ProductName = productName,
            Price = price,
            BeforeQty = beforeQty,
            SoldQty = soldQty,
            TimeStamp = DateTime.Now,
            CashierName = cashierName
        };

        if (s_transactions != null && s_transactions.Count > 0)
        {
            var maxId = s_transactions.Max(x => x.TransactionId);
            transaction.TransactionId = maxId + 1;
        }
        else
        {
            transaction.TransactionId = 1;
        }

        s_transactions?.Add(transaction);
    }
}