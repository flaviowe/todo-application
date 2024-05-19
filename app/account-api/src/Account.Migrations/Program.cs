// See https://aka.ms/new-console-template for more information
using Account.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

CreateHostBuilder(args).Build().Run();

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            services.AddDbContext<AccountContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable("ACCOUNT_CONNECTION_STRING")));
        });

