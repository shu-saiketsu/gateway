﻿using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;

namespace Saiketsu.Gateway.Application.Elections.Commands.DeleteElection;

public sealed class DeleteElectionCommandHandler : IRequestHandler<DeleteElectionCommand, bool>
{
    private readonly IElectionService _electionService;
    private readonly IValidator<DeleteElectionCommand> _validator;

    public DeleteElectionCommandHandler(IValidator<DeleteElectionCommand> validator, IElectionService electionService)
    {
        _validator = validator;
        _electionService = electionService;
    }

    public async Task<bool> Handle(DeleteElectionCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var deletedSuccessfully = await _electionService.DeleteElectionAsync(request.Id);

        return deletedSuccessfully;
    }
}