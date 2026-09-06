using KitchenOrderingSystem.Shared.Common;

using MediatR;

namespace Auth.Application.Command.ResetPassword;

public record ResetPasswordCommand(
    string Email,
    string OldPassword,
    string NewPassword,
    string ConfirmNewPassword) : IRequest<Result>;