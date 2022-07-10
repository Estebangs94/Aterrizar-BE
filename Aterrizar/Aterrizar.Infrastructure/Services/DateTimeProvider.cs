using Aterrizar.Application.Common.Services;

namespace Aterrizar.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}