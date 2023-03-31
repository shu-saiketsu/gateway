using MediatR;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommand : IRequest<UserEntity?>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}