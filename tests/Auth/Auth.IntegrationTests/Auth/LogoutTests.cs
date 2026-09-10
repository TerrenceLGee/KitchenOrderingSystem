using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography;

using Auth.Api.Endpoints.Constants;
using Auth.Api.Queries;

using FluentAssertions;

namespace Auth.IntegrationTests.Auth;

public class LogoutTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task Logout_Returns_StatusCode_200OK_OnSuccess()
    {
        var refreshToken = await LoginAndSetAuthenticationAsync("customer@example.com", "Pa$$w0rd");

        var logoutQuery = new LogoutQuery(refreshToken, true);

        var logoutResponse = await Client.PostAsJsonAsync($"{AuthConstants.BaseUri}{AuthConstants.Logout}",
            logoutQuery,
            TestContext.Current.CancellationToken);

        logoutResponse.IsSuccessStatusCode.Should().BeTrue();
        logoutResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Logout_Returns_StatusCode_404NotFound_When_RefreshToken_IsInvalid()
    {
        _ = await LoginAndSetAuthenticationAsync("customer@example.com", "Pa$$w0rd");
        
        var refreshToken = GenerateInvalidRefreshToken();

        var logoutQuery = new LogoutQuery(refreshToken, true);

        var logoutResponse = await Client.PostAsJsonAsync($"{AuthConstants.BaseUri}{AuthConstants.Logout}",
            logoutQuery,
            TestContext.Current.CancellationToken);

        logoutResponse.IsSuccessStatusCode.Should().BeFalse();
        logoutResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private static string GenerateInvalidRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}