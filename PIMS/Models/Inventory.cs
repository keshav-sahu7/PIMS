using System;
using System.ComponentModel.DataAnnotations;

namespace PIMS.Models;

public class Inventory
{
    [Key]
    public string InventoryId { get; set; }

    [Required]
    public string ProductId { get; set; }
    public Product Product { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required, MaxLength(100)]
    public string WarehouseLocation { get; set; }
    
    public string CreatedBy { get; set; }
    public User CreatedByNevigation { get; set; }
    public ICollection<InventoryTransaction> InventoryTransactions { get; set; }
    
    
    public Inventory()
    {
        InventoryTransactions = new List<InventoryTransaction>();
    }
}
