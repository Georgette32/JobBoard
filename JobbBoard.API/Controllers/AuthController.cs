using JobBoard.Application.DTOs;
using JobBoard.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobbBoard.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        try
        {
            await _authService.RegisterAsync(request);

            return Ok(new
            {
                message = "User registered successfully."
            });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            var token = await _authService.LoginAsync(request);

            return Ok(new
            {
                token = token
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new
            {
                message = ex.Message
            });
        }
    }
}