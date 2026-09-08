namespace Auth.Domain.Entities.ValueObjects.User;

public sealed record ChallengeQuestion
{
    public string Value { get; }

    public ChallengeQuestion(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Challenge question cannot be null empty or whitespace.");

        if (value.Length > 1024)
            throw new ArgumentException("Challenge question cannot exceed 1024 characters.");
        
        Value = value.ToLower();
    } 
}