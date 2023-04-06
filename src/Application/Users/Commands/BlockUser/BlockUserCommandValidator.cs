using FluentValidation;

namespace Saiketsu.Gateway.Application.Users.Commands.BlockUser;

public sealed class BlockUserCommandValidator : AbstractValidator<BlockUserCommand>
{
    public BlockUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}