
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

public sealed record EventPayloadDescriptor(
    [property: JsonPropertyName("payloadType")] String PayloadType,
    [property: JsonPropertyName("units")] String? Units = null,
    [property: JsonPropertyName("currency")] String? Currency = null);
