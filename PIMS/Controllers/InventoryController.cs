using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PIMS.Services.InventoryServices;

namespace PIMS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("{inventoryId}/adjust")]
    public async Task<IActionResult> AdjustInventory(string inventoryId, [FromBody] InventoryAdjustmentInput adjustmentDto)
    {
        try
        {
            adjustmentDto.InventoryId = inventoryId;
            await _inventoryService.UpdateInventoryAsync(adjustmentDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [Authorize(Roles = "Admin")]
    [HttpPost("{inventoryId}/audit")]
    public async Task<IActionResult> AuditInventory(string inventoryId, [FromBody] InventoryAuditInput auditDto)
    {
        try
        {
            await _inventoryService.PerformAuditAsync(inventoryId, auditDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [Authorize(Roles = "Admin")]
    [HttpGet("all")]
    public async Task<IActionResult> GetAllInventory()
    {
        try
        {
            var inventoryData = await _inventoryService.GetAllInventoriesAsync();
            return Ok(inventoryData);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet("{inventoryId}")]
    public async Task<IActionResult> GetInventory(string inventoryId)
    {
        try
        {
            var inventoryData = await _inventoryService.GetInventoryByIdAsync(inventoryId);
            return Ok(inventoryData);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}