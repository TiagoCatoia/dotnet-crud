using Microsoft.AspNetCore.Mvc;
using PersonApi.Api.Services.Authentication;

namespace PersonApi.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost]
    public IActionResult Login(string userId)
    {
        var token = authService.GenerateJwtToken(userId);
        return Ok(new { token });
    }
}