using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserEntity?>
{
    private readonly IUserService _userService;
    private readonly IValidator<CreateUserCommand> _validator;

    public CreateUserCommandHandler(IValidator<CreateUserCommand> validator, IUserService userService)
    {
        _validator = validator;
        _userService = userService;
    }

    public async Task<UserEntity?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var user = await _userService.CreateUserAsync(request);

        return user;
    }
}