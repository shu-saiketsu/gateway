using MediatR;

namespace Saiketsu.Gateway.Application.Users.Commands.DeleteUser;

public sealed class DeleteUserCommand : IRequest<bool>
{
    public string Id { get; set; } = null!;
}