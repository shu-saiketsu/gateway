using MediatR;
using Saiketsu.Gateway.Domain.Entities.Election;
using Saiketsu.Gateway.Domain.Enums;

namespace Saiketsu.Gateway.Application.Elections.Commands.CreateElection;

public sealed class CreateElectionCommand : IRequest<ElectionEntity?>
{
    public string Name { get; set; } = null!;
    public int Type { get; set; }
    public string OwnerId { get; set; } = null!;
}