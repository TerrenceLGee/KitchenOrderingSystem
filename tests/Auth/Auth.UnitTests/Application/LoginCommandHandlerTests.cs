using Auth.Application.Abstractions;
using Auth.Application.Command.Login;
using Auth.UnitTests.Domain.Resources;

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
    public async Task LoginComandHandler_Should_Return_Success_Result_Of_TokenResponse_When_Successful()
    {
        await using var db = TestDbContextFactory.Create();

        var userToAdd = UserResources.GetUserToAddToInMemoryDatabase();

        await db.Context.Users.AddAsync(userToAdd, TestContext.Current.CancellationToken);
        await db.Context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var loginCommand = new LoginCommand(
            userToAdd.Email.Value,
            userToAdd.Password.Value);

        var handler = new LoginCommandHandler(db.Context, _tokenService, _logger);

        var result = await handler.Handle(loginCommand, TestContext.Current.CancellationToken);

        result.IsSuccess.Should().BeTrue();
        result.Error.ErrorType.Should().Be(ErrorType.None);
        result.Value.Should().NotBeNull();
    }
}