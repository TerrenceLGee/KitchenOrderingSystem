using System.Net;
using System.Net.Http.Json;

using Auth.Api.Endpoints.Constants;
using Auth.Api.Queries;

using FluentAssertions;

namespace Auth.IntegrationTests.Auth;

public class UpdateUserTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task UpdateUser_Returns_StatusCode_204NoContent_When_Successful()
    {
        await LoginAndSetAuthenticationAsync("customer@example.com", "Pa$$w0rd");
        
        var update = new UpdateUserQuery(
            null,
            null,
            "updatedCustomer@example.com",
            null,
            null);
        
        var updateUserResponse = await Client.PutAsJsonAsync($"{AuthConstants.BaseUri}{AuthConstants.UpdateUserInfo}",
            update,
            TestContext.Current.CancellationToken);

        updateUserResponse.IsSuccessStatusCode.Should().BeTrue();
        updateUserResponse.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}