namespace Auth.Domain.Entities.ValueObjects.Role;

public sealed record RoleDescription
{
    public string Value { get; }

    public RoleDescription(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Role description cannot be null empty or whitespace");

        if (value.Length > 1000)
            throw new ArgumentException("Role description cannot exceed 1000 characters");
        
        Value = value;
    }
}