namespace Auth.Api.Queries;

public record UpdateUserQuery(
    string? FirstName = null,
    string? LastName = null,
    string? Email = null,
    string? ChallengeQuestion = null,
    string? ChallengeAnswer = null);