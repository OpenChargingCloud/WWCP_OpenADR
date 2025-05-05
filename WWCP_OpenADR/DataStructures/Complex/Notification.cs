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

using Newtonsoft.Json.Linq;

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.OpenADRv3
{

    /// <summary>
    /// Virtual Top Node (VTN) generated object included in request to subscription callback URL.
    /// </summary>
    public class Notification
    {

        #region Properties

        /// <summary>
        /// The object type.
        /// </summary>
        [Mandatory]
        public ObjectType              ObjectType    { get; }

        /// <summary>
        /// The operation on an object that triggered the notification.
        /// </summary>
        [Mandatory]
        public Operation               Operation     { get; }

        /// <summary>
        /// The object that is the subject of the notification.
        /// </summary>
        [Mandatory]
        public IOpenADRObject          Object        { get; }

        /// <summary>
        /// The enumeration of valuesMap objects.
        /// </summary>
        [Optional]
        public IEnumerable<ValuesMap>  Targets       { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new notification.
        /// </summary>
        /// <param name="ObjectType">An object type.</param>
        /// <param name="Operation">An operation on an object that triggered the notification.</param>
        /// <param name="Object">An object that is the subject of the notification.</param>
        /// <param name="Targets">An enumeration of valuesMap objects.</param>
        public Notification(ObjectType               ObjectType,
                            Operation                Operation,
                            IOpenADRObject           Object,
                            IEnumerable<ValuesMap>?  Targets   = null)
        {

            this.ObjectType  = ObjectType;
            this.Operation   = Operation;
            this.Object      = Object;
            this.Targets     = Targets?.Distinct() ?? [];

            unchecked
            {

                hashCode = this.ObjectType.GetHashCode() * 7 ^
                           this.Operation. GetHashCode() * 5 ^
                           this.Object.    GetHashCode() * 3 ^
                           this.Targets.   CalcHashCode();

            }

        }

        #endregion


        #region Documentation

        // notification:
        //   type: object
        //   description: |
        //     VTN generated object included in request to subscription callbackUrl.
        //   required:
        //     - objectType
        //     - operation
        //     - object
        //   properties:
        //     objectType:
        //       $ref: '#/components/schemas/objectTypes'
        //     operation:
        //       type: string
        //       description: the operation on on object that triggered the notification.
        //       example: POST
        //       enum: [GET, POST, PUT, DELETE]
        //     object:
        //       type: object
        //       description: the object that is the subject of the notification.
        //       example: {}
        //       oneOf:
        //         - $ref: '#/components/schemas/program'
        //         - $ref: '#/components/schemas/report'
        //         - $ref: '#/components/schemas/event'
        //         - $ref: '#/components/schemas/subscription'
        //         - $ref: '#/components/schemas/ven'
        //         - $ref: '#/components/schemas/resource'
        //       discriminator:
        //         propertyName: objectType
        //     targets:
        //       type: array
        //       description: A list of valuesMap objects.
        //       items:
        //         $ref: '#/components/schemas/valuesMap'
        //       nullable: true
        //       default: null

        #endregion

        #region (static) TryParse(JSON, out Notification, CustomNotificationParser = null)

        // Note: The following is needed to satisfy pattern matching delegates! Do not refactor it!

        /// <summary>
        /// Try to parse the given JSON representation of a notification.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="Notification">The parsed notification.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParse(JObject                                 JSON,
                                       [NotNullWhen(true)]  out Notification?  Notification,
                                       [NotNullWhen(false)] out String?        ErrorResponse)

            => TryParse(JSON,
                        out Notification,
                        out ErrorResponse,
                        null);


        /// <summary>
        /// Try to parse the given JSON representation of a notification.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="Notification">The parsed notification.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="CustomNotificationParser">A delegate to parse custom notifications.</param>
        public static Boolean TryParse(JObject                                     JSON,
                                       [NotNullWhen(true)]  out Notification?      Notification,
                                       [NotNullWhen(false)] out String?            ErrorResponse,
                                       CustomJObjectParserDelegate<Notification>?  CustomNotificationParser)
        {

            try
            {

                Notification = null;

                #region ObjectType       [mandatory]

                if (!JSON.ParseMandatory("objectType",
                                         "object type",
                                         ObjectType.TryParse,
                                         out ObjectType objectType,
                                         out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region Operation        [mandatory]

                if (!JSON.ParseMandatory("operation",
                                         "operation",
                                         Operation.TryParse,
                                         out Operation operation,
                                         out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region OpenADRObject    [mandatory]

                IOpenADRObject? openADRObject = null;

                var openADRObjectJSON = JSON["object"] as JObject;
                if (openADRObjectJSON is null)
                {
                    ErrorResponse = "The given JSON representation of a notification does not contain an valid OpenADR 'object' property!";
                    return false;
                }

                switch (objectType.ToString())
                {

                    case ObjectType.Defaults.EVENT:
                        if (!Event.TryParse(openADRObjectJSON,
                                            out var @event,
                                            out ErrorResponse))
                        {
                            return false;
                        }
                        openADRObject = @event;
                        break;

                    case ObjectType.Defaults.PROGRAM:
                        if (!Program.TryParse(openADRObjectJSON,
                                              out var program,
                                              out ErrorResponse))
                        {
                            return false;
                        }
                        openADRObject = program;
                        break;

                    case ObjectType.Defaults.REPORT:
                        if (!Report.TryParse(openADRObjectJSON,
                                              out var report,
                                              out ErrorResponse))
                        {
                            return false;
                        }
                        openADRObject = report;
                        break;

                    case ObjectType.Defaults.RESOURCE:
                        if (!Resource.TryParse(openADRObjectJSON,
                                               out var resource,
                                               out ErrorResponse))
                        {
                            return false;
                        }
                        openADRObject = resource;
                        break;

                    case ObjectType.Defaults.SUBSCRIPTION:
                        if (!Subscription.TryParse(openADRObjectJSON,
                                                   out var subscription,
                                                   out ErrorResponse))
                        {
                            return false;
                        }
                        openADRObject = subscription;
                        break;

                    case ObjectType.Defaults.VEN:
                        if (!VirtualEndNode.TryParse(openADRObjectJSON,
                                                     out var virtualEndNode,
                                                     out ErrorResponse))
                        {
                            return false;
                        }
                        openADRObject = virtualEndNode;
                        break;


                    default:
                        ErrorResponse = "The given JSON representation of a notification does not contain an valid OpenADR 'object' property!";
                        return false;

                }

                #endregion

                #region Targets          [optional]

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


                Notification = new Notification(
                                   objectType,
                                   operation,
                                   openADRObject,
                                   targets
                               );

                if (CustomNotificationParser is not null)
                    Notification = CustomNotificationParser(JSON,
                                                            Notification);

                return true;

            }
            catch (Exception e)
            {
                Notification   = default;
                ErrorResponse  = "The given JSON representation of a notification is invalid: " + e.Message;
                return false;
            }

        }

        #endregion

        #region ToJSON(CustomNotificationSerializer = null, CustomObjectOperationSerializer = null, ...)

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        /// <param name="CustomNotificationSerializer">A delegate to serialize custom notifications.</param>
        /// <param name="CustomObjectOperationSerializer">A delegate to serialize custom object operations.</param>
        /// <param name="CustomValuesMapSerializer">A delegate to serialize custom values maps.</param>
        public JObject ToJSON(CustomJObjectSerializerDelegate<Notification>?     CustomNotificationSerializer      = null,
                              CustomJObjectSerializerDelegate<ObjectOperation>?  CustomObjectOperationSerializer   = null,
                              CustomJObjectSerializerDelegate<ValuesMap>?        CustomValuesMapSerializer         = null)
        {

            var json = JSONObject.Create(

                                new JProperty("objectType",  ObjectType.ToString()),
                                new JProperty("operation",   Operation. ToString()),
                                new JProperty("object",      Object.    ToJSON()),

                           Targets.Any()
                              ? new JProperty("targets",     new JArray(Targets.Select(target => target.ToJSON(CustomValuesMapSerializer))))
                              : null

                       );

            return CustomNotificationSerializer is not null
                       ? CustomNotificationSerializer(this, json)
                       : json;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this notification.
        /// </summary>
        public Notification Clone()

            => new (
                   ObjectType.Clone(),
                   Operation. Clone(),
                   Object.    Clone(),
                   Targets.Select(target => target.Clone())
                );

        #endregion


        #region Operator overloading

        #region Operator == (Notification1, Notification2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Notification1">A notification.</param>
        /// <param name="Notification2">Another notification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Notification? Notification1,
                                           Notification? Notification2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(Notification1, Notification2))
                return true;

            // If one is null, but not both, return false.
            if (Notification1 is null || Notification2 is null)
                return false;

            return Notification1.Equals(Notification2);

        }

        #endregion

        #region Operator != (Notification1, Notification2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Notification1">A notification.</param>
        /// <param name="Notification2">Another notification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Notification? Notification1,
                                           Notification? Notification2)

            => !(Notification1 == Notification2);

        #endregion

        #endregion

        #region IEquatable<Notification> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two notifications for equality.
        /// </summary>
        /// <param name="Object">A notification to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is Notification notification &&
                   Equals(notification);

        #endregion

        #region Equals(Notification)

        /// <summary>
        /// Compares two notifications for equality.
        /// </summary>
        /// <param name="Notification">A notification to compare with.</param>
        public Boolean Equals(Notification? Notification)

            => Notification is not null &&

               ObjectType.     Equals       (Notification.ObjectType) &&
               Operation.      Equals       (Notification.Operation)  &&
               Object.         Equals       (Notification.Object)     &&

               Targets.Order().SequenceEqual(Notification.Targets.Order());

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

                   $"'{Operation}' on '{ObjectType}'",

                   Targets.Any()
                       ? $" at '{Targets.AggregateWith(", ")}'"
                       : "",

                   $", Object: {Object}"

               );

        #endregion

    }

}
