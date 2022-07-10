using FluentResults;

namespace Aterrizar.Domain.Common.Errors.User;

public class UserNotFound : IError
{
    public List<IError> Reasons => new();

    public string Message { get; }

    public Dictionary<string, object> Metadata { get; }

    public UserNotFound(string email)
    {
        Message = $"User with email {email} was not found";
        Metadata = new Dictionary<string, object>
        {
            {"StatusCode", 404}
        };
    }
}