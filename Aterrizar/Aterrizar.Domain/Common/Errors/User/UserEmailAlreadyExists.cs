using System.Globalization;
using FluentResults;

namespace Aterrizar.Domain.Common.Errors.User;

public class UserEmailAlreadyExists : IError
{
    public List<IError> Reasons => new();
    public string Message { get; }
    public Dictionary<string, object> Metadata { get; }

    public UserEmailAlreadyExists(string email)
    {
        Message = $"User with given {email} already exists";

        Metadata = new Dictionary<string, object>
        {
            { "StatusCode", 409 }
        };
    }

}