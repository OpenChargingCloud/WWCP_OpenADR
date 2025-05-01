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
    /// An object defining a temporal window and a list of valuesMaps.
    /// if interval present may set temporal aspects of interval or override event.interval.
    /// </summary>
    public class Interval : IEquatable<Interval>

    {

        #region Properties

        /// <summary>
        /// A random client generated number assigned an interval object.
        /// Not a sequence number.
        /// </summary>
        [Mandatory]
        public Interval_Id               Id                { get; }

        /// <summary>
        /// A list of valuesMap objects.
        /// </summary>
        [Mandatory]
        public IReadOnlyList<ValuesMap>  Payloads          { get; }

        /// <summary>
        /// The optional definition of the start and duration of the interval.
        /// </summary>
        [Optional]
        public IntervalPeriod?           IntervalPeriod    { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new interval.
        /// </summary>
        /// <param name="Id">A random client generated number assigned an interval object. Not a sequence number.</param>
        /// <param name="Payloads">A list of valuesMap objects.</param>
        /// <param name="IntervalPeriod">An optional definition of the start and duration of the interval.</param>
        public Interval(Interval_Id               Id,
                        IReadOnlyList<ValuesMap>  Payloads,
                        IntervalPeriod?           IntervalPeriod   = null)
        {

            this.Id              = Id;
            this.Payloads        = Payloads;
            this.IntervalPeriod  = IntervalPeriod;

            unchecked
            {
                hashCode = this.Id.             GetHashCode()  * 5 +
                           this.Payloads.       CalcHashCode() * 3 +
                          (this.IntervalPeriod?.GetHashCode() ?? 0);
            }

        }

        #endregion


        #region Documentation

        // interval:
        //   type: object
        //   description: |
        //     An object defining a temporal window and a list of valuesMaps.
        //     if interval present may set temporal aspects of interval or override event.interval.
        //   required:
        //     - id
        //     - payloads
        //   properties:
        //     id:
        //       type: integer
        //       format: int32
        //       description: A client generated number assigned an interval object. Not a sequence number.
        //       example: 0
        //     interval:
        //       $ref: '#/components/schemas/interval'
        //       # Defines start and duration of interval.
        //     payloads:
        //       type: array
        //       description: A list of valuesMap objects.
        //       items:
        //         $ref: '#/components/schemas/valuesMap'

        #endregion

        #region (static) TryParse(JSON, out Interval, CustomIntervalParser = null)

        // Note: The following is needed to satisfy pattern matching delegates! Do not refactor it!

        /// <summary>
        /// Try to parse the given JSON representation of an interval.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="Interval">The parsed interval period.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParse(JObject                             JSON,
                                       [NotNullWhen(true)]  out Interval?  Interval,
                                       [NotNullWhen(false)] out String?    ErrorResponse)

            => TryParse(JSON,
                        out Interval,
                        out ErrorResponse,
                        null);


        /// <summary>
        /// Try to parse the given JSON representation of an interval.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="Interval">The parsed interval period.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="CustomIntervalParser">A delegate to parse custom interval periods.</param>
        public static Boolean TryParse(JObject                                 JSON,
                                       [NotNullWhen(true)]  out Interval?      Interval,
                                       [NotNullWhen(false)] out String?        ErrorResponse,
                                       CustomJObjectParserDelegate<Interval>?  CustomIntervalParser)
        {

            try
            {

                Interval = null;

                #region Id                [mandatory]

                if (!JSON.ParseMandatory("id",
                                         "interval identification",
                                         Interval_Id.TryParse,
                                         out Interval_Id id,
                                         out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region Payloads          [mandatory]

                if (!JSON.ParseMandatoryList("payloads",
                                             "payloads",
                                             ValuesMap.TryParse,
                                             out List<ValuesMap> payloads,
                                             out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region IntervalPeriod    [optional]

                if (JSON.ParseOptionalJSON("interval",
                                           "interval period",
                                           IntervalPeriod.TryParse,
                                           out IntervalPeriod? interval,
                                           out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion


                Interval = new Interval(
                               id,
                               payloads,
                               interval
                           );

                if (CustomIntervalParser is not null)
                    Interval = CustomIntervalParser(JSON,
                                                    Interval);

                return true;

            }
            catch (Exception e)
            {
                Interval       = default;
                ErrorResponse  = "The given JSON representation of an interval is invalid: " + e.Message;
                return false;
            }

        }

        #endregion

        #region ToJSON(CustomIntervalSerializer = null, CustomIntervalPeriodSerializer = null)

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        /// <param name="CustomIntervalSerializer">A delegate to serialize custom intervals.</param>
        /// <param name="CustomIntervalPeriodSerializer">A delegate to serialize custom interval periods.</param>
        public JObject ToJSON(CustomJObjectSerializerDelegate<Interval>?        CustomIntervalSerializer         = null,
                              CustomJObjectSerializerDelegate<IntervalPeriod>?  CustomIntervalPeriodSerializer   = null)
        {

            var json = JSONObject.Create(

                                 new JProperty("id",               Id.Value),
                                 new JProperty("payloads",         new JArray(Payloads)),

                           IntervalPeriod is not null
                               ? new JProperty("interval",   IntervalPeriod.ToJSON(CustomIntervalPeriodSerializer))
                               : null

                       );

            return CustomIntervalSerializer is not null
                       ? CustomIntervalSerializer(this, json)
                       : json;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this interval.
        /// </summary>
        public Interval Clone()

            => new (Id,
                    [.. Payloads],
                    IntervalPeriod?.Clone());

        #endregion


        #region Operator overloading

        #region Operator == (Interval1, Interval2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Interval1">An interval.</param>
        /// <param name="Interval2">Another interval period.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Interval? Interval1,
                                           Interval? Interval2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(Interval1, Interval2))
                return true;

            // If one is null, but not both, return false.
            if (Interval1 is null || Interval2 is null)
                return false;

            return Interval1.Equals(Interval2);

        }

        #endregion

        #region Operator != (Interval1, Interval2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Interval1">An interval.</param>
        /// <param name="Interval2">Another interval period.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Interval? Interval1,
                                           Interval? Interval2)

            => !(Interval1 == Interval2);

        #endregion

        #endregion

        #region IEquatable<Interval> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two interval periods for equality.
        /// </summary>
        /// <param name="Object">An interval to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is Interval interval &&
                   Equals(interval);

        #endregion

        #region Equals(Interval)

        /// <summary>
        /// Compares two interval periods for equality.
        /// </summary>
        /// <param name="Interval">An interval to compare with.</param>
        public Boolean Equals(Interval? Interval)

            => Interval is not null &&

               Id.      Equals       (Interval.Id)       &&
               Payloads.SequenceEqual(Interval.Payloads) &&

             ((IntervalPeriod is null     && Interval.IntervalPeriod is null    ) ||
              (IntervalPeriod is not null && Interval.IntervalPeriod is not null && IntervalPeriod.Equals(Interval.IntervalPeriod)));

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

                   $"{Id}: {Payloads.AggregateWith(", ")}",

                   IntervalPeriod is not null
                       ? $", starting {IntervalPeriod.Start}"
                       : "",

                   IntervalPeriod is not null && IntervalPeriod.Duration.HasValue
                       ? $" for {IntervalPeriod.Duration.Value}"
                       : ""

               );

        #endregion

    }

}

