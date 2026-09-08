namespace Auth.Domain.Entities.ValueObjects.User;

public sealed record ChallengeAnswer
{
    public string Value { get; }

    public ChallengeAnswer(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Challenge answer cannot be null empty or whitespace");

        if (value.Length > 1024)
            throw new ArgumentException("Challenge answer cannot exceed 1024 characters.");
        
        Value = value.ToLower();
    }
}