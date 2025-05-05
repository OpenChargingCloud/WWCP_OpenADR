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
    /// Event object to communicate a Demand Response request to VEN.
    /// If intervalPeriod is present, sets default start time and duration of intervals.
    /// </summary>
    public class Event : AOpenADRObject<Event_Id>
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

                     Event_Id?                             Id                   = null,
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

        #region (static) TryParse(JSON, out Event, CustomEventParser = null)

        // Note: The following is needed to satisfy pattern matching delegates! Do not refactor it!

        /// <summary>
        /// Try to parse the given JSON representation of an event.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="Event">The parsed event.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParse(JObject                           JSON,
                                       [NotNullWhen(true)]  out Event?   Event,
                                       [NotNullWhen(false)] out String?  ErrorResponse)

            => TryParse(JSON,
                        out Event,
                        out ErrorResponse,
                        null);


        /// <summary>
        /// Try to parse the given JSON representation of an event.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="Event">The parsed event.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="CustomEventParser">A delegate to parse custom events.</param>
        public static Boolean TryParse(JObject                              JSON,
                                       [NotNullWhen(true)]  out Event?      Event,
                                       [NotNullWhen(false)] out String?     ErrorResponse,
                                       CustomJObjectParserDelegate<Event>?  CustomEventParser)
        {

            try
            {

                Event = null;

                #region ProgramId             [mandatory]

                if (!JSON.ParseMandatory("programID",
                                         "program identification",
                                         Object_Id.TryParse,
                                         out Object_Id programId,
                                         out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region Intervals             [mandatory]

                if (!JSON.ParseMandatoryHashSet("intervals",
                                                "intervals",
                                                Interval.TryParse,
                                                out HashSet<Interval> intervals,
                                                out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region EventName             [optional]

                if (JSON.ParseOptionalText("eventName",
                                           "event name",
                                           out String? eventName,
                                           out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region Priority              [optional]

                if (JSON.ParseOptional("priority",
                                       "priority",
                                       out UInt32? priority,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region Targets               [optional]

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

                #region ReportDescriptors     [optional]

                if (JSON.ParseOptionalHashSet("reportDescriptors",
                                              "report descriptors",
                                              ReportDescriptor.TryParse,
                                              out HashSet<ReportDescriptor> reportDescriptors,
                                              out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region PayloadDescriptors    [optional]

                if (JSON.ParseOptionalHashSet("payloadDescriptors",
                                              "payload descriptors",
                                              EventPayloadDescriptor.TryParse,
                                              out HashSet<EventPayloadDescriptor> payloadDescriptors,
                                              out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region IntervalPeriod        [optional]

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


                #region Id                    [optional]

                if (JSON.ParseOptional("id",
                                       "randomize start",
                                       Event_Id.TryParse,
                                       out Event_Id? id,
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


                Event = new Event(

                            programId,
                            intervals,
                            eventName,
                            priority,
                            targets,
                            reportDescriptors,
                            payloadDescriptors,
                            intervalPeriod,

                            id,
                            created,
                            lastModification

                        );

                if (CustomEventParser is not null)
                    Event = CustomEventParser(JSON,
                                              Event);

                return true;

            }
            catch (Exception e)
            {
                Event   = default;
                ErrorResponse  = "The given JSON representation of an event is invalid: " + e.Message;
                return false;
            }

        }

        #endregion

        #region ToJSON(CustomEventSerializer = null, CustomIntervalSerializer = null, ...)

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        /// <param name="CustomEventSerializer">A delegate to serialize custom events.</param>
        /// <param name="CustomIntervalSerializer">A delegate to serialize custom intervals.</param>
        /// <param name="CustomIntervalPeriodSerializer">A delegate to serialize custom interval periods.</param>
        /// <param name="CustomValuesMapSerializer">A delegate to serialize custom values maps.</param>
        /// <param name="CustomReportDescriptorSerializer">A delegate to serialize custom report descriptors.</param>
        /// <param name="CustomEventPayloadDescriptorSerializer">A delegate to serialize custom EventPayloadDescriptor objects.</param>
        public JObject ToJSON(CustomJObjectSerializerDelegate<Event>?                   CustomEventSerializer                    = null,
                              CustomJObjectSerializerDelegate<Interval>?                CustomIntervalSerializer                 = null,
                              CustomJObjectSerializerDelegate<IntervalPeriod>?          CustomIntervalPeriodSerializer           = null,
                              CustomJObjectSerializerDelegate<ValuesMap>?               CustomValuesMapSerializer                = null,
                              CustomJObjectSerializerDelegate<ReportDescriptor>?        CustomReportDescriptorSerializer         = null,
                              CustomJObjectSerializerDelegate<EventPayloadDescriptor>?  CustomEventPayloadDescriptorSerializer   = null)
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


                                 new JProperty("programID",            ProgramId.     ToString()),
                                 new JProperty("intervals",            new JArray(Intervals.         Select(interval               => interval.ToJSON(CustomIntervalSerializer,
                                                                                                                                                      CustomIntervalPeriodSerializer)))),

                            EventName is not null
                               ? new JProperty("eventName",            EventName)
                               : null,

                            Priority.HasValue
                               ? new JProperty("priority",             Priority.Value)
                               : null,

                            Targets.Any()
                               ? new JProperty("targets",              new JArray(Targets.           Select(target                 => target.                ToJSON(CustomValuesMapSerializer))))
                               : null,

                            ReportDescriptors.Any()
                               ? new JProperty("reportDescriptors",    new JArray(ReportDescriptors. Select(reportDescriptor       => reportDescriptor.      ToJSON(CustomReportDescriptorSerializer,
                                                                                                                                                                    CustomValuesMapSerializer))))
                               : null,

                            PayloadDescriptors.Any()
                               ? new JProperty("payloadDescriptors",   new JArray(PayloadDescriptors.Select(eventPayloadDescriptor => eventPayloadDescriptor.ToJSON(CustomEventPayloadDescriptorSerializer))))
                               : null,

                            IntervalPeriod is not null
                               ? new JProperty("intervalPeriod",       IntervalPeriod.ToJSON(CustomIntervalPeriodSerializer))
                               : null

                       );

            return CustomEventSerializer is not null
                       ? CustomEventSerializer(this, json)
                       : json;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this event.
        /// </summary>
        public override Event Clone()

            => new (

                   ProgramId.      Clone(),
                   Intervals.         Select(interval          => interval.         Clone()),
                   EventName?.     CloneString(),
                   Priority,
                   Targets.           Select(taraget           => taraget.          Clone()),
                   ReportDescriptors. Select(reportDescriptor  => reportDescriptor. Clone()),
                   PayloadDescriptors.Select(payloadDescriptor => payloadDescriptor.Clone()),
                   IntervalPeriod?.Clone(),

                   Id?.            Clone(),
                   Created,
                   LastModification

                );

        #endregion


        #region Operator overloading

        #region Operator == (Event1, Event2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Event1">An event.</param>
        /// <param name="Event2">Another event.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Event? Event1,
                                           Event? Event2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(Event1, Event2))
                return true;

            // If one is null, but not both, return false.
            if (Event1 is null || Event2 is null)
                return false;

            return Event1.Equals(Event2);

        }

        #endregion

        #region Operator != (Event1, Event2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Event1">An event.</param>
        /// <param name="Event2">Another event.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Event? Event1,
                                           Event? Event2)

            => !(Event1 == Event2);

        #endregion

        #endregion

        #region IEquatable<Event> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two events for equality.
        /// </summary>
        /// <param name="Object">An event to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is Event @event &&
                   Equals(@event);

        #endregion

        #region Equals(Event)

        /// <summary>
        /// Compares two events for equality.
        /// </summary>
        /// <param name="Event">An event to compare with.</param>
        public Boolean Equals(Event? Event)

            => Event is not null &&

               ProgramId.                 Equals       (Event.ProgramId) &&

            ((!Id.              HasValue    && !Event.Id.              HasValue) ||
              (Id.              HasValue    &&  Event.Id.              HasValue    && Id.              Value.Equals(Event.Id.              Value))) &&

            ((!Created.         HasValue    && !Event.Created.         HasValue) ||
              (Created.         HasValue    &&  Event.Created.         HasValue    && Created.         Value.Equals(Event.Created.         Value))) &&

            ((!LastModification.HasValue    && !Event.LastModification.HasValue) ||
              (LastModification.HasValue    &&  Event.LastModification.HasValue    && LastModification.Value.Equals(Event.LastModification.Value))) &&

             ((EventName        is null     &&  Event.EventName        is null) ||
              (EventName        is not null &&  Event.EventName        is not null && EventName.             Equals(Event.EventName)))              &&

            ((!Priority.        HasValue    && !Event.Priority.        HasValue) ||
              (Priority.        HasValue    &&  Event.Priority.        HasValue    && Priority.        Value.Equals(Event.Priority.        Value))) &&

             ((IntervalPeriod   is null     &&  Event.IntervalPeriod   is null) ||
              (IntervalPeriod   is not null &&  Event.IntervalPeriod   is not null && IntervalPeriod.        Equals(Event.IntervalPeriod)))         &&

               Intervals.         Order().SequenceEqual(Event.Intervals.         Order()) &&
               Targets.           Order().SequenceEqual(Event.Targets.           Order()) &&
               ReportDescriptors. Order().SequenceEqual(Event.ReportDescriptors. Order()) &&
               PayloadDescriptors.Order().SequenceEqual(Event.PayloadDescriptors.Order());

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

                   $"''{ProgramId}'",

                   EventName is not null
                       ? $" for event: '{EventName}'"
                       : "",

                   Targets.Any()
                       ? $" at '{Targets.AggregateWith(", ")}'"
                       : ""

               );

        #endregion


    }

}
