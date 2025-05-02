
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

public class AuthError
{
    [JsonPropertyName("error")]
    public string Error { get; set; } = string.Empty;

    [JsonPropertyName("error_description")]
    public string? ErrorDescription { get; set; }

    [JsonPropertyName("error_uri")]
    public string? ErrorUri { get; set; }
}
