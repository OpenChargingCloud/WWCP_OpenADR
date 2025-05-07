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
    /// The report object is used to inform about the status of a resource or group of resources.
    /// </summary>
    public class Report : AOpenADRObject<Report_Id>
    {

        #region Properties

        /// <summary>
        /// The identification of the program this subscription is associated with.
        /// </summary>
        [Mandatory]
        public Program_Id                            ProgramId             { get; }

        /// <summary>
        /// The identification of the event this subscription is associated with.
        /// </summary>
        [Mandatory]
        public Event_Id                              EventId               { get; }

        /// <summary>
        /// The name of the client. An user generated identifier which may also be a VEN identifier provisioned out-of-band.
        /// </summary>
        [Mandatory]
        public String                                ClientName            { get; }

        /// <summary>
        /// The optional user defined report name to be used for debugging
        /// or within the user interface.
        /// </summary>
        [Optional]
        public String?                               ReportName            { get; }

        /// <summary>
        /// The optional enumeration of report payload descriptors.
        /// </summary>
        [Optional]
        public IEnumerable<ReportPayloadDescriptor>  PayloadDescriptors    { get; }

        /// <summary>
        /// The enumeration of objects containing report data for a set of resources.
        /// </summary>
        [Mandatory]
        public IEnumerable<ResourceReport>           Resources             { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new report.
        /// </summary>
        /// <param name="ProgramId">The identification of the program this subscription is associated with.</param>
        /// <param name="EventId">The identification of the event this subscription is associated with.</param>
        /// <param name="ClientName">The name of the client. An user generated identifier which may also be a VEN identifier provisioned out-of-band.</param>
        /// <param name="Resources">The enumeration of objects containing report data for a set of resources.</param>
        /// <param name="ReportName">The optional user defined report name to be used for debugging or within the user interface.</param>
        /// <param name="PayloadDescriptors">An optional enumeration of report payload descriptors.</param>
        /// 
        /// <param name="Id">The optional unique identification of this report.</param>
        /// <param name="Created">The optional date and time when this report was created.</param>
        /// <param name="LastModification">The optional date and time when this report was last modified.</param>
        public Report(Program_Id                             ProgramId,
                      Event_Id                               EventId,
                      String                                 ClientName,
                      IEnumerable<ResourceReport>            Resources,
                      String?                                ReportName           = null,
                      IEnumerable<ReportPayloadDescriptor>?  PayloadDescriptors   = null,

                      Report_Id?                             Id                   = null,
                      DateTimeOffset?                        Created              = null,
                      DateTimeOffset?                        LastModification     = null)

            : base(ObjectType.REPORT,
                   Id,
                   Created,
                   LastModification)

        {

            this.ProgramId           = ProgramId;
            this.EventId             = EventId;
            this.ClientName          = ClientName;
            this.Resources           = Resources?.         Distinct() ?? [];
            this.ReportName          = ReportName;
            this.PayloadDescriptors  = PayloadDescriptors?.Distinct() ?? [];

            unchecked
            {

                hashCode = this.ProgramId.         GetHashCode()       * 13 +
                           this.EventId.           GetHashCode()       * 11 +
                           this.ClientName.        GetHashCode()       *  7 +
                           this.Resources.         CalcHashCode()      *  5 +
                          (this.ReportName?.       GetHashCode() ?? 0) *  3 +
                           this.PayloadDescriptors.CalcHashCode();

            }

        }

        #endregion


        #region Documentation

        // report:
        //   type: object
        //   description: report object.
        //   required:
        //     - programID
        //     - eventID
        //     - clientName
        //     - resources
        //   properties:
        //     id:
        //       $ref: '#/components/schemas/objectID'
        //       # VTN provisioned on object creation.
        //     createdDateTime:
        //       $ref: '#/components/schemas/dateTime'
        //       #  VTN provisioned on object creation.
        //     modificationDateTime:
        //       $ref: '#/components/schemas/dateTime'
        //       #  VTN provisioned on object modification.
        //     objectType:
        //       type: string
        //       description: Used as discriminator
        //       enum: [REPORT]
        //       # VTN provisioned on object creation.
        //
        //     programID:
        //       $ref: '#/components/schemas/objectID'
        //       # ID attribute of program object this report is associated with.
        //     eventID:
        //       $ref: '#/components/schemas/objectID'
        //       # ID attribute of event object this report is associated with.
        //     clientName:
        //       type: string
        //       description: User generated identifier; may be VEN ID provisioned out-of-band.
        //       minLength: 1
        //       maxLength: 128
        //       example: VEN-999
        //     reportName:
        //       type: string
        //       description: User defined string for use in debugging or User Interface.
        //       example: Battery_usage_04112023
        //       nullable: true
        //       default: null
        //     payloadDescriptors:
        //       type: array
        //       description: A list of reportPayloadDescriptors.
        //       items:
        //         $ref: '#/components/schemas/reportPayloadDescriptor'
        //       nullable: true
        //       default: null
        //       # An optional list of objects that provide context to payload types.
        //     resources:
        //       type: array
        //       description: A list of objects containing report data for a set of resources.
        //       items:
        //         type: object
        //         description: Report data associated with a resource.
        //         required:
        //           - resourceName
        //           - intervals
        //         properties:
        //           resourceName:
        //             type: string
        //             minLength: 1
        //             maxLength: 128
        //             description: User generated identifier. A value of AGGREGATED_REPORT indicates an aggregation of more that one resource's data
        //             example: RESOURCE-999
        //           intervalPeriod:
        //             $ref: '#/components/schemas/intervalPeriod'
        //             # Defines default start and durations of intervals.
        //           intervals:
        //             type: array
        //             description: A list of interval objects.
        //             items:
        //               $ref: '#/components/schemas/interval'

        #endregion

        #region (static) TryParse(JSON, out Report, CustomReportParser = null)

        // Note: The following is needed to satisfy pattern matching delegates! Do not refactor it!

        /// <summary>
        /// Try to parse the given JSON representation of a report.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="Report">The parsed report.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParse(JObject                           JSON,
                                       [NotNullWhen(true)]  out Report?  Report,
                                       [NotNullWhen(false)] out String?  ErrorResponse)

            => TryParse(JSON,
                        out Report,
                        out ErrorResponse,
                        null);


        /// <summary>
        /// Try to parse the given JSON representation of a report.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="Report">The parsed report.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="CustomReportParser">A delegate to parse custom reports.</param>
        public static Boolean TryParse(JObject                               JSON,
                                       [NotNullWhen(true)]  out Report?      Report,
                                       [NotNullWhen(false)] out String?      ErrorResponse,
                                       CustomJObjectParserDelegate<Report>?  CustomReportParser)
        {

            try
            {

                Report = null;

                #region ProgramId             [mandatory]

                if (!JSON.ParseMandatory("programID",
                                         "program identification",
                                         Program_Id.TryParse,
                                         out Program_Id programId,
                                         out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region EventId               [mandatory]

                if (!JSON.ParseMandatory("eventID",
                                         "event identification",
                                         Event_Id.TryParse,
                                         out Event_Id eventId,
                                         out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region ClientName            [mandatory]

                if (!JSON.ParseMandatoryText("clientName",
                                             "client name",
                                             out String? clientName,
                                             out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region Resources             [mandatory]

                if (!JSON.ParseMandatoryHashSet("resources",
                                                "resources",
                                                ResourceReport.TryParse,
                                                out HashSet<ResourceReport> resources,
                                                out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region ReportName            [optional]

                if (JSON.ParseOptionalText("reportName",
                                           "report name",
                                           out String? reportName,
                                           out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region PayloadDescriptors    [optional]

                if (JSON.ParseOptionalHashSet("payloadDescriptors",
                                              "payload descriptors",
                                              ReportPayloadDescriptor.TryParse,
                                              out HashSet<ReportPayloadDescriptor> payloadDescriptors,
                                              out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion


                #region Id                    [optional]

                if (JSON.ParseOptional("id",
                                       "randomize start",
                                       Report_Id.TryParse,
                                       out Report_Id? id,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region Created               [optional]

                if (JSON.ParseOptional("createdDateTime",
                                       "randomize start",
                                       out DateTimeOffset? created,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region LastModification      [optional]

                if (JSON.ParseOptional("modificationDateTime",
                                       "randomize start",
                                       out DateTimeOffset? lastModification,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion


                Report = new Report(

                             programId,
                             eventId,
                             clientName,
                             resources,
                             reportName,
                             payloadDescriptors,

                             id,
                             created,
                             lastModification

                         );

                if (CustomReportParser is not null)
                    Report = CustomReportParser(JSON,
                                                Report);

                return true;

            }
            catch (Exception e)
            {
                Report         = default;
                ErrorResponse  = "The given JSON representation of a report is invalid: " + e.Message;
                return false;
            }

        }

        #endregion

        #region ToJSON(CustomReportSerializer = null, CustomObjectOperationSerializer = null, ...)

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        /// <param name="CustomReportSerializer">A delegate to serialize custom reports.</param>
        /// <param name="CustomResourceReportSerializer">A delegate to serialize custom resource reports.</param>
        /// <param name="CustomIntervalPeriodSerializer">A delegate to serialize custom interval periods.</param>
        /// <param name="CustomIntervalSerializer">A delegate to serialize custom intervals.</param>
        /// <param name="CustomReportPayloadDescriptorSerializer">A delegate to serialize custom ReportPayloadDescriptor objects.</param>
        public JObject ToJSON(CustomJObjectSerializerDelegate<Report>?                   CustomReportSerializer                    = null,
                              CustomJObjectSerializerDelegate<ResourceReport>?           CustomResourceReportSerializer            = null,
                              CustomJObjectSerializerDelegate<IntervalPeriod>?           CustomIntervalPeriodSerializer            = null,
                              CustomJObjectSerializerDelegate<Interval>?                 CustomIntervalSerializer                  = null,
                              CustomJObjectSerializerDelegate<ReportPayloadDescriptor>?  CustomReportPayloadDescriptorSerializer   = null)
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


                                 new JProperty("programID",              ProgramId.             ToString()),
                                 new JProperty("eventID",                EventId.               ToString()),
                                 new JProperty("clientName",             ClientName),

                                 new JProperty("resources",              new JArray(Resources.         Select(resource                => resource.               ToJSON(CustomResourceReportSerializer,
                                                                                                                                                                        CustomIntervalPeriodSerializer,
                                                                                                                                                                        CustomIntervalSerializer)))),

                           ReportName.IsNotNullOrEmpty()
                               ? new JProperty("reportName",             ReportName)
                               : null,

                           PayloadDescriptors.Any()
                               ? new JProperty("payloadDescriptors",     new JArray(PayloadDescriptors.Select(reportPayloadDescriptor => reportPayloadDescriptor.ToJSON(CustomReportPayloadDescriptorSerializer))))
                               : null

                       );

            return CustomReportSerializer is not null
                       ? CustomReportSerializer(this, json)
                       : json;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this report.
        /// </summary>
        public override Report Clone()

            => new (

                   ProgramId.  Clone(),
                   EventId.    Clone(),
                   ClientName. CloneString(),
                   Resources.         Select(resource          => resource.         Clone()),
                   ReportName?.CloneString(),
                   PayloadDescriptors.Select(payloadDescriptor => payloadDescriptor.Clone()),

                   Id?.        Clone(),
                   Created,
                   LastModification

                );

        #endregion


        #region (static) FillMetadata(Report)

        /// <summary>
        /// Fill the metadata of this report.
        /// </summary>
        /// <param name="Report">A report.</param>
        public static Report FillMetadata(Report Report)
        {

            var now = Timestamp.Now;

            return new (

                       Report.ProgramId,
                       Report.EventId,
                       Report.ClientName,
                       Report.Resources,
                       Report.ReportName,
                       Report.PayloadDescriptors,

                       Report.Id               ?? Report_Id.NewRandom,
                       Report.Created          ?? now,
                       Report.LastModification ?? now

                   );

        }

        #endregion


        #region Operator overloading

        #region Operator == (Report1, Report2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Report1">A report.</param>
        /// <param name="Report2">Another report.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Report? Report1,
                                           Report? Report2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(Report1, Report2))
                return true;

            // If one is null, but not both, return false.
            if (Report1 is null || Report2 is null)
                return false;

            return Report1.Equals(Report2);

        }

        #endregion

        #region Operator != (Report1, Report2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Report1">A report.</param>
        /// <param name="Report2">Another report.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Report? Report1,
                                           Report? Report2)

            => !(Report1 == Report2);

        #endregion

        #endregion

        #region IEquatable<Report> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two reports for equality.
        /// </summary>
        /// <param name="Object">A report to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is Report report &&
                   Equals(report);

        #endregion

        #region Equals(Report)

        /// <summary>
        /// Compares two reports for equality.
        /// </summary>
        /// <param name="Report">A report to compare with.</param>
        public Boolean Equals(Report? Report)

            => Report is not null &&

               ProgramId. Equals(Report.ProgramId)  &&
               EventId.   Equals(Report.EventId)    &&
               ClientName.Equals(Report.ClientName) &&

            ((!Id.              HasValue    && !Report.Id.              HasValue) ||
              (Id.              HasValue    &&  Report.Id.              HasValue && Id.              Value.Equals(Report.Id.              Value))) &&

            ((!Created.         HasValue    && !Report.Created.         HasValue) ||
              (Created.         HasValue    &&  Report.Created.         HasValue && Created.         Value.Equals(Report.Created.         Value))) &&

            ((!LastModification.HasValue    && !Report.LastModification.HasValue) ||
              (LastModification.HasValue    &&  Report.LastModification.HasValue && LastModification.Value.Equals(Report.LastModification.Value))) &&

            ((!Id.              HasValue    && !Report.Id.              HasValue) ||
              (Id.              HasValue    &&  Report.Id.              HasValue && Id.              Value.Equals(Report.Id.              Value))) &&

             ((ReportName       is null     &&  Report.ReportName       is null) ||
              (ReportName       is not null &&  Report.ReportName       is not null && ReportName.         Equals(Report.ReportName)))             &&

               Resources.         Order().SequenceEqual(Report.Resources.         Order()) &&
               PayloadDescriptors.Order().SequenceEqual(Report.PayloadDescriptors.Order());

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

                   ReportName.IsNotNullOrEmpty()
                       ? $"'{ReportName}', "
                       : "",

                   $"'{ProgramId}' /  '{EventId}'  /  '{ClientName}' for '{Resources.AggregateWith(", ")}'",

                   PayloadDescriptors.Any()
                       ? $" payload: '{PayloadDescriptors.AggregateWith(", ")}'"
                       : ""

               );

        #endregion

    }

}
