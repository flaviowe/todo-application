namespace Ocelot.Domain.UseCases.Auth.Login;

public interface ILoginOutputPort
{
    void InvalidEmail();
    void InvalidPassword();
    void Ok(string token);
    void Undefined();
    void Undefined(string message);
}