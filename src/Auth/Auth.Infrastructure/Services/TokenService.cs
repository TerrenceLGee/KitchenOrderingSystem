using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using Auth.Application.Abstractions;
using Auth.Domain.Entities;
using Auth.Infrastructure.Settings;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Infrastructure.Services;

public class TokenService(
    IApplicationDbContext context,
    IOptions<JwtSettings> jwtSettings,
    ILogger<TokenService> logger) : ITokenService
{
    private readonly JwtSettings _settings = jwtSettings.Value;
    
    public async Task<TokenResponse> GetAccessTokenAsync(
        User user, 
        UserRole[] userRoles, 
        CancellationToken cancellationToken)
    {
        (string accessToken, DateTime accessTokenExpiration) = await GenerateJwtToken(user, userRoles);

        var refreshToken = GenerateRefreshToken();

        var hashedRefreshToken = HashToken(refreshToken);

        var refreshTokenEntity = RefreshToken.Create(
            hashedRefreshToken,
            user.Id,
            DateTime.UtcNow.AddDays(7),
            false);

        await context.RefreshTokens.AddAsync(refreshTokenEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation(
            "Access token granted for user: ({Email})",
            user.Email);

        return new TokenResponse(
            accessToken,
            refreshToken,
            accessTokenExpiration,
            refreshTokenEntity.ExpiresAtUtc);
    }

    private async Task<(string, DateTime)> GenerateJwtToken(User user, UserRole[] userRoles)
    {
        var signingKey = await context.SigningKeys.FirstOrDefaultAsync(k => k.IsActive);

        if (signingKey is null) throw new Exception("No active signing key available.");

        var privateKeyBytes = Convert.FromBase64String(signingKey.PrivateKey.Value);

        var rsa = RSA.Create();
        
        rsa.ImportRSAPrivateKey(privateKeyBytes, out _);

        var rsaSecurityKey = new RsaSecurityKey(rsa) { KeyId = signingKey.KeyId.ToString() };

        var creds = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.FirstName.Value),
            new(ClaimTypes.Email, user.Email.Value),
            new(ClaimTypes.Surname, user.LastName.Value)
        };
        
        claims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole.Role?.Name.Value ?? "")));

        var tokenExpirationDate = DateTime.UtcNow.AddHours(1);
        
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _settings.Issuer,
            Audience = _settings.Audience,
            Expires = tokenExpirationDate,
            SigningCredentials = creds
        };

        var handler = new JsonWebTokenHandler();

        var accessToken = handler.CreateToken(descriptor);

        return (accessToken, tokenExpirationDate);
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private static string HashToken(string token)
    {
        var hashedBytes = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        return Convert.ToBase64String(hashedBytes);
    }
}