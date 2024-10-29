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

    [HttpPost("register")]
    public IActionResult Register(UserRegistrationInput registrationDto)
    {
        try
        {
            var result = _userService.RegisterUser(registrationDto);
            return Created(nameof(Register), result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public IActionResult Login(UserLoginInput loginDto)
    {
        try
        {
            var token = _userService.Login(loginDto);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{userId}")]
    public IActionResult GetUserById(string userId)
    {
        try
        {
            var user = _userService.GetUserById(userId);
            if (user == null) return NotFound();
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}