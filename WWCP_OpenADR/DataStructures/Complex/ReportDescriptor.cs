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
    /// An object that may be used to request a report from a Virtual End Node.
    /// </summary>
    public class ReportDescriptor
    {

        #region Properties

        /// <summary>
        /// Signifying the nature of values.
        /// </summary>
        [Mandatory]
        public PayloadType             PayloadType      { get; }

        /// <summary>
        /// Signifying the type of reading.
        /// </summary>
        [Optional]
        public ReadingType?            ReadingType      { get; }

        /// <summary>
        /// Optional units of measure.
        /// </summary>
        [Optional]
        public UnitType?               Units            { get; }

        /// <summary>
        /// The optional enumeration of valuesMap objects.
        /// </summary>
        [Optional]
        public IEnumerable<ValuesMap>  Targets          { get; }

        /// <summary>
        /// True, if report should aggregate results from all targeted resources.
        /// False, if report includes results for each resource.
        /// </summary>
        public Boolean                 Aggregate        { get; }

        /// <summary>
        /// The interval on which to generate a report.
        /// -1 indicates generate report at end of last interval.
        /// </summary>
        [Optional]
        public Int32                   StartInterval    { get; }

        /// <summary>
        /// The number of intervals to include in a report.
        /// -1 indicates that all intervals are to be included.
        /// </summary>
        [Optional]
        public Int32                   NumIntervals     { get; }

        /// <summary>
        /// True, indicates report on intervals preceding startInterval.
        /// False, indicates report on intervals following startInterval (e.g. forecast).
        /// </summary>
        [Optional]
        public Boolean                 Historical       { get; }

        /// <summary>
        /// Number of intervals that elapse between reports.
        /// -1 indicates same as numIntervals.
        /// </summary>
        [Optional]
        public Int32                   Frequency        { get; }

        /// <summary>
        /// Number of times to repeat report.
        ///  1 indicates generate one report.
        /// -1 indicates repeat indefinitely.
        /// </summary>
        [Optional]
        public Int32                   Repeat           { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new report descriptor.
        /// </summary>
        /// <param name="PayloadType">Signifying the nature of values.</param>
        /// <param name="ReadingType">Signifying the type of reading.</param>
        /// <param name="Units">Optional Units of measure.</param>
        /// <param name="Targets">An optional enumeration of valuesMap objects.</param>
        /// <param name="Aggregate">True, if report should aggregate results from all targeted resources. False, if report includes results for each resource.</param>
        /// <param name="StartInterval">The interval on which to generate a report. -1 indicates generate report at end of last interval.</param>
        /// <param name="NumIntervals">The number of intervals to include in a report. -1 indicates that all intervals are to be included.</param>
        /// <param name="Historical">True, indicates report on intervals preceding startInterval. False, indicates report on intervals following startInterval (e.g. forecast).</param>
        /// <param name="Frequency">Number of intervals that elapse between reports. -1 indicates same as numIntervals.</param>
        /// <param name="Repeat">Number of times to repeat report. 1 indicates generate one report. -1 indicates repeat indefinitely.</param>
        public ReportDescriptor(PayloadType              PayloadType,
                                ReadingType?             ReadingType     = null,
                                UnitType?                Units           = null,
                                IEnumerable<ValuesMap>?  Targets         = null,
                                Boolean?                 Aggregate       = null,
                                Int32?                   StartInterval   = null,
                                Int32?                   NumIntervals    = null,
                                Boolean?                 Historical      = null,
                                Int32?                   Frequency       = null,
                                Int32?                   Repeat          = null)
        {

            this.PayloadType    = PayloadType;
            this.ReadingType    = ReadingType;
            this.Units          = Units;
            this.Targets        = Targets?.Distinct() ?? [];
            this.Aggregate      = Aggregate     ?? false;
            this.StartInterval  = StartInterval ?? -1;
            this.NumIntervals   = NumIntervals  ?? -1;
            this.Historical     = Historical    ?? true;
            this.Frequency      = Frequency     ?? -1;
            this.Repeat         = Repeat        ?? -1;

            unchecked
            {

                hashCode = this.PayloadType.  GetHashCode()       * 27 +
                          (this.ReadingType?. GetHashCode() ?? 0) * 23 +
                          (this.Units?.       GetHashCode() ?? 0) * 19 +
                           this.Targets.      CalcHashCode()      * 17 +
                           this.Aggregate.    GetHashCode()       * 13 +
                           this.StartInterval.GetHashCode()       * 11 +
                           this.NumIntervals. GetHashCode()       *  7 +
                           this.Historical.   GetHashCode()       *  5 +
                           this.Frequency.    GetHashCode()       *  3 +
                           this.Repeat.       GetHashCode();

            }

        }

        #endregion


        #region Documentation

        // reportDescriptor:
        //   type: object
        //   description: |
        //     An object that may be used to request a report from a VEN.
        //   required:
        //     - payloadType
        //   properties:
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
        //     targets:
        //       type: array
        //       description: A list of valuesMap objects.
        //       items:
        //         $ref: '#/components/schemas/valuesMap'
        //       nullable: true
        //       default: null
        //     aggregate:
        //       type: boolean
        //       description: |
        //         True if report should aggregate results from all targeted resources.
        //         False if report includes results for each resource.
        //       example: false
        //       default: false
        //     startInterval:
        //       type: integer
        //       format: int32
        //       description: |
        //         The interval on which to generate a report.
        //         -1 indicates generate report at end of last interval.
        //       example: -1
        //       default: -1
        //     numIntervals:
        //       type: integer
        //       format: int32
        //       description: |
        //         The number of intervals to include in a report.
        //         -1 indicates that all intervals are to be included.
        //       example: -1
        //       default: -1
        //     historical:
        //       type: boolean
        //       description: |
        //         True indicates report on intervals preceding startInterval.
        //         False indicates report on intervals following startInterval (e.g. forecast).
        //       example: true
        //       default: true
        //     frequency:
        //       type: integer
        //       format: int32
        //       description: |
        //         Number of intervals that elapse between reports.
        //         -1 indicates same as numIntervals.
        //       example: -1
        //       default: -1
        //     repeat:
        //       type: integer
        //       format: int32
        //       description: |
        //         Number of times to repeat report.
        //         1 indicates generate one report.
        //         -1 indicates repeat indefinitely.
        //       example: 1
        //       default: 1

        #endregion

        #region (static) TryParse(JSON, out ReportDescriptor, CustomReportDescriptorParser = null)

        // Note: The following is needed to satisfy pattern matching delegates! Do not refactor it!

        /// <summary>
        /// Try to parse the given JSON representation of a report descriptor.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="ReportDescriptor">The parsed report descriptor.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParse(JObject                                     JSON,
                                       [NotNullWhen(true)]  out ReportDescriptor?  ReportDescriptor,
                                       [NotNullWhen(false)] out String?            ErrorResponse)

            => TryParse(JSON,
                        out ReportDescriptor,
                        out ErrorResponse,
                        null);


        /// <summary>
        /// Try to parse the given JSON representation of a report descriptor.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="ReportDescriptor">The parsed report descriptor.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="CustomReportDescriptorParser">A delegate to parse custom report descriptors.</param>
        public static Boolean TryParse(JObject                                         JSON,
                                       [NotNullWhen(true)]  out ReportDescriptor?      ReportDescriptor,
                                       [NotNullWhen(false)] out String?                ErrorResponse,
                                       CustomJObjectParserDelegate<ReportDescriptor>?  CustomReportDescriptorParser)
        {

            try
            {

                ReportDescriptor = null;

                #region PayloadType      [mandatory]

                if (!JSON.ParseMandatory("payloadType",
                                         "payload type",
                                         PayloadType.TryParse,
                                         out PayloadType payloadType,
                                         out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region ReadingType      [optional]

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

                #region Units            [optional]

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

                #region Aggregate        [optional]

                if (JSON.ParseOptional("aggregate",
                                       "aggregate",
                                       out Boolean? aggregate,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region StartInterval    [optional]

                if (JSON.ParseOptional("startInterval",
                                       "start interval",
                                       out Int32? startInterval,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region NumIntervals     [optional]

                if (JSON.ParseOptional("numIntervals",
                                       "num intervals",
                                       out Int32? numIntervals,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region Historical       [optional]

                if (JSON.ParseOptional("historical",
                                       "historical",
                                       out Boolean? historical,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region Frequency        [optional]

                if (JSON.ParseOptional("frequency",
                                       "frequency",
                                       out Int32? frequency,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region Repeat           [optional]

                if (JSON.ParseOptional("repeat",
                                       "repeat",
                                       out Int32? repeat,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion


                ReportDescriptor = new ReportDescriptor(
                                       payloadType,
                                       readingType,
                                       units,
                                       targets,
                                       aggregate,
                                       startInterval,
                                       numIntervals,
                                       historical,
                                       frequency,
                                       repeat
                                   );

                if (CustomReportDescriptorParser is not null)
                    ReportDescriptor = CustomReportDescriptorParser(JSON,
                                                                    ReportDescriptor);

                return true;

            }
            catch (Exception e)
            {
                ReportDescriptor  = default;
                ErrorResponse     = "The given JSON representation of a report descriptor is invalid: " + e.Message;
                return false;
            }

        }

        #endregion

        #region ToJSON(CustomReportDescriptorSerializer = null, CustomValuesMapSerializer = null, ...)

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        /// <param name="CustomReportDescriptorSerializer">A delegate to serialize custom report descriptors.</param>
        /// <param name="CustomValuesMapSerializer">A delegate to serialize custom ValuesMap objects.</param>
        public JObject ToJSON(CustomJObjectSerializerDelegate<ReportDescriptor>?  CustomReportDescriptorSerializer   = null,
                              CustomJObjectSerializerDelegate<ValuesMap>?         CustomValuesMapSerializer          = null)
        {

            var json = JSONObject.Create(

                                 new JProperty("payloadType",     PayloadType),

                           ReadingType.HasValue
                               ? new JProperty("readingType",     ReadingType.Value.ToString())
                               : null,

                           Units.HasValue
                               ? new JProperty("units",           Units.Value.ToString())
                               : null,

                           Targets.Any()
                               ? new JProperty("targets",         new JArray(Targets.Select(target => target.ToJSON(CustomValuesMapSerializer))))
                               : null,

                           Aggregate     != false
                               ? new JProperty("aggregate",       Aggregate)
                               : null,

                           StartInterval != -1
                               ? new JProperty("startInterval",   StartInterval)
                               : null,

                           NumIntervals  != -1
                               ? new JProperty("numIntervals",    NumIntervals)
                               : null,

                           Historical    != true
                               ? new JProperty("historical",      Historical)
                               : null,

                           Frequency     != -1
                               ? new JProperty("frequency",       Frequency)
                               : null,

                           Repeat        != -1
                               ? new JProperty("repeat",          Repeat)
                               : null

                       );

            return CustomReportDescriptorSerializer is not null
                       ? CustomReportDescriptorSerializer(this, json)
                       : json;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this report descriptor.
        /// </summary>
        public ReportDescriptor Clone()

            => new (
                   PayloadType. Clone(),
                   ReadingType?.Clone(),
                   Units?.      Clone(),
                   Targets.Select(target => target.Clone()),
                   Aggregate,
                   StartInterval,
                   NumIntervals,
                   Historical,
                   Frequency,
                   Repeat
                );

        #endregion


        #region Operator overloading

        #region Operator == (ReportDescriptor1, ReportDescriptor2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReportDescriptor1">A report descriptor.</param>
        /// <param name="ReportDescriptor2">Another report descriptor.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ReportDescriptor? ReportDescriptor1,
                                           ReportDescriptor? ReportDescriptor2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(ReportDescriptor1, ReportDescriptor2))
                return true;

            // If one is null, but not both, return false.
            if (ReportDescriptor1 is null || ReportDescriptor2 is null)
                return false;

            return ReportDescriptor1.Equals(ReportDescriptor2);

        }

        #endregion

        #region Operator != (ReportDescriptor1, ReportDescriptor2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReportDescriptor1">A report descriptor.</param>
        /// <param name="ReportDescriptor2">Another report descriptor.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ReportDescriptor? ReportDescriptor1,
                                           ReportDescriptor? ReportDescriptor2)

            => !(ReportDescriptor1 == ReportDescriptor2);

        #endregion

        #endregion

        #region IEquatable<ReportDescriptor> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two report descriptors for equality.
        /// </summary>
        /// <param name="Object">A report descriptor to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is ReportDescriptor reportDescriptor &&
                   Equals(reportDescriptor);

        #endregion

        #region Equals(ReportDescriptor)

        /// <summary>
        /// Compares two report descriptors for equality.
        /// </summary>
        /// <param name="ReportDescriptor">A report descriptor to compare with.</param>
        public Boolean Equals(ReportDescriptor? ReportDescriptor)

            => ReportDescriptor is not null &&

               PayloadType.  Equals(ReportDescriptor.PayloadType)   &&

               Aggregate.    Equals(ReportDescriptor.Aggregate)     &&
               StartInterval.Equals(ReportDescriptor.StartInterval) &&
               NumIntervals. Equals(ReportDescriptor.NumIntervals)  &&
               Historical.   Equals(ReportDescriptor.Historical)    &&
               Frequency.    Equals(ReportDescriptor.Frequency)     &&
               Repeat.       Equals(ReportDescriptor.Repeat)        &&

            ((!ReadingType. HasValue && !ReportDescriptor.ReadingType. HasValue) ||
              (ReadingType. HasValue &&  ReportDescriptor.ReadingType. HasValue && ReadingType. Value.Equals(ReportDescriptor.ReadingType.Value))) &&

            ((!Units.       HasValue && !ReportDescriptor.Units.       HasValue) ||
              (Units.       HasValue &&  ReportDescriptor.Units.       HasValue && Units.       Value.Equals(ReportDescriptor.Units.      Value))) &&

               Targets.Order().SequenceEqual(ReportDescriptor.Targets.Order());

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
                       ? $" of '{ReadingType.Value}'"
                       : "",

                   Units.      HasValue
                       ? $" in '{Units.Value}'"
                       : "",

                   Targets.Any()
                       ? $" target(s): '{Targets.AggregateWith(", ")}'"
                       : ""

               );

        #endregion

    }

}
