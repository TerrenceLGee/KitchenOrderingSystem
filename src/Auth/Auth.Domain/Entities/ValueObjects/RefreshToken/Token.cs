namespace Auth.Domain.Entities.ValueObjects.RefreshToken;

public sealed record Token
{
    public string Value { get; }

    public Token(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Refresh token cannot be null empty or whitespace it must have a value");
        
        Value = value;
    }
}