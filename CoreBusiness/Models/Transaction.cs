namespace CoreBusiness;

public class Transaction
{
     public int TransactionId {  get;set; }
     public DateTime TimeStamp { get;set; }
     public int ProductId { get;set; }
     public required string ProductName { get;set; }
     public double Price { get;set; }
     public int BeforeQty { get;set; }
     public int SoldQty { get;set; }
     public required string CashierName { get;set; }
}