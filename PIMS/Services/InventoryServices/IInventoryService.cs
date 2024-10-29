namespace PIMS.Services.InventoryServices;

public interface IInventoryService
{
    void AdjustInventory(string productId, InventoryAdjustmentInput adjustmentDto);
    IEnumerable<LowInventoryProductOutput> GetLowInventoryProducts();
    void AuditInventory(InventoryAuditInput auditDto);
}