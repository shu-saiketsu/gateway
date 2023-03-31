using MediatR;
using Saiketsu.Gateway.Domain.Entities.Election;

namespace Saiketsu.Gateway.Application.Elections.Queries.GetElection;

public sealed class GetElectionQuery : IRequest<ElectionEntity?>
{
    public int Id { get; set; }
}