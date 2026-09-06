using KitchenOrderingSystem.Shared.Common;

using MediatR;

namespace Auth.Application.Command.UpdateUser;

public record UpdateUserCommand(
    Guid UserId,
    string? FirstName = null,
    string? LastName = null,
    string? Email = null,
    string? ChallengeQuestion = null,
    string? ChallengeAnswer = null) : IRequest<Result>;