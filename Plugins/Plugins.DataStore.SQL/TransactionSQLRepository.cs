using CoreBusiness;

using Microsoft.EntityFrameworkCore;

using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL;

public class TransactionSQLRepository(MarketContext db) : ITransactionRepository
{
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

        db.Transactions.Add(transaction);
        db.SaveChanges();
    }

    public IEnumerable<Transaction> GetByDayAndCashier(DateTime date, string cashierName)
    {
        if (string.IsNullOrWhiteSpace(cashierName))
        {
            return db.Transactions.Where(t => t.TimeStamp.Date == date.Date).ToList();
        }
        else
        {
            return db.Transactions.Where(
                t => t.TimeStamp.Date == date.Date
                    && EF.Functions.Like(t.CashierName, $"%{cashierName}%")).ToList();
        }
    }

    public IEnumerable<Transaction> Search(string cashierName, DateTime startDate, DateTime endDate)
    {
        if (string.IsNullOrWhiteSpace(cashierName))
        {
            return db.Transactions.Where(t => t.TimeStamp.Date >= startDate.Date && t.TimeStamp.Date <= endDate.Date.AddDays(1).Date)
                .ToList();
        }
        else
        {
            return db.Transactions.Where(t => EF.Functions.Like(t.CashierName, $"%{cashierName}%")
                && t.TimeStamp.Date >= startDate.Date
                && t.TimeStamp.Date <= endDate.Date.AddDays(1).Date).ToList();
        }
    }
}