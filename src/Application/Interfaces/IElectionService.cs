using Saiketsu.Gateway.Application.Elections.Commands.AddCandidateToElection;
using Saiketsu.Gateway.Application.Elections.Commands.AddUserToElection;
using Saiketsu.Gateway.Application.Elections.Commands.CreateElection;
using Saiketsu.Gateway.Application.Elections.Commands.RemoveCandidateFromElection;
using Saiketsu.Gateway.Application.Elections.Commands.RemoveUserFromElection;
using Saiketsu.Gateway.Application.Elections.Queries.GetElectionsForUser;
using Saiketsu.Gateway.Domain.Entities;
using Saiketsu.Gateway.Domain.Entities.Election;

namespace Saiketsu.Gateway.Application.Interfaces;

public interface IElectionService
{
    Task<List<ElectionEntity>> GetElectionsAsync();
    Task<ElectionEntity?> GetElectionAsync(int id);
    Task<bool> DeleteElectionAsync(int id);
    Task<ElectionEntity?> CreateElectionAsync(CreateElectionCommand command);
    Task<bool> AddUserToElectionAsync(AddUserToElectionCommand command);
    Task<bool> AddCandidateToElectionAsync(AddCandidateToElectionCommand command);
    Task<List<UserEntity>?> GetElectionUsersAsync(int id);
    Task<List<CandidateEntity>?> GetElectionCandidatesAsync(int id);
    Task<bool> RemoveUserFromElectionAsync(RemoveUserFromElectionCommand command);
    Task<bool> RemoveCandidateFromElectionAsync(RemoveCandidateFromElectionCommand command);
    Task<List<ElectionEntity>?> GetElectionsForUserAsync(GetElectionsForUserQuery query);
}