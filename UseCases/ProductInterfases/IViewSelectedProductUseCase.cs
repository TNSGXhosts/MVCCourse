using CoreBusiness;

namespace UseCases.ProductsUseCases;

public interface IViewSelectedProductUseCase
{
    Product? Execute(int productId, bool loadCategory = false);
}