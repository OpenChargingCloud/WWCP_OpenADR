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
    /// Contextual information used to interpret event valuesMap values.
    /// E.g. a PRICE payload simply contains a price value, an
    /// associated descriptor provides necessary context such as units and currency.
    /// </summary>
    public class EventPayloadDescriptor : APayloadDescriptor
    {

        #region Properties

        /// <summary>
        /// Units of measure.
        /// </summary>
        [Optional]
        public UnitType?  Units       { get; }

        /// <summary>
        /// Currency of price payload.
        /// </summary>
        [Optional]
        public Currency?  Currency    { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new event payload descriptor.
        /// </summary>
        /// <param name="PayloadType">Signifying the nature of values.</param>
        /// <param name="Units">Units of measure.</param>
        /// <param name="Currency">Currency of price payload.</param>
        public EventPayloadDescriptor(PayloadType  PayloadType,
                                      UnitType?    Units      = null,
                                      Currency?    Currency   = null)

            : base(ObjectType.EVENT_PAYLOAD_DESCRIPTOR,
                   PayloadType)

        {

            this.Units     = Units;
            this.Currency  = Currency;

            unchecked
            {

                hashCode = (this.Units?.   GetHashCode() ?? 0) * 5 +
                           (this.Currency?.GetHashCode() ?? 0) * 3 +
                            base.          GetHashCode();

            }

        }

        #endregion


        #region Documentation

        // eventPayloadDescriptor:
        //   type: object
        //   description: |
        //     Contextual information used to interpret event valuesMap values.
        //     E.g. a PRICE payload simply contains a price value, an
        //     associated descriptor provides necessary context such as units and currency.
        //   required:
        //     - payloadType
        //   properties:
        //     objectType:
        //       type: string
        //       description: Used as discriminator.
        //       enum: [EVENT_PAYLOAD_DESCRIPTOR]
        //     payloadType:
        //       type: string
        //       description: Enumerated or private string signifying the nature of values.
        //       minLength: 1
        //       maxLength: 128
        //       example: PRICE
        //     units:
        //       type: string
        //       description: Units of measure.
        //       example: KWH
        //       nullable: true
        //       default: null
        //     currency:
        //       type: string
        //       description: Currency of price payload.
        //       example: USD
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
