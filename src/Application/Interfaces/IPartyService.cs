using Saiketsu.Gateway.Application.Parties.Commands.CreateParty;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Interfaces;

public interface IPartyService
{
    Task<List<PartyEntity>> GetPartiesAsync();
    Task<PartyEntity?> GetPartyAsync(int id);
    Task<PartyEntity?> CreatePartyAsync(CreatePartyCommand command);
    Task<bool> DeletePartyAsync(int id);
}