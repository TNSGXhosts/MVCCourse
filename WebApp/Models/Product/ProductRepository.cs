namespace WebApp.Models;

public static class ProductRepository
{
    private static List<Product> _products = new List<Product>() {
        new Product() { ProductId = 1, Name = "Product 1", CategoryId = 1, Quantity = 5, Price = 10.0 },
        new Product() { ProductId = 2, Name = "Product 2", CategoryId = 1, Quantity = 10, Price = 25.0 },
        new Product() { ProductId = 3, Name = "Product 3", CategoryId = 1, Quantity = 25, Price = 50.0 },
    };

    public static void AddProduct(Product product)
    {
        if (_products != null && _products.Count > 0)
        {
            var maxId = _products.Max(x => x.ProductId);
            product.ProductId = maxId + 1;
        }
        else
        {
            product.ProductId = 1;
        }

        if (_products == null) _products = new List<Product>();

        _products.Add(product);
    }

    public static List<Product> GetProducts(bool loadCategories = false)
    {
        if (!loadCategories)
        {
            return _products;
        }
        else
        {
            if (_products != null && _products.Count > 0)
            {
                _products.ForEach(p => {
                    if (p.CategoryId.HasValue)
                    {
                        p.Category = CategoriesRepository.GetCategoryById(p.CategoryId.Value);
                    }
                });
            }

            return _products ?? new List<Product>();
        }
    }

    public static Product? GetProductById(int productId, bool loadCategory = false)
    {
        var product = _products.FirstOrDefault(x => x.ProductId == productId);
        if (product != null) 
        { 
            var mapper = ProductMapperConfig.Configure();
            var resultProduct = mapper.Map<Product>(product);

            if (loadCategory && resultProduct.CategoryId.HasValue)
                resultProduct.Category = CategoriesRepository.GetCategoryById(resultProduct.CategoryId.Value);

            return resultProduct; 
        }
        return null;
    }

    public static void UpdateProduct(int productId, Product product)
    {
        if (productId != product.ProductId) return;

        var productToUpdate = _products.FirstOrDefault(x => x.ProductId == productId);
        if (productToUpdate != null)
        {
            var mapper = ProductMapperConfig.Configure();
            mapper.Map(product, productToUpdate);
        }
    }

    public static void DeleteProduct(int productId)
    {
        var product = _products.FirstOrDefault(x => x.ProductId == productId);
        if (product != null)
        {
            _products.Remove(product);
        }
    }
}