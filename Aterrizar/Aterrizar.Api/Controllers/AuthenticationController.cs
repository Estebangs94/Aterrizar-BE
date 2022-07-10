using Aterrizar.Application.Services.Authentication;
using Aterrizar.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Aterrizar.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var authResult = await _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

        if (authResult.IsFailed) return Problem(authResult.Errors[0]);

        AuthenticationResponse response = MapToResponse(authResult.Value);

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var authResult = await _authenticationService.Login(request.Email, request.Password);

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