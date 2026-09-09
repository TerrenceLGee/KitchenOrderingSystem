using System.Net;
using System.Net.Http.Json;

using Auth.Api.Endpoints.Constants;
using Auth.Application.Command.Registration;

using FluentAssertions;

namespace Auth.IntegrationTests.Auth;

public class RegisterUserTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task RegisterUser_Returns_StatusCode_200OK_WhenSuccessful()
    {
        var registration = new RegisterUserCommand(
            "Johnny",
            "Carter",
            "jcarter@example.com",
            "Pa$$w0rd",
            "Pa$$w0rd",
            "Favorite food?",
            "Pizza");

        var registerResponse = await Client.PostAsJsonAsync($"{AuthConstants.BaseUri}{AuthConstants.Register}",
            registration,
            TestContext.Current.CancellationToken);

        registerResponse.IsSuccessStatusCode.Should().BeTrue();
        registerResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task RegisterUser_Returns_StatusCode_409Conflict_When_Email_Is_Already_InUse()
    {
        var registration = new RegisterUserCommand(
            "Johnny",
            "Carter",
            "customer@example.com",
            "Pa$$w0rd",
            "Pa$$w0rd",
            "Favorite food?",
            "Pizza");

        var registerResponse = await Client.PostAsJsonAsync($"{AuthConstants.BaseUri}{AuthConstants.Register}",
            registration,
            TestContext.Current.CancellationToken);

        registerResponse.IsSuccessStatusCode.Should().BeFalse();
        registerResponse.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    [Fact]
    public async Task RegisterUser_Returns_StatusCode_400BadRequest_When_Password_And_ConfirmPassword_Do_Not_Match()
    {
        var registration = new RegisterUserCommand(
            "Johnny",
            "Carter",
            "customer@example.com",
            "Pa$$w0rd",
            "Password",
            "Favorite food?",
            "Pizza");

        var registerResponse = await Client.PostAsJsonAsync($"{AuthConstants.BaseUri}{AuthConstants.Register}",
            registration,
            TestContext.Current.CancellationToken);

        registerResponse.IsSuccessStatusCode.Should().BeFalse();
        registerResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}