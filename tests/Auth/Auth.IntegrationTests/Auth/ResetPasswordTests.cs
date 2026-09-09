using System.Net;
using System.Net.Http.Json;

using Auth.Api.Endpoints.Constants;

using FluentAssertions;

namespace Auth.IntegrationTests.Auth;

public class ResetPasswordTests(IntegrationTestWebAppFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task ResetPassword_Returns_StatusCode_200OK_When_Successful()
    {
        var resetPasswordResponse = await Client.PutAsJsonAsync($"{AuthConstants.BaseUri}{AuthConstants.ResetPassword}",
            new
            {
                Email = "customer@example.com",
                PreviousPassword = "Pa$$w0rd",
                NewPassword = "Pr0gr@mmer123@",
                ConfirmNewPassword = "Pr0gr@mmer123@"
            },
            TestContext.Current.CancellationToken);

        resetPasswordResponse.IsSuccessStatusCode.Should().BeTrue();
        resetPasswordResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task ResetPassword_Returns_StatusCode_404NotFound_When_Email_Is_NotValid()
    {
        var resetPasswordResponse = await Client.PutAsJsonAsync($"{AuthConstants.BaseUri}{AuthConstants.ResetPassword}",
            new
            {
                Email = "unknown@example.com",
                PreviousPassword = "Pa$$w0rd",
                NewPassword = "Pr0gr@mmer123@",
                ConfirmNewPassword = "Pr0gr@mmer123@"
            },
            TestContext.Current.CancellationToken);

        resetPasswordResponse.IsSuccessStatusCode.Should().BeFalse();
        resetPasswordResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ResetPassword_Returns_StatusCode_401Unauthorized_When_PreviousPassword_IsInvalid()
    {
        var resetPasswordResponse = await Client.PutAsJsonAsync($"{AuthConstants.BaseUri}{AuthConstants.ResetPassword}",
            new
            {
                Email = "customer@example.com",
                PreviousPassword = "Unk0wn123@",
                NewPassword = "Pr0gr@mmer123@",
                ConfirmNewPassword = "Pr0gr@mmer123@"
            },
            TestContext.Current.CancellationToken);

        resetPasswordResponse.IsSuccessStatusCode.Should().BeFalse();
        resetPasswordResponse.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task
        ResetPassword_Returns_StatusCode_400BadRequest_When_Trying_To_ResetPassword_To_A_PreviousPassword()
    {
        var resetPasswordResponse = await Client.PutAsJsonAsync($"{AuthConstants.BaseUri}{AuthConstants.ResetPassword}",
            new
            {
                Email = "customer@example.com",
                PreviousPassword = "Pa$$w0rd",
                NewPassword = "Pa$$w0rd",
                ConfirmNewPassword = "Pa$$w0rd"
            },
            TestContext.Current.CancellationToken);

        resetPasswordResponse.IsSuccessStatusCode.Should().BeFalse();
        resetPasswordResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task
        ResetPassword_Returns_StatusCode_400BadRequest_When_NewPassword_And_ConfirmNewPassword_DoNot_Match()
    {
        var resetPasswordResponse = await Client.PutAsJsonAsync($"{AuthConstants.BaseUri}{AuthConstants.ResetPassword}",
            new
            {
                Email = "customer@example.com",
                PreviousPassword = "Pa$$w0rd",
                NewPassword = "Pr0gr@mmer123@",
                ConfirmNewPassword = "Programmer123@"
            },
            TestContext.Current.CancellationToken);

        resetPasswordResponse.IsSuccessStatusCode.Should().BeFalse();
        resetPasswordResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}