using Microsoft.AspNetCore.Mvc;
using PIMS.Services.ProductServices;

namespace PIMS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductInput productInput)
    {
        try
        {
            var productId = await _productService.CreateProduct(productInput);
            return Created(nameof(CreateProduct), new { id = productId });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{productid}")]
    public async Task<IActionResult> UpdateProduct(string productid, [FromBody] ProductInput productInput)
    {
        try
        {
            productInput.ProductId = productid;
            var result = await _productService.UpdateProduct(productInput);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("/adjust-price")]
    public async Task<IActionResult> AdjustPrice([FromBody]PriceAdjustmentInput adjustmentInput)
    {
        try
        {
            var result = await _productService.AdjustPrice(adjustmentInput);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    [HttpGet("{productId}")]
    public IActionResult GetProductById(string productId)
    {
        try
        {
            var product = _productService.GetProductById(productId);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("get-all")]
    public IActionResult GetProducts([FromBody]ProductFilterInput filterInput)
    {
        try
        {
            var products = _productService.GetProducts(filterInput);
            return Ok(products);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }
}