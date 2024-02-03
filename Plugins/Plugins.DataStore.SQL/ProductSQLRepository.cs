using CoreBusiness;

using Microsoft.EntityFrameworkCore;

using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL;

public class ProductSQLRepository(MarketContext db, IProductMapperConfig productMapperConfig) : IProductRepository
{
    public void AddProduct(Product product)
    {
        db.Products.Add(product);
        db.SaveChanges();
    }

    public void DeleteProduct(int productId)
    {
        var product = db.Products.Find(productId);

        if (product == null)
        {
            return;
        }

        db.Products.Remove(product);
        db.SaveChanges();
    }

    public Product? GetProductById(int productId, bool loadCategory = false)
    {
        if (loadCategory)
        {
            return db.Products
                .Include(p => p.Category)
                .FirstOrDefault(p => p.ProductId == productId);
        }
        else
        {
            return db.Products.FirstOrDefault(p => p.ProductId == productId);
        }
    }

    public IEnumerable<Product> GetProducts(bool loadCategories = false)
    {
        if (loadCategories)
        {
            return db.Products.Include(p => p.Category).OrderBy(p => p.ProductId).ToList();
        }

        return db.Products.OrderBy(p => p.ProductId).ToList();
    }

    public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
    {
        return db.Products.Where(p => p.CategoryId == categoryId).ToList();
    }

    public void UpdateProduct(int productId, Product product)
    {
        if (productId != product.ProductId)
        {
            return;
        }

        var dbProduct = db.Products.Find(productId);

        if (dbProduct == null)
        {
            return;
        }

        var mapper = productMapperConfig.Configure();
        mapper.Map(product, dbProduct);
        db.SaveChanges();
    }
}