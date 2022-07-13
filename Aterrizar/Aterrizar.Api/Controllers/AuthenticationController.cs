using Aterrizar.Application.Authentication.Commands.Register;
using Aterrizar.Application.Authentication.Common;
using Aterrizar.Application.Authentication.Queries.Login;
using Aterrizar.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aterrizar.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);
        var authResult = await _mediator.Send(command);

        if (authResult.IsFailed) return Problem(authResult.Errors[0]);

        AuthenticationResponse response = MapToResponse(authResult.Value);

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);
        var authResult = await _mediator.Send(query);

        if (authResult.IsFailed) return Problem(authResult.Errors[0]);

        AuthenticationResponse response = MapToResponse(authResult.Value);

        return Ok(response);
    }

    private static AuthenticationResponse MapToResponse(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token
        );
    }
}