using FluentValidation;

namespace Saiketsu.Gateway.Application.Elections.Commands.AddUserToElection;

public sealed class AddUserToElectionCommandValidator : AbstractValidator<AddUserToElectionCommand>
{
    public AddUserToElectionCommandValidator()
    {
        RuleFor(x => x.ElectionId)
            .NotEmpty();

        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}