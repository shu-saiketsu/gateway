using System.Text.Json.Serialization;

namespace Saiketsu.Gateway.Domain.Entities;

public sealed class PartyEntity
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; } = null!;
    [JsonPropertyName("description")] public string Description { get; set; } = null!;
}