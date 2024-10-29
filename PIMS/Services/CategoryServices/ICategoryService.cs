public interface ICategoryService
{
    CategoryOutput CreateCategory(CategoryInput categoryInput);
    CategoryOutput GetCategoryById(string categoryId);
    Task<List<CategoryOutput>> GetCategories();
}