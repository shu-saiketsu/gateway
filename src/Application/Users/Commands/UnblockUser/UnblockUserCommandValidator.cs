using FluentValidation;

namespace Saiketsu.Gateway.Application.Users.Commands.UnblockUser;

public sealed class UnblockUserCommandValidator : AbstractValidator<UnblockUserCommand>
{
    public UnblockUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}