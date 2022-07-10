using FluentResults;

namespace Aterrizar.Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<Result<AuthenticationResult>> Login(string email, string password);
    Task<Result<AuthenticationResult>> Register(string firstName, string lastName, string email, string password);
}