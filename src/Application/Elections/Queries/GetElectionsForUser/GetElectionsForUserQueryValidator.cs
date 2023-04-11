using FluentValidation;

namespace Saiketsu.Gateway.Application.Elections.Queries.GetElectionsForUser;

public sealed class GetElectionsForUserQueryValidator : AbstractValidator<GetElectionsForUserQuery>
{
    public GetElectionsForUserQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Eligible)
            .NotNull();
    }
}