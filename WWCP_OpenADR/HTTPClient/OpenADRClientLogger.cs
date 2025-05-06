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

using org.GraphDefined.Vanaheimr.Hermod.HTTP;
using org.GraphDefined.Vanaheimr.Hermod.Logging;

#endregion

namespace cloud.charging.open.protocols.OpenADRv3
{

    /// <summary>
    /// The OpenADR HTTP Client.
    /// </summary>
    public partial class OpenADRClient : IHTTPClient
    {

        /// <summary>
        /// The OpenADR HTTP client logger.
        /// </summary>
        public sealed class Logger : HTTPClientLogger
        {

            #region Data

            /// <summary>
            /// The default context for this logger.
            /// </summary>
            public const String DefaultContext = $"OpenADR{Version.String}_HTTPClient";

            #endregion

            #region Properties

            /// <summary>
            /// The attached HTTP client.
            /// </summary>
            public new OpenADRClient  HTTPClient    { get; }

            #endregion

            #region Constructor(s)

            /// <summary>
            /// Create a new HTTP client logger using the default logging delegates.
            /// </summary>
            /// <param name="HTTPClient">A HTTP client.</param>
            /// <param name="LoggingPath">The logging path.</param>
            /// <param name="Context">A context of this API.</param>
            /// <param name="LogfileCreator">A delegate to create a log file from the given context and log file name.</param>
            public Logger(OpenADRClient            HTTPClient,
                          String?                  LoggingPath,
                          String?                  Context          = DefaultContext,
                          LogfileCreatorDelegate?  LogfileCreator   = null)

                : base(HTTPClient,
                       LoggingPath,
                       Context ?? DefaultContext,
                       LogfileCreator: LogfileCreator)

            {

                this.HTTPClient = HTTPClient ?? throw new ArgumentNullException(nameof(HTTPClient), "The given HTTP client must not be null!");

                #region ~/programs

                RegisterEvent("GetProgramsRequest",
                              handler => HTTPClient.OnGetProgramsHTTPRequest += handler,
                              handler => HTTPClient.OnGetProgramsHTTPRequest -= handler,
                              "GetPrograms", "locations", "requests", "all").
                    RegisterDefaultConsoleLogTarget(this).
                    RegisterDefaultDiscLogTarget(this);

                RegisterEvent("GetProgramsResponse",
                              handler => HTTPClient.OnGetProgramsHTTPResponse += handler,
                              handler => HTTPClient.OnGetProgramsHTTPResponse -= handler,
                              "GetPrograms", "locations", "responses", "all").
                    RegisterDefaultConsoleLogTarget(this).
                    RegisterDefaultDiscLogTarget(this);


                RegisterEvent("PostProgramsRequest",
                              handler => HTTPClient.OnPostProgramsHTTPRequest += handler,
                              handler => HTTPClient.OnPostProgramsHTTPRequest -= handler,
                              "PostPrograms", "locations", "requests", "all").
                    RegisterDefaultConsoleLogTarget(this).
                    RegisterDefaultDiscLogTarget(this);

                RegisterEvent("PostProgramsResponse",
                              handler => HTTPClient.OnPostProgramsHTTPResponse += handler,
                              handler => HTTPClient.OnPostProgramsHTTPResponse -= handler,
                              "PostPrograms", "locations", "responses", "all").
                    RegisterDefaultConsoleLogTarget(this).
                    RegisterDefaultDiscLogTarget(this);



                RegisterEvent("GetProgramRequest",
                              handler => HTTPClient.OnGetProgramHTTPRequest += handler,
                              handler => HTTPClient.OnGetProgramHTTPRequest -= handler,
                              "GetProgram", "locations", "requests", "all").
                    RegisterDefaultConsoleLogTarget(this).
                    RegisterDefaultDiscLogTarget(this);

                RegisterEvent("GetProgramResponse",
                              handler => HTTPClient.OnGetProgramHTTPResponse += handler,
                              handler => HTTPClient.OnGetProgramHTTPResponse -= handler,
                              "GetProgram", "locations", "responses", "all").
                    RegisterDefaultConsoleLogTarget(this).
                    RegisterDefaultDiscLogTarget(this);


                RegisterEvent("PutProgramRequest",
                              handler => HTTPClient.OnPutProgramHTTPRequest += handler,
                              handler => HTTPClient.OnPutProgramHTTPRequest -= handler,
                              "PutProgram", "locations", "requests", "all").
                    RegisterDefaultConsoleLogTarget(this).
                    RegisterDefaultDiscLogTarget(this);

                RegisterEvent("PutProgramResponse",
                              handler => HTTPClient.OnPutProgramHTTPResponse += handler,
                              handler => HTTPClient.OnPutProgramHTTPResponse -= handler,
                              "PutProgram", "locations", "responses", "all").
                    RegisterDefaultConsoleLogTarget(this).
                    RegisterDefaultDiscLogTarget(this);


                RegisterEvent("DeleteProgramRequest",
                              handler => HTTPClient.OnDeleteProgramHTTPRequest += handler,
                              handler => HTTPClient.OnDeleteProgramHTTPRequest -= handler,
                              "DeleteProgram", "locations", "requests", "all").
                    RegisterDefaultConsoleLogTarget(this).
                    RegisterDefaultDiscLogTarget(this);

                RegisterEvent("DeleteProgramResponse",
                              handler => HTTPClient.OnDeleteProgramHTTPResponse += handler,
                              handler => HTTPClient.OnDeleteProgramHTTPResponse -= handler,
                              "DeleteProgram", "locations", "responses", "all").
                    RegisterDefaultConsoleLogTarget(this).
                    RegisterDefaultDiscLogTarget(this);

                #endregion

            }

            #endregion

        }

    }

}
