using FluentValidation;

namespace Saiketsu.Gateway.Application.Candidates.Queries.GetCandidate;

public sealed class GetCandidateQueryValidator : AbstractValidator<GetCandidateQuery>
{
    public GetCandidateQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}