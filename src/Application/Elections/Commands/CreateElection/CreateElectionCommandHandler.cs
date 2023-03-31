using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities.Election;

namespace Saiketsu.Gateway.Application.Elections.Commands.CreateElection;

public sealed class CreateElectionCommandHandler : IRequestHandler<CreateElectionCommand, ElectionEntity?>
{
    private readonly IElectionService _electionService;
    private readonly IValidator<CreateElectionCommand> _validator;

    public CreateElectionCommandHandler(IValidator<CreateElectionCommand> validator, IElectionService electionService)
    {
        _validator = validator;
        _electionService = electionService;
    }

    public async Task<ElectionEntity?> Handle(CreateElectionCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var election = await _electionService.CreateElectionAsync(request);

        return election;
    }
}