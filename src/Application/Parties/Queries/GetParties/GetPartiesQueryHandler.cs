using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Parties.Queries.GetParties;

public sealed class GetPartiesQueryHandler : IRequestHandler<GetPartiesQuery, List<PartyEntity>>
{
    private readonly IPartyService _partyService;
    private readonly IValidator<GetPartiesQuery> _validator;

    public GetPartiesQueryHandler(IValidator<GetPartiesQuery> validator, IPartyService partyService)
    {
        _validator = validator;
        _partyService = partyService;
    }

    public async Task<List<PartyEntity>> Handle(GetPartiesQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var parties = await _partyService.GetPartiesAsync();

        return parties;
    }
}