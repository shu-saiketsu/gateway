using MediatR;
using Saiketsu.Gateway.Domain.Entities;
using Saiketsu.Gateway.Domain.Enums;

namespace Saiketsu.Gateway.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommand : IRequest<UserEntity?>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public RoleEnum? Role { get; set; }
}