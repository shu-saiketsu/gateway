using FluentValidation;

namespace Saiketsu.Gateway.Application.Elections.Commands.DeleteElection;

public sealed class DeleteElectionCommandValidator : AbstractValidator<DeleteElectionCommand>
{
    public DeleteElectionCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}