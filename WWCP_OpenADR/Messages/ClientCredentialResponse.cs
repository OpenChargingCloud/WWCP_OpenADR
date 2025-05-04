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
    /// Body of POST response from /auth/token.
    /// </summary>
    /// <remarks>
    /// Note snake case per https://www.rfc-editor.org/rfc/rfc6749
    /// </remarks>
    public class ClientCredentialResponse
    {

        #region Properties

        /// <summary>
        /// The access token povided by Authorization service.
        /// </summary>
        [Mandatory]
        public String     AccessToken     { get; }

        /// <summary>
        /// The token type, must be "Bearer".
        /// </summary>
        [Mandatory]
        public String     TokenType       { get; } = "Bearer";

        /// <summary>
        /// The optional token expiration period.
        /// </summary>
        [Optional]
        public TimeSpan?  ExpiresIn       { get; }

        /// <summary>
        /// The optional refresh token povided by Authorization service.
        /// </summary>
        [Optional]
        public String?    RefreshToken    { get; }

        /// <summary>
        /// The optional application defined HTTP Authentication scope.
        /// </summary>
        [Optional]
        public String?    Scope           { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new ClientCredential response.
        /// </summary>
        /// <param name="AccessToken">An access token povided by Authorization service.</param>
        /// <param name="ExpiresIn">An optional token expiration period.</param>
        /// <param name="RefreshToken">An optional refresh token povided by Authorization service.</param>
        /// <param name="Scope">An optional application defined HTTP Authentication scope.</param>
        public ClientCredentialResponse(String     AccessToken,
                                        TimeSpan?  ExpiresIn      = null,
                                        String?    RefreshToken   = null,
                                        String?    Scope          = null)
        {

            this.AccessToken   = AccessToken;
            this.ExpiresIn     = ExpiresIn;
            this.RefreshToken  = RefreshToken;
            this.Scope         = Scope;

            unchecked
            {

                hashCode = this.AccessToken.  GetHashCode()       * 11 ^
                           this.TokenType.    GetHashCode()       *  7 ^
                          (this.ExpiresIn?.   GetHashCode() ?? 0) *  5 ^
                          (this.RefreshToken?.GetHashCode() ?? 0) *  3 ^
                           this.Scope?.       GetHashCode() ?? 0;

            }

        }

        #endregion


        #region Documentation

        // clientCredentialResponse:
        //   type: object
        //   description: |
        //     Body response from /auth/token. Note snake case per https://www.rfc-editor.org/rfc/rfc6749
        //   required:
        //     - access_token
        //     - token_type
        //   properties:
        //     access_token:
        //       type: string
        //       minLength: 1
        //       maxLength: 4096
        //       description: access token povided by Authorization service
        //       example: MTQ0NjJkZmQ5OTM2NDE1ZTZjNGZmZjI3
        //     token_type:
        //       type: string
        //       description: token type, must be Bearer.
        //       example: Bearer
        //       enum: [Bearer]
        //     expires_in:
        //       type: integer
        //       description: expiration period in seconds.
        //       example: 3600
        //     refresh_token:
        //       type: string
        //       minLength: 1
        //       maxLength: 4096
        //       description: refresh token povided by Authorization service
        //       example: IwOGYzYTlmM2YxOTQ5MGE3YmNmMDFkNTVk
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
