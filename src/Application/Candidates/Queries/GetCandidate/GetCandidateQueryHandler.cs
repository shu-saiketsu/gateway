using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Candidates.Queries.GetCandidate;

public sealed class GetCandidateQueryHandler : IRequestHandler<GetCandidateQuery, CandidateEntity?>
{
    private readonly ICandidateService _candidateService;
    private readonly IValidator<GetCandidateQuery> _validator;

    public GetCandidateQueryHandler(ICandidateService candidateService, IValidator<GetCandidateQuery> validator)
    {
        _candidateService = candidateService;
        _validator = validator;
    }

    public async Task<CandidateEntity?> Handle(GetCandidateQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var candidate = await _candidateService.GetCandidateAsync(request.Id);

        return candidate;
    }
}