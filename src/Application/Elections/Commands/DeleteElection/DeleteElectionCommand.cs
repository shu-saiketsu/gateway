using MediatR;

namespace Saiketsu.Gateway.Application.Elections.Commands.DeleteElection;

public sealed class DeleteElectionCommand : IRequest<bool>
{
    public int Id { get; set; }
}