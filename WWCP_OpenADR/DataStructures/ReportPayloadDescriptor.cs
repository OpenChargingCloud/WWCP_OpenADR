
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

public sealed record ReportPayloadDescriptor(
    [property: JsonPropertyName("payloadType")] String PayloadType,
    [property: JsonPropertyName("readingType")] String? ReadingType = "DIRECT_READ",
    [property: JsonPropertyName("units")] String? Units = null,
    [property: JsonPropertyName("accuracy")] Double? Accuracy = null,
    [property: JsonPropertyName("confidence")] Double? Confidence = null);
