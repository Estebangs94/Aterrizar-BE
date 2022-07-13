using Aterrizar.Application.Authentication.Common;
using Aterrizar.Application.Common.Interfaces.Authentication;
using Aterrizar.Application.Common.Interfaces.Persistance;
using Aterrizar.Domain.Common.Errors.User;
using Aterrizar.Domain.Entities;
using FluentResults;
using MediatR;

namespace Aterrizar.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(command.Email) is not null)
            return Result.Fail(new UserEmailAlreadyExists(command.Email));

        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        };

        _userRepository.Add(user);

        var token = await _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}