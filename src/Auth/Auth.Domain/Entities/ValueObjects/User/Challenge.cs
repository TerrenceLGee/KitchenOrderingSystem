namespace Auth.Domain.Entities.ValueObjects;

public sealed record Challenge
{
    public string Question { get; }
    public string Answer { get;  }

    public Challenge(string question, string answer)
    {
        if (string.IsNullOrWhiteSpace(question))
            throw new ArgumentException("Question cannot be null or empty or whitespace", nameof(question));

        if (string.IsNullOrWhiteSpace(answer))
            throw new ArgumentException("Answer cannot be null or empty or whitespace", nameof(answer));
        
        Question = question;
        Answer = answer;
    }
}