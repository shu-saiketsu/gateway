using MediatR;

namespace Saiketsu.Gateway.Application.Elections.Commands.AddUserToElection;

public sealed class AddUserToElectionCommand : IRequest<bool>
{
    public int ElectionId { get; set; }
    public string UserId { get; set; } = null!;
}