
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

public sealed record ReportDescriptor(
    [property: JsonPropertyName("payloadType")] String PayloadType,
    [property: JsonPropertyName("readingType")] String? ReadingType,
    [property: JsonPropertyName("targets")] IReadOnlyList<Target>? Targets,
    [property: JsonPropertyName("aggregate")] Boolean Aggregate = false,
    [property: JsonPropertyName("startInterval")] int StartInterval = -1,
    [property: JsonPropertyName("numIntervals")] int NumIntervals = -1,
    [property: JsonPropertyName("historical")] Boolean Historical = true,
    [property: JsonPropertyName("frequency")] int Frequency = -1,
    [property: JsonPropertyName("repeat")] int Repeat = 1);
