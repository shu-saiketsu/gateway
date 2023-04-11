using FluentValidation;

namespace Saiketsu.Gateway.Application.Elections.Commands.RemoveCandidateFromElection;

public sealed class RemoveCandidateFromElectionCommandValidator : AbstractValidator<RemoveCandidateFromElectionCommand>
{
    public RemoveCandidateFromElectionCommandValidator()
    {
        RuleFor(x => x.CandidateId)
            .NotEmpty();

        RuleFor(x => x.ElectionId)
            .NotEmpty();
    }
}