using Aterrizar.Domain.Entities;

namespace Aterrizar.Application.Services.Authentication;

public record AuthenticationResult(
    User User,
    string Token);