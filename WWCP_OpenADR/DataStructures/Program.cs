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
