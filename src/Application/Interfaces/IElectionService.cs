using Saiketsu.Gateway.Application.Elections.Commands.AddUserToElection;
using Saiketsu.Gateway.Application.Elections.Commands.CreateElection;
using Saiketsu.Gateway.Domain.Entities.Election;

namespace Saiketsu.Gateway.Application.Interfaces;

public interface IElectionService
{
    Task<List<ElectionEntity>> GetElectionsAsync();
    Task<ElectionEntity?> GetElectionAsync(int id);
    Task<bool> DeleteElectionAsync(int id);
    Task<ElectionEntity?> CreateElectionAsync(CreateElectionCommand command);
    Task<bool> AddUserToElectionAsync(AddUserToElectionCommand command);
}