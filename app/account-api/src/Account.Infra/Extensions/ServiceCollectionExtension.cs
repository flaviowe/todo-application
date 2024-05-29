using Account.Domain.Repositories;
using Account.Domain.UseCases.User.ValidatePassword;
using Account.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Infra.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAccountInfra(this IServiceCollection services, Settings settings)
    {
        services
            .AddUseCases()
            .AddDatabase(settings.AccountConnectionString)
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IValidatePasswordUseCase, ValidatePasswordUseCase>();

        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AccountContext>(
            options => options.UseNpgsql(connectionString)
        );

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IUserRepository, UserRepository>();
            
        return services;
    }

}
