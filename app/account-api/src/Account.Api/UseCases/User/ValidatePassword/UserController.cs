using Account.Domain.UseCases.User.ValidatePassword;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Account.Api.UseCases.User.ValidatePassword;

[ApiController]
[Authorize]
[Route("[controller]")]
public class UserController : Controller, IValidatePasswordOutputPort
{
    private readonly IValidatePasswordUseCase _validatePasswordUseCase;

    private List<string> _errors = new();
    private bool _success = false;

    public UserController(
        IValidatePasswordUseCase validatePasswordUseCase
    )
    {
        _validatePasswordUseCase = validatePasswordUseCase;
    }

    [HttpGet("ValidatePassword")]
    public async Task<IActionResult> ValidatePasswordAsync(string email, string password)
    {
        await _validatePasswordUseCase.ExecuteAsync(email, password);

        if (!_success)
            return _errors.Any(error => error == "INVALID_USER") ?
                NotFound() : BadRequest(_errors);

        return Ok();
    }

    void IValidatePasswordOutputPort.Ok()
        => _success = true;

    void IValidatePasswordOutputPort.InvalidUser()
        => _errors.Add("INVALID_USER");

    void IValidatePasswordOutputPort.PasswordEmpty()
        => _errors.Add("INVALID_PASSWORD");

    void IValidatePasswordOutputPort.UserEmpty()
        => _errors.Add("INVALID_EMAIL");
}