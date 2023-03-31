using System.Text.Json.Serialization;

namespace Saiketsu.Gateway.Domain.Entities;

public sealed class CandidateEntity
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; } = null!;

    [JsonPropertyName("partyId")] public int? PartyId { get; set; }

    [JsonPropertyName("party")] public PartyEntity? Party { get; set; }
}