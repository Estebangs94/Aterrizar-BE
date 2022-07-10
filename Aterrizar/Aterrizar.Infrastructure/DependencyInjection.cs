using Aterrizar.Application.Common.Interfaces.Authentication;
using Aterrizar.Application.Common.Interfaces.Persistance;
using Aterrizar.Application.Common.Services;
using Aterrizar.Infrastructure.Authentication;
using Aterrizar.Infrastructure.Persistance;
using Aterrizar.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aterrizar.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, InMemoryUserRepository>();

        return services;
    }
}