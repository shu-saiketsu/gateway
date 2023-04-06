using FluentValidation;

namespace Saiketsu.Gateway.Application.Elections.Queries.GetElection;

public sealed class GetElectionQueryValidator : AbstractValidator<GetElectionQuery>
{
    public GetElectionQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}