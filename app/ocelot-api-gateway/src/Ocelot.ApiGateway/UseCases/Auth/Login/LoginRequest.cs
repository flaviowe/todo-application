namespace Ocelot.ApiGateway.UseCases.Auth.Login;

public struct LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
