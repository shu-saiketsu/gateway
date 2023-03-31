using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Candidates.Commands.CreateCandidate;

public sealed class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, CandidateEntity?>
{
    private readonly ICandidateService _candidateService;
    private readonly IValidator<CreateCandidateCommand> _validator;

    public CreateCandidateCommandHandler(ICandidateService candidateService,
        IValidator<CreateCandidateCommand> validator)
    {
        _candidateService = candidateService;
        _validator = validator;
    }

    public async Task<CandidateEntity?> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var candidate = await _candidateService.CreateCandidateAsync(request);

        return candidate;
    }
}