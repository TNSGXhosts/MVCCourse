﻿using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory;

public class CategoriesInMemoryRepository : ICategoryRepository
{
    private static List<Category> _categories = new List<Category>()
    {
        new Category { CategoryId = 1, Name = "Beverage", Description = "Beverage" },
        new Category { CategoryId = 2, Name = "Bakery", Description = "Bakery" },
        new Category { CategoryId = 3, Name = "Meat", Description = "Meat" }
    };

    public void AddCategory(Category category)
    {
        if (_categories != null && _categories.Count > 0)
        {
            var maxId = _categories.Max(x => x.CategoryId);
            category.CategoryId = maxId + 1;
        }
        else
        {
            category.CategoryId = 1;
        }

        if (_categories == null) _categories = new List<Category>();
        
        _categories.Add(category);
    }

    public IEnumerable<Category> GetCategories() => _categories;

    public Category? GetCategoryById(int categoryId)
    {
        var category = _categories.FirstOrDefault(x => x.CategoryId == categoryId);
        if (category != null) 
        { 
            var mapper = CategoryMapperConfig.Configure();
            return mapper.Map<Category>(category);
        }

        return null;
    }

    public void UpdateCategory(int categoryId, Category category)
    {
        if (categoryId != category.CategoryId) return;

        var categoryToUpdate = _categories.FirstOrDefault(x => x.CategoryId == categoryId);
        if (categoryToUpdate != null)
        {
            var mapper = CategoryMapperConfig.Configure();
            mapper.Map(category, categoryToUpdate);
        }
    }

    public void DeleteCategory(int categoryId)
    {
        var category = _categories.FirstOrDefault(x => x.CategoryId == categoryId);
        if (category != null)
        {
            _categories.Remove(category);
        }
    }
}