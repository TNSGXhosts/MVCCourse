using CoreBusiness;

namespace UseCases.DataStorePluginInterfaces
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        IEnumerable<Product> GetProducts(bool loadCategories = false);
        Product? GetProductById(int productId, bool loadCategory = false);
        void UpdateProduct(int productId, Product product);
        void DeleteProduct(int productId);
        IEnumerable<Product> GetProductsByCategoryId(int categoryId);
    }
}