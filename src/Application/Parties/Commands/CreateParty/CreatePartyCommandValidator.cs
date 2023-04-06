using FluentValidation;

namespace Saiketsu.Gateway.Application.Parties.Commands.CreateParty;

public sealed class CreatePartyCommandValidator : AbstractValidator<CreatePartyCommand>
{
    public CreatePartyCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(100);
    }
}