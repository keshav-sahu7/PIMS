using PIMS.Models;

namespace PIMS.Services.InventoryServices;

public class InventoryProductInput
{
    public string ProductId { get; set; }
    public int Quantity { get; set; }
    public string WarehouseLocation { get; set; }
    public string UserId { get; set; }
    public Inventory ToInventoryEntity(string inventoryId)
    {
        return new Inventory()
        {
            InventoryId = inventoryId,
            ProductId = ProductId,
            Quantity = Quantity,
            WarehouseLocation = WarehouseLocation,
            CreatedBy = UserId
        };
    }

    public InventoryTransactionDto ToTransactionLog(string inventoryId, string transactionType)
    {
        return new InventoryTransactionDto()
        {
            InventoryId = inventoryId,
            Quantity = Quantity,
            UserId = UserId,
            TransactionType = transactionType
        };
    }
    
}