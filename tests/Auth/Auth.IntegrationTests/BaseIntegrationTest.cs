using System.Net.Http.Headers;
using System.Net.Http.Json;

using Auth.Application.Abstractions;
using Auth.Infrastructure.Persistence;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace Auth.IntegrationTests;

public class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    protected readonly IMediator Sender;
    protected readonly ApplicationDbContext Context;
    protected readonly HttpClient Client;
    protected IntegrationTestWebAppFactory Factory { get; }

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        Factory = factory;
        using var scope = factory.Services.CreateScope();
        Client = factory.CreateClient();
        Sender = scope.ServiceProvider.GetRequiredService<IMediator>();
        Context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }

    protected async Task LoginAndSetAuthenticationAsync(string email, string password)
    {
        var loginResponse = await Client
            .PostAsJsonAsync("/api/auth/login", new { Email = email, Password = password },
                TestContext.Current.CancellationToken);
        loginResponse.EnsureSuccessStatusCode();

        var loginResult = await loginResponse.Content
            .ReadFromJsonAsync<TokenResponse>(TestContext.Current.CancellationToken);

        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", loginResult!.AccessToken);
    }
}