using FluentValidation;

namespace Saiketsu.Gateway.Application.Parties.Commands.DeleteParty;

public sealed class DeletePartyCommandValidator : AbstractValidator<DeletePartyCommand>
{
    public DeletePartyCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}