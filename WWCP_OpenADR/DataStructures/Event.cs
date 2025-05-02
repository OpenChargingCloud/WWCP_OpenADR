
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

//public sealed record Event(
//    [property: JsonPropertyName("id")] String Id,
//    [property: JsonPropertyName("createdDateTime")] DateTimeOffset Created,
//    [property: JsonPropertyName("modificationDateTime")] DateTimeOffset Modified,

//    [property: JsonPropertyName("programID")] String ProgramId,
//    [property: JsonPropertyName("eventName")] String EventName,
//    [property: JsonPropertyName("priority")] int Priority,

//    [property: JsonPropertyName("targets")] IReadOnlyList<Target>? Targets,
//    [property: JsonPropertyName("reportDescriptors")] IReadOnlyList<ReportDescriptor>? ReportDescriptors,
//    [property: JsonPropertyName("payloadDescriptors")] IReadOnlyList<EventPayloadDescriptor> PayloadDescriptors,
//    [property: JsonPropertyName("intervalPeriod")] IntervalPeriod IntervalPeriod,
//    [property: JsonPropertyName("intervals")] IReadOnlyList<Interval> Intervals
//) : IOpenADRObject;

public class Event
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("createdDateTime")]
    public DateTime? CreatedDateTime { get; set; }

    [JsonPropertyName("modificationDateTime")]
    public DateTime? ModificationDateTime { get; set; }

    [JsonPropertyName("objectType")]
    public ObjectType ObjectType { get; set; } = ObjectType.EVENT;

    [JsonPropertyName("programID")]
    public string ProgramID { get; set; } = string.Empty;

    [JsonPropertyName("eventName")]
    public string? EventName { get; set; }

    [JsonPropertyName("priority")]
    public int? Priority { get; set; }

    [JsonPropertyName("targets")]
    public IReadOnlyList<ValuesMap>? Targets { get; set; }

    [JsonPropertyName("reportDescriptors")]
    public IReadOnlyList<ReportDescriptor>? ReportDescriptors { get; set; }

    [JsonPropertyName("payloadDescriptors")]
    public IReadOnlyList<EventPayloadDescriptor>? PayloadDescriptors { get; set; }

    [JsonPropertyName("intervalPeriod")]
    public IntervalPeriod? IntervalPeriod { get; set; }

    [JsonPropertyName("intervals")]
    public IReadOnlyList<Interval> Intervals { get; set; } = Array.Empty<Interval>();
}
