using System.Net;
using System.Net.Http.Json;

using Auth.Api.Endpoints.Constants;
using Auth.Application.Abstractions;

using FluentAssertions;

namespace Auth.IntegrationTests.Auth;

public class LoginTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Login_Returns_StatusCode_200OK_WhenSuccessful()
    {
        var loginResponse = await Client.PostAsJsonAsync($"{AuthConstants.BaseUri}{AuthConstants.Login}",
            new { Email = "customer@example.com", Password = "Pa$$w0rd" },
            TestContext.Current.CancellationToken);

        var loginResult = await loginResponse.Content.ReadFromJsonAsync<TokenResponse>(
            TestContext.Current.CancellationToken);

        loginResponse.IsSuccessStatusCode.Should().BeTrue();
        loginResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        loginResult.Should().NotBeNull();
        loginResult.AccessToken.Should().BeOfType<string>();
        loginResult.RefreshToken.Should().BeOfType<string>();
    }

    [Fact]
    public async Task Login_Returns_StatusCode_404NotFound_When_User_Email_NotFound()
    {
        var loginResponse = await Client.PostAsJsonAsync($"{AuthConstants.BaseUri}{AuthConstants.Login}",
            new { Email = "unknown@example.com", Password = "Pa$$w0rd" },
            TestContext.Current.CancellationToken);

        var loginResult = await loginResponse.Content.ReadFromJsonAsync<TokenResponse>(
            TestContext.Current.CancellationToken);

        loginResponse.IsSuccessStatusCode.Should().BeFalse();
        loginResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        loginResult.Should().NotBeNull();
    }

    [Fact]
    public async Task Login_Returns_StatusCode_401Unauthorized_When_WrongPassword_IsEntered()
    {
        var loginResponse = await Client.PostAsJsonAsync($"{AuthConstants.BaseUri}{AuthConstants.Login}",
            new { Email = "customer@example.com", Password = "password" },
            TestContext.Current.CancellationToken);

        var loginResult = await loginResponse.Content.ReadFromJsonAsync<TokenResponse>(
            TestContext.Current.CancellationToken);

        loginResponse.IsSuccessStatusCode.Should().BeFalse();
        loginResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        loginResult.Should().NotBeNull();
    }
}