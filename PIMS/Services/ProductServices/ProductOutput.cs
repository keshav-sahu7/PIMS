using PIMS.Models;

namespace PIMS.Services.ProductServices;

public class ProductOutput
{
    public ProductOutput(Product product)
    {
        Name = product.Name;
        Price = product.Price;
        Description = product.Description;
        CreatedBy = product.CreatedByNevigation?.Username;
        CreatedDate = product.CreatedDate;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public List<string> CategoryList { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string CreatedBy { get; set; }
}