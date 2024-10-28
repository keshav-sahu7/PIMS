namespace PIMS.Models;

using System;
using System.ComponentModel.DataAnnotations;

public class InventoryTransaction
{
    [Key]
    public string TransactionId { get; set; }

    [Required]
    public string InventoryId { get; set; }
    public Inventory Inventory { get; set; }

    [Required]
    public int QuantityChange { get; set; }

    public DateTime TransactionDate { get; set; }

    [Required, MaxLength(50)]
    public string Reason { get; set; }

    [Required]
    public string PerformedBy { get; set; }

    public InventoryTransaction()
    {
        TransactionDate = DateTime.UtcNow;
    }
}
