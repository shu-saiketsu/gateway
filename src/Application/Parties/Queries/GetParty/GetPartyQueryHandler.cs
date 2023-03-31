using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Parties.Queries.GetParty;

public sealed class GetPartyQueryHandler : IRequestHandler<GetPartyQuery, PartyEntity?>
{
    private readonly IPartyService _partyService;
    private readonly IValidator<GetPartyQuery> _validator;

    public GetPartyQueryHandler(IValidator<GetPartyQuery> validator, IPartyService partyService)
    {
        _validator = validator;
        _partyService = partyService;
    }

    public async Task<PartyEntity?> Handle(GetPartyQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var party = await _partyService.GetPartyAsync(request.Id);

        return party;
    }
}