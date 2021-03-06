using Aterrizar.Application.Authentication.Common;
using FluentResults;
using MediatR;

namespace Aterrizar.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password) : IRequest<Result<AuthenticationResult>>;