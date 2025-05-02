
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

//public sealed record EventPayloadDescriptor(
//    [property: JsonPropertyName("payloadType")] String PayloadType,
//    [property: JsonPropertyName("units")] String? Units = null,
//    [property: JsonPropertyName("currency")] String? Currency = null);


public class EventPayloadDescriptor : PayloadDescriptor
{
    public EventPayloadDescriptor()
    {
        ObjectType = "EVENT_PAYLOAD_DESCRIPTOR";
    }

    [JsonPropertyName("units")]
    public string? Units { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

}
