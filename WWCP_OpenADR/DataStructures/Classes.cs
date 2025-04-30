
using System.Text.Json.Serialization;

namespace OpenADR3.Models;


/* ----------  Value helpers (used by many top-level objects) ---------- */

public sealed record IntervalPeriod(
    [property: JsonPropertyName("start")] DateTimeOffset? Start,
    [property: JsonPropertyName("duration")] String? DurationIso,          // ISO-8601 (“PT1H”)
    [property: JsonPropertyName("randomizeStart")] String? RandomizeStartIso = null);

public sealed record ValuesMap(
    [property: JsonPropertyName("type")] String Type,
    [property: JsonPropertyName("values")] IReadOnlyList<object> Values);

public sealed record EventPayloadDescriptor(
    [property: JsonPropertyName("payloadType")] String PayloadType,
    [property: JsonPropertyName("units")] String? Units = null,
    [property: JsonPropertyName("currency")] String? Currency = null);

public sealed record ReportPayloadDescriptor(
    [property: JsonPropertyName("payloadType")] String PayloadType,
    [property: JsonPropertyName("readingType")] String? ReadingType = "DIRECT_READ",
    [property: JsonPropertyName("units")] String? Units = null,
    [property: JsonPropertyName("accuracy")] Double? Accuracy = null,
    [property: JsonPropertyName("confidence")] Double? Confidence = null);

public sealed record Target(
    [property: JsonPropertyName("type")] String Type,   // e.g. “VEN_NAME”, “PROGRAM_NAME” … :contentReference[oaicite:0]{index=0}
    [property: JsonPropertyName("values")] IReadOnlyList<String> Values);

public sealed record Interval(
    [property: JsonPropertyName("id")] ulong Id, // = 0
    [property: JsonPropertyName("intervalPeriod")] IntervalPeriod Period,
    [property: JsonPropertyName("payloads")] IReadOnlyList<ValuesMap> Payloads); // can mix PRICE + DATA_QUALITY, etc.






/* ----------  PROGRAM ---------- */
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




/* ----------  EVENT ---------- */
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




/* ----------  REPORT ---------- */
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

public sealed record ReportResource(
    [property: JsonPropertyName("resourceName")] String ResourceName,
    [property: JsonPropertyName("intervalPeriod")] IntervalPeriod IntervalPeriod,
    [property: JsonPropertyName("intervals")] IReadOnlyList<Interval> Intervals);

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





/* ----------  SUBSCRIPTION ---------- */
public sealed record SubscriptionDto(
    [property: JsonPropertyName("id")] String Id,
    [property: JsonPropertyName("createdDateTime")] DateTimeOffset Created,
    [property: JsonPropertyName("modificationDateTime")] DateTimeOffset Modified,

    [property: JsonPropertyName("clientName")] String ClientName,
    [property: JsonPropertyName("programID")] String ProgramId,

    [property: JsonPropertyName("objectOperations")] IReadOnlyList<ObjectOperation> ObjectOperations,
    [property: JsonPropertyName("callbackUrl")] Uri CallbackUrl,
    [property: JsonPropertyName("bearerToken")] String BearerToken
) : IOpenADRObject;

public sealed record ObjectOperation(
    [property: JsonPropertyName("objects")] IReadOnlyList<String> Objects,   // e.g. ["EVENT","REPORT"]
    [property: JsonPropertyName("operations")] IReadOnlyList<String> Operations // e.g. ["CREATE","UPDATE"]
);




/* ----------  VEN & RESOURCE ---------- */
public sealed record VenDto(
    [property: JsonPropertyName("id")] String Id,
    [property: JsonPropertyName("createdDateTime")] DateTimeOffset Created,
    [property: JsonPropertyName("modificationDateTime")] DateTimeOffset Modified,

    [property: JsonPropertyName("venName")] String VenName,
    [property: JsonPropertyName("attributes")] IReadOnlyList<ValuesMap>? Attributes,
    [property: JsonPropertyName("resources")] IReadOnlyList<ResourceDto>? Resources
) : IOpenADRObject;

public sealed record ResourceDto(
    [property: JsonPropertyName("id")] String Id,
    [property: JsonPropertyName("createdDateTime")] DateTimeOffset Created,
    [property: JsonPropertyName("modificationDateTime")] DateTimeOffset Modified,

    [property: JsonPropertyName("resourceName")] String ResourceName,
    [property: JsonPropertyName("venID")] String VenId,
    [property: JsonPropertyName("attributes")] IReadOnlyList<ValuesMap>? Attributes
) : IOpenADRObject;

