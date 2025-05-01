
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

public sealed record EventDto(
    [property: JsonPropertyName("id")] String Id,
    [property: JsonPropertyName("createdDateTime")] DateTimeOffset Created,
    [property: JsonPropertyName("modificationDateTime")] DateTimeOffset Modified,

    [property: JsonPropertyName("programID")] String ProgramId,
    [property: JsonPropertyName("eventName")] String EventName,
    [property: JsonPropertyName("priority")] int Priority,

    [property: JsonPropertyName("targets")] IReadOnlyList<Target>? Targets,
    [property: JsonPropertyName("reportDescriptors")] IReadOnlyList<ReportDescriptor>? ReportDescriptors,
    [property: JsonPropertyName("payloadDescriptors")] IReadOnlyList<EventPayloadDescriptor> PayloadDescriptors,
    [property: JsonPropertyName("intervalPeriod")] IntervalPeriod IntervalPeriod,
    [property: JsonPropertyName("intervals")] IReadOnlyList<Interval> Intervals
) : IOpenADRObject;
