using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModel;

public class SalesViewModel_EnsureProperQuantity : ValidationAttribute
{
    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        var salesViewModel = validationContext.ObjectInstance as SalesViewModel;
        if (salesViewModel != null)
        {
            if (salesViewModel.QuantityToSell <= 0)
            {
                return new ValidationResult("The quantity to sell has to be greater than zero.");
            } else {
                var product = ProductRepository.GetProductById(salesViewModel.SelectedProductId);
                
                if (product != null)
                {
                    if (salesViewModel.QuantityToSell > product? .Quantity)
                    {
                        return new ValidationResult("The quantity to sell cannot be greater than the quantity in stock.");
                    }
                }
                else {
                    return new ValidationResult("The selected product does not exist.");
                }
            }
        }
        return ValidationResult.Success;
    }
}