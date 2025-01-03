using Microsoft.EntityFrameworkCore;
using PIMS.Models;
using PIMS.Repository;

namespace PIMS.Services.CategoryServices;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Category> _categories;

    public CategoryService(IUnitOfWork dbContext)
    {
        _unitOfWork = dbContext;
        _categories = _unitOfWork.GetRepository<Category>();
    }

    public CategoryOutput CreateCategory(CategoryInput categoryInput)
    {
        var category = categoryInput.ToCategoryEntity();
        
        _categories.Add(category);
        _unitOfWork.SaveChangesAsync();
        return new CategoryOutput(category);
    }

    public CategoryOutput GetCategoryById(string categoryId)
    {
        var categoryInfo = (from category in _categories.GetAll()
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
        var categoryInfoList = await (from category in _categories.GetAll()
            select new CategoryOutput(category)).ToListAsync();
        return (categoryInfoList);
    }
}