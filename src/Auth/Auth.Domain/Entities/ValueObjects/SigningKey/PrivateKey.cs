namespace Auth.Domain.Entities.ValueObjects.SigningKey;

public sealed record PrivateKey
{
    public string Value { get; }

    public PrivateKey(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Private key cannot be null empty or whitespace");

        Value = value;
    }
}