using Auth.Application.Abstractions;
using Auth.Application.Command.Login;
using Auth.Domain.Entities;
using Auth.UnitTests.TestResources;

using FluentAssertions;

using KitchenOrderingSystem.Shared.Common;

using Microsoft.Extensions.Logging;

using NSubstitute;

namespace Auth.UnitTests.Application;

public class LoginCommandHandlerTests
{
    private readonly ILogger<LoginCommandHandler> _logger =
        Substitute.For<ILogger<LoginCommandHandler>>();

    private readonly ITokenService _tokenService =
        Substitute.For<ITokenService>();

    [Fact]
    public async Task LoginCommandHandler_Should_Return_Success_Result_Of_TokenResponse_When_Successful()
    {
        await using var db = TestDbContextFactory.Create();
        
        var loginCommand = new LoginCommand(
            "customer@example.com",
            "Pa$$w0rd");

        _tokenService
            .GetAccessTokenAsync(
                Arg.Any<User>(), 
                Arg.Any<UserRole[]>(),
                Arg.Any<CancellationToken>())
            .Returns(new TokenResponse(
                "accessTokenValue",
                "refreshTokenValue",
                DateTime.UtcNow.AddHours(1),
                DateTime.UtcNow.AddDays(7)));

        var handler = new LoginCommandHandler(db.Context, _tokenService, _logger);

        var result = await handler.Handle(loginCommand, TestContext.Current.CancellationToken);

        result.IsSuccess.Should().BeTrue();
        result.Error.ErrorType.Should().Be(ErrorType.None);
        result.Value.Should().NotBeNull();
    }

    [Fact]
    public async Task LoginCommandHandler_Should_Return_Failure_Result_When_User_Is_NonExistent()
    {
        await using var db = TestDbContextFactory.Create();

        var loginCommand = new LoginCommand(
            "customer2@example.com",
            "Pa$$w0rd");

        var handler = new LoginCommandHandler(db.Context, _tokenService, _logger);

        var result = await handler.Handle(loginCommand, TestContext.Current.CancellationToken);

        result.IsSuccess.Should().BeFalse();
        result.Error.ErrorType.Should().Be(ErrorType.NotFound);
        result.Error.Code.Should().Be("Invalid.Credentials");
    }

    [Fact]
    public async Task LoginCommandHandler_Should_Return_Failure_Result_When_Invalid_Password_Is_Input()
    {
        await using var db = TestDbContextFactory.Create();

        var loginCommand = new LoginCommand(
            "customer@example.com",
            "Password");
        
        var handler = new LoginCommandHandler(db.Context, _tokenService, _logger);

        var result = await handler.Handle(loginCommand, TestContext.Current.CancellationToken);

        result.IsSuccess.Should().BeFalse();
        result.Error.ErrorType.Should().Be(ErrorType.Unauthorized);
        result.Error.Code.Should().Be("Invalid Credentials");
    }
}