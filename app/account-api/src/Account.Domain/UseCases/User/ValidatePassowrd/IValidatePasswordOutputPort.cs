namespace Account.Domain.UseCases.User.ValidatePassword;

public interface IValidatePasswordOutputPort
{
    void InvalidUser();
    void Ok();
    void PasswordEmpty();
    void UserEmpty();
}