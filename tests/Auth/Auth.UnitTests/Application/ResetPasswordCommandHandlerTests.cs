using Auth.Application.Command.ResetPassword;
using Auth.Domain.Entities;
using Auth.UnitTests.TestResources;

using FluentAssertions;

using KitchenOrderingSystem.Shared.Common;

using Microsoft.Extensions.Logging;

using NSubstitute;

namespace Auth.UnitTests.Application;

public class ResetPasswordCommandHandlerTests
{
    private readonly ILogger<ResetPasswordCommandHandler> _logger =
        Substitute.For<ILogger<ResetPasswordCommandHandler>>();

    [Fact]
    public async Task ResetPasswordCommandHandler_Returns_Success_Result_When_Given_Valid_Input()
    {
        await using var db = TestDbContextFactory.Create();

        var user = UserFactory.Create();

        await db.Context.Users.AddAsync(user, TestContext.Current.CancellationToken);

        var userPassword = UserPassword.Create(user.Id, user.Password.Value);
        await db.Context.UserPasswords.AddAsync(userPassword, TestContext.Current.CancellationToken);
        await db.Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var resetPasswordCommand = new ResetPasswordCommand(
            user.Email.Value,
            "Pa$$w0rd",
            "Pr0gr@mmer",
            "Pr0gr@mmer");

        var handler = new ResetPasswordCommandHandler(db.Context, _logger);

        var result = await handler.Handle(resetPasswordCommand, TestContext.Current.CancellationToken);

        result.IsSuccess.Should().BeTrue();
        result.Error.ErrorType.Should().Be(ErrorType.None);
    }

    [Fact]
    public async Task
        ResetPasswordCommandHandler_Returns_Failure_Result_When_UnRegistered_User_Tries_To_Reset_Password()
    {
        await using var db = TestDbContextFactory.Create();

        var resetPasswordCommand = new ResetPasswordCommand(
            "unknownUser@example.com",
            "Pa$$w0rd",
            "Pr0gr@mmer",
            "Pr0gr@mmer");

        var handler = new ResetPasswordCommandHandler(db.Context, _logger);

        var result = await handler.Handle(resetPasswordCommand, TestContext.Current.CancellationToken);

        result.IsSuccess.Should().BeFalse();
        result.Error.ErrorType.Should().Be(ErrorType.NotFound);
        result.Error.Code.Should().Be("Invalid.Credentials");
    }

    [Fact]
    public async Task
        ResetPasswordCommandHandler_Returns_Failure_Result_When_Trying_To_Reset_Password_To_Previous_Password()
    {
        await using var db = TestDbContextFactory.Create();

        var user = UserFactory.Create();

        await db.Context.Users.AddAsync(user, TestContext.Current.CancellationToken);

        var userPassword = UserPassword.Create(user.Id, user.Password.Value);
        await db.Context.UserPasswords.AddAsync(userPassword, TestContext.Current.CancellationToken);
        await db.Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var resetPasswordCommand = new ResetPasswordCommand(
            user.Email.Value,
            "Pa$$w0rd",
            "Pa$$w0rd",
            "Pa$$w0rd");

        var handler = new ResetPasswordCommandHandler(db.Context, _logger);

        var result = await handler.Handle(resetPasswordCommand, TestContext.Current.CancellationToken);

        result.IsSuccess.Should().BeFalse();
        result.Error.ErrorType.Should().Be(ErrorType.BadRequest);
        result.Error.Code.Should().Be("PreviousPassword.CannotBeReused");
    }

    [Fact]
    public async Task ResetPasswordCommandHandler_Returns_Failure_Result_When_PreviousPassword_Input_Is_Invalid()
    {
        await using var db = TestDbContextFactory.Create();

        var user = UserFactory.Create();

        await db.Context.Users.AddAsync(user, TestContext.Current.CancellationToken);

        var userPassword = UserPassword.Create(user.Id, user.Password.Value);
        await db.Context.UserPasswords.AddAsync(userPassword, TestContext.Current.CancellationToken);
        await db.Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var resetPasswordCommand = new ResetPasswordCommand(
            user.Email.Value,
            "password",
            "Pr0gr@mmer",
            "Pr0gr@mmer");

        var handler = new ResetPasswordCommandHandler(db.Context, _logger);

        var result = await handler.Handle(resetPasswordCommand, TestContext.Current.CancellationToken);

        result.IsSuccess.Should().BeFalse();
        result.Error.ErrorType.Should().Be(ErrorType.Unauthorized);
        result.Error.Code.Should().Be("PreviousPassword.Invalid");
    }
}