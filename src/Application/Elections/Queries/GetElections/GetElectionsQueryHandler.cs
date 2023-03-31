using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities.Election;

namespace Saiketsu.Gateway.Application.Elections.Queries.GetElections;

public sealed class GetElectionsQueryHandler : IRequestHandler<GetElectionsQuery, List<ElectionEntity>>
{
    private readonly IElectionService _electionService;
    private readonly IValidator<GetElectionsQuery> _validator;

    public GetElectionsQueryHandler(IValidator<GetElectionsQuery> validator, IElectionService electionService)
    {
        _validator = validator;
        _electionService = electionService;
    }

    public async Task<List<ElectionEntity>> Handle(GetElectionsQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var elections = await _electionService.GetElectionsAsync();

        return elections;
    }
}