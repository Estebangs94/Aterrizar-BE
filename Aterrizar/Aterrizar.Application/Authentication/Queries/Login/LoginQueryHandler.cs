using Aterrizar.Application.Authentication.Common;
using Aterrizar.Application.Common.Interfaces.Authentication;
using Aterrizar.Application.Common.Interfaces.Persistance;
using Aterrizar.Domain.Common.Errors.User;
using FluentResults;
using MediatR;

namespace Aterrizar.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginQueryHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetUserByEmail(query.Email);

        if (user is null)
            return Result.Fail(new UserNotFound(query.Email));

        if (user.Password != query.Password)
            return Result.Fail(new UserInvalidPassword());

        var token = await _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}