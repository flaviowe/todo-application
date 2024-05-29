using Ocelot.Domain.Services;
using Ocelot.Domain.Services.Account;

namespace Ocelot.Domain.UseCases.Auth.Login;

public class LoginUseCase : ILoginUseCase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private ILoginOutputPort? _outputPort;

    public LoginUseCase(
        IUserService userService,
        ITokenService tokenService
    )
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    public void SetOutputPort(ILoginOutputPort outputPort)
        => _outputPort = outputPort;

    public async Task ExecuteAsync(string email, string password)
    {
        bool validate = true;

        if (string.IsNullOrWhiteSpace(email))
        {
            validate = false;
            _outputPort?.InvalidEmail();
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            validate = false;
            _outputPort?.InvalidPassword();
        }

        if (!validate)
            return;

        var response = await _userService.ValidatePasswordAsync(email, password);

        if (response.Success && response.Content != null)
        {
            var token = _tokenService.CreateToken(
                response.Content.UserId,
                response.Content.Email
            );
            _outputPort?.Ok(token);
        }
        else
        {
            if (response.Errors == null)
            {
                _outputPort?.Undefined();
                return;
            }

            foreach (var error in response.Errors)
            {
                switch (error.Error)
                {
                    case ValidatePasswordErrors.InvalidEmail:
                        _outputPort?.InvalidEmail();
                        break;

                    case ValidatePasswordErrors.InvalidPassword:
                        _outputPort?.InvalidPassword();
                        break;

                    case ValidatePasswordErrors.NotFound:
                        _outputPort?.Unauthorized();
                        break;
                        
                    default:
                        if (string.IsNullOrWhiteSpace(error.Message))
                            _outputPort?.Undefined();
                        else
                            _outputPort?.Undefined(error.Message);
                        break;
                }
            }
        }
    }
}