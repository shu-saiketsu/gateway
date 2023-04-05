using MediatR;

namespace Saiketsu.Gateway.Application.Users.Commands.BlockUser;

public sealed class BlockUserCommand : IRequest<bool>
{
    public string Id { get; set; } = null!;
}