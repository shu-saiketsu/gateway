using MediatR;
using Saiketsu.Gateway.Domain.Entities.Election;

namespace Saiketsu.Gateway.Application.Elections.Queries.GetElections;

public sealed class GetElectionsQuery : IRequest<List<ElectionEntity>>
{
}