using FluentValidation;

namespace Saiketsu.Gateway.Application.Parties.Queries.GetParty;

public sealed class GetPartyQueryValidator : AbstractValidator<GetPartyQuery>
{
    public GetPartyQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}