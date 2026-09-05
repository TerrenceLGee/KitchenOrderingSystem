namespace Auth.Domain.Entities.ValueObjects;

public sealed record Password
{
    public string Value { get; }

    public Password(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Password cannot be null empty or whitespace");

        if (value.Length < 8)
            throw new ArgumentException("Password must be at least 8 characters");

        if (!IsValidPassword(value))
            throw new ArgumentException("Password is not in a valid format");
        
        Value = value;
    }

    private static bool IsValidPassword(string password)
    {
        var hasLower = false;
        var hasUpper = false;
        var hasNumeric = false;
        var hasSpecial = false;

        foreach (var letter in password)
        {
            if (char.IsLower(letter)) hasLower = true;
            if (char.IsUpper(letter)) hasUpper = true;
            if (char.IsDigit(letter)) hasNumeric = true;
            if (!char.IsAsciiLetterOrDigit(letter)) hasSpecial = true;
        }

        return hasLower && hasUpper && hasNumeric && hasSpecial;
    }
}