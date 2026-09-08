namespace Auth.Domain.Entities.ValueObjects.User;

public sealed record FirstName
{
    public string Value { get; }

    public FirstName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("First name must have a value.");

        if (value.Length > 75)
            throw new ArgumentException("First name cannot exceed 75 characters.");
        
        Value = value.ToLower();
    }
}