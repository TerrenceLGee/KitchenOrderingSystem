using Auth.Application.Abstractions;

using KitchenOrderingSystem.Shared.Common;

using MediatR;

namespace Auth.Application.Command.Login;

public record LoginCommand(
    string Email,
    string Password) : IRequest<Result<TokenResponse>>;