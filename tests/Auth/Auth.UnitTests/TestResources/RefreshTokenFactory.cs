using System.Security.Cryptography;
using System.Text;

using Auth.Domain.Entities;

namespace Auth.UnitTests.TestResources;

public static class RefreshTokenFactory
{
    public static (RefreshToken, string) CreateValidToken(Guid userId)
    {
        var tokenString = GenerateRefreshToken();
        return (RefreshToken.Create(
            HashToken(tokenString),
            userId,
            DateTime.UtcNow.AddDays(7),
            false),
                tokenString);
    }

    public static (RefreshToken, string) CreateInvalidToken(Guid userId)
    {
        var tokenString = GenerateRefreshToken();
        return (RefreshToken.Create(
                HashToken(tokenString),
                userId,
                DateTime.UtcNow.AddDays(7),
                false),
            GenerateRefreshToken());
    }

    public static (RefreshToken, string) CreateExpiredToken(Guid userId)
    {
        var tokenString = GenerateRefreshToken();
        return (RefreshToken.Create(
            HashToken(tokenString),
            userId,
            DateTime.UtcNow.AddDays(-7),
            false),
                tokenString);
    }

    public static (RefreshToken, string) CreateRevokedToken(Guid userId)
    {
        var tokenString = GenerateRefreshToken();
        return (RefreshToken.Create(
            HashToken(tokenString),
            userId,
            DateTime.UtcNow.AddDays(7),
            true),
                tokenString);
    }

    private static string HashToken(string token)
    {
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        return Convert.ToBase64String(hashedBytes);
    }
    
    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}