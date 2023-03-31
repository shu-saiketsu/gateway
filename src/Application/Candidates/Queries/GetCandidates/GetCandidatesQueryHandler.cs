using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Candidates.Queries.GetCandidates;

public sealed class GetCandidatesQueryHandler : IRequestHandler<GetCandidatesQuery, List<CandidateEntity>>
{
    private readonly ICandidateService _candidateService;
    private readonly IValidator<GetCandidatesQuery> _validator;

    public GetCandidatesQueryHandler(ICandidateService candidateService, IValidator<GetCandidatesQuery> validator)
    {
        _candidateService = candidateService;
        _validator = validator;
    }

    public async Task<List<CandidateEntity>> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var candidates = await _candidateService.GetCandidatesAsync();

        return candidates;
    }
}