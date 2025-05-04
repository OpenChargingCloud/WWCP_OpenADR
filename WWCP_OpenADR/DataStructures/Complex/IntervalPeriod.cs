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
    /// Defines temporal aspects of intervals.
    /// A duration of default PT0S indicates instantaneous or infinity, depending on payloadType.
    /// A randomizeStart of default null indicates no randomization.
    /// </summary>
    public class IntervalPeriod : IEquatable<IntervalPeriod>

    {

        #region Properties

        /// <summary>
        /// The start time of an interval or set of intervals,
        /// e.g. "2023-06-15T12:58:08.000Z".
        /// </summary>
        [Mandatory]
        public DateTimeOffset  Start             { get; }

        /// <summary>
        /// The optional duration of an interval or set of intervals,
        /// e.g. "PT1H".
        /// </summary>
        [Optional]
        public TimeSpan?       Duration          { get; }

        /// <summary>
        /// Indicates an optional randomization time that may be applied to start,
        /// e.g. "PT5M".
        /// </summary>
        [Optional]
        public TimeSpan?       RandomizeStart    { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new interval period.
        /// </summary>
        /// <param name="Start">The start time of an interval or set of intervals, e.g. "2023-06-15T12:58:08.000Z".</param>
        /// <param name="Duration">The optional duration of an interval or set of intervals, e.g. "PT1H".</param>
        /// <param name="RandomizeStart">Indicates an optional randomization time that may be applied to start, e.g. "PT5M".</param>
        public IntervalPeriod(DateTimeOffset  Start,
                              TimeSpan?       Duration         = null,
                              TimeSpan?       RandomizeStart   = null)
        {

            this.Start           = Start;
            this.Duration        = Duration;
            this.RandomizeStart  = RandomizeStart;

            unchecked
            {
                hashCode = this.Start.          GetHashCode()       * 5 ^
                          (this.Duration?.      GetHashCode() ?? 0) * 3 ^
                          (this.RandomizeStart?.GetHashCode() ?? 0);
            }

        }

        #endregion


        #region Documentation

        // intervalPeriod:
        //   type: object
        //   description: |
        //     Defines temporal aspects of intervals.
        //     A duration of default PT0S indicates instantaneous or infinity, depending on payloadType.
        //     A randomizeStart of default null indicates no randomization.
        //   required:
        //     - start
        //   properties:
        //     start:
        //       $ref: '#/components/schemas/dateTime'
        //       #  The start time of an interval or set of intervals.
        //     duration:
        //       $ref: '#/components/schemas/duration'
        //       #  The duration of an interval or set of intervals.
        //     randomizeStart:
        //       $ref: '#/components/schemas/duration'
        //       #  Indicates a randomization time that may be applied to start.

        #endregion

        #region (static) TryParse(JSON, out IntervalPeriod, CustomIntervalPeriodParser = null)

        // Note: The following is needed to satisfy pattern matching delegates! Do not refactor it!

        /// <summary>
        /// Try to parse the given JSON representation of an interval period.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="IntervalPeriod">The parsed interval period.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParse(JObject                                   JSON,
                                       [NotNullWhen(true)]  out IntervalPeriod?  IntervalPeriod,
                                       [NotNullWhen(false)] out String?          ErrorResponse)

            => TryParse(JSON,
                        out IntervalPeriod,
                        out ErrorResponse,
                        null);


        /// <summary>
        /// Try to parse the given JSON representation of an interval period.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="IntervalPeriod">The parsed interval period.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="CustomIntervalPeriodParser">A delegate to parse custom interval periods.</param>
        public static Boolean TryParse(JObject                                       JSON,
                                       [NotNullWhen(true)]  out IntervalPeriod?      IntervalPeriod,
                                       [NotNullWhen(false)] out String?              ErrorResponse,
                                       CustomJObjectParserDelegate<IntervalPeriod>?  CustomIntervalPeriodParser)
        {

            try
            {

                IntervalPeriod = null;

                #region Start             [mandatory]

                if (!JSON.ParseMandatory("start",
                                         "start",
                                         out DateTimeOffset start,
                                         out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region Duration          [optional]

                if (JSON.ParseOptional("duration",
                                       "duration",
                                       TimeSpanExtensions.TryParseISO8601,
                                       out TimeSpan? duration,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region RandomizeStart    [optional]

                if (JSON.ParseOptional("randomizeStart",
                                       "randomize start",
                                       TimeSpanExtensions.TryParseISO8601,
                                       out TimeSpan? randomizeStart,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion


                IntervalPeriod = new IntervalPeriod(
                                     start,
                                     duration,
                                     randomizeStart
                                 );

                if (CustomIntervalPeriodParser is not null)
                    IntervalPeriod = CustomIntervalPeriodParser(JSON,
                                                                IntervalPeriod);

                return true;

            }
            catch (Exception e)
            {
                IntervalPeriod  = default;
                ErrorResponse    = "The given JSON representation of an interval period is invalid: " + e.Message;
                return false;
            }

        }

        #endregion

        #region ToJSON(CustomIntervalPeriodSerializer = null)

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        /// <param name="CustomIntervalPeriodSerializer">A delegate to serialize custom interval periods.</param>
        public JObject ToJSON(CustomJObjectSerializerDelegate<IntervalPeriod>? CustomIntervalPeriodSerializer = null)
        {

            var json = JSONObject.Create(

                                 new JProperty("start",            Start.         ToISO8601()),

                           Duration.      HasValue
                               ? new JProperty("duration",         Duration.      ToISO8601())
                               : null,

                           RandomizeStart.HasValue
                               ? new JProperty("randomizeStart",   RandomizeStart.ToISO8601())
                               : null

                       );

            return CustomIntervalPeriodSerializer is not null
                       ? CustomIntervalPeriodSerializer(this, json)
                       : json;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this interval period.
        /// </summary>
        public IntervalPeriod Clone()

            => new (Start,
                    Duration,
                    RandomizeStart);

        #endregion


        #region Operator overloading

        #region Operator == (IntervalPeriod1, IntervalPeriod2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IntervalPeriod1">An interval period.</param>
        /// <param name="IntervalPeriod2">Another interval period.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (IntervalPeriod? IntervalPeriod1,
                                           IntervalPeriod? IntervalPeriod2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(IntervalPeriod1, IntervalPeriod2))
                return true;

            // If one is null, but not both, return false.
            if (IntervalPeriod1 is null || IntervalPeriod2 is null)
                return false;

            return IntervalPeriod1.Equals(IntervalPeriod2);

        }

        #endregion

        #region Operator != (IntervalPeriod1, IntervalPeriod2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IntervalPeriod1">An interval period.</param>
        /// <param name="IntervalPeriod2">Another interval period.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (IntervalPeriod? IntervalPeriod1,
                                           IntervalPeriod? IntervalPeriod2)

            => !(IntervalPeriod1 == IntervalPeriod2);

        #endregion

        #endregion

        #region IEquatable<IntervalPeriod> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two interval periods for equality.
        /// </summary>
        /// <param name="Object">An interval period to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is IntervalPeriod intervalPeriod &&
                   Equals(intervalPeriod);

        #endregion

        #region Equals(IntervalPeriod)

        /// <summary>
        /// Compares two interval periods for equality.
        /// </summary>
        /// <param name="IntervalPeriod">An interval period to compare with.</param>
        public Boolean Equals(IntervalPeriod? IntervalPeriod)

            => IntervalPeriod is not null &&

               Start.Equals(IntervalPeriod.Start) &&

               ((!Duration.      HasValue && !IntervalPeriod.Duration.      HasValue) ||
                 (Duration.      HasValue &&  IntervalPeriod.Duration.      HasValue && Duration.      Value.Equals(IntervalPeriod.Duration.      Value))) &&

               ((!RandomizeStart.HasValue && !IntervalPeriod.RandomizeStart.HasValue) ||
                 (RandomizeStart.HasValue &&  IntervalPeriod.RandomizeStart.HasValue && RandomizeStart.Value.Equals(IntervalPeriod.RandomizeStart.Value)));

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

                   Start.ToISO8601(),

                   Duration.HasValue
                       ? $" for {Duration.Value.ToISO8601()}"
                       : "",

                   RandomizeStart.HasValue
                       ? $" with a randomization of {RandomizeStart.Value.ToISO8601()}"
                       : ""

               );

        #endregion

    }

}
