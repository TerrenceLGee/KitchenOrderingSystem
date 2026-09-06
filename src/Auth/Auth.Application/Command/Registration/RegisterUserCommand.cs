using KitchenOrderingSystem.Shared.Common;

using MediatR;

namespace Auth.Application.Command.Registration;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword,
    string ChallengeQuestion,
    string ChallengeAnswer) : IRequest<Result>;