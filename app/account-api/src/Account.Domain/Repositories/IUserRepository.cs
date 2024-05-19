
namespace Account.Domain.Repositories;

public interface IUserRepository
{
    Task<bool> ExistsAsync(string email, string password);
}
