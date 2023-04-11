using MediatR;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Elections.Queries.GetElectionCandidates;

public sealed class GetElectionCandidatesQuery : IRequest<List<CandidateEntity>?>
{
    public int ElectionId { get; set; }
}