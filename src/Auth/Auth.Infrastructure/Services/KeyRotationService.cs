using System.Security.Cryptography;

using Auth.Application.Abstractions;
using Auth.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Auth.Infrastructure.Services;

public class KeyRotationService(IServiceProvider serviceProvider) : BackgroundService
{
    private readonly TimeSpan _rotationInterval = TimeSpan.FromDays(7);
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await RotateKeysAsync();

            await Task.Delay(_rotationInterval, stoppingToken);
        } 
    }

    private async Task RotateKeysAsync()
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

        var activeKey = await context.SigningKeys
            .FirstOrDefaultAsync(k => k.IsActive);

        if (activeKey is null || activeKey.ExpiresAtUtc <= DateTime.UtcNow.AddDays(10))
        {
            if (activeKey is not null)
            {
                activeKey.DeactivateKey();
                context.SigningKeys.Update(activeKey);
            }

            using var rsa = RSA.Create(2048);

            var privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());

            var publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());

            var newKeyId = Guid.NewGuid();

            var newKey = SigningKey.Create(
                newKeyId,
                privateKey,
                publicKey,
                true,
                DateTime.UtcNow.AddYears(1));

            await context.SigningKeys.AddAsync(newKey);

            await context.SaveChangesAsync();
        }
    }
}