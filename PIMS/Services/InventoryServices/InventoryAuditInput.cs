namespace PIMS.Services.InventoryServices;

public class InventoryAuditInput
{
    public string InventoryId { get; set; }
    public int Quantity { get; set; }
    public string UserId { get; set; } 
    public InventoryTransactionDto ToTransactionLog(string tranasactionType)
    {
        return new InventoryTransactionDto()
        {
            Quantity = Quantity,
            InventoryId = InventoryId,
            TransactionType = tranasactionType,
            UserId = UserId
        };
    }
}