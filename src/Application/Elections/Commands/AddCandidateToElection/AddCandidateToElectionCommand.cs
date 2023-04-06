using MediatR;

namespace Saiketsu.Gateway.Application.Elections.Commands.AddCandidateToElection;

public sealed class AddCandidateToElectionCommand : IRequest<bool>
{
    public int ElectionId { get; set; }
    public int CandidateId { get; set; }
}