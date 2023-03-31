using MediatR;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Users.Queries.GetUsers;

public sealed class GetUsersQuery : IRequest<List<UserEntity>>
{
}