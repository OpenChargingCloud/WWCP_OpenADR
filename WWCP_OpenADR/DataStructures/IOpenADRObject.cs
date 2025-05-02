
//using System.Text.Json.Serialization;

//namespace cloud.charging.open.protocols.OpenADRv3;

///// <summary>
///// Common wrapper put on *every* top-level object.
///// </summary>
//public interface IOpenADRObject
//{
//    [JsonPropertyName("id")]                    String          Id          { get; init; }
//    [JsonPropertyName("createdDateTime")]       DateTimeOffset  Created     { get; init; }
//    [JsonPropertyName("modificationDateTime")]  DateTimeOffset  Modified    { get; init; }
//    [JsonPropertyName("objectType")]            ObjectType     ObjectType

//        => GetType().Name.ToUpper() switch {
//               "PROGRAMDTO"       => ObjectType.PROGRAM,
//               "EVENTDTO"         => ObjectType.EVENT,
//               "REPORTDTO"        => ObjectType.REPORT,
//               "SUBSCRIPTIONDTO"  => ObjectType.SUBSCRIPTION,
//               "VENDTO"           => ObjectType.VEN,
//               "RESOURCEDTO"      => ObjectType.RESOURCE,
//               _                  => throw new NotSupportedException()
//           };

//}
