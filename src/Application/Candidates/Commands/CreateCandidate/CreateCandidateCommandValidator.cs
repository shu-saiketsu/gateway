using FluentValidation;

namespace Saiketsu.Gateway.Application.Candidates.Commands.CreateCandidate;

public sealed class CreateCandidateCommandValidator : AbstractValidator<CreateCandidateCommand>
{
    public CreateCandidateCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}