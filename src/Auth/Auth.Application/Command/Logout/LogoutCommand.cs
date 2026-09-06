using KitchenOrderingSystem.Shared.Common;

using MediatR;

namespace Auth.Application.Command.Logout;

public record LogoutCommand(
    Guid UserId,
    string RefreshToken,
    bool IsLogoutFromAllDevices,
    string? Email = null) : IRequest<Result>;