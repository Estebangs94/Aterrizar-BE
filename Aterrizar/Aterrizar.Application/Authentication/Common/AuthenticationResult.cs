using Aterrizar.Domain.Entities;

namespace Aterrizar.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);