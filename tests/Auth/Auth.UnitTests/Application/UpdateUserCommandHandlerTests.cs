using Auth.Application.Command.UpdateUser;
using Auth.UnitTests.TestResources;

using FluentAssertions;

using KitchenOrderingSystem.Shared.Common;

using Microsoft.Extensions.Logging;

using NSubstitute;

namespace Auth.UnitTests.Application;

public class UpdateUserCommandHandlerTests
{
    private readonly ILogger<UpdateUserCommandHandler> _logger =
        Substitute.For<ILogger<UpdateUserCommandHandler>>();

    [Fact]
    public async Task UpdateUserCommandHandler_Returns_Success_Result_With_Valid_Input()
    {
        await using var db = TestDbContextFactory.Create();

        var user = UserFactory.Create();

        await db.Context.Users.AddAsync(user, TestContext.Current.CancellationToken);
        await db.Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var updateUserCommand = new UpdateUserCommand(
            user.Id,
            null,
            null,
            null,
            "Favorite food?",
            "Cheeseburgers");

        var handler = new UpdateUserCommandHandler(db.Context, _logger);

        var result = await handler.Handle(updateUserCommand, TestContext.Current.CancellationToken);

        result.IsSuccess.Should().BeTrue();
        result.Error.ErrorType.Should().Be(ErrorType.None);
    }

    [Fact]
    public async Task UpdateUserCommandHandler_Returns_Failure_Result_When_Trying_To_Update_Non_Existent_User()
    {
        await using var db = TestDbContextFactory.Create();

        var userId = Guid.CreateVersion7();

        var updateUserCommand = new UpdateUserCommand(
            userId,
            null,
            null,
            null,
            "Favorite food?",
            "Cheeseburgers");

        var handler = new UpdateUserCommandHandler(db.Context, _logger);

        var result = await handler.Handle(updateUserCommand, TestContext.Current.CancellationToken);

        result.IsSuccess.Should().BeFalse();
        result.Error.ErrorType.Should().Be(ErrorType.NotFound);
        result.Error.Code.Should().Be("User.NotFound");
    }
}