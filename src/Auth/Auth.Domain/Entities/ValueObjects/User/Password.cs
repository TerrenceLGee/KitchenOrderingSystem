namespace Auth.Domain.Entities.ValueObjects.User;

public sealed record Password
{
    public string Value { get; }

    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Password cannot be null empty or whitespace");
        
        Value = value;
    }
}