using Auth.Application.Abstractions;
using Auth.Application.Command.Login;
using Auth.Application.Command.Logout;
using Auth.Domain.Entities;
using Auth.Domain.Entities.ValueObjects.User;
using Auth.Infrastructure.Persistence;
using Auth.UnitTests.TestResources;

using FluentAssertions;

using KitchenOrderingSystem.Shared.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using NSubstitute;

namespace Auth.UnitTests.Application;

public class LogoutCommandHandlerTests
{
    private readonly ILogger<LogoutCommandHandler> _logger =
        Substitute.For<ILogger<LogoutCommandHandler>>();

    private readonly ILogger<LoginCommandHandler> _loggerToGetToken =
        Substitute.For<ILogger<LoginCommandHandler>>();

    private readonly ITokenService _tokenServiceToGetToken =
        Substitute.For<ITokenService>();

    private const string Email = "customer@example.com";

    [Fact]
    public async Task LogoutCommandHandler_Should_Return_Success_Result_When_Token_Is_Valid()
    {
        await using var db = TestDbContextFactory.Create();

        var userId = await GetUserId(db.Context, Email);
        (RefreshToken refreshToken, string tokenValue) = RefreshTokenFactory.CreateValidToken(userId);

        await db.Context.RefreshTokens.AddAsync(refreshToken, TestContext.Current.CancellationToken);
        await db.Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var logoutCommand = new LogoutCommand(
            userId,
            tokenValue,
            true,
            Email);

        var handler = new LogoutCommandHandler(db.Context, _logger);

        var result = await handler.Handle(logoutCommand, TestContext.Current.CancellationToken);

        result.IsSuccess.Should().BeTrue();
        result.Error.ErrorType.Should().Be(ErrorType.None);
    }

    [Fact]
    public async Task LogoutCommandHandler_Should_Return_Failure_Result_When_Token_Is_Already_Expired()
    {
        await using var db = TestDbContextFactory.Create();

        var userId = await GetUserId(db.Context, Email);
        (RefreshToken refreshToken, string tokenValue) = RefreshTokenFactory.CreateExpiredToken(userId);

        await db.Context.RefreshTokens.AddAsync(refreshToken, TestContext.Current.CancellationToken);
        await db.Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var logoutCommand = new LogoutCommand(
            userId,
            tokenValue,
            true,
            Email);

        var handler = new LogoutCommandHandler(db.Context, _logger);

        var result = await handler.Handle(logoutCommand, TestContext.Current.CancellationToken);

        result.IsSuccess.Should().BeFalse();
        result.Error.ErrorType.Should().Be(ErrorType.BadRequest);
        result.Error.Code.Should().Be("RefreshToken.Expired");
    }

    [Fact]
    public async Task LogoutCommandHandler_Should_Return_Failure_Result_When_Token_Is_Already_Revoked()
    {
        await using var db = TestDbContextFactory.Create();

        var userId = await GetUserId(db.Context, Email);
        (RefreshToken refreshToken, string tokenValue) = RefreshTokenFactory.CreateRevokedToken(userId);

        refreshToken.Revoke();
        await db.Context.RefreshTokens.AddAsync(refreshToken, TestContext.Current.CancellationToken);
        await db.Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var logoutCommand = new LogoutCommand(
            userId,
            tokenValue,
            true,
            Email);

        var handler = new LogoutCommandHandler(db.Context, _logger);

        var result = await handler.Handle(logoutCommand, TestContext.Current.CancellationToken);

        result.IsSuccess.Should().BeFalse();
        result.Error.ErrorType.Should().Be(ErrorType.Conflict);
        result.Error.Code.Should().Be("RefreshToken.Revoked");
    }

    [Fact]
    public async Task LogoutCommandHandler_Should_Return_Failure_Result_When_Token_IsInvalid()
    {
        await using var db = TestDbContextFactory.Create();

        var userId = await GetUserId(db.Context, Email);
        (RefreshToken refreshToken, string tokenValue) = RefreshTokenFactory.CreateInvalidToken(userId);

        await db.Context.RefreshTokens.AddAsync(refreshToken, TestContext.Current.CancellationToken);
        await db.Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var logoutCommand = new LogoutCommand(
            userId,
            tokenValue,
            true,
            Email);

        var handler = new LogoutCommandHandler(db.Context, _logger);

        var result = await handler.Handle(logoutCommand, TestContext.Current.CancellationToken);

        result.IsSuccess.Should().BeFalse();
        result.Error.ErrorType.Should().Be(ErrorType.NotFound);
        result.Error.Code.Should().Be("RefreshToken.Invalid");
    }

    private static async Task<Guid> GetUserId(ApplicationDbContext context, string userEmail)
    {
        var email = new Email(userEmail);
        var user = await context.Users
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync(TestContext.Current.CancellationToken);

        return user?.Id ?? Guid.CreateVersion7();
    }
}