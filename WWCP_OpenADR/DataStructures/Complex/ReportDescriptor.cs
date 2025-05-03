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
        /// Units of measure.
        /// </summary>
        [Optional]
        public UnitType?               Units            { get; }

        /// <summary>
        /// The enumeration of valuesMap objects.
        /// </summary>
        [Optional]
        public IEnumerable<ValuesMap>  Targets          { get; }

        /// <summary>
        /// True if report should aggregate results from all targeted resources.
        /// False if report includes results for each resource.
        /// </summary>
        public Boolean                 Aggregate        { get; } = false;

        /// <summary>
        /// The interval on which to generate a report.
        /// -1 indicates generate report at end of last interval.
        /// </summary>
        [Optional]
        public Int32                   StartInterval    { get; } = -1;

        /// <summary>
        /// The number of intervals to include in a report.
        /// -1 indicates that all intervals are to be included.
        /// </summary>
        [Optional]
        public Int32                   NumIntervals     { get; } = -1;

        /// <summary>
        /// True indicates report on intervals preceding startInterval.
        /// False indicates report on intervals following startInterval (e.g. forecast).
        /// </summary>
        [Optional]
        public Boolean                 Historical       { get; } = true;

        /// <summary>
        /// Number of intervals that elapse between reports.
        /// -1 indicates same as numIntervals.
        /// </summary>
        [Optional]
        public Int32                   Frequency        { get; } = -1;

        /// <summary>
        /// Number of times to repeat report.
        /// 1 indicates generate one report.
        /// -1 indicates repeat indefinitely.
        /// </summary>
        [Optional]
        public Int32                   Repeat           { get; } = 1;

        #endregion

        #region Constructor(s)

        public ReportDescriptor(PayloadType              PayloadType,
                                ReadingType?             ReadingType     = null,
                                UnitType?                Units           = null,
                                IEnumerable<ValuesMap>?  Targets         = null,
                                Boolean                  Aggregate       = false,
                                Int32                    StartInterval   = -1,
                                Int32                    NumIntervals    = -1,
                                Boolean                  Historical      = true,
                                Int32                    Frequency       = -1,
                                Int32                    Repeat          = 1)
        {

            this.PayloadType    = PayloadType;
            this.ReadingType    = ReadingType;
            this.Units          = Units;
            this.Targets        = Targets?.Distinct() ?? [];
            this.Aggregate      = Aggregate;
            this.StartInterval  = StartInterval;
            this.NumIntervals   = NumIntervals;
            this.Historical     = Historical;
            this.Frequency      = Frequency;
            this.Repeat         = Repeat;

            unchecked
            {

                hashCode = this.PayloadType.  GetHashCode()       * 31 +
                          (this.ReadingType?. GetHashCode() ?? 0) * 29 +
                          (this.Units?.       GetHashCode() ?? 0) * 23 +
                           this.Targets.      CalcHashCode()      * 19 +
                           this.Aggregate.    GetHashCode()       * 17 +
                           this.StartInterval.GetHashCode()       * 13 +
                           this.NumIntervals. GetHashCode()       * 11 +
                           this.Historical.   GetHashCode()       *  7 +
                           this.Frequency.    GetHashCode()       *  5 +
                           this.Repeat.       GetHashCode()       *  3 +
                           this.Targets.      CalcHashCode();

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
