
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

public class PayloadDescriptor
{
    [JsonPropertyName("objectType")]
    public string ObjectType { get; set; } = string.Empty;

    [JsonPropertyName("payloadType")]
    public string PayloadType { get; set; } = string.Empty;
}
