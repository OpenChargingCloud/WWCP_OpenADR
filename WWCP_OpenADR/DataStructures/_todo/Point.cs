
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

public class Point
{
    [JsonPropertyName("x")]
    public float X { get; set; }

    [JsonPropertyName("y")]
    public float Y { get; set; }
}
