namespace Account.Domain.Strategies;

public class PasswordValidator : IPasswordValidator
{
    public bool Validate(string passoword)
    {
        if (passoword == null)
            return false;

        if (passoword.Length < 5)
            return false;

        if (!passoword.Any(char.IsLetter))
            return false;

        if (!passoword.Any(char.IsNumber))
            return false;
        
        return true;
    }
}