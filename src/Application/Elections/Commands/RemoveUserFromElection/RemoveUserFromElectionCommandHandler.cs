using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;

namespace Saiketsu.Gateway.Application.Elections.Commands.RemoveUserFromElection;

public sealed class RemoveUserFromElectionCommandHandler : IRequestHandler<RemoveUserFromElectionCommand, bool>
{
    private readonly IElectionService _electionService;
    private readonly IValidator<RemoveUserFromElectionCommand> _validator;

    public RemoveUserFromElectionCommandHandler(IElectionService electionService,
        IValidator<RemoveUserFromElectionCommand> validator)
    {
        _electionService = electionService;
        _validator = validator;
    }

    public async Task<bool> Handle(RemoveUserFromElectionCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var removedSuccessfully = await _electionService.RemoveUserFromElectionAsync(request);

        return removedSuccessfully;
    }
}