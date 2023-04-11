using FluentValidation;

namespace Saiketsu.Gateway.Application.Elections.Commands.RemoveUserFromElection;

public sealed class RemoveUserFromElectionCommandValidator : AbstractValidator<RemoveUserFromElectionCommand>
{
    public RemoveUserFromElectionCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.ElectionId)
            .NotEmpty();
    }
}