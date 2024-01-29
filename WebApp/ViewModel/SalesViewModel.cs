using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModel ;

public class SalesViewModel
{
    public int SelectedCategoryId { get; set; }
    public List<Category> Categories { get; set; } = new List<Category>(); 
    public int SelectedProductId { get;set; } 
    [Display(Name = "Quantity")]
    [Range(1, int.MaxValue)]
    [SalesViewModel_EnsureProperQuantity]
    public int QuantityToSell { get;set; }
}