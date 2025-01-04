namespace PIMS.Services.InventoryServices;

public class InventoryAdjustmentInput
{
    public int Quantity { get; set; }
    public string InventoryId { get; set; }
    public string UserId { get; set; }

    public InventoryTransactionDto ToTransactionLog(string transactionType)
    {
        return new InventoryTransactionDto()
        {
            TransactionType = transactionType,
            Quantity = Quantity,
            InventoryId = InventoryId,
            UserId = UserId
        };
    }
}