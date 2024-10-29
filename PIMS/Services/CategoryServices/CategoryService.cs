using Microsoft.EntityFrameworkCore;
using PIMS.Models;

public class CategoryService : ICategoryService
{
    private readonly PimsContext _dbContext;

    public CategoryService(PimsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public CategoryOutput CreateCategory(CategoryInput categoryInput)
    {
        var category = categoryInput.GetCategoryEntity();
        
        _dbContext.Categories.Add(category);
        _dbContext.SaveChanges();
        return new CategoryOutput(category);
    }

    public CategoryOutput GetCategoryById(string categoryId)
    {
        var categoryInfo = (from category in _dbContext.Categories
            where category.CategoryId == categoryId
            select category).FirstOrDefault();
        if (categoryInfo == null)
        {
            throw new Exception("Category not found.");
        }

        return new CategoryOutput(categoryInfo);
    }
}