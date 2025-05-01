
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

public sealed record ReportDto(
    [property: JsonPropertyName("id")] String Id,
    [property: JsonPropertyName("createdDateTime")] DateTimeOffset Created,
    [property: JsonPropertyName("modificationDateTime")] DateTimeOffset Modified,

    [property: JsonPropertyName("programID")] String ProgramId,
    [property: JsonPropertyName("eventID")] String? EventId,
    [property: JsonPropertyName("clientName")] String ClientName,
    [property: JsonPropertyName("reportName")] String ReportName,

    [property: JsonPropertyName("payloadDescriptors")] IReadOnlyList<ReportPayloadDescriptor> PayloadDescriptors,
    [property: JsonPropertyName("resources")] IReadOnlyList<ReportResource> Resources,
    [property: JsonPropertyName("intervalPeriod")] IntervalPeriod? IntervalPeriod
) : IOpenADRObject;

