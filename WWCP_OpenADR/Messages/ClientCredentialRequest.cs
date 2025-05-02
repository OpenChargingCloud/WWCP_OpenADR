
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

public class ClientCredentialRequest
{
    [JsonPropertyName("grant_type")]
    public string GrantType { get; set; } = "client_credentials";

    [JsonPropertyName("client_id")]
    public string ClientId { get; set; } = string.Empty;

    [JsonPropertyName("client_secret")]
    public string ClientSecret { get; set; } = string.Empty;

    [JsonPropertyName("scope")]
    public string? Scope { get; set; }
}