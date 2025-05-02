
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

public class ProgramDescription
{
    [JsonPropertyName("URL")]
    public string URL { get; set; } = string.Empty;
}
