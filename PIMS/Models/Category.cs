
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PIMS.Models;

public class Category
{
    [Key]
    public string CategoryId { get; set; }

    [Required, MaxLength(50)]
    public string Name { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }

    public Category()
    {
        ProductCategories = new List<ProductCategory>();
    }
}
