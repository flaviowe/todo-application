namespace Ocelot.Domain.UseCases.Auth.Login;

public interface ILoginUseCase
{
    void SetOutputPort(ILoginOutputPort loginOutputPort);
    Task ExecuteAsync(string email, string password);
}