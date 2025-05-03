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

    //public sealed record Event(

    //    [property: JsonPropertyName("programID")] String ProgramId,
    //    [property: JsonPropertyName("eventName")] String EventName,
    //    [property: JsonPropertyName("priority")] int Priority,

    //    [property: JsonPropertyName("targets")] IReadOnlyList<Target>? Targets,
    //    [property: JsonPropertyName("reportDescriptors")] IReadOnlyList<ReportDescriptor>? ReportDescriptors,
    //    [property: JsonPropertyName("payloadDescriptors")] IReadOnlyList<EventPayloadDescriptor> PayloadDescriptors,
    //    [property: JsonPropertyName("intervalPeriod")] IntervalPeriod IntervalPeriod,
    //    [property: JsonPropertyName("intervals")] IReadOnlyList<Interval> Intervals
    //) : IOpenADRObject;


    /// <summary>
    /// Event object to communicate a Demand Response request to VEN.
    /// If intervalPeriod is present, sets default start time and duration of intervals.
    /// </summary>
    public class Event : AOpenADRObject
    {

        #region Properties

        /// <summary>
        /// The identification of the program this subscription is associated with.
        /// </summary>
        [Mandatory]
        public Object_Id                            ProgramId             { get; }

        /// <summary>
        /// The optional user defined event name to be used for debugging
        /// or within the user interface.
        /// </summary>
        [Optional]
        public String?                              EventName             { get; }

        /// <summary>
        /// The optional relative priority of the event.
        /// A lower number is a higher priority.
        /// </summary>
        [Optional]
        public UInt32?                              Priority              { get; }

        /// <summary>
        /// The optional enumeration of valuesMap objects.
        /// </summary>
        [Optional]
        public IEnumerable<ValuesMap>               Targets               { get; }

        /// <summary>
        /// The optional enumeration of reportDescriptor objects used to request reports from a Virtual End Node.
        /// </summary>
        [Optional]
        public IEnumerable<ReportDescriptor>        ReportDescriptors     { get; }

        /// <summary>
        /// The optional enumeration of payload descriptor objects.
        /// </summary>
        [Optional]
        public IEnumerable<EventPayloadDescriptor>  PayloadDescriptors    { get; }

        /// <summary>
        /// The optional definition of default start and durations of intervals.
        /// </summary>
        [Optional]
        public IntervalPeriod?                      IntervalPeriod        { get; }

        /// <summary>
        /// The enumeration of intervals.
        /// </summary>
        [Mandatory]
        public IEnumerable<Interval>                Intervals             { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new event.
        /// </summary>
        /// <param name="ProgramId">The identification of the program this event is associated with.</param>
        /// <param name="Intervals">The enumeration of intervals.</param>
        /// <param name="EventName">The optional user defined event name to be used for debugging or within the user interface.</param>
        /// <param name="Priority">The optional relative priority of the event. A lower number is a higher priority.</param>
        /// <param name="Targets">An optional enumeration of valuesMap objects.</param>
        /// <param name="ReportDescriptors">An optional enumeration of reportDescriptor objects used to request reports from a Virtual End Node.</param>
        /// <param name="PayloadDescriptors">An optional enumeration of payload descriptor objects.</param>
        /// <param name="IntervalPeriod">An optional definition of default start and durations of intervals.</param>
        /// 
        /// <param name="Id">The optional unique identification of this event.</param>
        /// <param name="Created">The optional date and time when this event was created.</param>
        /// <param name="LastModification">The optional date and time when this event was last modified.</param>
        public Event(Object_Id                             ProgramId,
                     IEnumerable<Interval>                 Intervals,
                     String?                               EventName            = null,
                     UInt32?                               Priority             = null,
                     IEnumerable<ValuesMap>?               Targets              = null,
                     IEnumerable<ReportDescriptor>?        ReportDescriptors    = null,
                     IEnumerable<EventPayloadDescriptor>?  PayloadDescriptors   = null,
                     IntervalPeriod?                       IntervalPeriod       = null,

                     Object_Id?                            Id                   = null,
                     DateTimeOffset?                       Created              = null,
                     DateTimeOffset?                       LastModification     = null)

            : base(ObjectType.EVENT,
                   Id,
                   Created,
                   LastModification)

        {

            this.ProgramId           = ProgramId;
            this.Intervals           = Intervals?.         Distinct() ?? [];
            this.EventName           = EventName;
            this.Priority            = Priority;
            this.Targets             = Targets?.           Distinct() ?? [];
            this.ReportDescriptors   = ReportDescriptors?. Distinct() ?? [];
            this.PayloadDescriptors  = PayloadDescriptors?.Distinct() ?? [];
            this.IntervalPeriod      = IntervalPeriod;

            unchecked
            {

                hashCode = this.ProgramId.         GetHashCode()        * 19 +
                           this.Intervals.         CalcHashCode()       * 17 +
                          (this.EventName?.        GetHashCode()  ?? 0) * 13 +
                          (this.Priority?.         GetHashCode()  ?? 0) * 11 +
                           this.Targets.           CalcHashCode()       *  7 +
                           this.ReportDescriptors. CalcHashCode()       *  5 +
                           this.PayloadDescriptors.CalcHashCode()       *  3 +
                           this.IntervalPeriod?.   GetHashCode()  ?? 0;

            }

        }

        #endregion


        #region Documentation

        // event:
        //   type: object
        //   description: |
        //     Event object to communicate a Demand Response request to VEN.
        //     If intervalPeriod is present, sets default start time and duration of intervals.
        //   required:
        //     - programID
        //     - intervals
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
        //       enum: [EVENT]
        //       # VTN provisioned on object creation.
        //
        //     programID:
        //       $ref: '#/components/schemas/objectID'
        //       # ID attribute of program object this event is associated with.
        //     eventName:
        //       type: string
        //       description: User defined string for use in debugging or User Interface.
        //       example: price event 11-18-2022
        //       nullable: true
        //       default: null
        //     priority:
        //       type: integer
        //       minimum: 0
        //       description: Relative priority of event. A lower number is a higher priority.
        //       example: 0
        //       nullable: true
        //       default: null
        //     targets:
        //       type: array
        //       description: A list of valuesMap objects.
        //       items:
        //         $ref: '#/components/schemas/valuesMap'
        //       nullable: true
        //       default: null
        //     reportDescriptors:
        //       type: array
        //       description: A list of reportDescriptor objects. Used to request reports from VEN.
        //       items:
        //         $ref: '#/components/schemas/reportDescriptor'
        //       nullable: true
        //       default: null
        //     payloadDescriptors:
        //       type: array
        //       description: A list of payloadDescriptor objects.
        //       items:
        //         $ref: '#/components/schemas/eventPayloadDescriptor'
        //       nullable: true
        //       default: null
        //     intervalPeriod:
        //       $ref: '#/components/schemas/intervalPeriod'
        //       # Defines default start and durations of intervals.
        //     intervals:
        //       type: array
        //       description: A list of interval objects.
        //       items:
        //         $ref: '#/components/schemas/interval'

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
