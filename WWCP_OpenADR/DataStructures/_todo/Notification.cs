
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

public class Notification
{
    [JsonPropertyName("objectType")]
    public ObjectType ObjectType { get; set; }

    [JsonPropertyName("operation")]
    public Operation Operation { get; set; }

    [JsonPropertyName("object")]
    public object Object { get; set; } = new { };

    [JsonPropertyName("targets")]
    public IReadOnlyList<ValuesMap>? Targets { get; set; }
}
