using FluentValidation;
using MediatR;
using Saiketsu.Gateway.Application.Interfaces;

namespace Saiketsu.Gateway.Application.Candidates.Commands.DeleteCandidate;

public sealed class DeleteCandidateCommandHandler : IRequestHandler<DeleteCandidateCommand, bool>
{
    private readonly ICandidateService _candidateService;
    private readonly IValidator<DeleteCandidateCommand> _validator;

    public DeleteCandidateCommandHandler(ICandidateService candidateService,
        IValidator<DeleteCandidateCommand> validator)
    {
        _candidateService = candidateService;
        _validator = validator;
    }

    public async Task<bool> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var success = await _candidateService.DeleteCandidateAsync(request.Id);

        return success;
    }
}