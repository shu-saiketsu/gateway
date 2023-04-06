using FluentValidation;

namespace Saiketsu.Gateway.Application.Candidates.Commands.CreateCandidate;

public sealed class CreateCandidateCommandValidator : AbstractValidator<CreateCandidateCommand>
{
    public CreateCandidateCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.PartyId).Custom((i, context) =>
        {
            switch (i)
            {
                case null:
                    return;
                case default(int):
                    context.AddFailure("Party cannot be empty.");
                    break;
            }
        });
    }
}