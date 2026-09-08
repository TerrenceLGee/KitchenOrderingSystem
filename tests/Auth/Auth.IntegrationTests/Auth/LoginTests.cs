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
}