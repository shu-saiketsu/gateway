using System.Text.Json.Serialization;
using MediatR;
using Saiketsu.Gateway.Domain.Entities;

namespace Saiketsu.Gateway.Application.Candidates.Commands.CreateCandidate;

public sealed class CreateCandidateCommand : IRequest<CandidateEntity?>
{
    [JsonPropertyName("name")] public string Name { get; set; } = null!;

    [JsonPropertyName("partyId")] public int? PartyId { get; set; }
}