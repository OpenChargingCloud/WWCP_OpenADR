
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

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