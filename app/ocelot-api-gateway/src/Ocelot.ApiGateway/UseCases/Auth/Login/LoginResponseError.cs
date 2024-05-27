namespace Ocelot.ApiGateway.UseCases.Auth.Login;

public struct LoginResponseError
{
    public required string Error { get; set; }
    public string Message { get; set; } 
}