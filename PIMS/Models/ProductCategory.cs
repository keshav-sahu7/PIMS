using System.ComponentModel.DataAnnotations;

namespace PIMS.Models;

public class ProductCategory
{
    [Key]
    public string ProductcategoryId { get; set; }
    public string ProductId { get; set; }
    public Product Product { get; set; }

    public string CategoryId { get; set; }
    public Category Category { get; set; }
}
