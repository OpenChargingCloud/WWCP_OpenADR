
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

//public sealed record Report(
//    [property: JsonPropertyName("id")] String Id,
//    [property: JsonPropertyName("createdDateTime")] DateTimeOffset Created,
//    [property: JsonPropertyName("modificationDateTime")] DateTimeOffset Modified,

//    [property: JsonPropertyName("programID")] String ProgramId,
//    [property: JsonPropertyName("eventID")] String? EventId,
//    [property: JsonPropertyName("clientName")] String ClientName,
//    [property: JsonPropertyName("reportName")] String ReportName,

//    [property: JsonPropertyName("payloadDescriptors")] IReadOnlyList<ReportPayloadDescriptor> PayloadDescriptors,
//    [property: JsonPropertyName("resources")] IReadOnlyList<ReportResource> Resources,
//    [property: JsonPropertyName("intervalPeriod")] IntervalPeriod? IntervalPeriod
//) : IOpenADRObject;

public class Report
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("createdDateTime")]
    public DateTime? CreatedDateTime { get; set; }

    [JsonPropertyName("modificationDateTime")]
    public DateTime? ModificationDateTime { get; set; }

    [JsonPropertyName("objectType")]
    public ObjectType ObjectType { get; set; } = ObjectType.REPORT;




    [JsonPropertyName("programID")]
    public string ProgramID { get; set; } = string.Empty;

    [JsonPropertyName("eventID")]
    public string EventID { get; set; } = string.Empty;

    [JsonPropertyName("clientName")]
    public string ClientName { get; set; } = string.Empty;

    [JsonPropertyName("reportName")]
    public string? ReportName { get; set; }

    [JsonPropertyName("payloadDescriptors")]
    public IReadOnlyList<ReportPayloadDescriptor>? PayloadDescriptors { get; set; }

    [JsonPropertyName("resources")]
    public IReadOnlyList<ResourceReport> Resources { get; set; } = Array.Empty<ResourceReport>();
}
