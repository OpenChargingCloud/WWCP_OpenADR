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
    /// Report data associated with a resource.
    /// </summary>
    public class ResourceReport
    {

        #region Properties

        /// <summary>
        /// The user generated identifier.
        /// A value of AGGREGATED_REPORT indicates an aggregation of more that one resource's data
        /// </summary>
        [Mandatory]
        public String                 ResourceName      { get; }

        /// <summary>
        /// The enumeration of intervals.
        /// </summary>
        [Mandatory]
        public IEnumerable<Interval>  Intervals         { get;  }

        /// <summary>
        /// The optional definition of default start and durations of intervals.
        /// </summary>
        [Optional]
        public IntervalPeriod?        IntervalPeriod    { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new resource report.
        /// </summary>
        /// <param name="ResourceName">The user generated identifier. A value of AGGREGATED_REPORT indicates an aggregation of more that one resource's data</param>
        /// <param name="Intervals">The enumeration of intervals.</param>
        /// <param name="IntervalPeriod">An optional definition of default start and durations of intervals.</param>
        public ResourceReport(String                 ResourceName,
                              IEnumerable<Interval>  Intervals,
                              IntervalPeriod?        IntervalPeriod   = null)
        {

            this.ResourceName    = ResourceName;
            this.Intervals       = Intervals.Distinct();
            this.IntervalPeriod  = IntervalPeriod;

            unchecked
            {

                hashCode = this.ResourceName.   GetHashCode()  * 5 +
                           this.Intervals.      CalcHashCode() * 3 +
                           this.IntervalPeriod?.GetHashCode() ?? 0;

            }

        }

        #endregion


        #region Documentation

        // resource:
        //   type: object
        //   description: Report data associated with a resource.
        //   required:
        //     - resourceName
        //     - intervals
        //   properties:
        //     resourceName:
        //       type: string
        //       minLength: 1
        //       maxLength: 128
        //       description: User generated identifier. A value of AGGREGATED_REPORT indicates an aggregation of more that one resource's data
        //       example: RESOURCE-999
        //     intervalPeriod:
        //       $ref: '#/components/schemas/intervalPeriod'
        //       # Defines default start and durations of intervals.
        //     intervals:
        //       type: array
        //       description: A list of interval objects.
        //       items:
        //         $ref: '#/components/schemas/interval'

        #endregion

        #region (static) TryParse(JSON, out ResourceReport, CustomResourceReportParser = null)

        // Note: The following is needed to satisfy pattern matching delegates! Do not refactor it!

        /// <summary>
        /// Try to parse the given JSON representation of a resource report.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="ResourceReport">The parsed resource report.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParse(JObject                                   JSON,
                                       [NotNullWhen(true)]  out ResourceReport?  ResourceReport,
                                       [NotNullWhen(false)] out String?          ErrorResponse)

            => TryParse(JSON,
                        out ResourceReport,
                        out ErrorResponse,
                        null);


        /// <summary>
        /// Try to parse the given JSON representation of a resource report.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="ResourceReport">The parsed resource report.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="CustomResourceReportParser">A delegate to parse custom resource reports.</param>
        public static Boolean TryParse(JObject                                       JSON,
                                       [NotNullWhen(true)]  out ResourceReport?      ResourceReport,
                                       [NotNullWhen(false)] out String?              ErrorResponse,
                                       CustomJObjectParserDelegate<ResourceReport>?  CustomResourceReportParser)
        {

            try
            {

                ResourceReport = null;

                #region ResourceName      [mandatory]

                if (!JSON.ParseMandatoryText("resourceName",
                                             "resource name",
                                             out String? resourceName,
                                             out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region IntervalPeriod    [optional]

                if (JSON.ParseOptionalJSON("intervalPeriod",
                                           "interval period",
                                           IntervalPeriod.TryParse,
                                           out IntervalPeriod? intervalPeriod,
                                           out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region Intervals         [mandatory]

                if (!JSON.ParseMandatoryHashSet("intervals",
                                                "intervals",
                                                Interval.TryParse,
                                                out HashSet<Interval> intervals,
                                                out ErrorResponse))
                {
                    return false;
                }

                #endregion


                ResourceReport = new ResourceReport(
                                     resourceName,
                                     intervals,
                                     intervalPeriod
                                 );

                if (CustomResourceReportParser is not null)
                    ResourceReport = CustomResourceReportParser(JSON,
                                                                ResourceReport);

                return true;

            }
            catch (Exception e)
            {
                ResourceReport  = default;
                ErrorResponse   = "The given JSON representation of a resource report is invalid: " + e.Message;
                return false;
            }

        }

        #endregion

        #region ToJSON(CustomResourceReportSerializer = null, CustomIntervalPeriodSerializer = null, ...)

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        /// <param name="CustomResourceReportSerializer">A delegate to serialize custom resource reports.</param>
        /// <param name="CustomIntervalPeriodSerializer">A delegate to serialize custom interval periods.</param>
        /// <param name="CustomIntervalSerializer">A delegate to serialize custom intervals.</param>
        public JObject ToJSON(CustomJObjectSerializerDelegate<ResourceReport>?  CustomResourceReportSerializer   = null,
                              CustomJObjectSerializerDelegate<IntervalPeriod>?  CustomIntervalPeriodSerializer   = null,
                              CustomJObjectSerializerDelegate<Interval>?        CustomIntervalSerializer         = null)
        {

            var json = JSONObject.Create(

                                 new JProperty("resourceName",     ResourceName),

                           IntervalPeriod is not null
                               ? new JProperty("intervalPeriod",   IntervalPeriod.ToJSON(CustomIntervalPeriodSerializer))
                               : null,

                                 new JProperty("intervals",        new JArray(Intervals.Select(interval => interval.ToJSON(CustomIntervalSerializer,
                                                                                                                           CustomIntervalPeriodSerializer))))

                       );

            return CustomResourceReportSerializer is not null
                       ? CustomResourceReportSerializer(this, json)
                       : json;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this resource report.
        /// </summary>
        public ResourceReport Clone()

            => new (
                   ResourceName.   CloneString(),
                   Intervals.Select(interval => interval.Clone()),
                   IntervalPeriod?.Clone()
                );

        #endregion


        #region Operator overloading

        #region Operator == (ResourceReport1, ResourceReport2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ResourceReport1">A resource report.</param>
        /// <param name="ResourceReport2">Another resource report.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ResourceReport? ResourceReport1,
                                           ResourceReport? ResourceReport2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(ResourceReport1, ResourceReport2))
                return true;

            // If one is null, but not both, return false.
            if (ResourceReport1 is null || ResourceReport2 is null)
                return false;

            return ResourceReport1.Equals(ResourceReport2);

        }

        #endregion

        #region Operator != (ResourceReport1, ResourceReport2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ResourceReport1">A resource report.</param>
        /// <param name="ResourceReport2">Another resource report.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ResourceReport? ResourceReport1,
                                           ResourceReport? ResourceReport2)

            => !(ResourceReport1 == ResourceReport2);

        #endregion

        #endregion

        #region IEquatable<ResourceReport> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two resource reports for equality.
        /// </summary>
        /// <param name="Object">A resource report to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is ResourceReport resourceReport &&
                   Equals(resourceReport);

        #endregion

        #region Equals(ResourceReport)

        /// <summary>
        /// Compares two resource reports for equality.
        /// </summary>
        /// <param name="ResourceReport">A resource report to compare with.</param>
        public Boolean Equals(ResourceReport? ResourceReport)

            => ResourceReport is not null &&

               ResourceName.     Equals       (ResourceReport.ResourceName)      &&
               Intervals.Order().SequenceEqual(ResourceReport.Intervals.Order()) &&

             ((IntervalPeriod is null     && ResourceReport.IntervalPeriod is null) ||
              (IntervalPeriod is not null && ResourceReport.IntervalPeriod is not null && IntervalPeriod.Equals(ResourceReport.IntervalPeriod)));

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

                   $"'{ResourceName}' {Intervals.AggregateWith(", ")}",

                   IntervalPeriod is not null
                       ? $" interval period: '{IntervalPeriod}'"
                       : ""

               );

        #endregion

    }

}
