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
    /// The abstract base of all OpenADR top-level objects.
    /// </summary>
    /// <param name="ObjectType">The type of this OpenADR object.</param>
    /// <param name="Id">The optional unique identification of this OpenADR object.</param>
    /// <param name="Created">The optional date and time when this OpenADR object was created.</param>
    /// <param name="LastModification">The optional date and time when this OpenADR object was last modified.</param>

    public abstract class AOpenADRObject(ObjectType       ObjectType,
                                         Object_Id?       Id                 = null,
                                         DateTimeOffset?  Created            = null,
                                         DateTimeOffset?  LastModification   = null)
    {

        #region Properties

        /// <summary>
        /// The type of this OpenADR object.
        /// </summary>
        [Mandatory]
        public ObjectType       ObjectType          { get; } = ObjectType;

        /// <summary>
        /// The optional unique identification of this OpenADR object.
        /// </summary>
        [Optional]
        public Object_Id?       Id                  { get; } = Id;

        /// <summary>
        /// The optional date and time when this OpenADR object was created.
        /// </summary>
        [Optional]
        public DateTimeOffset?  Created             { get; } = Created;

        /// <summary>
        /// The optional date and time when this OpenADR object was last modified.
        /// </summary>
        [Optional]
        public DateTimeOffset?  LastModification    { get; } = LastModification;

        #endregion


        #region CommonInfo

        /// <summary>
        /// Return a text representation of this object.
        /// </summary>
        public String[] CommonInfo

            => [

                   Id.HasValue
                       ? $"Id: {Id.Value}"
                       : "",

                   Created.HasValue
                       ? $"created: {Created.Value.ToISO8601()}"
                       : "",

                   LastModification.HasValue
                       ? $"last modification: {LastModification.Value.ToISO8601()}"
                       : ""

               ];

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this object.
        /// </summary>
        public override String ToString()

            => CommonInfo.AggregateWith(", ");

        #endregion

    }

}
