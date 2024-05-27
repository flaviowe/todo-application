using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Domain.Services;

namespace Ocelot.Infra.Services;

public class JwtTokenService : ITokenService
{
    private readonly Settings _settings;

    public JwtTokenService(
        Settings settings
    )
    {
        _settings = settings;
    }

    public string CreateToken(Guid userId, string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_settings.TokenKey);
        var jwtIssuer = _settings.TokenIssuer;
        var jwtAudience = _settings.TokenAudience;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("userId", userId.ToString()),
                new Claim(ClaimTypes.Email, email)
            }),

            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = jwtIssuer,
            Audience = jwtAudience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
