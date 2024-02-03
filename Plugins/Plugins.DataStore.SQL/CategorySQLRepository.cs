using CoreBusiness;

using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL;

public class CategorySQLRepository(MarketContext db, ICategoryMapperConfig categoryMapperConfig) : ICategoryRepository
{
    public void AddCategory(Category category)
    {
        db.Categories.Add(category);
        db.SaveChanges();
    }

    public void DeleteCategory(int categoryId)
    {
        var category = db.Categories.Find(categoryId);

        if (category == null)
        {
            return;
        }

        db.Categories.Remove(category);
        db.SaveChanges();
    }

    public IEnumerable<Category> GetCategories()
    {
        return db.Categories.ToList();
    }

    public Category? GetCategoryById(int categoryId)
    {
        return db.Categories.Find(categoryId);
    }

    public void UpdateCategory(int categoryId, Category category)
    {
        if (category.CategoryId != categoryId)
        {
            return;
        }

        var dbCategory = db.Categories.Find(categoryId);

        if (dbCategory == null)
        {
            return;
        }

        var mapper = categoryMapperConfig.Configure();
        mapper.Map(category, dbCategory);
        db.SaveChanges();
    }
}