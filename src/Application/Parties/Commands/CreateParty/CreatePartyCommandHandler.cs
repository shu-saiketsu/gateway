using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Parties.Commands.CreateParty;

public sealed class CreatePartyCommandHandler : IRequestHandler<CreatePartyCommand, PartyEntity?>
{
    private readonly IPartyService _partyService;
    private readonly IValidator<CreatePartyCommand> _validator;

    public CreatePartyCommandHandler(IValidator<CreatePartyCommand> validator, IPartyService partyService)
    {
        _validator = validator;
        _partyService = partyService;
    }

    public async Task<PartyEntity?> Handle(CreatePartyCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var party = await _partyService.CreatePartyAsync(request);

        return party;
    }
}