using PIMS.Models;

public class CategoryInput
{
    
    public Category GetCategoryEntity()
    {
        Category category = new Category()
        {
            CategoryId = Guid.NewGuid().ToString(),
            Name = CategoryName
        };
        return category;
    }

    public string CategoryName { get; set; }
}