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

    [HttpPost("{productId}/adjust")]
    public IActionResult AdjustInventory(string productId, InventoryAdjustmentInput adjustmentDto)
    {
        try
        {
            _inventoryService.AdjustInventory(productId, adjustmentDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("low-inventory")]
    public IActionResult GetLowInventoryProducts()
    {
        try
        {
            var lowInventoryProducts = _inventoryService.GetLowInventoryProducts();
            return Ok(lowInventoryProducts);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("audit")]
    public IActionResult AuditInventory(InventoryAuditInput auditDto)
    {
        try
        {
            _inventoryService.AuditInventory(auditDto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}