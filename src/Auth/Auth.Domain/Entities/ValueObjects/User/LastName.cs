namespace Auth.Domain.Entities.ValueObjects.User;

public sealed record LastName
{
    public string Value { get; }

    public LastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Last name cannot be null or empty or whitespace.");

        if (value.Length > 75)
            throw new ArgumentException("Last name cannot exceed 75 characters.");
        
        Value = value;
    }
}