using FluentResults;

namespace Aterrizar.Domain.Common.Errors.User;

public class UserInvalidPassword : IError
{
    public List<IError> Reasons => new();

    public string Message { get; }

    public Dictionary<string, object> Metadata { get; }

    public UserInvalidPassword()
    {
        Message = "Invalid password";
        Metadata = new Dictionary<string, object>
        {
            {"StatusCode", 409}
        };
    }
}