using PIMS.Models;

public class CategoryOutput
{
    public CategoryOutput(Category category)
    {
        CategoryId = category.CategoryId;
        Name = category.Name;
    }

    public string Name { get; set; }
    public string CategoryId { get; set; }
}