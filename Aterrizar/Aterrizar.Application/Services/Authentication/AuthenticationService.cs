using Aterrizar.Application.Common.Interfaces.Authentication;
using Aterrizar.Application.Common.Interfaces.Persistance;
using Aterrizar.Domain.Entities;

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

    public async Task<AuthenticationResult> Login(string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not User user)
                throw new InvalidOperationException($"User with given email {email} already exists");

            if (user.Password != password)
                throw new InvalidOperationException("Invalid password.");

            var token = await _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
    }

    public async Task<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        if (_userRepository.GetUserByEmail(email) is not null)
                throw new Exception($"User with given email {email} already exists");

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