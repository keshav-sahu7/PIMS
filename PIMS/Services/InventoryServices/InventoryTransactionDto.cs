using PIMS.Models;

namespace PIMS.Services.InventoryServices;

public class InventoryTransactionDto
{
    public string InventoryId { get; set; }
    public int Quantity { get; set; }
    public string TransactionType { get; set; }
    public string UserId { get; set; }

    public InventoryTransaction ToInventoryTransactionEntity()
    {
        return new InventoryTransaction()
        {
            TransactionId = Guid.NewGuid().ToString(),
            QuantityChange = Quantity,
            Reason = TransactionType,
            TransactionDate = DateTime.UtcNow,
            InventoryId = InventoryId,
            PerformedBy = UserId
        };
    }

    
}