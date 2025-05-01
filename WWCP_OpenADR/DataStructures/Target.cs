
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;


public sealed record Target(
    [property: JsonPropertyName("type")] String Type,   // e.g. “VEN_NAME”, “PROGRAM_NAME” … :contentReference[oaicite:0]{index=0}
    [property: JsonPropertyName("values")] IReadOnlyList<String> Values);
