namespace Auth.Domain.Entities.ValueObjects;

public sealed record LastName
{
    public string Value { get; }

    public LastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Last name cannot be null or empty or whitespace");
        Value = value;
    }
}