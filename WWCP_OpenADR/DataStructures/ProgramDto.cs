
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

public sealed record ProgramDto(
    /* mandatory */
    [property: JsonPropertyName("id")] String Id,
    [property: JsonPropertyName("createdDateTime")] DateTimeOffset Created,
    [property: JsonPropertyName("modificationDateTime")] DateTimeOffset Modified,

    /* core identity */
    [property: JsonPropertyName("programName")] String ProgramName,
    [property: JsonPropertyName("programLongName")] String? ProgramLongName,
    [property: JsonPropertyName("retailerName")] String? RetailerName,
    [property: JsonPropertyName("retailerLongName")] String? RetailerLongName,

    /* classification */
    [property: JsonPropertyName("programType")] String ProgramType,     // e.g. “PRICING_TARIFF” :contentReference[oaicite:1]{index=1}
    [property: JsonPropertyName("country")] String? CountryIso2,
    [property: JsonPropertyName("principalSubdivision")] String? SubdivisionIso,

    /* time domain */
    [property: JsonPropertyName("timeZoneOffset")] String? TimeZoneOffset, // “PT7H” etc.
    [property: JsonPropertyName("intervalPeriod")] IntervalPeriod? IntervalPeriod,

    /* options */
    [property: JsonPropertyName("bindingEvents")] Boolean BindingEvents = false,
    [property: JsonPropertyName("localPrice")] Boolean LocalPrice = false,

    /* lists */
    [property: JsonPropertyName("programDescriptions")] IReadOnlyList<Uri>? ProgramDescriptions = null,
    [property: JsonPropertyName("payloadDescriptors")] IReadOnlyList<EventPayloadDescriptor>? PayloadDescriptors = null,
    [property: JsonPropertyName("targets")] IReadOnlyList<Target>? Targets = null
) : IOpenADRObject;

