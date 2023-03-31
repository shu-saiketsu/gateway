using System.Text.Json.Serialization;
using Saiketsu.Gateway.Domain.Enums;

namespace Saiketsu.Gateway.Domain.Entities.Election;

public sealed class ElectionTypeEntity
{
    [JsonPropertyName("id")] public ElectionType Id { get; set; }
    [JsonPropertyName("name")] public string Name { get; set; } = null!;
}