using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory;

public class ProductsInMemoryRepository(ICategoryRepository categoriesRepository) : IProductRepository
{
    private static List<Product> s_products = new List<Product>() {
        new Product() { ProductId = 1, Name = "Product 1", CategoryId = 1, Quantity = 5, Price = 10.0 },
        new Product() { ProductId = 2, Name = "Product 2", CategoryId = 1, Quantity = 10, Price = 25.0 },
        new Product() { ProductId = 3, Name = "Product 3", CategoryId = 1, Quantity = 25, Price = 50.0 },
    };

    public void AddProduct(Product product)
    {
        if (s_products != null && s_products.Count > 0)
        {
            var maxId = s_products.Max(x => x.ProductId);
            product.ProductId = maxId + 1;
        }
        else
        {
            product.ProductId = 1;
        }

        if (s_products == null) s_products = new List<Product>();

        s_products.Add(product);
    }

    public IEnumerable<Product> GetProducts(bool loadCategories = false)
    {
        if (!loadCategories)
        {
            return s_products;
        }
        else
        {
            if (s_products != null && s_products.Count > 0)
            {
                s_products.ForEach(p =>
                {
                    if (p.CategoryId.HasValue)
                    {
                        p.Category = categoriesRepository.GetCategoryById(p.CategoryId.Value);
                    }
                });
            }

            return s_products ?? new List<Product>();
        }
    }

    public Product? GetProductById(int productId, bool loadCategory = false)
    {
        var product = s_products.FirstOrDefault(x => x.ProductId == productId);
        if (product != null)
        {
            var mapper = ProductMapperConfig.Configure();
            var resultProduct = mapper.Map<Product>(product);

            if (loadCategory && resultProduct.CategoryId.HasValue)
                resultProduct.Category = categoriesRepository.GetCategoryById(resultProduct.CategoryId.Value);

            return resultProduct;
        }
        return null;
    }

    public void UpdateProduct(int productId, Product product)
    {
        if (productId != product.ProductId) return;

        var productToUpdate = s_products.FirstOrDefault(x => x.ProductId == productId);
        if (productToUpdate != null)
        {
            var mapper = ProductMapperConfig.Configure();
            mapper.Map(product, productToUpdate);
        }
    }

    public void DeleteProduct(int productId)
    {
        var product = s_products.FirstOrDefault(x => x.ProductId == productId);
        if (product != null)
        {
            s_products.Remove(product);
        }
    }

    public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
    {
        var products = s_products.Where(p => p.CategoryId == categoryId);

        if (products != null)
            return products.ToList();
        else
            return new List<Product>();
    }
}