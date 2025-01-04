using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PIMS.Services.UserServices;

namespace PIMS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationInput registrationDto)
    {
        try
        {
            var result = await _userService.RegisterUser(registrationDto);
            return Created(nameof(Register), result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginInput loginDto)
    {
        try
        {
            var token = await _userService.Login(loginDto);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [Authorize]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(string userId)
    {
        try
        {
            var user = await _userService.GetUserById(userId);
            if (user == null) return NotFound();
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}