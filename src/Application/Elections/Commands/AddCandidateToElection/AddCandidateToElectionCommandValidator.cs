using FluentValidation;

namespace Saiketsu.Gateway.Application.Elections.Commands.AddCandidateToElection;

public sealed class AddCandidateToElectionCommandValidator : AbstractValidator<AddCandidateToElectionCommand>
{
    public AddCandidateToElectionCommandValidator()
    {
        RuleFor(x => x.CandidateId)
            .NotEmpty();

        RuleFor(x => x.ElectionId)
            .NotEmpty();
    }
}