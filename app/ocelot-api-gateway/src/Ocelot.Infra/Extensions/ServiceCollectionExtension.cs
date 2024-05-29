using Microsoft.Extensions.DependencyInjection;
using Ocelot.Domain.Services.Account;
using Ocelot.Domain.UseCases.Auth.Login;
using Ocelot.Infra.Services.Account;

namespace Ocelot.Infra.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, string accountEndpoint)
    {
        services
            .AddAccountServices(accountEndpoint)
            .AddUseCases();

        return services;
    }

    private static IServiceCollection AddAccountServices(this IServiceCollection services,  string accountEndpoint)
    {
        services
            .AddHttpClient(
                "AccountApi",
                httpClient => httpClient.BaseAddress = new Uri(accountEndpoint));

        services
            .AddScoped<IUserService, UserService>();

        return services;
    }


    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services
            .AddScoped<ILoginUseCase, LoginUseCase>();

        return services;
    }
}