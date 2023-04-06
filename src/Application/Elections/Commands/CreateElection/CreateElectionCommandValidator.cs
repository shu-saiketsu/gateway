using FluentValidation;

namespace Saiketsu.Gateway.Application.Elections.Commands.CreateElection;

public sealed class CreateElectionCommandValidator : AbstractValidator<CreateElectionCommand>
{
    public CreateElectionCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Type)
            .NotEmpty()
            .IsInEnum();

        RuleFor(x => x.OwnerId)
            .NotEmpty();
    }
}