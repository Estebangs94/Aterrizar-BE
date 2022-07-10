using Aterrizar.Application.Common.Interfaces.Authentication;
using Aterrizar.Application.Common.Interfaces.Persistance;
using Aterrizar.Domain.Common.Errors.User;
using Aterrizar.Domain.Entities;
using FluentResults;

namespace Aterrizar.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<AuthenticationResult>> Login(string email, string password)
    {
        var user = _userRepository.GetUserByEmail(email);

        if (user is null)
            return Result.Fail(new UserNotFound(email));

        if (user.Password != password)
            return Result.Fail(new UserInvalidPassword());

        var token = await _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }

    public async Task<Result<AuthenticationResult>> Register(string firstName, string lastName, string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not null)
            return Result.Fail(new UserEmailAlreadyExists(email));

        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        _userRepository.Add(user);

        var token = await _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}