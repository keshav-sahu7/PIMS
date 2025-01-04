using Microsoft.AspNetCore.Mvc;

namespace PIMS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryInput categoryDto)
    {
        try
        {
            var result = await _categoryService.CreateCategory(categoryDto);
            return Created(nameof(GetCategoryById), result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{categoryId}")]
    public IActionResult GetCategoryById(string categoryId)
    {
        try
        {
            var category = _categoryService.GetCategoryById(categoryId);
            if (category == null) return NotFound();
            return Ok(category);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        try
        {
            var categories = await _categoryService.GetCategories();
            if (categories == null) return NotFound();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}