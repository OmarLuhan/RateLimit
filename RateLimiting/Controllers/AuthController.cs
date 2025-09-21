using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace RateLimiting.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private static readonly Dictionary<string,string> Users = new()
    {
        {"1234","admin"},
        {"12345","user"}
    };

    [HttpGet]
    [EnableRateLimiting("bucket")]
    public ActionResult<string> Get([FromQuery] string username, [FromQuery] string password)
    {
       if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return BadRequest();
       if (Users.TryGetValue(password, out string? value) && value != username) return Unauthorized();
       return Ok(Users[password]);
    }
}