using Ocelot.Infra.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.ApiGateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

string accountEndpoint = Environment.GetEnvironmentVariable("ACCOUNT_ENDPOINT") ?? "localhost";
string tokenKey = Environment.GetEnvironmentVariable("TOKEN_KEY") ?? "tokenKey";
string tokenIssuer = Environment.GetEnvironmentVariable("TOKEN_ISSUER") ?? "tokenIssuer";
string tokenAudience = Environment.GetEnvironmentVariable("TOKEN_AUDIENCE") ?? "tokenAudience";

builder.Configuration.AddJsonFile("ocelot.json");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOcelot();

builder.Services.AddApiGatewayServices(
    tokenKey: tokenKey,
    tokenIssuer: tokenIssuer,
    tokenAudience: tokenAudience
);

builder.Services.AddInfraestructure(accountEndpoint);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.UseOcelot();

app.Run();