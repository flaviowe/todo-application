namespace Ocelot.Infra.Services.Account;

public struct DefaultError
{
    public required string Error { get; set; }
    public string Message { get; set; }
}