using MediatR;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Users.Queries.GetUser;

public sealed class GetUserQuery : IRequest<UserEntity?>
{
    public string Id { get; set; } = null!;
}