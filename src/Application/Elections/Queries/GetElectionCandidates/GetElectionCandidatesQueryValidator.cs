using FluentValidation;

namespace Saiketsu.Gateway.Application.Elections.Queries.GetElectionCandidates;

public sealed class GetElectionCandidatesQueryValidator : AbstractValidator<GetElectionCandidatesQuery>
{
    public GetElectionCandidatesQueryValidator()
    {
        RuleFor(x => x.ElectionId)
            .NotEmpty();
    }
}