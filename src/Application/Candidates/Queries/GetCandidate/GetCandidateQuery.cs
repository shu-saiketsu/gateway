using MediatR;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Candidates.Queries.GetCandidate;

public sealed class GetCandidateQuery : IRequest<CandidateEntity?>
{
    public int Id { get; set; }
}