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

using Newtonsoft.Json.Linq;
using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.OpenADRv3
{

    /// <summary>
    /// The common interface of all OpenADR top-level objects.
    /// </summary>
    public interface IOpenADRObject
    {

        /// <summary>
        /// The type of this OpenADR object.
        /// </summary>
        [Mandatory]
        ObjectType       ObjectType          { get; }

        /// <summary>
        /// The optional unique identification of this OpenADR object.
        /// </summary>
        [Optional]
        Object_Id?       Id                  { get; }

        /// <summary>
        /// The optional date and time when this OpenADR object was created.
        /// </summary>
        [Optional]
        DateTimeOffset?  Created             { get; }

        /// <summary>
        /// The optional date and time when this OpenADR object was last modified.
        /// </summary>
        [Optional]
        DateTimeOffset?  LastModification    { get; }

        JObject ToJSON();

        IOpenADRObject Clone();

    }


    /// <summary>
    /// The common interface of all OpenADR top-level objects.
    /// </summary>
    public interface IOpenADRObject<T> : IOpenADRObject
        where T : struct
    {

        /// <summary>
        /// The optional unique identification of this OpenADR object.
        /// </summary>
        [Optional]
        new T?           Id                  { get; }


        new IOpenADRObject<T> Clone();

    }

}
