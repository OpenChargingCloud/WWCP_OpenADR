
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

//public sealed record ReportPayloadDescriptor(
//    [property: JsonPropertyName("payloadType")] String PayloadType,
//    [property: JsonPropertyName("readingType")] String? ReadingType = "DIRECT_READ",
//    [property: JsonPropertyName("units")] String? Units = null,
//    [property: JsonPropertyName("accuracy")] Double? Accuracy = null,
//    [property: JsonPropertyName("confidence")] Double? Confidence = null);


public class ReportPayloadDescriptor : PayloadDescriptor
{
    public ReportPayloadDescriptor()
    {
        ObjectType = "REPORT_PAYLOAD_DESCRIPTOR";
    }

    [JsonPropertyName("readingType")]
    public string? ReadingType { get; set; }

    [JsonPropertyName("units")]
    public string? Units { get; set; }

    [JsonPropertyName("accuracy")]
    public float? Accuracy { get; set; }

    [JsonPropertyName("confidence")]
    public int? Confidence { get; set; }
}
