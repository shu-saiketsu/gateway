using MediatR;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Candidates.Queries.GetCandidates;

public sealed class GetCandidatesQuery : IRequest<List<CandidateEntity>>
{
}