using MediatR;

namespace Saiketsu.Gateway.Application.Users.Commands.UnblockUser;

public sealed class UnblockUserCommand : IRequest<bool>
{
    public string Id { get; set; } = null!;
}