namespace Ocelot.Domain.Services.Account;

public enum ValidatePasswordErrors
{
    InvalidEmail = 0,
    InvalidPassword = 1,
    NotFound = 2,
    Other = 3,
}