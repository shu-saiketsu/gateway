using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities.Election;

namespace Saiketsu.Gateway.Application.Elections.Queries.GetElectionsForUser;

public sealed class GetElectionsForUserQueryHandler : IRequestHandler<GetElectionsForUserQuery, List<ElectionEntity>?>
{
    private readonly IElectionService _electionService;
    private readonly IValidator<GetElectionsForUserQuery> _validator;

    public GetElectionsForUserQueryHandler(IElectionService electionService,
        IValidator<GetElectionsForUserQuery> validator)
    {
        _electionService = electionService;
        _validator = validator;
    }

    public async Task<List<ElectionEntity>?> Handle(GetElectionsForUserQuery request,
        CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var elections = await _electionService.GetElectionsForUserAsync(request);

        return elections;
    }
}