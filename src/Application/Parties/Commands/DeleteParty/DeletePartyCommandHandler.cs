using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;

namespace Saiketsu.Gateway.Application.Parties.Commands.DeleteParty;

public sealed class DeletePartyCommandHandler : IRequestHandler<DeletePartyCommand, bool>
{
    private readonly IPartyService _partyService;
    private readonly IValidator<DeletePartyCommand> _validator;

    public DeletePartyCommandHandler(IPartyService partyService, IValidator<DeletePartyCommand> validator)
    {
        _partyService = partyService;
        _validator = validator;
    }

    public async Task<bool> Handle(DeletePartyCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var success = await _partyService.DeletePartyAsync(request.Id);

        return success;
    }
}