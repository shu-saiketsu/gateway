using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Elections.Queries.GetElectionCandidates;

public sealed class
    GetElectionCandidatesQueryHandler : IRequestHandler<GetElectionCandidatesQuery, List<CandidateEntity>?>
{
    private readonly IElectionService _electionService;
    private readonly IValidator<GetElectionCandidatesQuery> _validator;

    public GetElectionCandidatesQueryHandler(IElectionService electionService,
        IValidator<GetElectionCandidatesQuery> validator)
    {
        _electionService = electionService;
        _validator = validator;
    }

    public async Task<List<CandidateEntity>?> Handle(GetElectionCandidatesQuery request,
        CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var electionCandidates = await _electionService.GetElectionCandidatesAsync(request.ElectionId);

        return electionCandidates;
    }
}