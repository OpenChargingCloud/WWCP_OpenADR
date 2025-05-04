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
using org.GraphDefined.Vanaheimr.Hermod.HTTP;

#endregion

namespace cloud.charging.open.protocols.OpenADRv3
{

    /// <summary>
    /// Provides program specific metadata from a Virtual Top Node (VTN)
    /// to a Virtuel End Node (VEN).
    /// </summary>
    public class Program : AOpenADRObject
    {

        #region Properties

        /// <summary>
        /// The short name to uniquely identify the program.
        /// </summary>
        [Mandatory]
        public String                           ProgramName             { get; }

        /// <summary>
        /// The optional long name of program for human readability.
        /// </summary>
        [Optional]
        public String?                          ProgramLongName         { get; }

        /// <summary>
        /// The optional short name of energy retailer providing the program.
        /// </summary>
        [Optional]
        public String?                          RetailerName            { get; }

        /// <summary>
        /// The optional long name of energy retailer for human readability.
        /// </summary>
        [Optional]
        public String?                          RetailerLongName        { get; }

        /// <summary>
        /// The optional program defined categorization.
        /// </summary>
        [Optional]
        public ProgramType?                     ProgramType             { get; }

        /// <summary>
        /// The optional country.
        /// </summary>
        [Optional]
        public Country?                         Country                 { get; }

        /// <summary>
        /// Coding per ISO 3166-2. E.g. state in US.
        /// </summary>
        [Optional]
        public String?                          PrincipalSubdivision    { get; }

        /// <summary>
        /// Number of hours different from UTC for the standard time applicable to the program.
        /// (ISO 8601 duration)
        /// </summary>
        [Optional]
        public TimeSpan?                        TimeZoneOffset          { get; }

        /// <summary>
        /// The temporal span of the program, could be years long.
        /// </summary>
        [Optional]
        public IntervalPeriod?                  IntervalPeriod          { get; }

        /// <summary>
        /// The optional enumeration of program descriptions.
        /// </summary>
        [Optional]
        public IEnumerable<URL>                 ProgramDescriptions     { get; }

        /// <summary>
        /// True if events are fixed once transmitted.
        /// </summary>
        [Optional]
        public Boolean?                         BindingEvents           { get; }

        /// <summary>
        /// True if events have been adapted from a grid event.
        /// </summary>
        [Optional]
        public Boolean?                         LocalPrice              { get; }

        /// <summary>
        /// The enumeration of payload descriptors.
        /// </summary>
        [Optional]
        public IEnumerable<APayloadDescriptor>  PayloadDescriptors      { get; }

        /// <summary>
        /// The enumeration of valuesMap objects.
        /// </summary>
        [Optional]
        public IEnumerable<ValuesMap>           Targets                 { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new program.
        /// </summary>
        /// <param name="ProgramName">A short name to uniquely identify the program.</param>
        /// 
        /// <param name="ProgramLongName">An optional long name of program for human readability.</param>
        /// <param name="RetailerName">An optional short name of energy retailer providing the program.</param>
        /// <param name="RetailerLongName">An optional long name of energy retailer for human readability.</param>
        /// <param name="ProgramType">A program defined categorization.</param>
        /// <param name="Country">An optional country.</param>
        /// <param name="PrincipalSubdivision">A coding per ISO 3166-2. E.g. state in US.</param>
        /// <param name="TimeZoneOffset">The optional number of hours different from UTC for the standard time applicable to the program.</param>
        /// <param name="IntervalPeriod">An optional temporal span of the program, could be years long.</param>
        /// <param name="ProgramDescriptions">An optional enumeration of program descriptions.</param>
        /// <param name="BindingEvents">An optional boolean indicating if events are fixed once transmitted.</param>
        /// <param name="LocalPrice">An optional boolean indicating if events have been adapted from a grid event.</param>
        /// <param name="PayloadDescriptors">An optional enumeration of payload descriptors.</param>
        /// <param name="Targets">An optional enumeration of valuesMap objects.</param>
        /// 
        /// <param name="Id">The optional unique identification of this program.</param>
        /// <param name="Created">The optional date and time when this program was created.</param>
        /// <param name="LastModification">The optional date and time when this program was last modified.</param>
        public Program(String                            ProgramName,

                       String?                           ProgramLongName        = null,
                       String?                           RetailerName           = null,
                       String?                           RetailerLongName       = null,
                       ProgramType?                      ProgramType            = null,
                       Country?                          Country                = null,
                       String?                           PrincipalSubdivision   = null,
                       TimeSpan?                         TimeZoneOffset         = null,
                       IntervalPeriod?                   IntervalPeriod         = null,
                       IEnumerable<URL>?                 ProgramDescriptions    = null,
                       Boolean?                          BindingEvents          = null,
                       Boolean?                          LocalPrice             = null,
                       IEnumerable<APayloadDescriptor>?  PayloadDescriptors     = null,
                       IEnumerable<ValuesMap>?           Targets                = null,

                       Object_Id?                        Id                     = null,
                       DateTimeOffset?                   Created                = null,
                       DateTimeOffset?                   LastModification       = null)

            : base(ObjectType.PROGRAM,
                   Id,
                   Created,
                   LastModification)

        {

            this.ProgramName           = ProgramName;

            this.ProgramLongName       = ProgramLongName;
            this.RetailerName          = RetailerName;
            this.RetailerLongName      = RetailerLongName;
            this.ProgramType           = ProgramType;
            this.Country               = Country;
            this.PrincipalSubdivision  = PrincipalSubdivision;
            this.TimeZoneOffset        = TimeZoneOffset;
            this.IntervalPeriod        = IntervalPeriod;
            this.ProgramDescriptions   = ProgramDescriptions?.Distinct() ?? [];
            this.BindingEvents         = BindingEvents;
            this.LocalPrice            = LocalPrice;
            this.PayloadDescriptors    = PayloadDescriptors?. Distinct() ?? [];
            this.Targets               = Targets?.            Distinct() ?? [];

            unchecked
            {

                hashCode = this.ProgramName.          GetHashCode()       * 47 +

                          (this.ProgramLongName?.     GetHashCode() ?? 0) * 43 +
                          (this.RetailerName?.        GetHashCode() ?? 0) * 41 +
                          (this.RetailerLongName?.    GetHashCode() ?? 0) * 37 +
                          (this.ProgramType?.         GetHashCode() ?? 0) * 31 +
                          (this.Country?.             GetHashCode() ?? 0) * 29 +
                          (this.PrincipalSubdivision?.GetHashCode() ?? 0) * 23 +
                          (this.TimeZoneOffset?.      GetHashCode() ?? 0) * 19 +
                          (this.IntervalPeriod?.      GetHashCode() ?? 0) * 17 +
                           this.ProgramDescriptions.  CalcHashCode()      * 13 +
                          (this.BindingEvents?.       GetHashCode() ?? 0) * 11 +
                          (this.LocalPrice?.          GetHashCode() ?? 0) *  7 +
                           this.PayloadDescriptors.   CalcHashCode()      *  5 +
                           this.Targets.              CalcHashCode()      *  3 +

                           base.                      GetHashCode();

            }

        }

        #endregion


        #region Documentation

        // program:
        //   type: object
        //   description: Provides program specific metadata from VTN to VEN.
        //   required:
        //     - programName
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
        //       enum: [PROGRAM]
        //       # VTN provisioned on object creation.
        //     programName:
        //       type: string
        //       description: Short name to uniquely identify program.
        //       minLength: 1
        //       maxLength: 128
        //       example: ResTOU
        //     programLongName:
        //       type: string
        //       description: Long name of program for human readability.
        //       example: Residential Time of Use-A
        //       nullable: true
        //       default: null
        //     retailerName:
        //       type: string
        //       description: Short name of energy retailer providing the program.
        //       example: ACME
        //       nullable: true
        //       default: null
        //     retailerLongName:
        //       type: string
        //       description: Long name of energy retailer for human readability.
        //       example: ACME Electric Inc.
        //       nullable: true
        //       default: null
        //     programType:
        //       type: string
        //       description: A program defined categorization.
        //       example: PRICING_TARIFF
        //       nullable: true
        //       default: null
        //     country:
        //       type: string
        //       description: Alpha-2 code per ISO 3166-1.
        //       example: US
        //       nullable: true
        //       default: null
        //     principalSubdivision:
        //       type: string
        //       description: Coding per ISO 3166-2. E.g. state in US.
        //       example: CO
        //       nullable: true
        //       default: null
        //     timeZoneOffset:
        //       $ref: '#/components/schemas/duration'
        //       # Number of hours different from UTC for the standard time applicable to the program.
        //     intervalPeriod:
        //       $ref: '#/components/schemas/intervalPeriod'
        //       # The temporal span of the program, could be years long.
        //     programDescriptions:
        //       type: array
        //       description: A list of programDescriptions
        //       items:
        //         required:
        //           - URL
        //         properties:
        //           URL:
        //             type: string
        //             format: uri
        //             description: A human or machine readable program description
        //             example: www.myCorporation.com/myProgramDescription
        //       nullable: true
        //       default: null
        //     bindingEvents:
        //       type: boolean
        //       description: True if events are fixed once transmitted.
        //       example: false
        //       nullable: true
        //       default: null
        //     localPrice:
        //       type: boolean
        //       description: True if events have been adapted from a grid event.
        //       example: false
        //       nullable: true
        //       default: null
        //     payloadDescriptors:
        //       type: array
        //       description: A list of payloadDescriptors.
        //       items:
        //         anyOf:
        //           - $ref: '#/components/schemas/eventPayloadDescriptor'
        //           - $ref: '#/components/schemas/reportPayloadDescriptor'
        //         discriminator:
        //           propertyName: objectType
        //       nullable: true
        //       default: null
        //     targets:
        //       type: array
        //       description: A list of valuesMap objects.
        //       items:
        //         $ref: '#/components/schemas/valuesMap'
        //       nullable: true
        //       default: null

        #endregion

        #region (static) TryParse(JSON, out Program, CustomProgramParser = null)

        // Note: The following is needed to satisfy pattern matching delegates! Do not refactor it!

        /// <summary>
        /// Try to parse the given JSON representation of a program.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="Program">The parsed program.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParse(JObject                            JSON,
                                       [NotNullWhen(true)]  out Program?  Program,
                                       [NotNullWhen(false)] out String?   ErrorResponse)

            => TryParse(JSON,
                        out Program,
                        out ErrorResponse,
                        null);


        /// <summary>
        /// Try to parse the given JSON representation of a program.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="Program">The parsed program.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="CustomProgramParser">A delegate to parse custom programs.</param>
        public static Boolean TryParse(JObject                                JSON,
                                       [NotNullWhen(true)]  out Program?      Program,
                                       [NotNullWhen(false)] out String?       ErrorResponse,
                                       CustomJObjectParserDelegate<Program>?  CustomProgramParser)
        {

            try
            {

                Program = null;

                #region ProgramName             [mandatory]

                if (!JSON.ParseMandatoryText("programName",
                                             "program name",
                                             out String? programName,
                                             out ErrorResponse))
                {
                    return false;
                }

                #endregion


                #region ProgramLongName         [optional]

                if (JSON.ParseOptionalText("programLongName",
                                           "program long name",
                                           out String? programLongName,
                                           out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region RetailerName            [optional]

                if (JSON.ParseOptionalText("retailerName",
                                           "retailerName",
                                           out String? retailerName,
                                           out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region RetailerLongName        [optional]

                if (JSON.ParseOptionalText("retailerLongName",
                                           "retailer long name",
                                           out String? retailerLongName,
                                           out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region ProgramType             [optional]

                if (JSON.ParseOptional("programType",
                                       "program type",
                                       OpenADRv3.ProgramType.TryParse,
                                       out ProgramType? programType,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region Country                 [optional]

                if (JSON.ParseOptional("country",
                                       "country",
                                       Country.TryParse,
                                       out Country? country,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region PrincipalSubdivision    [optional]

                if (JSON.ParseOptionalText("principalSubdivision",
                                           "principal subdivision",
                                           out String? principalSubdivision,
                                           out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region TimeZoneOffset          [optional]

                if (JSON.ParseOptional("timeZoneOffset",
                                       "time zone offset",
                                       out TimeSpan? timeZoneOffset,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region IntervalPeriod          [optional]

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

                #region ProgramDescriptions     [optional]

                if (JSON.ParseOptionalHashSet("programDescriptions",
                                              "interval period",
                                              URL.TryParse,
                                              out HashSet<URL> programDescriptions,
                                              out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region BindingEvents           [optional]

                if (JSON.ParseOptional("bindingEvents",
                                       "binding events",
                                       out Boolean? bindingEvents,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region LocalPrice              [optional]

                if (JSON.ParseOptional("localPrice",
                                       "local price",
                                       out Boolean? localPrice,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region PayloadDescriptors      [optional]

                var payloadDescriptors = new List<APayloadDescriptor>();

                if (JSON["payloadDescriptors"] is JArray payloadDescriptorJSONArray)
                {
                    foreach (var payloadDescriptorJSONToken in payloadDescriptorJSONArray)
                    {
                        if (payloadDescriptorJSONToken is JObject payloadDescriptor)
                        {
                            switch (payloadDescriptor["objectType"]?.Value<String>())
                            {

                                case "EVENT_PAYLOAD_DESCRIPTOR":
                                    if (EventPayloadDescriptor.TryParse(payloadDescriptor,
                                                                        out var eventPayloadDescriptor,
                                                                        out var errorResponse1))
                                        payloadDescriptors.Add(eventPayloadDescriptor);
                                    else
                                    {
                                        ErrorResponse = "The given JSON representation of a payload descriptor is invalid: " + errorResponse1;
                                        return false;
                                    }
                                    break;

                                case "REPORT_PAYLOAD_DESCRIPTOR":
                                    if (ReportPayloadDescriptor.TryParse(payloadDescriptor,
                                                                         out var reportPayloadDescriptor,
                                                                         out var errorResponse2))
                                        payloadDescriptors.Add(reportPayloadDescriptor);
                                    else
                                    {
                                        ErrorResponse = "The given JSON representation of a payload descriptor is invalid: " + errorResponse2;
                                        return false;
                                    }
                                    break;

                            }
                        }
                        else
                        {
                            ErrorResponse = "The given JSON representation of a payload descriptor is invalid!";
                            return false;
                        }
                    }
                }

                #endregion

                #region Targets                 [optional]

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


                #region Id                      [optional]

                if (JSON.ParseOptional("id",
                                       "randomize start",
                                       Object_Id.TryParse,
                                       out Object_Id? id,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region Created                 [optional]

                if (JSON.ParseOptional("createdDateTime",
                                       "randomize start",
                                       out DateTimeOffset? created,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region LastModification        [optional]

                if (JSON.ParseOptional("modificationDateTime",
                                       "randomize start",
                                       out DateTimeOffset? lastModification,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion


                Program = new Program(

                              programName,

                              programLongName,
                              retailerName,
                              retailerLongName,
                              programType,
                              country,
                              principalSubdivision,
                              timeZoneOffset,
                              intervalPeriod,
                              programDescriptions,
                              bindingEvents,
                              localPrice,
                              payloadDescriptors,
                              targets,

                              id,
                              created,
                              lastModification

                          );

                if (CustomProgramParser is not null)
                    Program = CustomProgramParser(JSON,
                                                  Program);

                return true;

            }
            catch (Exception e)
            {
                Program        = default;
                ErrorResponse  = "The given JSON representation of a program is invalid: " + e.Message;
                return false;
            }

        }

        #endregion

        #region ToJSON(CustomProgramSerializer = null, CustomIntervalPeriodSerializer = null, ...)

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        /// <param name="CustomProgramSerializer">A delegate to serialize custom programs.</param>
        /// <param name="CustomIntervalPeriodSerializer">A delegate to serialize custom interval periods.</param>
        /// <param name="CustomEventPayloadDescriptorSerializer">A delegate to serialize custom EventPayloadDescriptor objects.</param>
        /// <param name="CustomReportPayloadDescriptorSerializer">A delegate to serialize custom ReportPayloadDescriptor objects.</param>
        /// <param name="CustomValuesMapSerializer">A delegate to serialize custom values maps.</param>
        public JObject ToJSON(CustomJObjectSerializerDelegate<Program>?                  CustomProgramSerializer                   = null,
                              CustomJObjectSerializerDelegate<IntervalPeriod>?           CustomIntervalPeriodSerializer            = null,
                              CustomJObjectSerializerDelegate<EventPayloadDescriptor>?   CustomEventPayloadDescriptorSerializer    = null,
                              CustomJObjectSerializerDelegate<ReportPayloadDescriptor>?  CustomReportPayloadDescriptorSerializer   = null,
                              CustomJObjectSerializerDelegate<ValuesMap>?                CustomValuesMapSerializer                 = null)
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

                                 new JProperty("objectType",             ObjectType.            ToString()),

                                 new JProperty("programName",            ProgramName),

                           ProgramLongName      is not null
                               ? new JProperty("programLongName",        ProgramLongName)
                               : null,

                           RetailerName         is not null
                               ? new JProperty("retailerName",           RetailerName)
                               : null,

                           RetailerLongName     is not null
                               ? new JProperty("retailerLongName",       RetailerLongName)
                               : null,

                           ProgramType          is not null
                               ? new JProperty("programType",            ProgramType)
                               : null,

                           Country              is not null
                               ? new JProperty("country",                Country.Alpha2Code)
                               : null,

                           PrincipalSubdivision is not null
                               ? new JProperty("principalSubdivision",   PrincipalSubdivision)
                               : null,

                           TimeZoneOffset.HasValue
                               ? new JProperty("timeZoneOffset",         TimeZoneOffset.ToISO8601())
                               : null,

                           IntervalPeriod       is not null
                               ? new JProperty("intervalPeriod",         IntervalPeriod.ToJSON(CustomIntervalPeriodSerializer))
                               : null,

                           ProgramDescriptions.Any()
                               ? new JProperty("programDescriptions",    new JArray(ProgramDescriptions.Select(url => url.ToString())))
                               : null,

                           BindingEvents.HasValue
                               ? new JProperty("bindingEvents",          BindingEvents.Value)
                               : null,

                           LocalPrice.   HasValue
                               ? new JProperty("localPrice",             LocalPrice.   Value)
                               : null,

                           PayloadDescriptors.Any()
                               ? new JProperty("payloadDescriptors",     new JArray(PayloadDescriptors.Select(aPayloadDescriptor => {

                                     if (aPayloadDescriptor is EventPayloadDescriptor  eventPayloadDescriptor)
                                         return eventPayloadDescriptor. ToJSON(CustomEventPayloadDescriptorSerializer);

                                     if (aPayloadDescriptor is ReportPayloadDescriptor reportPayloadDescriptor)
                                         return reportPayloadDescriptor.ToJSON(CustomReportPayloadDescriptorSerializer);

                                     return aPayloadDescriptor.ToJSON();

                                 })))
                               : null,

                           Targets.Any()
                               ? new JProperty("targets",                 new JArray(Targets.           Select(target             => target.            ToJSON(CustomValuesMapSerializer))))
                               : null

                       );

            return CustomProgramSerializer is not null
                       ? CustomProgramSerializer(this, json)
                       : json;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this program.
        /// </summary>
        public Program Clone()

            => new (

                   ProgramName.          CloneString(),

                   ProgramLongName?.     CloneString(),
                   RetailerName?.        CloneString(),
                   RetailerLongName?.    CloneString(),
                   ProgramType?.         Clone(),
                   Country?.             Clone(),
                   PrincipalSubdivision?.CloneString(),
                   TimeZoneOffset,
                   IntervalPeriod?.      Clone(),
                   ProgramDescriptions.Select(programDescription => programDescription.Clone()),
                   BindingEvents,
                   LocalPrice,
                   PayloadDescriptors. Select(payloadDescriptor  => payloadDescriptor. Clone()),
                   Targets.            Select(taraget            => taraget.           Clone()),

                   Id?.                  Clone(),
                   Created,
                   LastModification

                );

        #endregion


        #region Operator overloading

        #region Operator == (Program1, Program2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Program1">A program.</param>
        /// <param name="Program2">Another program.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Program? Program1,
                                           Program? Program2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(Program1, Program2))
                return true;

            // If one is null, but not both, return false.
            if (Program1 is null || Program2 is null)
                return false;

            return Program1.Equals(Program2);

        }

        #endregion

        #region Operator != (Program1, Program2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Program1">A program.</param>
        /// <param name="Program2">Another program.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Program? Program1,
                                           Program? Program2)

            => !(Program1 == Program2);

        #endregion

        #endregion

        #region IEquatable<Program> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two programs for equality.
        /// </summary>
        /// <param name="Object">A program to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is Program program &&
                   Equals(program);

        #endregion

        #region Equals(Program)

        /// <summary>
        /// Compares two programs for equality.
        /// </summary>
        /// <param name="Program">A program to compare with.</param>
        public Boolean Equals(Program? Program)

            => Program is not null &&

               ProgramName.         Equals        (Program.ProgramName) &&

             ((Id                   is null     && Program.Id                   is null) ||
              (Id                   is not null && Program.Id                   is not null && Id.                  Equals(Program.Id))) &&

             ((Created              is null     && Program.Created              is null) ||
              (Created              is not null && Program.Created              is not null && Created.             Equals(Program.Created))) &&

             ((LastModification     is null     && Program.LastModification     is null) ||
              (LastModification     is not null && Program.LastModification     is not null && LastModification.    Equals(Program.LastModification))) &&


             ((ProgramLongName      is null     && Program.ProgramLongName      is null) ||
              (ProgramLongName      is not null && Program.ProgramLongName      is not null && ProgramLongName.     Equals(Program.ProgramLongName))) &&

             ((RetailerName         is null     && Program.RetailerName         is null) ||
              (RetailerName         is not null && Program.RetailerName         is not null && RetailerName.        Equals(Program.RetailerName))) &&

             ((RetailerLongName     is null     && Program.RetailerLongName     is null) ||
              (RetailerLongName     is not null && Program.RetailerLongName     is not null && RetailerLongName.    Equals(Program.RetailerLongName))) &&

            ((!ProgramType.         HasValue    && !Program.ProgramType.        HasValue) ||
              (ProgramType.         HasValue    &&  Program.ProgramType.        HasValue && ProgramType.  Value.    Equals(Program.ProgramType.Value))) &&

             ((Country              is null     && Program.Country              is null) ||
              (Country              is not null && Program.Country              is not null && Country.             Equals(Program.Country))) &&

             ((PrincipalSubdivision is null     && Program.PrincipalSubdivision is null) ||
              (PrincipalSubdivision is not null && Program.PrincipalSubdivision is not null && PrincipalSubdivision.Equals(Program.PrincipalSubdivision))) &&

            ((!TimeZoneOffset.      HasValue    && !Program.TimeZoneOffset.     HasValue) ||
              (TimeZoneOffset.      HasValue    &&  Program.TimeZoneOffset.     HasValue && TimeZoneOffset.Value.   Equals(Program.TimeZoneOffset.Value))) &&

             ((IntervalPeriod       is null     && Program.IntervalPeriod       is null) ||
              (IntervalPeriod       is not null && Program.IntervalPeriod       is not null && IntervalPeriod.      Equals(Program.IntervalPeriod))) &&

             ((BindingEvents        is null     && Program.BindingEvents        is null) ||
              (BindingEvents        is not null && Program.BindingEvents        is not null && BindingEvents.       Equals(Program.BindingEvents))) &&

             ((LocalPrice           is null     && Program.LocalPrice           is null) ||
              (LocalPrice           is not null && Program.LocalPrice           is not null && LocalPrice.          Equals(Program.LocalPrice))) &&

               ProgramDescriptions.Order().SequenceEqual(Program.ProgramDescriptions.Order()) &&
               PayloadDescriptors. Order().SequenceEqual(Program.PayloadDescriptors. Order()) &&
               Targets.            Order().SequenceEqual(Program.Targets.            Order());

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

                   $"'{ProgramName}' /  '{RetailerName}'",

                   Targets.Any()
                       ? $" at '{Targets.AggregateWith(", ")}'"
                       : ""

               );

        #endregion

    }

}
