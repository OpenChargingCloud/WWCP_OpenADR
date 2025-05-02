
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

public class ResourceReport
{

    [JsonPropertyName("resourceName")]
    public String ResourceName { get; set; } = string.Empty;

    [JsonPropertyName("intervalPeriod")]
    public IntervalPeriod? IntervalPeriod { get; set; }

    [JsonPropertyName("intervals")]
    public IReadOnlyList<Interval> Intervals { get; set; } = Array.Empty<Interval>();

}
