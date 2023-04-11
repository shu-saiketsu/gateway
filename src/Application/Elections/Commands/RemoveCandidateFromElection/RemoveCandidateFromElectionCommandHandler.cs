using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;

namespace Saiketsu.Gateway.Application.Elections.Commands.RemoveCandidateFromElection;

public sealed class
    RemoveCandidateFromElectionCommandHandler : IRequestHandler<RemoveCandidateFromElectionCommand, bool>
{
    private readonly IElectionService _electionService;
    private readonly IValidator<RemoveCandidateFromElectionCommand> _validator;

    public RemoveCandidateFromElectionCommandHandler(IElectionService electionService,
        IValidator<RemoveCandidateFromElectionCommand> validator)
    {
        _electionService = electionService;
        _validator = validator;
    }

    public async Task<bool> Handle(RemoveCandidateFromElectionCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var removedSuccessfully = await _electionService.RemoveCandidateFromElectionAsync(request);

        return removedSuccessfully;
    }
}