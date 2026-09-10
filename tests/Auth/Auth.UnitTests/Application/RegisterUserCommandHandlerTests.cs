using Auth.Application.Command.Registration;
using Auth.UnitTests.TestResources;

using FluentAssertions;

using KitchenOrderingSystem.Shared.Common;

using Microsoft.Extensions.Logging;

using NSubstitute;

namespace Auth.UnitTests.Application;

public class RegisterUserCommandHandlerTests
{
    private readonly ILogger<RegisterUserCommandHandler> _logger =
        Substitute.For<ILogger<RegisterUserCommandHandler>>();

    [Fact]
    public async Task RegisterUserCommandHandler_Returns_Success_Result_OnValid_Input()
    {
        await using var db = TestDbContextFactory.Create();

        var registerUserCommand = new RegisterUserCommand(
            "Glenn",
            "Leonard",
            "gleonard@example.com",
            "Pa$$w0rd",
            "Pa$$w0rd",
            "Favorite restaurant?",
            "Red Lobster");

        var handler = new RegisterUserCommandHandler(db.Context, _logger);

        var result = await handler.Handle(registerUserCommand, TestContext.Current.CancellationToken);

        result.IsSuccess.Should().BeTrue();
        result.Error.ErrorType.Should().Be(ErrorType.None);
    }

    [Fact]
    public async Task
        RegisterUserCommandHandler_Returns_Failure_Result_When_Trying_To_Register_With_An_Already_Registered_Email()
    {
        await using var db = TestDbContextFactory.Create();

        var user = UserFactory.Create();

        await db.Context.Users.AddAsync(user, TestContext.Current.CancellationToken);
        await db.Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var registerUserCommand = new RegisterUserCommand(
            "Glenn",
            "Leonard",
            user.Email.Value,
            "Pa$$w0rd",
            "Pa$$w0rd",
            "Favorite restaurant?",
            "Red Lobster");

        var handler = new RegisterUserCommandHandler(db.Context, _logger);

        var result = await handler.Handle(registerUserCommand, TestContext.Current.CancellationToken);

        result.IsSuccess.Should().BeFalse();
        result.Error.ErrorType.Should().Be(ErrorType.Conflict);
        result.Error.Code.Should().Be("Email.AlreadyRegistered");
    }
}