using Saiketsu.Gateway.Application.Candidates.Commands.CreateCandidate;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Interfaces;

public interface ICandidateService
{
    Task<CandidateEntity?> GetCandidateAsync(int id);
    Task<List<CandidateEntity>> GetCandidatesAsync();
    Task<CandidateEntity?> CreateCandidateAsync(CreateCandidateCommand command);
    Task<bool> DeleteCandidateAsync(int id);
}