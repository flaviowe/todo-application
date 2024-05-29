using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ocelot.ApiGateway.Services;
using Ocelot.Domain.Services;

namespace Ocelot.ApiGateway.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApiGatewayServices(
        this IServiceCollection services,
        string tokenKey,
        string tokenIssuer,
        string tokenAudience)
    {
        services
            .AddSingleton(new JwtTokenSettings()
            {
                TokenKey = tokenKey,
                TokenIssuer = tokenIssuer,
                TokenAudience = tokenAudience
            })
            .AddSwaggerDocumentation()
            .AddAuthentication(
                tokenKey,
                tokenIssuer,
                tokenAudience
            );

        return services;
    }

    private static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        string tokenKey,
        string tokenIssuer,
        string tokenAudience)
    {
        var key = Encoding.ASCII.GetBytes(tokenKey);
        services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenIssuer,
                    ValidAudience = tokenAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });

        services.AddScoped<ITokenService, JwtTokenService>();

        return services;
    }

    private static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(setup =>
            {
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { jwtSecurityScheme, Array.Empty<string>() }
                    });
            });

        return services;
    }
}