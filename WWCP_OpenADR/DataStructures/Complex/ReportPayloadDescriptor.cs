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
    /// Contextual information used to interpret report payload values.
    /// E.g. a USAGE payload simply contains a usage value, an
    /// associated descriptor provides necessary context such as units and data quality.
    /// </summary>
    public class ReportPayloadDescriptor : APayloadDescriptor
    {

        #region Properties

        /// <summary>
        /// Signifying the type of reading.
        /// </summary>
        [Optional]
        public ReadingType?     ReadingType    { get; }

        /// <summary>
        /// Units of measure.
        /// </summary>
        [Optional]
        public UnitType?        Units          { get; }

        /// <summary>
        /// A quantification of the accuracy of a set of payload values.
        /// </summary>
        [Optional]
        public Single?          Accuracy       { get; }

        /// <summary>
        /// A quantification of the confidence in a set of payload values.
        /// </summary>
        [Optional]
        public PercentageByte?  Confidence     { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new report payload descriptor.
        /// </summary>
        /// <param name="PayloadType">Signifying the nature of values.</param>
        /// <param name="ReadingType">Signifying the type of reading.</param>
        /// <param name="Units">Units of measure.</param>
        /// <param name="Accuracy">A quantification of the accuracy of a set of payload values.</param>
        /// <param name="Confidence">A quantification of the confidence in a set of payload values.</param>
        public ReportPayloadDescriptor(PayloadType      PayloadType,
                                       ReadingType?     ReadingType   = null,
                                       UnitType?        Units         = null,
                                       Single?          Accuracy      = null,
                                       PercentageByte?  Confidence    = null)

            : base(ObjectType.REPORT_PAYLOAD_DESCRIPTOR,
                   PayloadType)

        {

            this.ReadingType  = ReadingType;
            this.Units        = Units;
            this.Accuracy     = Accuracy;
            this.Confidence   = Confidence;

            unchecked
            {

                hashCode = (this.ReadingType?.GetHashCode() ?? 0) * 11 +
                           (this.Units?.      GetHashCode() ?? 0) *  7 +
                           (this.Accuracy?.   GetHashCode() ?? 0) *  5 +
                           (this.Confidence?. GetHashCode() ?? 0) *  3 +
                            base.             GetHashCode();

            }

        }

        #endregion


        #region Documentation

        // reportPayloadDescriptor:
        //   type: object
        //   description: |
        //     Contextual information used to interpret report payload values.
        //     E.g. a USAGE payload simply contains a usage value, an
        //     associated descriptor provides necessary context such as units and data quality.
        //   required:
        //     - payloadType
        //   properties:
        //     objectType:
        //       type: string
        //       description: Used as discriminator.
        //       enum: [REPORT_PAYLOAD_DESCRIPTOR]
        //     payloadType:
        //       type: string
        //       description: Enumerated or private string signifying the nature of values.
        //       minLength: 1
        //       maxLength: 128
        //       example: USAGE
        //     readingType:
        //       type: string
        //       description: Enumerated or private string signifying the type of reading.
        //       example: DIRECT_READ
        //       nullable: true
        //       default: null
        //     units:
        //       type: string
        //       description: Units of measure.
        //       example: KWH
        //       nullable: true
        //       default: null
        //     accuracy:
        //       type: number
        //       format: float
        //       description: A quantification of the accuracy of a set of payload values.
        //       example: 0.0
        //       nullable: true
        //       default: null
        //     confidence:
        //       type: integer
        //       format: int32
        //       minimum: 0
        //       maximum: 100
        //       description: A quantification of the confidence in a set of payload values.
        //       example: 100
        //       nullable: true
        //       default: null

        #endregion

        #region (static) TryParse(JSON, out ReportPayloadDescriptor, CustomReportPayloadDescriptorParser = null)

        // Note: The following is needed to satisfy pattern matching delegates! Do not refactor it!

        /// <summary>
        /// Try to parse the given JSON representation of a report payload descriptor.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="ReportPayloadDescriptor">The parsed report payload descriptor.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParse(JObject                                            JSON,
                                       [NotNullWhen(true)]  out ReportPayloadDescriptor?  ReportPayloadDescriptor,
                                       [NotNullWhen(false)] out String?                   ErrorResponse)

            => TryParse(JSON,
                        out ReportPayloadDescriptor,
                        out ErrorResponse,
                        null);


        /// <summary>
        /// Try to parse the given JSON representation of a report payload descriptor.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="ReportPayloadDescriptor">The parsed report payload descriptor.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="CustomReportPayloadDescriptorParser">A delegate to parse custom report payload descriptors.</param>
        public static Boolean TryParse(JObject                                                JSON,
                                       [NotNullWhen(true)]  out ReportPayloadDescriptor?      ReportPayloadDescriptor,
                                       [NotNullWhen(false)] out String?                       ErrorResponse,
                                       CustomJObjectParserDelegate<ReportPayloadDescriptor>?  CustomReportPayloadDescriptorParser)
        {

            try
            {

                ReportPayloadDescriptor = null;

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

                #region ReadingType    [optional]

                if (JSON.ParseOptional("readingType",
                                       "reading type",
                                       OpenADRv3.ReadingType.TryParse,
                                       out ReadingType? readingType,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
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

                #region Accuracy       [optional]

                if (JSON.ParseOptional("currency",
                                       "currency",
                                       out Single? accuracy,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region Confidence     [optional]

                if (JSON.ParseOptional("confidence",
                                       "confidence",
                                       PercentageByte.TryParse,
                                       out PercentageByte? confidence,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion


                ReportPayloadDescriptor = new ReportPayloadDescriptor(
                                              payloadType,
                                              readingType,
                                              units,
                                              accuracy,
                                              confidence
                                          );

                if (CustomReportPayloadDescriptorParser is not null)
                    ReportPayloadDescriptor = CustomReportPayloadDescriptorParser(JSON,
                                                                                  ReportPayloadDescriptor);

                return true;

            }
            catch (Exception e)
            {
                ReportPayloadDescriptor  = default;
                ErrorResponse            = "The given JSON representation of a report payload descriptor is invalid: " + e.Message;
                return false;
            }

        }

        #endregion

        #region ToJSON(CustomReportPayloadDescriptorSerializer = null)

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        public override JObject ToJSON()
            => ToJSON(null);


        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        /// <param name="CustomReportPayloadDescriptorSerializer">A delegate to serialize custom ReportPayloadDescriptor objects.</param>
        public JObject ToJSON(CustomJObjectSerializerDelegate<ReportPayloadDescriptor>? CustomReportPayloadDescriptorSerializer)
        {

            var json = JSONObject.Create(

                                 new JProperty("objectType",    ObjectType.       ToString()),
                                 new JProperty("payloadType",   PayloadType.      ToString()),

                           ReadingType.HasValue
                               ? new JProperty("readingType",   ReadingType.Value.ToString())
                               : null,

                           Units.      HasValue
                               ? new JProperty("units",         Units.            ToString())
                               : null,

                           Accuracy.   HasValue
                               ? new JProperty("accuracy",      Accuracy.  Value)
                               : null,

                           Confidence. HasValue
                               ? new JProperty("confidence",    Confidence.Value.Value)
                               : null

                       );

            return CustomReportPayloadDescriptorSerializer is not null
                       ? CustomReportPayloadDescriptorSerializer(this, json)
                       : json;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this report payload descriptor.
        /// </summary>
        public override ReportPayloadDescriptor Clone()

            => new (
                   PayloadType. Clone(),
                   ReadingType?.Clone(),
                   Units?.      Clone(),
                   Accuracy,
                   Confidence?. Clone()
               );

        #endregion


        #region Operator overloading

        #region Operator == (ReportPayloadDescriptor1, ReportPayloadDescriptor2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReportPayloadDescriptor1">A report payload descriptor.</param>
        /// <param name="ReportPayloadDescriptor2">Another report payload descriptor.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ReportPayloadDescriptor? ReportPayloadDescriptor1,
                                           ReportPayloadDescriptor? ReportPayloadDescriptor2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(ReportPayloadDescriptor1, ReportPayloadDescriptor2))
                return true;

            // If one is null, but not both, return false.
            if (ReportPayloadDescriptor1 is null || ReportPayloadDescriptor2 is null)
                return false;

            return ReportPayloadDescriptor1.Equals(ReportPayloadDescriptor2);

        }

        #endregion

        #region Operator != (ReportPayloadDescriptor1, ReportPayloadDescriptor2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReportPayloadDescriptor1">A report payload descriptor.</param>
        /// <param name="ReportPayloadDescriptor2">Another report payload descriptor.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ReportPayloadDescriptor? ReportPayloadDescriptor1,
                                           ReportPayloadDescriptor? ReportPayloadDescriptor2)

            => !(ReportPayloadDescriptor1 == ReportPayloadDescriptor2);

        #endregion

        #endregion

        #region IEquatable<ReportPayloadDescriptor> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two report payload descriptors for equality.
        /// </summary>
        /// <param name="Object">A report payload descriptor to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is ReportPayloadDescriptor reportPayloadDescriptor &&
                   Equals(reportPayloadDescriptor);

        #endregion

        #region Equals(ReportPayloadDescriptor)

        /// <summary>
        /// Compares two report payload descriptors for equality.
        /// </summary>
        /// <param name="ReportPayloadDescriptor">A report payload descriptor to compare with.</param>
        public Boolean Equals(ReportPayloadDescriptor? ReportPayloadDescriptor)

            => ReportPayloadDescriptor is not null &&

               PayloadType.Equals(ReportPayloadDescriptor.PayloadType) &&

            ((!ReadingType.HasValue && !ReportPayloadDescriptor.ReadingType.HasValue) ||
              (ReadingType.HasValue &&  ReportPayloadDescriptor.ReadingType.HasValue    && ReadingType.Value.Equals(ReportPayloadDescriptor.ReadingType.Value))) &&

            ((!Units.      HasValue && !ReportPayloadDescriptor.Units.      HasValue) ||
              (Units.      HasValue &&  ReportPayloadDescriptor.Units.      HasValue    && Units.      Value.Equals(ReportPayloadDescriptor.Units.      Value))) &&

            ((!Accuracy.   HasValue && !ReportPayloadDescriptor.Accuracy.   HasValue) ||
              (Accuracy.   HasValue &&  ReportPayloadDescriptor.Accuracy.   HasValue    && Accuracy.   Value.Equals(ReportPayloadDescriptor.Accuracy.   Value))) &&

            ((!Confidence. HasValue && !ReportPayloadDescriptor.Confidence. HasValue) ||
              (Confidence. HasValue &&  ReportPayloadDescriptor.Confidence. HasValue    && Confidence. Value.Equals(ReportPayloadDescriptor.Confidence. Value)));

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

                   ReadingType.HasValue
                       ? $", of {ReadingType}"
                       : "",

                   Units.HasValue
                       ? $", in {Units}"
                       : "",

                   Accuracy.HasValue
                       ? $", accuracy: {Accuracy}"
                       : "",

                   Confidence.HasValue
                       ? $", confidence: {Confidence}"
                       : ""

               );

        #endregion

    }

}
