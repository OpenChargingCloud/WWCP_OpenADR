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
    /// Body of POST request to /auth/token.
    /// </summary>
    /// <remarks>
    /// Note snake case per https://www.rfc-editor.org/rfc/rfc6749
    /// </remarks>
    public class ClientCredentialRequest
    {

        #region Properties

        /// <summary>
        /// The OAuth2 grant type. Must be 'client_credentials'
        /// </summary>
        [Mandatory]
        public String   GrantType       { get; } = "client_credentials";

        /// <summary>
        /// The client identification to exchange for HTTP Bearer token.
        /// </summary>
        [Mandatory]
        public String   ClientId        { get; }

        /// <summary>
        /// The client secret to exchange for HTTP Bearer token.
        /// </summary>
        [Mandatory]
        public String   ClientSecret    { get; }

        /// <summary>
        /// The optional application defined HTTP Authentication scope.
        /// </summary>
        [Optional]
        public String?  Scope           { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new ClientCredential request.
        /// </summary>
        /// <param name="ClientId">The client identification to exchange for HTTP Bearer token.</param>
        /// <param name="ClientSecret">The client secret to exchange for HTTP Bearer token.</param>
        /// <param name="Scope">An optional application defined HTTP Authentication scope.</param>
        public ClientCredentialRequest(String   ClientId,
                                       String   ClientSecret,
                                       String?  Scope   = null)
        {

            this.ClientId      = ClientId;
            this.ClientSecret  = ClientSecret;
            this.Scope         = Scope;

            unchecked
            {

                hashCode = this.GrantType.   GetHashCode() * 7 ^
                           this.ClientId.    GetHashCode() * 5 ^
                           this.ClientSecret.GetHashCode() * 3 ^
                           this.Scope?.      GetHashCode() ?? 0;

            }

        }

        #endregion


        #region Documentation

        // clientCredentialRequest:
        //   type: object
        //   description: |
        //     Body of POST request to /auth/token. Note snake case per https://www.rfc-editor.org/rfc/rfc6749
        //   required:
        //     - grant_type
        //     - client_id
        //     - client_secret
        //   properties:
        //     grant_type:
        //       type: string
        //       description: OAuth2 grant type, must be 'client_credentials'
        //       example: client_credentials
        //       enum: [client_credentials]
        //     client_id:
        //       type: string
        //       minLength: 1
        //       maxLength: 4096
        //       description: client ID to exchange for bearer token.
        //       example: ven_client_99
        //     client_secret:
        //       type: string
        //       minLength: 1
        //       maxLength: 4096
        //       description: client secret to exchange for bearer token.
        //       example: ven_secret_99
        //     scope:
        //       type: string
        //       minLength: 0
        //       maxLength: 4096
        //       description: application defined scope.
        //       example: read_all

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
