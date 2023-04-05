using System.Text.Json.Serialization;

namespace Saiketsu.Gateway.Domain.Entities;

public sealed class UserEntity
{
    [JsonPropertyName("id")] public string Id { get; set; } = null!;
    [JsonPropertyName("email")] public string Email { get; set; } = null!;
    [JsonPropertyName("updatedAt")] public DateTime? UpdatedAt { get; set; }
    [JsonPropertyName("createdAt")] public DateTime? CreatedAt { get; set; }
}