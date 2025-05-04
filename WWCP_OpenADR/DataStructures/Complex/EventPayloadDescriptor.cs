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
    /// Contextual information used to interpret event valuesMap values.
    /// E.g. a PRICE payload simply contains a price value, an
    /// associated descriptor provides necessary context such as units and currency.
    /// </summary>
    public class EventPayloadDescriptor : APayloadDescriptor
    {

        #region Properties

        /// <summary>
        /// Units of measure.
        /// </summary>
        [Optional]
        public UnitType?  Units       { get; }

        /// <summary>
        /// Currency of price payload.
        /// </summary>
        [Optional]
        public Currency?  Currency    { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new event payload descriptor.
        /// </summary>
        /// <param name="PayloadType">Signifying the nature of values.</param>
        /// <param name="Units">Units of measure.</param>
        /// <param name="Currency">Currency of price payload.</param>
        public EventPayloadDescriptor(PayloadType  PayloadType,
                                      UnitType?    Units      = null,
                                      Currency?    Currency   = null)

            : base(ObjectType.EVENT_PAYLOAD_DESCRIPTOR,
                   PayloadType)

        {

            this.Units     = Units;
            this.Currency  = Currency;

            unchecked
            {

                hashCode = (this.Units?.   GetHashCode() ?? 0) * 5 ^
                           (this.Currency?.GetHashCode() ?? 0) * 3 ^
                            base.          GetHashCode();

            }

        }

        #endregion


        #region Documentation

        // eventPayloadDescriptor:
        //   type: object
        //   description: |
        //     Contextual information used to interpret event valuesMap values.
        //     E.g. a PRICE payload simply contains a price value, an
        //     associated descriptor provides necessary context such as units and currency.
        //   required:
        //     - payloadType
        //   properties:
        //     objectType:
        //       type: string
        //       description: Used as discriminator.
        //       enum: [EVENT_PAYLOAD_DESCRIPTOR]
        //     payloadType:
        //       type: string
        //       description: Enumerated or private string signifying the nature of values.
        //       minLength: 1
        //       maxLength: 128
        //       example: PRICE
        //     units:
        //       type: string
        //       description: Units of measure.
        //       example: KWH
        //       nullable: true
        //       default: null
        //     currency:
        //       type: string
        //       description: Currency of price payload.
        //       example: USD
        //       nullable: true
        //       default: null

        #endregion

        #region (static) TryParse(JSON, out EventPayloadDescriptor, CustomEventPayloadDescriptorParser = null)

        // Note: The following is needed to satisfy pattern matching delegates! Do not refactor it!

        /// <summary>
        /// Try to parse the given JSON representation of an event payload descriptor.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="EventPayloadDescriptor">The parsed event payload descriptor.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParse(JObject                                           JSON,
                                       [NotNullWhen(true)]  out EventPayloadDescriptor?  EventPayloadDescriptor,
                                       [NotNullWhen(false)] out String?                  ErrorResponse)

            => TryParse(JSON,
                        out EventPayloadDescriptor,
                        out ErrorResponse,
                        null);


        /// <summary>
        /// Try to parse the given JSON representation of an event payload descriptor.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="EventPayloadDescriptor">The parsed event payload descriptor.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="CustomEventPayloadDescriptorParser">A delegate to parse custom event payload descriptors.</param>
        public static Boolean TryParse(JObject                                               JSON,
                                       [NotNullWhen(true)]  out EventPayloadDescriptor?      EventPayloadDescriptor,
                                       [NotNullWhen(false)] out String?                      ErrorResponse,
                                       CustomJObjectParserDelegate<EventPayloadDescriptor>?  CustomEventPayloadDescriptorParser)
        {

            try
            {

                EventPayloadDescriptor = null;

                #region PayloadType    [mandatory]

                if (!JSON.ParseMandatory("payloadType",
                                         "payload type",
                                         PayloadType.TryParse,
                                         out PayloadType payloadType,
                                         out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region Units          [optional]

                if (JSON.ParseOptional("units",
                                       "units",
                                       UnitType.TryParse,
                                       out UnitType? units,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region Currency       [optional]

                if (JSON.ParseOptional("currency",
                                       "currency",
                                       Currency.TryParse,
                                       out Currency? currency,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion


                EventPayloadDescriptor = new EventPayloadDescriptor(
                                             payloadType,
                                             units,
                                             currency
                                         );

                if (CustomEventPayloadDescriptorParser is not null)
                    EventPayloadDescriptor = CustomEventPayloadDescriptorParser(JSON,
                                                                                EventPayloadDescriptor);

                return true;

            }
            catch (Exception e)
            {
                EventPayloadDescriptor  = default;
                ErrorResponse           = "The given JSON representation of an event payload descriptor is invalid: " + e.Message;
                return false;
            }

        }

        #endregion

        #region ToJSON(CustomEventPayloadDescriptorSerializer = null)

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        public override JObject ToJSON()
            => ToJSON(null);


        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        /// <param name="CustomEventPayloadDescriptorSerializer">A delegate to serialize custom EventPayloadDescriptor objects.</param>
        public JObject ToJSON(CustomJObjectSerializerDelegate<EventPayloadDescriptor>? CustomEventPayloadDescriptorSerializer)
        {

            var json = JSONObject.Create(

                                 new JProperty("objectType",    ObjectType. ToString()),
                                 new JProperty("payloadType",   PayloadType.ToString()),

                           Units.HasValue
                               ? new JProperty("units",         Units.Value.ToString())
                               : null,

                           Currency is not null
                               ? new JProperty("currency",      Currency.   ToString())
                               : null

                       );

            return CustomEventPayloadDescriptorSerializer is not null
                       ? CustomEventPayloadDescriptorSerializer(this, json)
                       : json;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this event payload descriptor.
        /// </summary>
        public override EventPayloadDescriptor Clone()

            => new (
                   PayloadType.Clone(),
                   Units?.     Clone(),
                   Currency?.  Clone()
               );

        #endregion


        #region Operator overloading

        #region Operator == (EventPayloadDescriptor1, EventPayloadDescriptor2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EventPayloadDescriptor1">An event payload descriptor.</param>
        /// <param name="EventPayloadDescriptor2">Another event payload descriptor.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (EventPayloadDescriptor? EventPayloadDescriptor1,
                                           EventPayloadDescriptor? EventPayloadDescriptor2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(EventPayloadDescriptor1, EventPayloadDescriptor2))
                return true;

            // If one is null, but not both, return false.
            if (EventPayloadDescriptor1 is null || EventPayloadDescriptor2 is null)
                return false;

            return EventPayloadDescriptor1.Equals(EventPayloadDescriptor2);

        }

        #endregion

        #region Operator != (EventPayloadDescriptor1, EventPayloadDescriptor2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="EventPayloadDescriptor1">An event payload descriptor.</param>
        /// <param name="EventPayloadDescriptor2">Another event payload descriptor.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (EventPayloadDescriptor? EventPayloadDescriptor1,
                                           EventPayloadDescriptor? EventPayloadDescriptor2)

            => !(EventPayloadDescriptor1 == EventPayloadDescriptor2);

        #endregion

        #endregion

        #region IEquatable<EventPayloadDescriptor> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two event payload descriptors for equality.
        /// </summary>
        /// <param name="Object">An event payload descriptor to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is EventPayloadDescriptor eventPayloadDescriptor &&
                   Equals(eventPayloadDescriptor);

        #endregion

        #region Equals(EventPayloadDescriptor)

        /// <summary>
        /// Compares two event payload descriptors for equality.
        /// </summary>
        /// <param name="EventPayloadDescriptor">An event payload descriptor to compare with.</param>
        public Boolean Equals(EventPayloadDescriptor? EventPayloadDescriptor)

            => EventPayloadDescriptor is not null &&

               PayloadType.Equals(EventPayloadDescriptor.PayloadType) &&

            ((!Units.   HasValue    && !EventPayloadDescriptor.Units.  HasValue) ||
              (Units.   HasValue    &&  EventPayloadDescriptor.Units.  HasValue    && Units.Value.Equals(EventPayloadDescriptor.Units.Value))) &&

             ((Currency is null     && EventPayloadDescriptor.Currency is null) ||
              (Currency is not null && EventPayloadDescriptor.Currency is not null && Currency.   Equals(EventPayloadDescriptor.Currency)));

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

                   $"'{PayloadType}'",

                   Units.HasValue
                       ? $", in {Units}"
                       : "",

                   Currency is not null
                       ? $", payable in {Currency}"
                       : ""

               );

        #endregion

    }

}
