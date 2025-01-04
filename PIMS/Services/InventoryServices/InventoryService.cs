using System.Linq;
using Microsoft.EntityFrameworkCore;
using PIMS.Models;
using PIMS.Repository;

namespace PIMS.Services.InventoryServices;

public class InventoryService : IInventoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<User> _users;
    private readonly IRepository<Inventory> _inventories;
    private readonly IRepository<InventoryTransaction> _inventoryTransactions;
    
    public InventoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _users = _unitOfWork.GetRepository<User>();
        _inventories = _unitOfWork.GetRepository<Inventory>();
        _inventoryTransactions = _unitOfWork.GetRepository<InventoryTransaction>();
    }

    public async Task<List<InventoryProductOutput>> GetAllInventoriesAsync()
    {
        var query = from inventory in _inventories.GetAll().Include(i => i.Product)
            let userName = (from transaction in _inventoryTransactions.GetAll()
                join user in _users.GetAll() on transaction.PerformedBy equals user.UserId
                where transaction.InventoryId == inventory.InventoryId
                select user.Username).FirstOrDefault()
            select new InventoryProductOutput(inventory, userName);

        return query.ToList();
    }

    public async Task<InventoryProductOutput> GetInventoryByIdAsync(string inventoryId)
    {
        var query = from inventory in _inventories.GetAll().Include(i => i.Product)
            let userName = (from transaction in _inventoryTransactions.GetAll()
                join user in _users.GetAll() on transaction.PerformedBy equals user.UserId
                where transaction.InventoryId == inventory.InventoryId
                select user.Username).FirstOrDefault()
            where inventory.InventoryId == inventoryId
            select new InventoryProductOutput(inventory, userName);
        
        return query.FirstOrDefault();
    }

    public async Task<string> AddInventoryAsync(InventoryProductInput input)
    {
        string inventoryId = null;
        var inventory = await _inventories.GetAll()
            .FirstOrDefaultAsync(i => i.ProductId == input.ProductId);

        if (inventory == null)
        {
            inventoryId = Guid.NewGuid().ToString();
            _inventories.Add(input.ToInventoryEntity(inventoryId));
            await LogTransaction(input.ToTransactionLog(inventoryId, "Addition"));
        }
        else
        {
            inventoryId = inventory.InventoryId;
            inventory.Quantity += input.Quantity;
            await LogTransaction(input.ToTransactionLog(inventoryId, "Update"));
        }

        await _unitOfWork.SaveChangesAsync();
        return inventoryId;
    }

    public async Task<bool> UpdateInventoryAsync( InventoryAdjustmentInput adjustmentInput)
    {
        var inventory = await _inventories.GetByIdAsync(adjustmentInput.InventoryId);
        if (inventory == null) return false;

        inventory.Quantity += adjustmentInput.Quantity;

        if (inventory.Quantity < 0) throw new InvalidOperationException("Quantity cannot be negative.");

        await LogTransaction(adjustmentInput.ToTransactionLog("Update"));
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> PerformAuditAsync(string inventoryId, InventoryAuditInput auditInput)
    {
        var inventory = await _inventories.GetByIdAsync(inventoryId);
        if (inventory == null) return false;

        inventory.Quantity += auditInput.Quantity;

        if (inventory.Quantity < 0) throw new InvalidOperationException("Quantity cannot be negative.");

        await LogTransaction(auditInput.ToTransactionLog("Audit"));
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
    
    private async Task LogTransaction(InventoryTransactionDto transactionDto)
    {
        _inventoryTransactions.Add(transactionDto.ToInventoryTransactionEntity());
    }
}