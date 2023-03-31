using MediatR;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Parties.Queries.GetParty;

public sealed class GetPartyQuery : IRequest<PartyEntity?>
{
    public int Id { get; set; }
}