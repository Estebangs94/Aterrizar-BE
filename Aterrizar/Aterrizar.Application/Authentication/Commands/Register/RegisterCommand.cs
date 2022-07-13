using Aterrizar.Application.Authentication.Common;
using FluentResults;
using MediatR;

namespace Aterrizar.Application.Authentication.Commands.Register;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<Result<AuthenticationResult>>;
