using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PIMS.Models;

public class Product
{
    [Key]
    public string ProductId { get; set; }    // stock keeping unit

    [Required, MaxLength(100)]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; } 
    
    public string CreatedBy { get; set; }
    public User CreatedByNevigation { get; set; }
    public DateTime? CreatedDate { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; }

    public Product()
    {
        ProductCategories = new List<ProductCategory>();
        CreatedDate = DateTime.UtcNow;
    }
}
