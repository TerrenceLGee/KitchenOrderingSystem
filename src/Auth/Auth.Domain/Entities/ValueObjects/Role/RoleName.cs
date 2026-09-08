namespace Auth.Domain.Entities.ValueObjects.Role;

public sealed record RoleName
{
    public string Value { get; }

    public RoleName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Role name cannot be null empty or whitespace");

        if (value.Length > 50)
            throw new ArgumentException("Role name cannot exceed 50 characters");

        Value = value.ToLower();
    }
}