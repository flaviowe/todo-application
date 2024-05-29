using Account.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Account.Infra.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AccountContext _context;

    public UserRepository(AccountContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync(string email, string password)
    {
        var query = from user in _context.Users
                    where user.Email == email &&
                    user.Password == password
                    select user;

        return await query.AnyAsync();
    }
}