using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Ocelot.ApiGateway.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{

    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger)
    {
        _logger = logger;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Aqui você autenticaria o usuário. Este é um exemplo simplificado.
        if (request.Username == "user" && request.Password == "password")
        {
            var token = GenerateJwtToken(request.Username);
            return Ok(new { token });
        }

        return Unauthorized();
    }

    private string GenerateJwtToken(string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("key");
        var jwtIssuer = "";
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim(ClaimTypes.Name, username)
                }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = jwtIssuer,
            Audience = jwtIssuer,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

public class LoginRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}