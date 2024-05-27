using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;
using Ocelot.Domain.UseCases.Auth.Login;
using Ocelot.Errors;

namespace Ocelot.ApiGateway.UseCases.Auth.Login;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase, ILoginOutputPort
{

    private readonly ILogger<AuthController> _logger;
    private readonly ILoginUseCase _loginUseCase;
    private string? _token;
    private List<LoginResponseError> _errors = new();

    public AuthController(
        ILogger<AuthController> logger,
        ILoginUseCase loginUseCase)
    {
        _logger = logger;
        _loginUseCase = loginUseCase;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        await _loginUseCase.ExecuteAsync(
            request.Email,
            request.Password
        );

        if (!string.IsNullOrWhiteSpace(_token))
            return Ok(new LoginResponse { Token = _token });

        if (_errors.Any(error => error.Error == "UNAUTHORIZED"))
            return Unauthorized();

        return BadRequest(_errors);
    }

    void ILoginOutputPort.Ok(string token)
        => _token = token;

    void ILoginOutputPort.Undefined()
        => _errors.Add(new() { Error = "UNDEFINED" });

    void ILoginOutputPort.Undefined(string message)
        => _errors.Add(new() { Error = "UNDEFINED", Message = message });

    void ILoginOutputPort.InvalidEmail()
        => _errors.Add(new() { Error = "INVALID_EMAIL" });

    void ILoginOutputPort.InvalidPassword()
        => _errors.Add(new() { Error = "INVALID_PASSWORD" });

    void ILoginOutputPort.Unauthorized()
        => _errors.Add(new() { Error = "UNAUTHORIZED" });
}