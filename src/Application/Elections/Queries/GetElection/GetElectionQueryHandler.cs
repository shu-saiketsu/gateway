using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities.Election;

namespace Saiketsu.Gateway.Application.Elections.Queries.GetElection;

public sealed class GetElectionQueryHandler : IRequestHandler<GetElectionQuery, ElectionEntity?>
{
    private readonly IElectionService _electionService;
    private readonly IValidator<GetElectionQuery> _validator;

    public GetElectionQueryHandler(IValidator<GetElectionQuery> validator, IElectionService electionService)
    {
        _validator = validator;
        _electionService = electionService;
    }

    public async Task<ElectionEntity?> Handle(GetElectionQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var election = await _electionService.GetElectionAsync(request.Id);

        return election;
    }
}