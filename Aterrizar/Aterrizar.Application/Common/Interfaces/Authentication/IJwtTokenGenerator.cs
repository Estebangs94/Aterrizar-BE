using Aterrizar.Domain.Entities;

namespace Aterrizar.Application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateToken(User user);
    }
}
