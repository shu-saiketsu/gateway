using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;

namespace Saiketsu.Gateway.Application.Elections.Commands.AddCandidateToElection;

public sealed class AddCandidateToElectionCommandHandler : IRequestHandler<AddCandidateToElectionCommand, bool>
{
    private readonly IElectionService _electionService;
    private readonly IValidator<AddCandidateToElectionCommand> _validator;

    public AddCandidateToElectionCommandHandler(IElectionService electionService,
        IValidator<AddCandidateToElectionCommand> validator)
    {
        _electionService = electionService;
        _validator = validator;
    }

    public async Task<bool> Handle(AddCandidateToElectionCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var result = await _electionService.AddCandidateToElectionAsync(request);

        return result;
    }
}