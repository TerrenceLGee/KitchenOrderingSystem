namespace Auth.Domain.Entities.ValueObjects.SigningKey;

public sealed record PublicKey
{
    public string Value { get; }

    public PublicKey(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Public key cannot be null empty or whitespace");

        Value = value;
    }
}