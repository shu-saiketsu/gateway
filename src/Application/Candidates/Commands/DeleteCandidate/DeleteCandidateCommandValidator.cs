using FluentValidation;

namespace Saiketsu.Gateway.Application.Candidates.Commands.DeleteCandidate;

public sealed class DeleteCandidateCommandValidator : AbstractValidator<DeleteCandidateCommand>
{
    public DeleteCandidateCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}