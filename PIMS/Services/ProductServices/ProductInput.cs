using PIMS.Models;

namespace PIMS.Services.ProductServices;

public class ProductInput
{
    public Product ToProudctEntity()
    {
        ProductId ??= Guid.NewGuid().ToString();
        Product product = new Product()
        {
            ProductId = ProductId,
            Description = Description,
            CreatedDate = DateTime.UtcNow,
            Name = ProductName,
            Price = Price,
            CreatedBy = UserId
        };
        
        return product;
    }
    
    
    public string ProductId { get; set; }

    public string UserId { get; set; }

    public decimal Price { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public List<string> CategoryIds { get; set; }

    public bool Hascategories()
    {
        return CategoryIds.Count > 0;
    }

    public List<ProductCategory> ToProductCategoryEntities()
    {
        return CategoryIds.Select(categoryId => new ProductCategory()
        {
            ProductcategoryId = Guid.NewGuid().ToString(),
            ProductId = ProductId,
            CategoryId = categoryId
        }).ToList();
    }

    public Product UpdateProductEntity(Product product)
    {
        product.Description = Description;
        product.Name = ProductName;
        product.Price = Price;
        return product;
    }
}