using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserEntity?>
{
    private readonly IUserService _userService;
    private readonly IValidator<CreateUserCommand> _validator;

    public CreateUserCommandHandler(IUserService userService, IValidator<CreateUserCommand> validator)
    {
        _userService = userService;
        _validator = validator;
    }

    public async Task<UserEntity?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var user = await _userService.CreateUserAsync(request);

        return user;
    }
}