using System.Text.Json.Serialization;

namespace Saiketsu.Gateway.Domain.Entities.Election;

public sealed class ElectionEntity
{
    [JsonPropertyName("id")] public int Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; } = null!;
    [JsonPropertyName("typeId")] public int TypeId { get; set; }
    [JsonPropertyName("type")] public ElectionTypeEntity Type { get; set; } = null!;
    [JsonPropertyName("ownerId")] public string OwnerId { get; set; } = null!;
    [JsonPropertyName("owner")] public UserEntity Owner { get; set; } = null!;
}