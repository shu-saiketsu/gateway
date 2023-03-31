using MediatR;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Parties.Queries.GetParties;

public sealed class GetPartiesQuery : IRequest<List<PartyEntity>>
{
}