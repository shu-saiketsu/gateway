using Saiketsu.Gateway.Application.Votes.Commands.CreateVote;

namespace Saiketsu.Gateway.Application.Interfaces;

public interface IVoteService
{
    Task<bool> CastVoteAsync(CreateVoteCommand command);
    Task<Dictionary<int, int>?> CalculateVoteAsync(int electionId);
}