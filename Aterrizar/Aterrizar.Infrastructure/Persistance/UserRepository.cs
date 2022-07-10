using Aterrizar.Application.Common.Interfaces.Persistance;
using Aterrizar.Domain.Entities;

namespace Aterrizar.Infrastructure.Persistance
{
    internal class InMemoryUserRepository : IUserRepository
    {
        private static readonly List<User> _users = new();

        public void Add(User user)
        {
            _users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
            return _users.SingleOrDefault(e => e.Email == email);
        }
    }
}
