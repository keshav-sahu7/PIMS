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
    public IActionResult CreateProduct(ProductInput productInput)
    {
        try
        {
            var productId = _productService.CreateProduct(productInput);
            return Created(nameof(CreateProduct), new { id = productId });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{productid}")]
    public IActionResult UpdateProduct(string productid, ProductInput productInput)
    {
        try
        {
            productInput.ProductId = productid;
            var result = _productService.UpdateProduct(productInput);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("/adjust-price")]
    public IActionResult AdjustPrice(PriceAdjustmentInput adjustmentInput)
    {
        try
        {
            var result = _productService.AdjustPrice(adjustmentInput);
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

    [HttpPost]
    public IActionResult GetProducts(ProductFilterInput filterInput)
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