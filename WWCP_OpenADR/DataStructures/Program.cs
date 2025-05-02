
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

//public sealed record Program(
//    /* mandatory */
//    [property: JsonPropertyName("id")] String Id,
//    [property: JsonPropertyName("createdDateTime")] DateTimeOffset Created,
//    [property: JsonPropertyName("modificationDateTime")] DateTimeOffset Modified,

//    /* core identity */
//    [property: JsonPropertyName("programName")] String ProgramName,
//    [property: JsonPropertyName("programLongName")] String? ProgramLongName,
//    [property: JsonPropertyName("retailerName")] String? RetailerName,
//    [property: JsonPropertyName("retailerLongName")] String? RetailerLongName,

//    /* classification */
//    [property: JsonPropertyName("programType")] String ProgramType,     // e.g. “PRICING_TARIFF” :contentReference[oaicite:1]{index=1}
//    [property: JsonPropertyName("country")] String? CountryIso2,
//    [property: JsonPropertyName("principalSubdivision")] String? SubdivisionIso,

//    /* time domain */
//    [property: JsonPropertyName("timeZoneOffset")] String? TimeZoneOffset, // “PT7H” etc.
//    [property: JsonPropertyName("intervalPeriod")] IntervalPeriod? IntervalPeriod,

//    /* options */
//    [property: JsonPropertyName("bindingEvents")] Boolean BindingEvents = false,
//    [property: JsonPropertyName("localPrice")] Boolean LocalPrice = false,

//    /* lists */
//    [property: JsonPropertyName("programDescriptions")] IReadOnlyList<Uri>? ProgramDescriptions = null,
//    [property: JsonPropertyName("payloadDescriptors")] IReadOnlyList<EventPayloadDescriptor>? PayloadDescriptors = null,
//    [property: JsonPropertyName("targets")] IReadOnlyList<Target>? Targets = null
//) : IOpenADRObject;

public class Program
{

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("createdDateTime")]
    public DateTime? CreatedDateTime { get; set; }

    [JsonPropertyName("modificationDateTime")]
    public DateTime? ModificationDateTime { get; set; }

    [JsonPropertyName("objectType")]
    public ObjectType ObjectType { get; set; } = ObjectType.PROGRAM;

    [JsonPropertyName("programName")]
    public string ProgramName { get; set; } = string.Empty;

    [JsonPropertyName("programLongName")]
    public string? ProgramLongName { get; set; }

    [JsonPropertyName("retailerName")]
    public string? RetailerName { get; set; }

    [JsonPropertyName("retailerLongName")]
    public string? RetailerLongName { get; set; }

    [JsonPropertyName("programType")]
    public string? ProgramType { get; set; }

    [JsonPropertyName("country")]
    public string? Country { get; set; }

    [JsonPropertyName("principalSubdivision")]
    public string? PrincipalSubdivision { get; set; }

    [JsonPropertyName("timeZoneOffset")]
    public string? TimeZoneOffset { get; set; } // ISO 8601 duration

    [JsonPropertyName("intervalPeriod")]
    public IntervalPeriod? IntervalPeriod { get; set; }

    [JsonPropertyName("programDescriptions")]
    public IReadOnlyList<ProgramDescription>? ProgramDescriptions { get; set; }

    [JsonPropertyName("bindingEvents")]
    public bool? BindingEvents { get; set; }

    [JsonPropertyName("localPrice")]
    public bool? LocalPrice { get; set; }

    [JsonPropertyName("payloadDescriptors")]
    public IReadOnlyList<PayloadDescriptor>? PayloadDescriptors { get; set; }

    [JsonPropertyName("targets")]
    public IReadOnlyList<ValuesMap>? Targets { get; set; }
}
