using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;

namespace Saiketsu.Gateway.Application.Users.Commands.DeleteUser;

public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUserService _userService;
    private readonly IValidator<DeleteUserCommand> _validator;

    public DeleteUserCommandHandler(IUserService userService, IValidator<DeleteUserCommand> validator)
    {
        _userService = userService;
        _validator = validator;
    }

    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var user = await _userService.DeleteUserAsync(request.Id);

        return user;
    }
}