using MediatR;
using Saiketsu.Gateway.Domain.Entities.Election;

namespace Saiketsu.Gateway.Application.Elections.Queries.GetElectionsForUser;

public sealed class GetElectionsForUserQuery : IRequest<List<ElectionEntity>?>
{
    public string UserId { get; set; } = null!;
    public bool Eligible { get; set; }
}