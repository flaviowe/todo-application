namespace Ocelot.Domain.Services.Account;

public interface IUserService
{
    Task<Result<ValidatePasswordResponse, ValidatePasswordErrors>> ValidatePasswordAsync(string email, string password);
}
