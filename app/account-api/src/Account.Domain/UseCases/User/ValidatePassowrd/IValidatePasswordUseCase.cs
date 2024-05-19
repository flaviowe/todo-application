namespace Account.Domain.UseCases.User.ValidatePassword;

public interface IValidatePasswordUseCase 
{
    void SetOutputPort(IValidatePasswordOutputPort outputPort);
    Task ExecuteAsync(string email, string password);
}