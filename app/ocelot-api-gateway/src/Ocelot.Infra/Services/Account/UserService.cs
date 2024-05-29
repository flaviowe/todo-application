using System.Net;
using System.Text.Json;
using Ocelot.Domain.Services.Account;

namespace Ocelot.Infra.Services.Account;

public class UserService : IUserService
{
    private HttpClient _httpClient;

    public UserService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("AccountApi");
    }

    public async Task<Result<ValidatePasswordResponse, ValidatePasswordErrors>> ValidatePasswordAsync(string email, string password)
    {
        var response = await _httpClient.GetAsync("/user/validatepassword");

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
            return new()
            {
                Success = true,
                Content = JsonSerializer.Deserialize<ValidatePasswordResponse>(content)
            };

        switch (response.StatusCode)
        {
            case HttpStatusCode.BadRequest:
                return new()
                {
                    Success = false,
                    Errors = GetError(content)
                };

            case HttpStatusCode.NotFound:
                return new()
                {
                    Success = false,
                    Errors = new[] { new ResultError<ValidatePasswordErrors>() {
                        Error = ValidatePasswordErrors.NotFound
                     } }
                };

            default:
                return new()
                {
                    Success = false,
                    Errors = new[] { new ResultError<ValidatePasswordErrors>() {
                        Error = ValidatePasswordErrors.Other
                     } }
                };
        }

    }

    private ResultError<ValidatePasswordErrors>[] GetError(string content)
    {
        List<ResultError<ValidatePasswordErrors>> resultError = new();

        var defaultErrors = JsonSerializer.Deserialize<DefaultError[]>(content);

        if (defaultErrors == null)
        {
            resultError.Add(new ResultError<ValidatePasswordErrors>()
            {
                Error = ValidatePasswordErrors.Other,
                Message = content
            });

            return resultError.ToArray();
        }

        foreach (var defaultError in defaultErrors)
        {
            switch (defaultError.Error)
            {
                case "INVALID_EMAIL":
                    resultError.Add(new ResultError<ValidatePasswordErrors>
                    {
                        Error = ValidatePasswordErrors.InvalidEmail
                    });
                    break;

                case "INVALID_PASSWORD":
                    resultError.Add(new ResultError<ValidatePasswordErrors>
                    {
                        Error = ValidatePasswordErrors.InvalidPassword
                    });
                    break;

                default:
                    resultError.Add(new ResultError<ValidatePasswordErrors>
                    {
                        Error = ValidatePasswordErrors.Other,
                        Message = defaultError.Message
                    });
                    break;
            }

        }
        
        return resultError.ToArray();
    }
}