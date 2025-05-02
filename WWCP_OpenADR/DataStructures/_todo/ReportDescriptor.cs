
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

//public sealed record ReportDescriptor(
//    [property: JsonPropertyName("payloadType")] String PayloadType,
//    [property: JsonPropertyName("readingType")] String? ReadingType,
//    [property: JsonPropertyName("targets")] IReadOnlyList<Target>? Targets,
//    [property: JsonPropertyName("aggregate")] Boolean Aggregate = false,
//    [property: JsonPropertyName("startInterval")] int StartInterval = -1,
//    [property: JsonPropertyName("numIntervals")] int NumIntervals = -1,
//    [property: JsonPropertyName("historical")] Boolean Historical = true,
//    [property: JsonPropertyName("frequency")] int Frequency = -1,
//    [property: JsonPropertyName("repeat")] int Repeat = 1);

public class ReportDescriptor
{
    [JsonPropertyName("payloadType")]
    public string PayloadType { get; set; } = string.Empty;

    [JsonPropertyName("readingType")]
    public string? ReadingType { get; set; }

    [JsonPropertyName("units")]
    public string? Units { get; set; }

    [JsonPropertyName("targets")]
    public IReadOnlyList<ValuesMap>? Targets { get; set; }

    [JsonPropertyName("aggregate")]
    public bool Aggregate { get; set; } = false;

    [JsonPropertyName("startInterval")]
    public int StartInterval { get; set; } = -1;

    [JsonPropertyName("numIntervals")]
    public int NumIntervals { get; set; } = -1;

    [JsonPropertyName("historical")]
    public bool Historical { get; set; } = true;

    [JsonPropertyName("frequency")]
    public int Frequency { get; set; } = -1;

    [JsonPropertyName("repeat")]
    public int Repeat { get; set; } = 1;
}