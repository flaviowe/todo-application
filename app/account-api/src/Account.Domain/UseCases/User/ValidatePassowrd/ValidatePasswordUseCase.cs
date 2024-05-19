using Account.Domain.Repositories;

namespace Account.Domain.UseCases.User.ValidatePassword;

public class ValidatePasswordUseCase : IValidatePasswordUseCase
{
    private readonly IUserRepository _userRepository;
    private IValidatePasswordOutputPort? _outputPort;

    public ValidatePasswordUseCase(
        IUserRepository userRepository
    )
    {
        _userRepository = userRepository;
    }

    public void SetOutputPort(IValidatePasswordOutputPort outputPort)
        => _outputPort = outputPort;

    public async Task ExecuteAsync(string email, string password)
    {
        bool validate = true;

        if (string.IsNullOrWhiteSpace(email))
        {
            validate = false;
            _outputPort?.UserEmpty();
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            validate = false;
            _outputPort?.PasswordEmpty();
        }

        if (!validate)
            return;

        validate = await _userRepository.ExistsAsync(email, password);

        if (validate)
            _outputPort?.Ok();
        else
            _outputPort?.InvalidUser();
    }
}