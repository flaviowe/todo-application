namespace Ocelot.ApiGateway.Services;

public class JwtTokenSettings
{
    public required string TokenKey { get; set; }
    public required string TokenIssuer { get; set; }
    public required string TokenAudience { get; set; }
}