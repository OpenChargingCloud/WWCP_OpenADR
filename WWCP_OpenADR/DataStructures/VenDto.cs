
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

public sealed record VenDto(
    [property: JsonPropertyName("id")] String Id,
    [property: JsonPropertyName("createdDateTime")] DateTimeOffset Created,
    [property: JsonPropertyName("modificationDateTime")] DateTimeOffset Modified,

    [property: JsonPropertyName("venName")] String VenName,
    [property: JsonPropertyName("attributes")] IReadOnlyList<ValuesMap>? Attributes,
    [property: JsonPropertyName("resources")] IReadOnlyList<ResourceDto>? Resources
) : IOpenADRObject;