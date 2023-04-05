using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;

namespace Saiketsu.Gateway.Application.Users.Commands.BlockUser;

public sealed class BlockUserCommandHandler : IRequestHandler<BlockUserCommand, bool>
{
    private readonly IUserService _userService;
    private readonly IValidator<BlockUserCommand> _validator;

    public BlockUserCommandHandler(IUserService userService, IValidator<BlockUserCommand> validator)
    {
        _userService = userService;
        _validator = validator;
    }

    public async Task<bool> Handle(BlockUserCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var blockedSuccessfully = await _userService.BlockUserAsync(request);

        return blockedSuccessfully;
    }
}