namespace Auth.Domain.Entities.ValueObjects;

public sealed record FirstName
{
    public string Value { get; }

    public FirstName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("First name must have a value.");
        Value = value;
    }
}