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
    public IActionResult CreateCategory(CategoryInput categoryDto)
    {
        try
        {
            var result = _categoryService.CreateCategory(categoryDto);
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
    public IActionResult GetCategories()
    {
        try
        {
            var category = _categoryService.GetCategories();
            if (category == null) return NotFound();
            return Ok(category);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}