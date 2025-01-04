using PIMS.Models;

namespace PIMS.Services.InventoryServices;

public class InventoryProductOutput
{
    public InventoryProductOutput(Inventory inventory, string userName)
    {
        InventoryId = inventory.InventoryId;
        ProductId = inventory.ProductId;
        ProductName = inventory.Product.Name;
        Quantity = inventory.Quantity;
        WarehouseLocation = inventory.WarehouseLocation;
        PerformedByUser = userName;
    }
    public string InventoryId { get; set; }
    public string ProductId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public string WarehouseLocation { get; set; }
    public string PerformedByUser { get; set; }
}