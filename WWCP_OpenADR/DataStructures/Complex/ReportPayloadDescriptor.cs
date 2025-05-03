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
    /// Contextual information used to interpret report payload values.
    /// E.g. a USAGE payload simply contains a usage value, an
    /// associated descriptor provides necessary context such as units and data quality.
    /// </summary>
    public class ReportPayloadDescriptor : APayloadDescriptor
    {

        #region Properties

        /// <summary>
        /// Signifying the type of reading.
        /// </summary>
        [Optional]
        public ReadingType?     ReadingType    { get; }

        /// <summary>
        /// Units of measure.
        /// </summary>
        [Optional]
        public UnitType?        Units          { get; }

        /// <summary>
        /// A quantification of the accuracy of a set of payload values.
        /// </summary>
        [Optional]
        public Single?          Accuracy       { get; }

        /// <summary>
        /// A quantification of the confidence in a set of payload values.
        /// </summary>
        [Optional]
        public PercentageByte?  Confidence     { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new report payload descriptor.
        /// </summary>
        /// <param name="PayloadType">Signifying the nature of values.</param>
        /// <param name="ReadingType">Signifying the type of reading.</param>
        /// <param name="Units">Units of measure.</param>
        /// <param name="Accuracy">A quantification of the accuracy of a set of payload values.</param>
        /// <param name="Confidence">A quantification of the confidence in a set of payload values.</param>
        public ReportPayloadDescriptor(PayloadType      PayloadType,
                                       ReadingType?     ReadingType   = null,
                                       UnitType?        Units         = null,
                                       Single?          Accuracy      = null,
                                       PercentageByte?  Confidence    = null)

            : base(ObjectType.REPORT_PAYLOAD_DESCRIPTOR,
                   PayloadType)

        {

            this.ReadingType  = ReadingType;
            this.Units        = Units;
            this.Accuracy     = Accuracy;
            this.Confidence   = Confidence;

            unchecked
            {

                hashCode = (this.ReadingType?.GetHashCode() ?? 0) * 11 +
                           (this.Units?.      GetHashCode() ?? 0) *  7 +
                           (this.Accuracy?.   GetHashCode() ?? 0) *  5 +
                           (this.Confidence?. GetHashCode() ?? 0) *  3 +
                            base.             GetHashCode();

            }

        }

        #endregion


        #region Documentation

        // reportPayloadDescriptor:
        //   type: object
        //   description: |
        //     Contextual information used to interpret report payload values.
        //     E.g. a USAGE payload simply contains a usage value, an
        //     associated descriptor provides necessary context such as units and data quality.
        //   required:
        //     - payloadType
        //   properties:
        //     objectType:
        //       type: string
        //       description: Used as discriminator.
        //       enum: [REPORT_PAYLOAD_DESCRIPTOR]
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
        //     accuracy:
        //       type: number
        //       format: float
        //       description: A quantification of the accuracy of a set of payload values.
        //       example: 0.0
        //       nullable: true
        //       default: null
        //     confidence:
        //       type: integer
        //       format: int32
        //       minimum: 0
        //       maximum: 100
        //       description: A quantification of the confidence in a set of payload values.
        //       example: 100
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
