using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModel;

namespace WebApp.Controllers;

public class TransactionsController : Controller
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

        var transactions = TransactionRepository.Search(searchEntity.CashierName ?? string.Empty, searchEntity.StartDate, searchEntity.EndDate);
        searchEntity.Transactions = transactions;
        return View(nameof(Index), searchEntity);
    }
}