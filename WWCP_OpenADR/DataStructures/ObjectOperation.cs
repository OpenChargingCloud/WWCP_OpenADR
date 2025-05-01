
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

public sealed record ObjectOperation(
    [property: JsonPropertyName("objects")] IReadOnlyList<String> Objects,   // e.g. ["EVENT","REPORT"]
    [property: JsonPropertyName("operations")] IReadOnlyList<String> Operations // e.g. ["CREATE","UPDATE"]
);
