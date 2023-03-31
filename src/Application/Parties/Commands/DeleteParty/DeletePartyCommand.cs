using MediatR;

namespace Saiketsu.Gateway.Application.Parties.Commands.DeleteParty;

public sealed class DeletePartyCommand : IRequest<bool>
{
    public int Id { get; set; }
}