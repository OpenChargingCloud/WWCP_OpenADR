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

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.OpenADRv3
{

    /// <summary>
    /// The abstract base class for all payload descriptors.
    /// </summary>
    public abstract class APayloadDescriptor
    {

        #region Properties

        /// <summary>
        /// The object type.
        /// </summary>
        [Mandatory]
        public ObjectType   ObjectType     { get; }


        /// <summary>
        /// Signifying the nature of values.
        /// </summary>
        [Mandatory]
        public PayloadType  PayloadType    { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new payload descriptor.
        /// </summary>
        /// <param name="ObjectType">An object type.</param>
        /// <param name="PayloadType">Signifying the nature of values.</param>
        public APayloadDescriptor(ObjectType   ObjectType,
                                  PayloadType  PayloadType)
        {

            this.ObjectType   = ObjectType;
            this.PayloadType  = PayloadType;

            unchecked
            {

                hashCode = this.ObjectType. GetHashCode() * 3 +
                           this.PayloadType.GetHashCode();

            }

        }

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
