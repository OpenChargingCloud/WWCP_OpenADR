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
    /// An Auth Error reponse on HTTP 400 from auth/token per https://www.rfc-editor.org/rfc/rfc6749
    /// </summary>
    public class AuthError
    {

        #region Properties

        /// <summary>
        /// The error as described in rfc6749.
        /// </summary>
        [Mandatory]
        public AuthErrorType  Error          { get; }

        /// <summary>
        /// The optional error description.
        /// Should be a sentence or two at most describing the circumstance of the error.
        /// </summary>
        [Optional]
        public String?        Description    { get; }

        /// <summary>
        /// The optional reference to a more detailed error description.
        /// </summary>
        [Optional]
        public String?        URI            { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new Auth Error reponse.
        /// </summary>
        /// <param name="Error">An error as described in rfc6749.</param>
        /// <param name="Description">An optional error description. Should be a sentence or two at most describing the circumstance of the error.</param>
        /// <param name="URI">An optional reference to a more detailed error description.</param>
        public AuthError(AuthErrorType  Error,
                         String?        Description   = null,
                         String?        URI           = null)
        {

            this.Error        = Error;
            this.Description  = Description;
            this.URI          = URI;

            unchecked
            {

                hashCode = this.Error.       GetHashCode()        * 5 ^
                          (this.Description?.GetHashCode()  ?? 0) * 3 ^
                           this.URI?.        GetHashCode()  ?? 0;

            }

        }

        #endregion


        #region Documentation

        // authError:
        //   type: object
        //   description: error reponse on HTTP 400 from auth/token per https://www.rfc-editor.org/rfc/rfc6749
        //   required:
        //     - error
        //   properties:
        //     error:
        //       type: string
        //       description: As described in rfc6749 |
        //         invalid_request – The request is missing a parameter so the server can’t proceed with the request. This may also be returned if the request includes an unsupported parameter or repeats a parameter.
        //         invalid_client – Client authentication failed, such as if the request contains an invalid client ID or secret. Send an HTTP 401 response in this case.
        //         invalid_grant – The authorization code (or user’s password for the password grant type) is invalid or expired. This is also the error you would return if the redirect URL given in the authorization grant does not match the URL provided in this access token request.
        //         invalid_scope – For access token requests that include a scope (password or client_credentials grants), this error indicates an invalid scope value in the request.
        //         unauthorized_client – This client is not authorized to use the requested grant type. For example, if you restrict which applications can use the Implicit grant, you would return this error for the other apps.
        //         unsupported_grant_type – If a grant type is requested that the authorization server doesn’t recognize, use this code. Note that unknown grant types also use this specific error code rather than using the invalid_request above.
        //       example: invalid_request
        //       enum: [invalid_request, invalid_client, invalid_grant, invalid_scope, unauthorized_client, unsupported_grant_type]
        //     error_description:
        //       type: string
        //       description: Should be a sentence or two at most describing the circumstance of the error
        //       example: Request was missing the 'client_id' parameter.
        //     error_uri:
        //       type: string
        //       format: uri
        //       description: Optional reference to more detailed error description
        //       example: See the full API docs at https://authorization-server.com/docs/access_toke

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
