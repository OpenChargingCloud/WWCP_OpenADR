/*
 * Copyright (c) 2014-2025 GraphDefined GmbH <achim.friedland@graphdefined.com>
 * This file is part of WWCP OpenADR <https://github.com/OpenChargingCloud/WWCP_OpenADR>
 *
 * Licensed under the Affero GPL license, Version 3.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.gnu.org/licenses/agpl.html
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System.Diagnostics.CodeAnalysis;
using cloud.charging.open.protocols.WWCP;
using Newtonsoft.Json.Linq;

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.OpenADRv3
{

    /// <summary>
    /// An object created by a client to receive notification of operations on objects.
    /// Clients may subscribe to be notified when a type of object is created,
    /// updated, or deleted.
    /// </summary>
    public class Subscription : AOpenADRObject
    {

        #region Properties

        /// <summary>
        /// The client name. An user generated identifier which may also
        /// be a VEN identifier provisioned out-of-band.
        /// </summary>
        [Mandatory]
        public String                        ClientName          { get; }

        /// <summary>
        /// The identification of the program this subscription is associated with.
        /// </summary>
        [Mandatory]
        public Object_Id                     ProgramId           { get; }

        /// <summary>
        /// The enumeration of objects and operations to subscribe to.
        /// </summary>
        [Mandatory]
        public IEnumerable<ObjectOperation>  ObjectOperations    { get; }

        /// <summary>
        /// The optional enumeration of valuesMap objects used by the server to filter callbacks.
        /// </summary>
        [Optional]
        public IEnumerable<ValuesMap>        Targets             { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new subscription.
        /// </summary>
        /// <param name="ClientName">The client name. An user generated identifier which may also be a VEN identifier provisioned out-of-band.</param>
        /// <param name="ProgramId">The identification of the program this subscription is associated with.</param>
        /// <param name="ObjectOperations">The enumeration of objects and operations to subscribe to.</param>
        /// <param name="Targets">An optional enumeration of valuesMap objects used by the server to filter callbacks.</param>
        /// 
        /// <param name="Id">The optional unique identification of this subscription.</param>
        /// <param name="Created">The optional date and time when this subscription was created.</param>
        /// <param name="LastModification">The optional date and time when this subscription was last modified.</param>
        public Subscription(String                        ClientName,
                            Object_Id                     ProgramId,
                            IEnumerable<ObjectOperation>  ObjectOperations,
                            IEnumerable<ValuesMap>?       Targets            = null,

                            Object_Id?                    Id                 = null,
                            DateTimeOffset?               Created            = null,
                            DateTimeOffset?               LastModification   = null)

            : base(ObjectType.SUBSCRIPTION,
                   Id,
                   Created,
                   LastModification)

        {

            this.ClientName        = ClientName;
            this.ProgramId         = ProgramId;
            this.ObjectOperations  = ObjectOperations?.Distinct() ?? [];
            this.Targets           = Targets?.         Distinct() ?? [];

            unchecked
            {

                hashCode = this.ClientName.      GetHashCode()  * 7 +
                           this.ProgramId.       GetHashCode()  * 5 +
                           this.ObjectOperations.CalcHashCode() * 3 +
                           this.Targets.         CalcHashCode();

            }

        }

        #endregion


        #region Documentation

        // subscription:
        //   type: object
        //   description: |
        //     An object created by a client to receive notification of operations on objects.
        //     Clients may subscribe to be notified when a type of object is created,
        //     updated, or deleted.
        //   required:
        //     - clientName
        //     - programID
        //     - objectOperations
        //   properties:
        //     id:
        //       $ref: '#/components/schemas/objectID'
        //       #  VTN provisioned on object creation.
        //     createdDateTime:
        //       $ref: '#/components/schemas/dateTime'
        //       #  VTN provisioned on object creation.
        //     modificationDateTime:
        //       $ref: '#/components/schemas/dateTime'
        //       #  VTN provisioned on object modification.
        //     objectType:
        //       type: string
        //       description: Used as discriminator.
        //       enum: [SUBSCRIPTION]
        //       # VTN provisioned on object creation.
        //
        //     clientName:
        //       type: string
        //       description: User generated identifier, may be VEN identifier provisioned out-of-band.
        //       minLength: 1
        //       maxLength: 128
        //       example: VEN-999
        //     programID:
        //       $ref: '#/components/schemas/objectID'
        //       # ID attribute of program object this subscription is associated with.
        //     objectOperations:
        //       type: array
        //       description: list of objects and operations to subscribe to.
        //       items:
        //         type: object
        //         description: object type, operations, and callbackUrl.
        //         required:
        //           - objects
        //           - operations
        //           - callbackUrl
        //         properties:
        //           objects:
        //             type: array
        //             description: list of objects to subscribe to.
        //             items:
        //               $ref: '#/components/schemas/objectTypes'
        //           operations:
        //             type: array
        //             description: list of operations to subscribe to.
        //             items:
        //               type: string
        //               description: object operation to subscribe to.
        //               example: POST
        //               enum: [GET, POST, PUT, DELETE]
        //           callbackUrl:
        //             type: string
        //             format: uri
        //             description: User provided webhook URL.
        //             example: https://myserver.com/send/callback/here
        //           bearerToken:
        //             type: string
        //             description: |
        //               User provided token.
        //               To avoid custom integrations, callback endpoints
        //               should accept the provided bearer token to authenticate VTN requests.
        //             example: NCEJGI9E8ER9802UT9HUG
        //             nullable: true
        //             default: null
        //     targets:
        //       type: array
        //       description: A list of valuesMap objects. Used by server to filter callbacks.
        //       items:
        //         $ref: '#/components/schemas/valuesMap'
        //       nullable: true
        //       default: null

        #endregion

        #region (static) TryParse(JSON, out Subscription, CustomSubscriptionParser = null)

        // Note: The following is needed to satisfy pattern matching delegates! Do not refactor it!

        /// <summary>
        /// Try to parse the given JSON representation of a subscription.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="Subscription">The parsed subscription.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParse(JObject                                 JSON,
                                       [NotNullWhen(true)]  out Subscription?  Subscription,
                                       [NotNullWhen(false)] out String?        ErrorResponse)

            => TryParse(JSON,
                        out Subscription,
                        out ErrorResponse,
                        null);


        /// <summary>
        /// Try to parse the given JSON representation of a subscription.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="Subscription">The parsed subscription.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="CustomSubscriptionParser">A delegate to parse custom subscriptions.</param>
        public static Boolean TryParse(JObject                                     JSON,
                                       [NotNullWhen(true)]  out Subscription?      Subscription,
                                       [NotNullWhen(false)] out String?            ErrorResponse,
                                       CustomJObjectParserDelegate<Subscription>?  CustomSubscriptionParser)
        {

            try
            {

                Subscription = null;

                #region ClientName          [mandatory]

                if (!JSON.ParseMandatoryText("clientName",
                                             "client name",
                                             out String? clientName,
                                             out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region ProgramId           [mandatory]

                if (!JSON.ParseMandatory("programID",
                                         "program identification",
                                         Object_Id.TryParse,
                                         out Object_Id programId,
                                         out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region ObjectOperations    [mandatory]

                if (!JSON.ParseMandatoryHashSet("objectOperations",
                                                "object operations",
                                                ObjectOperation.TryParse,
                                                out HashSet<ObjectOperation> objectOperations,
                                                out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region Targets             [optional]

                if (JSON.ParseOptionalHashSet("targets",
                                              "targets",
                                              ValuesMap.TryParse,
                                              out HashSet<ValuesMap> targets,
                                              out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion


                #region Id                  [optional]

                if (JSON.ParseOptional("id",
                                       "randomize start",
                                       Object_Id.TryParse,
                                       out Object_Id? id,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region Created             [optional]

                if (JSON.ParseOptional("createdDateTime",
                                       "randomize start",
                                       out DateTimeOffset? created,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region LastModification    [optional]

                if (JSON.ParseOptional("modificationDateTime",
                                       "randomize start",
                                       out DateTimeOffset? lastModification,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion


                Subscription = new Subscription(

                                   clientName,
                                   programId,
                                   objectOperations,
                                   targets,

                                   id,
                                   created,
                                   lastModification

                               );

                if (CustomSubscriptionParser is not null)
                    Subscription = CustomSubscriptionParser(JSON,
                                                            Subscription);

                return true;

            }
            catch (Exception e)
            {
                Subscription   = default;
                ErrorResponse  = "The given JSON representation of a subscription is invalid: " + e.Message;
                return false;
            }

        }

        #endregion

        #region ToJSON(CustomSubscriptionSerializer = null, CustomObjectOperationSerializer = null, ...)

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        /// <param name="CustomSubscriptionSerializer">A delegate to serialize custom subscriptions.</param>
        /// <param name="CustomObjectOperationSerializer">A delegate to serialize custom object operations.</param>
        /// <param name="CustomValuesMapSerializer">A delegate to serialize custom values maps.</param>
        public JObject ToJSON(CustomJObjectSerializerDelegate<Subscription>?     CustomSubscriptionSerializer      = null,
                              CustomJObjectSerializerDelegate<ObjectOperation>?  CustomObjectOperationSerializer   = null,
                              CustomJObjectSerializerDelegate<ValuesMap>?        CustomValuesMapSerializer         = null)
        {

            var json = JSONObject.Create(

                           Id.              HasValue
                               ? new JProperty("id",                     Id.                    ToString())
                               : null,

                           Created.         HasValue
                               ? new JProperty("createdDateTime",        Created.         Value.ToISO8601())
                               : null,

                           LastModification.HasValue
                               ? new JProperty("modificationDateTime",   LastModification.Value.ToISO8601())
                               : null,

                                 new JProperty("objectType",             ObjectType.            ToString()),


                                 new JProperty("clientName",             ClientName),
                                 new JProperty("programID",              ProgramId.             ToString()),
                                 new JProperty("objectOperations",       new JArray(ObjectOperations.Select(objectOperation => objectOperation.ToJSON(CustomObjectOperationSerializer)))),

                            Targets.Any()
                               ? new JProperty("targets",                new JArray(Targets.         Select(target          => target.         ToJSON(CustomValuesMapSerializer))))
                               : null

                       );

            return CustomSubscriptionSerializer is not null
                       ? CustomSubscriptionSerializer(this, json)
                       : json;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this subscription.
        /// </summary>
        public Subscription Clone()

            => new (

                   ClientName.      CloneString(),
                   ProgramId.       Clone(),
                   ObjectOperations.Select(objectOperation => objectOperation.Clone()),
                   Targets.         Select(taraget         => taraget.        Clone()),

                   Id?.             Clone(),
                   Created,
                   LastModification

                );

        #endregion


        #region Operator overloading

        #region Operator == (Subscription1, Subscription2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Subscription1">A subscription.</param>
        /// <param name="Subscription2">Another subscription.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Subscription? Subscription1,
                                           Subscription? Subscription2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(Subscription1, Subscription2))
                return true;

            // If one is null, but not both, return false.
            if (Subscription1 is null || Subscription2 is null)
                return false;

            return Subscription1.Equals(Subscription2);

        }

        #endregion

        #region Operator != (Subscription1, Subscription2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Subscription1">A subscription.</param>
        /// <param name="Subscription2">Another subscription.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Subscription? Subscription1,
                                           Subscription? Subscription2)

            => !(Subscription1 == Subscription2);

        #endregion

        #endregion

        #region IEquatable<Subscription> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two subscriptions for equality.
        /// </summary>
        /// <param name="Object">A subscription to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is Subscription subscription &&
                   Equals(subscription);

        #endregion

        #region Equals(Subscription)

        /// <summary>
        /// Compares two subscriptions for equality.
        /// </summary>
        /// <param name="Subscription">A subscription to compare with.</param>
        public Boolean Equals(Subscription? Subscription)

            => Subscription is not null &&

               ClientName.              Equals       (Subscription.ClientName)               &&
               ProgramId.               Equals       (Subscription.ProgramId)                &&
               ObjectOperations.Order().SequenceEqual(Subscription.ObjectOperations.Order()) &&
               Targets.         Order().SequenceEqual(Subscription.Targets.         Order()) &&

            ((!Id.              HasValue && !Subscription.Id.              HasValue) ||
              (Id.              HasValue &&  Subscription.Id.              HasValue && Id.              Value.Equals(Subscription.Id.              Value))) &&

            ((!Created.         HasValue && !Subscription.Created.         HasValue) ||
              (Created.         HasValue &&  Subscription.Created.         HasValue && Created.         Value.Equals(Subscription.Created.         Value))) &&

            ((!LastModification.HasValue && !Subscription.LastModification.HasValue) ||
              (LastModification.HasValue &&  Subscription.LastModification.HasValue && LastModification.Value.Equals(Subscription.LastModification.Value)));

        #endregion

        #endregion

        #region (override) GetHashCode()

        private readonly Int32 hashCode;

        /// <summary>
        /// Return the hash code of this object.
        /// </summary>
        public override Int32 GetHashCode()
            => hashCode;

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this object.
        /// </summary>
        public override String ToString()

            => String.Concat(

                   CommonInfo.Length > 0
                       ? $"{CommonInfo.AggregateWith(", ")}, "
                       : "",

                   $"'{ClientName}' /  '{ProgramId}' for '{ObjectOperations.AggregateWith(", ")}'",

                   Targets.Any()
                       ? $" at '{Targets.AggregateWith(", ")}'"
                       : ""

               );

        #endregion

    }

}
