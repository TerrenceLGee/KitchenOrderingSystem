using Auth.Domain.Entities.ValueObjects.SigningKey;

using KitchenOrderingSystem.Shared.Common;

namespace Auth.Domain.Entities;

public class SigningKey : BaseEntity
{
    public Guid KeyId { get; private set; }
    public PrivateKey PrivateKey { get; private set; } = null!;
    public PublicKey PublicKey { get; private set; } = null!;
    public bool IsActive { get; private set; }
    public DateTime ExpiresAtUtc { get; private set; }
    
    private SigningKey() {}

    private SigningKey(
        Guid keyId,
        PrivateKey privateKey,
        PublicKey publicKey,
        bool isActive,
        DateTime expiresAtUtc)
    {
        KeyId = keyId;
        PrivateKey = privateKey;
        PublicKey = publicKey;
        IsActive = isActive;
        ExpiresAtUtc = expiresAtUtc;
    }

    public static SigningKey Create(
        Guid keyId,
        string privateKeyValue,
        string publicKeyValue,
        bool isActive,
        DateTime expiresAtUtc)
    {
        var privateKey = new PrivateKey(privateKeyValue);
        var publicKey = new PublicKey(publicKeyValue);

        return new SigningKey(
            keyId,
            privateKey,
            publicKey,
            isActive,
            expiresAtUtc);
    }

    public void DeactivateKey()
    {
        IsActive = false;
    }
}