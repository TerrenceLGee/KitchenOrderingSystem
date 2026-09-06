namespace Auth.Domain.Entities.ValueObjects.User;

public sealed record Challenge(ChallengeQuestion Question, ChallengeAnswer Answer);