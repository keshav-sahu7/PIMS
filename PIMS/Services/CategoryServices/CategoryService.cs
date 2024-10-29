using Microsoft.EntityFrameworkCore;
using PIMS.Models;

namespace PIMS.Services.CategoryServices;

public class CategoryService : ICategoryService
{
    private readonly PimsContext _dbContext;

    public CategoryService(PimsContext dbContext)
    {
        _dbContext = dbContext;
    }

    public CategoryOutput CreateCategory(CategoryInput categoryInput)
    {
        var category = categoryInput.ToCategoryEntity();
        
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

    public async Task<List<CategoryOutput>> GetCategories()
    {
        var categoryInfoList = await (from category in _dbContext.Categories
            select new CategoryOutput(category)).ToListAsync();
        return (categoryInfoList);
    }
}