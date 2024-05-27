namespace Ocelot.Domain.Services.Account;

public record ResultError<T>
{
   public required T Error {get;set;}
   public string? Message {get;set;}
}

