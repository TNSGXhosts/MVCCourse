using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.ViewComponents;

[ViewComponent]
public class TransactionsViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(string username)
    {
        var transactions = TransactionRepository.GetByDayAndCashier(DateTime.UtcNow , username);
        
        return View(transactions);
    } 
} 