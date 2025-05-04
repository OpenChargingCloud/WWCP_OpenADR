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
    /// Virtual Top Node (VTN) generated object included in request to subscription callback URL.
    /// </summary>
    public class Notification
    {

        #region Properties

        /// <summary>
        /// The object type.
        /// </summary>
        [Mandatory]
        public ObjectType              ObjectType    { get; }

        /// <summary>
        /// The operation on an object that triggered the notification.
        /// </summary>
        [Mandatory]
        public Operation               Operation     { get; }

        /// <summary>
        /// The object that is the subject of the notification.
        /// </summary>
        [Mandatory]
        public AOpenADRObject          Object        { get; }

        /// <summary>
        /// The enumeration of valuesMap objects.
        /// </summary>
        [Optional]
        public IEnumerable<ValuesMap>  Targets       { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new notification.
        /// </summary>
        /// <param name="ObjectType">An object type.</param>
        /// <param name="Operation">An operation on an object that triggered the notification.</param>
        /// <param name="Object">An object that is the subject of the notification.</param>
        /// <param name="Targets">An enumeration of valuesMap objects.</param>
        public Notification(ObjectType               ObjectType,
                            Operation                Operation,
                            AOpenADRObject           Object,
                            IEnumerable<ValuesMap>?  Targets   = null)
        {

            this.ObjectType  = ObjectType;
            this.Operation   = Operation;
            this.Object      = Object;
            this.Targets     = Targets?.Distinct() ?? [];

            unchecked
            {

                hashCode = this.ObjectType.GetHashCode() * 7 ^
                           this.Operation. GetHashCode() * 5 ^
                           this.Object.    GetHashCode() * 3 ^
                           this.Targets.   CalcHashCode();

            }

        }

        #endregion


        #region Documentation

        // notification:
        //   type: object
        //   description: |
        //     VTN generated object included in request to subscription callbackUrl.
        //   required:
        //     - objectType
        //     - operation
        //     - object
        //   properties:
        //     objectType:
        //       $ref: '#/components/schemas/objectTypes'
        //     operation:
        //       type: string
        //       description: the operation on on object that triggered the notification.
        //       example: POST
        //       enum: [GET, POST, PUT, DELETE]
        //     object:
        //       type: object
        //       description: the object that is the subject of the notification.
        //       example: {}
        //       oneOf:
        //         - $ref: '#/components/schemas/program'
        //         - $ref: '#/components/schemas/report'
        //         - $ref: '#/components/schemas/event'
        //         - $ref: '#/components/schemas/subscription'
        //         - $ref: '#/components/schemas/ven'
        //         - $ref: '#/components/schemas/resource'
        //       discriminator:
        //         propertyName: objectType
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
