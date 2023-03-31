using MediatR;

namespace Saiketsu.Gateway.Application.Candidates.Commands.DeleteCandidate;

public sealed class DeleteCandidateCommand : IRequest<bool>
{
    public int Id { get; set; }
}