using MediatR;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Elections.Queries.GetElectionUsers;

public sealed class GetElectionUsersQuery : IRequest<List<UserEntity>?>
{
    public int ElectionId { get; set; }
}