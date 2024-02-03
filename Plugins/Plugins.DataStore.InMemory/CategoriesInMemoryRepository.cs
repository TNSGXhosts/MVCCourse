using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory;

public class CategoriesInMemoryRepository(ICategoryMapperConfig categoryMapperConfig) : ICategoryRepository
{
    private static ICollection<Category> s_categories = new List<Category>()
    {
        new Category { CategoryId = 1, Name = "Beverage", Description = "Beverage" },
        new Category { CategoryId = 2, Name = "Bakery", Description = "Bakery" },
        new Category { CategoryId = 3, Name = "Meat", Description = "Meat" }
    };

    public void AddCategory(Category category)
    {
        if (s_categories != null && s_categories.Count > 0)
        {
            var maxId = s_categories.Max(x => x.CategoryId);
            category.CategoryId = maxId + 1;
        }
        else
        {
            category.CategoryId = 1;
        }

        if (s_categories == null) s_categories = new List<Category>();

        s_categories.Add(category);
    }

    public IEnumerable<Category> GetCategories() => s_categories;

    public Category? GetCategoryById(int categoryId)
    {
        var category = s_categories.FirstOrDefault(x => x.CategoryId == categoryId);
        if (category != null)
        {
            var mapper = categoryMapperConfig.Configure();
            return mapper.Map<Category>(category);
        }

        return null;
    }

    public void UpdateCategory(int categoryId, Category category)
    {
        if (categoryId != category.CategoryId) return;

        var categoryToUpdate = s_categories.FirstOrDefault(x => x.CategoryId == categoryId);
        if (categoryToUpdate != null)
        {
            var mapper = categoryMapperConfig.Configure();
            mapper.Map(category, categoryToUpdate);
        }
    }

    public void DeleteCategory(int categoryId)
    {
        var category = s_categories.FirstOrDefault(x => x.CategoryId == categoryId);
        if (category != null)
        {
            s_categories.Remove(category);
        }
    }
}
