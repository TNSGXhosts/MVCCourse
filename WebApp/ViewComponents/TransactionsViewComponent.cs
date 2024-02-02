using Microsoft.AspNetCore.Mvc;
using UseCases.TransactionsUseCases;

namespace WebApp.ViewComponents;

[ViewComponent]
public class TransactionsViewComponent(IGetTransactionsByDayAndCashierUseCase getTransactionsByDayAndCashierUseCase) : ViewComponent
{
    public IViewComponentResult Invoke(string username)
    {
        var transactions = getTransactionsByDayAndCashierUseCase.Execute(DateTime.UtcNow , username);

        return View(transactions);
    }
}