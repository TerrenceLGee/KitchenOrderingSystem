namespace Auth.Application.Helpers;

public static class ValidationHelpers
{
    public static bool IsValidPassword(string password)
    {
        var hasUpper = false;
        var hasLower = false;
        var hasNumeric = false;
        var hasSpecialCharacter = false;

        foreach (var letter in password)
        {
            if (Char.IsUpper(letter)) hasUpper = true;
            if (Char.IsLower(letter)) hasLower = true;
            if (Char.IsNumber(letter)) hasNumeric = true;
            if (!Char.IsLetterOrDigit(letter)) hasSpecialCharacter = true;
        }

        return hasUpper && hasLower && hasNumeric && hasSpecialCharacter && password.Length >= 8;
    }
}