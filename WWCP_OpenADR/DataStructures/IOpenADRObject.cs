
using System.Text.Json.Serialization;

namespace cloud.charging.open.protocols.OpenADRv3;

/// <summary>
/// Common wrapper put on *every* top-level object.
/// </summary>
public interface IOpenADRObject
{
    [JsonPropertyName("id")]                    String          Id          { get; init; }
    [JsonPropertyName("createdDateTime")]       DateTimeOffset  Created     { get; init; }
    [JsonPropertyName("modificationDateTime")]  DateTimeOffset  Modified    { get; init; }
    [JsonPropertyName("objectType")]            ObjectTypes      ObjectType

        => GetType().Name.ToUpper() switch {
               "PROGRAMDTO"       => ObjectTypes.PROGRAM,
               "EVENTDTO"         => ObjectTypes.EVENT,
               "REPORTDTO"        => ObjectTypes.REPORT,
               "SUBSCRIPTIONDTO"  => ObjectTypes.SUBSCRIPTION,
               "VENDTO"           => ObjectTypes.VEN,
               "RESOURCEDTO"      => ObjectTypes.RESOURCE,
               _                  => throw new NotSupportedException()
           };

}
