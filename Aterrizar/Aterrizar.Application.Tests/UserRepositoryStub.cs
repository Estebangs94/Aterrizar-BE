using Aterrizar.Application.Common.Interfaces.Persistance;
using Aterrizar.Domain.Entities;

namespace Aterrizar.Application.Tests;

internal class UserRepositoryStub : IUserRepository
{
    private static readonly User? NULL_USER = null;
    private readonly Dictionary<string, User> _users;

    public UserRepositoryStub()
    {
        _users = new Dictionary<string, User>
        {
            {"testuser@example.com", new User {Password = "testpass"}}
        };
    }

    public void Add(User user)
    {
    }

    public User? GetUserByEmail(string email)
    {
        if (!_users.ContainsKey(email))
        {
            return NULL_USER;
        }

        return _users[email];
    }
}