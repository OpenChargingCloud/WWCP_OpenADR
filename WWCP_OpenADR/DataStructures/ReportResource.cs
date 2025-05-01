
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

public sealed record ReportResource(
    [property: JsonPropertyName("resourceName")] String ResourceName,
    [property: JsonPropertyName("intervalPeriod")] IntervalPeriod IntervalPeriod,
    [property: JsonPropertyName("intervals")] IReadOnlyList<Interval> Intervals);
