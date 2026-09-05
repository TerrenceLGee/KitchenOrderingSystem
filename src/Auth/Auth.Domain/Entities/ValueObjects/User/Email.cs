using System.Text.RegularExpressions;

namespace Auth.Domain.Entities.ValueObjects;

public sealed partial record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email address cannot be null empty or whitespace");

        if (!IsValidEmail(value))
            throw new ArgumentException("Email address is invalid");
        
        Value = value;
    }

    private static bool IsValidEmail(string emailAddress)
    {
        return EmailRegex().IsMatch(emailAddress);
    }

    [GeneratedRegex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
    private static partial Regex EmailRegex();
}