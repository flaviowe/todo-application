namespace Ocelot.Domain.Services.Account;

public record Result<T, TErrors> where TErrors : Enum 
{
    public bool Success { get; set; }
    public T? Content {get;set;}
    public ResultError<TErrors>[]? Errors { get; set; }
}