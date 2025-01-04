namespace PIMS.Services.InventoryServices;

public interface IInventoryService
{
    Task<List<InventoryProductOutput>> GetAllInventoriesAsync();
    Task<InventoryProductOutput> GetInventoryByIdAsync(string inventoryId);
    Task<string> AddInventoryAsync(InventoryProductInput input);
    Task<bool> UpdateInventoryAsync( InventoryAdjustmentInput adjustmentInput);
    Task<bool> PerformAuditAsync(string inventoryId, InventoryAuditInput auditInput);
}