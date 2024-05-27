namespace Ocelot.Infra;

public struct Settings
{
    public required string TokenKey { get; set; }
    public required string TokenIssuer { get; set; }
    public required string TokenAudience { get; set; }
}