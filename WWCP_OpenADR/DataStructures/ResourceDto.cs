
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

public sealed record ResourceDto(
    [property: JsonPropertyName("id")] String Id,
    [property: JsonPropertyName("createdDateTime")] DateTimeOffset Created,
    [property: JsonPropertyName("modificationDateTime")] DateTimeOffset Modified,

    [property: JsonPropertyName("resourceName")] String ResourceName,
    [property: JsonPropertyName("venID")] String VenId,
    [property: JsonPropertyName("attributes")] IReadOnlyList<ValuesMap>? Attributes
) : IOpenADRObject;

