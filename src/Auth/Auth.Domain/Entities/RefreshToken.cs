using Auth.Domain.Entities.ValueObjects.RefreshToken;

using KitchenOrderingSystem.Shared.Common;

namespace Auth.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public Token Token { get; private set; } = null!;
    public Guid UserId { get; private set; }
    public User User { get; set; } = null!;
    public DateTime ExpiresAtUtc { get; private set; }
    public bool IsRevoked { get; private set; }
    public DateTime? RevokedAtUtc { get; private set; }
    
    private RefreshToken() {}

    private RefreshToken(
        Token token,
        Guid userId,
        DateTime expiresAtUtc,
        bool isRevoked)
    {
        Token = token;
        UserId = userId;
        ExpiresAtUtc = expiresAtUtc;
        IsRevoked = isRevoked;
    }

    public static RefreshToken Create(
        string tokenValue,
        Guid userId,
        DateTime expiresAtUtc,
        bool isRevoked)
    {
        var token = new Token(tokenValue);

        return new RefreshToken(
            token,
            userId,
            expiresAtUtc,
            isRevoked);
    }

    public void Revoke()
    {
        IsRevoked = true;
        RevokedAtUtc = DateTime.UtcNow;
    }
}