using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UseCases.TransactionsUseCases;
using WebApp.ViewModel;

namespace WebApp.Controllers;

public class TransactionsController(
    ISearchTransactionsUseCase searchTransactionsUseCase
    ) : Controller
{
    public IActionResult Index()
    {
        var searchEntity = new TransactionsSearchViewModel();

        return View(searchEntity);
    }

    public IActionResult Search(TransactionsSearchViewModel searchEntity)
    {
        if (searchEntity == null)
            throw new ValidationException("Please provide search criteria");

        var transactions = searchTransactionsUseCase.Execute(searchEntity.CashierName ?? string.Empty, searchEntity.StartDate, searchEntity.EndDate);
        searchEntity.Transactions = transactions;
        return View(nameof(Index), searchEntity);
    }
}