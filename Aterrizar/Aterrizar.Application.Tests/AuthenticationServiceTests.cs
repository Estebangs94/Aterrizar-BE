using Aterrizar.Application.Common.Interfaces.Authentication;
using Aterrizar.Application.Services.Authentication;
using Aterrizar.Domain.Common.Errors.User;
using Moq;

namespace Aterrizar.Application.Tests;

public class AuthenticationServiceTests
{
    [Fact]
    public async Task Login_WhenWrongEmail_ShouldReturnNotFoundUser()
    {
        IAuthenticationService target = SutFactory();

        var result = await target.Login("test@example.com", "testpass");

        Assert.IsType<UserNotFound>(result.Errors[0] as UserNotFound);
    }

    [Fact]
    public async Task Login_WhenWrongPassword_ShouldReturnInvalidPassword()
    {
        IAuthenticationService target = SutFactory();

        var result = await target.Login("testuser@example.com", "testbad");

        Assert.IsType<UserInvalidPassword>(result.Errors[0] as UserInvalidPassword);
    }

    [Fact]
    public async Task Login_ShouldReturnUser()
    {
        IAuthenticationService target = SutFactory();

        var result = await target.Login("testuser@example.com", "testpass");

        Assert.IsType<AuthenticationResult>(result.Value);
    }

    private static IAuthenticationService SutFactory()
    {
        return new AuthenticationService(new UserRepositoryStub(), new Mock<IJwtTokenGenerator>().Object);
    }
}