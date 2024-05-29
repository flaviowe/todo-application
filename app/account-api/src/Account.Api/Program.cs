using Account.Api.Extensions;
using Account.Infra.Extensions;

string accountConnectionString = Environment.GetEnvironmentVariable("ACCOUNT_CONNECTION_STRING") ?? "";
string tokenKey = Environment.GetEnvironmentVariable("TOKEN_KEY") ?? "tokenKey";
string tokenIssuer = Environment.GetEnvironmentVariable("TOKEN_ISSUER") ?? "tokenIssuer";
string tokenAudience = Environment.GetEnvironmentVariable("TOKEN_AUDIENCE") ?? "tokenAudience";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiServices(
    tokenKey: tokenKey,
    tokenIssuer: tokenIssuer,
    tokenAudience: tokenAudience
);

builder.Services.AddAccountInfra(
    new()
    {
        AccountConnectionString = accountConnectionString,
    }
);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
