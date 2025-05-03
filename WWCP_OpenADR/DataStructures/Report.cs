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
    public class Report : AOpenADRObject
    {

        #region Properties

        /// <summary>
        /// The identification of the program this subscription is associated with.
        /// </summary>
        [Mandatory]
        public Object_Id                             ProgramId             { get; }

        /// <summary>
        /// The identification of the event this subscription is associated with.
        /// </summary>
        [Mandatory]
        public Object_Id                             EventId               { get; }

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
        public Report(Object_Id                              ProgramId,
                      Object_Id                              EventId,
                      String                                 ClientName,
                      IEnumerable<ResourceReport>            Resources,
                      String?                                ReportName           = null,
                      IEnumerable<ReportPayloadDescriptor>?  PayloadDescriptors   = null,

                      Object_Id?                             Id                   = null,
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


        #region (override) GetHashCode()

        private readonly Int32 hashCode;

        /// <summary>
        /// Return the hash code of this object.
        /// </summary>
        public override Int32 GetHashCode()
            => hashCode;

        #endregion


    }

}
