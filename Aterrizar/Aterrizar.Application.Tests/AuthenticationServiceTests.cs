using Aterrizar.Application.Authentication.Commands.Register;
using Aterrizar.Application.Authentication.Common;
using Aterrizar.Application.Authentication.Queries.Login;
using Aterrizar.Application.Common.Interfaces.Authentication;
using Aterrizar.Domain.Common.Errors.User;
using MediatR;
using Moq;

namespace Aterrizar.Application.Tests;

public class AuthenticationServiceTests
{
    [Fact]
    public async Task Login_WhenWrongEmail_ShouldReturnNotFoundUser()
    {
        var loginQuery = new LoginQuery("test@example.com", "testpass");
        var loginQueryHandler = new LoginQueryHandler(new UserRepositoryStub(), new Mock<IJwtTokenGenerator>().Object);

        var result = await loginQueryHandler.Handle(loginQuery, default);

        Assert.IsType<UserNotFound>(result.Errors[0] as UserNotFound);
    }

    [Fact]
    public async Task Login_WhenWrongPassword_ShouldReturnInvalidPassword()
    {
        var loginQuery = new LoginQuery("testuser@example.com", "testbad");
        var loginQueryHandler = new LoginQueryHandler(new UserRepositoryStub(), new Mock<IJwtTokenGenerator>().Object);

        var result = await loginQueryHandler.Handle(loginQuery, default);

        Assert.IsType<UserInvalidPassword>(result.Errors[0] as UserInvalidPassword);
    }

    [Fact]
    public async Task Login_ShouldReturnAuthenticationResult()
    {
        var loginQuery = new LoginQuery("testuser@example.com", "testpass");
        var loginQueryHandler = new LoginQueryHandler(new UserRepositoryStub(), new Mock<IJwtTokenGenerator>().Object);

        var result = await loginQueryHandler.Handle(loginQuery, default);

        Assert.IsType<AuthenticationResult>(result.Value);
    }

    [Fact]
    public async Task Register_ShouldReturnAuthenticationResult()
    {
        var registerCommand = new RegisterCommand("juan", "perez","testuser@gmail.com", "testpass");
        var registerCommandHandler = new RegisterCommandHandler(new UserRepositoryStub(), new Mock<IJwtTokenGenerator>().Object);

        var result = await registerCommandHandler.Handle(registerCommand, default);

        Assert.IsType<AuthenticationResult>(result.Value);
    }

    [Fact]
    public async Task Register_DuplicateEmail_ShouldReturnUserAlreadyExists()
    {
        var registerCommand = new RegisterCommand("juan", "perez","testuser@example.com", "testpass");
        var registerCommandHandler = new RegisterCommandHandler(new UserRepositoryStub(), new Mock<IJwtTokenGenerator>().Object);

        var result = await registerCommandHandler.Handle(registerCommand, default);

        Assert.IsType<UserEmailAlreadyExists>(result.Errors[0] as UserEmailAlreadyExists);
    }
}