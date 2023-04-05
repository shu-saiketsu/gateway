using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;

namespace Saiketsu.Gateway.Application.Users.Commands.UnblockUser;

public sealed class UnblockUserCommandHandler : IRequestHandler<UnblockUserCommand, bool>
{
    private readonly IUserService _userService;
    private readonly IValidator<UnblockUserCommand> _validator;

    public UnblockUserCommandHandler(IUserService userService, IValidator<UnblockUserCommand> validator)
    {
        _userService = userService;
        _validator = validator;
    }

    public async Task<bool> Handle(UnblockUserCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var unblockedSuccessfully = await _userService.UnblockUserAsync(request);

        return unblockedSuccessfully;
    }
}