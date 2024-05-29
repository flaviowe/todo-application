using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Ocelot.Domain.Services;

namespace Ocelot.ApiGateway.Services;

public class JwtTokenService : ITokenService
{
    private readonly JwtTokenSettings _jwtTokenSettings;

    public JwtTokenService(
        JwtTokenSettings jwtTokenSettings
    )
    {
        _jwtTokenSettings = jwtTokenSettings;
    }

    public string CreateToken(Guid userId, string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtTokenSettings.TokenKey);
        var jwtIssuer = _jwtTokenSettings.TokenIssuer;
        var jwtAudience = _jwtTokenSettings.TokenAudience;

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