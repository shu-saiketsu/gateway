using MediatR;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Parties.Commands.CreateParty;

public sealed class CreatePartyCommand : IRequest<PartyEntity?>
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}