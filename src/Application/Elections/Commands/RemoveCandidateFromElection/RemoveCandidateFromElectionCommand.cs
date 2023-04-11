using MediatR;

namespace Saiketsu.Gateway.Application.Elections.Commands.RemoveCandidateFromElection;

public sealed class RemoveCandidateFromElectionCommand : IRequest<bool>
{
    public int ElectionId { get; set; }
    public int CandidateId { get; set; }
}