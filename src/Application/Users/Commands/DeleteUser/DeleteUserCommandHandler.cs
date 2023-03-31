using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;

namespace Saiketsu.Gateway.Application.Users.Commands.DeleteUser;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserService _userService;
    private readonly IValidator<DeleteUserCommand> _validator;

    public DeleteUserCommandHandler(IValidator<DeleteUserCommand> validator, IUserService userService)
    {
        _validator = validator;
        _userService = userService;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var user = await _userService.DeleteUserAsync(request.Id);

        return user;
    }
}