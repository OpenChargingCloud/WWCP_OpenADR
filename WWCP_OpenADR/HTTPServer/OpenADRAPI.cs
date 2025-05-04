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

using System.Reflection;
using System.Security.Authentication;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json.Linq;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Hermod;
using org.GraphDefined.Vanaheimr.Hermod.HTTP;
using org.GraphDefined.Vanaheimr.Hermod.SMTP;
using org.GraphDefined.Vanaheimr.Hermod.Mail;
using org.GraphDefined.Vanaheimr.Hermod.DNS;
using org.GraphDefined.Vanaheimr.Hermod.Logging;
using org.GraphDefined.Vanaheimr.Hermod.Sockets;
using org.GraphDefined.Vanaheimr.Hermod.Sockets.TCP;

using cloud.charging.open.protocols.WWCP;
using System.Collections.Concurrent;

#endregion

namespace cloud.charging.open.protocols.OpenADRv3
{

    /// <summary>
    /// Extension methods for the OpenADR API.
    /// </summary>
    public static class OpenADRAPIExtensions
    {

        #region ParseProgramId (this Request, out ProgramId, out HTTPResponseBuilder)

        /// <summary>
        /// Parse the given HTTP request and return the party identification
        /// or an HTTP error response.
        /// </summary>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="ProgramId">The parsed party identification.</param>
        /// <param name="HTTPResponseBuilder">An HTTP response builder.</param>
        public static Boolean ParseProgramId(this HTTPRequest                                Request,
                                             [NotNullWhen(true)]  out Program_Id?            ProgramId,
                                             [NotNullWhen(false)] out HTTPResponse.Builder?  HTTPResponseBuilder)
        {

            ProgramId            = default;
            HTTPResponseBuilder  = default;

            if (Request.ParsedURLParameters.Length < 1)
            {

                HTTPResponseBuilder = new HTTPResponse.Builder(Request) {
                    HTTPStatusCode             = HTTPStatusCode.BadRequest,
                    AccessControlAllowHeaders  = [ "Authorization" ],
                    Connection                 = ConnectionType.Close
                };

                return false;

            }

            if (!Program_Id.TryParse(Request.ParsedURLParameters[0], out var programId))
            {

                HTTPResponseBuilder = new HTTPResponse.Builder(Request) {
                    HTTPStatusCode             = HTTPStatusCode.BadRequest,
                    AccessControlAllowHeaders  = [ "Authorization" ],
                    Connection                 = ConnectionType.Close
                };

                return false;

            }

            ProgramId = programId;
            return true;

        }

        #endregion



    }


    /// <summary>
    /// The OpenADR HTTP API.
    /// </summary>
    public class OpenADRAPI : HTTPExtAPI
    {

        #region Data

        /// <summary>
        /// The default HTTP server name.
        /// </summary>
        public new const       String               DefaultHTTPServerName                          = "OpenADR API";

        /// <summary>
        /// The default HTTP service name.
        /// </summary>
        public new const       String               DefaultHTTPServiceName                         = "OpenADR API";

        /// <summary>
        /// The HTTP root for embedded resources.
        /// </summary>
        public new const       String               HTTPRoot                                       = "cloud.charging.open.API.HTTPRoot.";

        public const           String               DefaultOpenADRAPI_DatabaseFileName   = "OpenADRAPI.db";
        public const           String               DefaultOpenADRAPI_LogfileName        = "OpenADRAPI.log";

        public static readonly HTTPEventSource_Id   DebugLogId                                     = HTTPEventSource_Id.Parse("DebugLog");
        public static readonly HTTPEventSource_Id   ImporterLogId                                  = HTTPEventSource_Id.Parse("ImporterLog");
        public static readonly HTTPEventSource_Id   ForwardingInfosId                              = HTTPEventSource_Id.Parse("ForwardingInfos");

        public                 WWWAuthenticate      WWWAuthenticateDefaults                        = WWWAuthenticate.Basic("OpenADR");

        #endregion

        #region Additional HTTP methods

        public readonly static HTTPMethod RESERVE      = HTTPMethod.TryParse("RESERVE",     IsSafe: false, IsIdempotent: true)!;
        public readonly static HTTPMethod SETEXPIRED   = HTTPMethod.TryParse("SETEXPIRED",  IsSafe: false, IsIdempotent: true)!;
        public readonly static HTTPMethod AUTHSTART    = HTTPMethod.TryParse("AUTHSTART",   IsSafe: false, IsIdempotent: true)!;
        public readonly static HTTPMethod AUTHSTOP     = HTTPMethod.TryParse("AUTHSTOP",    IsSafe: false, IsIdempotent: true)!;
        public readonly static HTTPMethod REMOTESTART  = HTTPMethod.TryParse("REMOTESTART", IsSafe: false, IsIdempotent: true)!;
        public readonly static HTTPMethod REMOTESTOP   = HTTPMethod.TryParse("REMOTESTOP",  IsSafe: false, IsIdempotent: true)!;
        public readonly static HTTPMethod SENDCDR      = HTTPMethod.TryParse("SENDCDR",     IsSafe: false, IsIdempotent: true)!;

        #endregion

        #region Properties

        /// <summary>
        /// The API version hash (git commit hash value).
        /// </summary>
        public new String                                   APIVersionHash                { get; }

        public String                                       OpenADRAPIPath      { get; }



        /// <summary>
        /// Send debug information via HTTP Server Sent Events.
        /// </summary>
        public HTTPEventSource<JObject>                     DebugLog                      { get; }

        #endregion

        #region Events

        #region (protected internal) CreateRoamingNetworkRequest (Request)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        public HTTPRequestLogEvent OnCreateRoamingNetworkRequest = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task CreateRoamingNetworkRequest(DateTime     Timestamp,
                                                            HTTPAPI      API,
                                                            HTTPRequest  Request)

            => OnCreateRoamingNetworkRequest?.WhenAll(Timestamp,
                                                      API ?? this,
                                                      Request) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) CreateRoamingNetworkResponse(Response)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnCreateRoamingNetworkResponse = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task CreateRoamingNetworkResponse(DateTime      Timestamp,
                                                             HTTPAPI       API,
                                                             HTTPRequest   Request,
                                                             HTTPResponse  Response)

            => OnCreateRoamingNetworkResponse?.WhenAll(Timestamp,
                                                       API ?? this,
                                                       Request,
                                                       Response) ?? Task.CompletedTask;

        #endregion


        #region (protected internal) DeleteRoamingNetworkRequest (Request)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        public HTTPRequestLogEvent OnDeleteRoamingNetworkRequest = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task DeleteRoamingNetworkRequest(DateTime     Timestamp,
                                                            HTTPAPI      API,
                                                            HTTPRequest  Request)

            => OnDeleteRoamingNetworkRequest?.WhenAll(Timestamp,
                                                      API ?? this,
                                                      Request) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) DeleteRoamingNetworkResponse(Response)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnDeleteRoamingNetworkResponse = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task DeleteRoamingNetworkResponse(DateTime      Timestamp,
                                                             HTTPAPI       API,
                                                             HTTPRequest   Request,
                                                             HTTPResponse  Response)

            => OnDeleteRoamingNetworkResponse?.WhenAll(Timestamp,
                                                       API ?? this,
                                                       Request,
                                                       Response) ?? Task.CompletedTask;

        #endregion



        #region (protected internal) CreateChargingPoolRequest (Request)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        public HTTPRequestLogEvent OnCreateChargingPoolRequest = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task CreateChargingPoolRequest(DateTime     Timestamp,
                                                          HTTPAPI      API,
                                                          HTTPRequest  Request)

            => OnCreateChargingPoolRequest?.WhenAll(Timestamp,
                                                    API ?? this,
                                                    Request) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) CreateChargingPoolResponse(Response)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnCreateChargingPoolResponse = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task CreateChargingPoolResponse(DateTime      Timestamp,
                                                           HTTPAPI       API,
                                                           HTTPRequest   Request,
                                                           HTTPResponse  Response)

            => OnCreateChargingPoolResponse?.WhenAll(Timestamp,
                                                     API ?? this,
                                                     Request,
                                                     Response) ?? Task.CompletedTask;

        #endregion


        #region (protected internal) DeleteChargingPoolRequest (Request)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        public HTTPRequestLogEvent OnDeleteChargingPoolRequest = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task DeleteChargingPoolRequest(DateTime     Timestamp,
                                                          HTTPAPI      API,
                                                          HTTPRequest  Request)

            => OnDeleteChargingPoolRequest?.WhenAll(Timestamp,
                                                    API ?? this,
                                                    Request) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) DeleteChargingPoolResponse(Response)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnDeleteChargingPoolResponse = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task DeleteChargingPoolResponse(DateTime      Timestamp,
                                                           HTTPAPI       API,
                                                           HTTPRequest   Request,
                                                           HTTPResponse  Response)

            => OnDeleteChargingPoolResponse?.WhenAll(Timestamp,
                                                     API ?? this,
                                                     Request,
                                                     Response) ?? Task.CompletedTask;

        #endregion



        #region (protected internal) CreateChargingStationRequest (Request)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        public HTTPRequestLogEvent OnCreateChargingStationRequest = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task CreateChargingStationRequest(DateTime     Timestamp,
                                                             HTTPAPI      API,
                                                             HTTPRequest  Request)

            => OnCreateChargingStationRequest?.WhenAll(Timestamp,
                                                       API ?? this,
                                                       Request) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) CreateChargingStationResponse(Response)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnCreateChargingStationResponse = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task CreateChargingStationResponse(DateTime      Timestamp,
                                                              HTTPAPI       API,
                                                              HTTPRequest   Request,
                                                              HTTPResponse  Response)

            => OnCreateChargingStationResponse?.WhenAll(Timestamp,
                                                        API ?? this,
                                                        Request,
                                                        Response) ?? Task.CompletedTask;

        #endregion


        #region (protected internal) DeleteChargingStationRequest (Request)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        public HTTPRequestLogEvent OnDeleteChargingStationRequest = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task DeleteChargingStationRequest(DateTime     Timestamp,
                                                             HTTPAPI      API,
                                                             HTTPRequest  Request)

            => OnDeleteChargingStationRequest?.WhenAll(Timestamp,
                                                       API ?? this,
                                                       Request) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) DeleteChargingStationResponse(Response)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnDeleteChargingStationResponse = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task DeleteChargingStationResponse(DateTime      Timestamp,
                                                              HTTPAPI       API,
                                                              HTTPRequest   Request,
                                                              HTTPResponse  Response)

            => OnDeleteChargingStationResponse?.WhenAll(Timestamp,
                                                        API ?? this,
                                                        Request,
                                                        Response) ?? Task.CompletedTask;

        #endregion



        #region (protected internal) SendGetEVSEsStatusRequest (Request)

        /// <summary>
        /// An event sent whenever an EVSEs->Status request was received.
        /// </summary>
        public HTTPRequestLogEvent OnGetEVSEsStatusRequest = new ();

        /// <summary>
        /// An event sent whenever an EVSEs->Status request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task SendGetEVSEsStatusRequest(DateTime     Timestamp,
                                                          HTTPAPI      API,
                                                          HTTPRequest  Request)

            => OnGetEVSEsStatusRequest?.WhenAll(Timestamp,
                                                API ?? this,
                                                Request) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) SendGetEVSEsStatusResponse(Response)

        /// <summary>
        /// An event sent whenever an EVSEs->Status response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnGetEVSEsStatusResponse = new ();

        /// <summary>
        /// An event sent whenever an EVSEs->Status response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task SendGetEVSEsStatusResponse(DateTime      Timestamp,
                                                           HTTPAPI       API,
                                                           HTTPRequest   Request,
                                                           HTTPResponse  Response)

            => OnGetEVSEsStatusResponse?.WhenAll(Timestamp,
                                                 API ?? this,
                                                 Request,
                                                 Response) ?? Task.CompletedTask;

        #endregion



        #region (protected internal) SendRemoteStartEVSERequest (Request)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        public HTTPRequestLogEvent OnSendRemoteStartEVSERequest = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task SendRemoteStartEVSERequest(DateTime     Timestamp,
                                                           HTTPAPI      API,
                                                           HTTPRequest  Request)

            => OnSendRemoteStartEVSERequest?.WhenAll(Timestamp,
                                                     API ?? this,
                                                     Request) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) SendRemoteStartEVSEResponse(Response)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnSendRemoteStartEVSEResponse = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task SendRemoteStartEVSEResponse(DateTime      Timestamp,
                                                            HTTPAPI       API,
                                                            HTTPRequest   Request,
                                                            HTTPResponse  Response)

            => OnSendRemoteStartEVSEResponse?.WhenAll(Timestamp,
                                                      API ?? this,
                                                      Request,
                                                      Response) ?? Task.CompletedTask;

        #endregion


        #region (protected internal) SendRemoteStopEVSERequest (Request)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        public HTTPRequestLogEvent OnSendRemoteStopEVSERequest = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task SendRemoteStopEVSERequest(DateTime     Timestamp,
                                                          HTTPAPI      API,
                                                          HTTPRequest  Request)

            => OnSendRemoteStopEVSERequest?.WhenAll(Timestamp,
                                                    API ?? this,
                                                    Request) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) SendRemoteStopEVSEResponse(Response)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnSendRemoteStopEVSEResponse = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task SendRemoteStopEVSEResponse(DateTime      Timestamp,
                                                           HTTPAPI       API,
                                                           HTTPRequest   Request,
                                                           HTTPResponse  Response)

            => OnSendRemoteStopEVSEResponse?.WhenAll(Timestamp,
                                                     API ?? this,
                                                     Request,
                                                     Response) ?? Task.CompletedTask;

        #endregion



        #region (protected internal) SendReserveEVSERequest     (Request)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        public HTTPRequestLogEvent OnSendReserveEVSERequest = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task SendReserveEVSERequest(DateTime     Timestamp,
                                                       HTTPAPI      API,
                                                       HTTPRequest  Request)

            => OnSendReserveEVSERequest?.WhenAll(Timestamp,
                                                 API ?? this,
                                                 Request) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) SendReserveEVSEResponse    (Response)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnSendReserveEVSEResponse = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task SendReserveEVSEResponse(DateTime      Timestamp,
                                                        HTTPAPI       API,
                                                        HTTPRequest   Request,
                                                        HTTPResponse  Response)

            => OnSendReserveEVSEResponse?.WhenAll(Timestamp,
                                                  API ?? this,
                                                  Request,
                                                  Response) ?? Task.CompletedTask;

        #endregion


        #region (protected internal) SendAuthStartEVSERequest   (Request)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        public HTTPRequestLogEvent OnAuthStartEVSERequest = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task SendAuthStartEVSERequest(DateTime     Timestamp,
                                                         HTTPAPI      API,
                                                         HTTPRequest  Request)

            => OnAuthStartEVSERequest?.WhenAll(Timestamp,
                                               API ?? this,
                                               Request) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) SendAuthStartEVSEResponse  (Response)

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnAuthStartEVSEResponse = new ();

        /// <summary>
        /// An event sent whenever a authenticate start EVSE response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task SendAuthStartEVSEResponse(DateTime      Timestamp,
                                                          HTTPAPI       API,
                                                          HTTPRequest   Request,
                                                          HTTPResponse  Response)

            => OnAuthStartEVSEResponse?.WhenAll(Timestamp,
                                                API ?? this,
                                                Request,
                                                Response) ?? Task.CompletedTask;

        #endregion


        #region (protected internal) SendAuthStopEVSERequest    (Request)

        /// <summary>
        /// An event sent whenever a authenticate stop EVSE request was received.
        /// </summary>
        public HTTPRequestLogEvent OnAuthStopEVSERequest = new ();

        /// <summary>
        /// An event sent whenever a authenticate stop EVSE request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task SendAuthStopEVSERequest(DateTime     Timestamp,
                                                        HTTPAPI      API,
                                                        HTTPRequest  Request)

            => OnAuthStopEVSERequest?.WhenAll(Timestamp,
                                              API ?? this,
                                              Request) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) SendAuthStopEVSEResponse   (Response)

        /// <summary>
        /// An event sent whenever a authenticate stop EVSE response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnAuthStopEVSEResponse = new ();

        /// <summary>
        /// An event sent whenever a authenticate stop EVSE response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task SendAuthStopEVSEResponse(DateTime      Timestamp,
                                                         HTTPAPI       API,
                                                         HTTPRequest   Request,
                                                         HTTPResponse  Response)

            => OnAuthStopEVSEResponse?.WhenAll(Timestamp,
                                               API ?? this,
                                               Request,
                                               Response) ?? Task.CompletedTask;

        #endregion


        #region (protected internal) SendCDRsRequest            (Request)

        /// <summary>
        /// An event sent whenever a charge detail record was received.
        /// </summary>
        public HTTPRequestLogEvent OnSendCDRsRequest = new ();

        /// <summary>
        /// An event sent whenever a charge detail record was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task SendCDRsRequest(DateTime     Timestamp,
                                                HTTPAPI      API,
                                                HTTPRequest  Request)

            => OnSendCDRsRequest?.WhenAll(Timestamp,
                                          API ?? this,
                                          Request) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) SendCDRsResponse           (Response)

        /// <summary>
        /// An event sent whenever a charge detail record response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnSendCDRsResponse = new ();

        /// <summary>
        /// An event sent whenever a charge detail record response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task SendCDRsResponse(DateTime      Timestamp,
                                                 HTTPAPI       API,
                                                 HTTPRequest   Request,
                                                 HTTPResponse  Response)

            => OnSendCDRsResponse?.WhenAll(Timestamp,
                                           API ?? this,
                                           Request,
                                           Response) ?? Task.CompletedTask;

        #endregion

        #endregion

        #region Custom JSON parsers

        #endregion

        #region Custom JSON serializers

        public CustomJObjectSerializerDelegate<Program>?                  CustomProgramSerializer                   { get; set; }
        public CustomJObjectSerializerDelegate<ObjectOperation>?          CustomObjectOperationSerializer           { get; set; }
        public CustomJObjectSerializerDelegate<ValuesMap>?                CustomValuesMapSerializer                 { get; set; }

        public CustomJObjectSerializerDelegate<IntervalPeriod>?           CustomIntervalPeriodSerializer            { get; set; }
        public CustomJObjectSerializerDelegate<EventPayloadDescriptor>?   CustomEventPayloadDescriptorSerializer    { get; set; }
        public CustomJObjectSerializerDelegate<ReportPayloadDescriptor>?  CustomReportPayloadDescriptorSerializer   { get; set; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new OpenADR HTTP API.
        /// </summary>
        /// <param name="HTTPHostname">The HTTP hostname for all URLs within this API.</param>
        /// <param name="ExternalDNSName">The official URL/DNS name of this service, e.g. for sending e-mails.</param>
        /// <param name="HTTPServerPort">A TCP port to listen on.</param>
        /// <param name="BasePath">When the API is served from an optional subdirectory path.</param>
        /// <param name="HTTPServerName">The default HTTP server name, used whenever no HTTP Host-header has been given.</param>
        /// 
        /// <param name="URLPathPrefix">A common prefix for all URLs.</param>
        /// <param name="HTTPServiceName">The name of the HTTP service.</param>
        /// <param name="APIVersionHashes">The API version hashes (git commit hash values).</param>
        /// 
        /// <param name="ServerCertificateSelector">An optional delegate to select a TLS server certificate.</param>
        /// <param name="ClientCertificateValidator">An optional delegate to verify the TLS client certificate used for authentication.</param>
        /// <param name="LocalCertificateSelector">An optional delegate to select the TLS client certificate used for authentication.</param>
        /// <param name="AllowedTLSProtocols">The TLS protocol(s) allowed for this connection.</param>
        /// 
        /// <param name="TCPPort"></param>
        /// <param name="UDPPort"></param>
        /// 
        /// <param name="APIRobotEMailAddress">An e-mail address for this API.</param>
        /// <param name="APIRobotGPGPassphrase">A GPG passphrase for this API.</param>
        /// <param name="SMTPClient">A SMTP client for sending e-mails.</param>
        /// 
        /// <param name="PasswordQualityCheck">A delegate to ensure a minimal password quality.</param>
        /// <param name="CookieName">The name of the HTTP Cookie for authentication.</param>
        /// <param name="UseSecureCookies">Force the web browser to send cookies only via HTTPS.</param>
        /// 
        /// <param name="ServerThreadName">The optional name of the TCP server thread.</param>
        /// <param name="ServerThreadPriority">The optional priority of the TCP server thread.</param>
        /// <param name="ServerThreadIsBackground">Whether the TCP server thread is a background thread or not.</param>
        /// <param name="ConnectionIdBuilder">An optional delegate to build a connection identification based on IP socket information.</param>
        /// <param name="ConnectionTimeout">The TCP client timeout for all incoming client connections in seconds (default: 30 sec).</param>
        /// <param name="MaxClientConnections">The maximum number of concurrent TCP client connections (default: 4096).</param>
        /// 
        /// <param name="DisableMaintenanceTasks">Disable all maintenance tasks.</param>
        /// <param name="MaintenanceInitialDelay">The initial delay of the maintenance tasks.</param>
        /// <param name="MaintenanceEvery">The maintenance interval.</param>
        /// 
        /// <param name="DisableWardenTasks">Disable all warden tasks.</param>
        /// <param name="WardenInitialDelay">The initial delay of the warden tasks.</param>
        /// <param name="WardenCheckEvery">The warden interval.</param>
        /// 
        /// <param name="RemoteAuthServers">Servers for remote authorization.</param>
        /// <param name="RemoteAuthAPIKeys">API keys for incoming remote authorizations.</param>
        /// 
        /// <param name="IsDevelopment">This HTTP API runs in development mode.</param>
        /// <param name="DevelopmentServers">An enumeration of server names which will imply to run this service in development mode.</param>
        /// <param name="SkipURLTemplates">Skip URL templates.</param>
        /// <param name="DatabaseFileName">The name of the database file for this API.</param>
        /// <param name="DisableNotifications">Disable external notifications.</param>
        /// <param name="DisableLogging">Disable the log file.</param>
        /// <param name="LoggingPath">The path for all logfiles.</param>
        /// <param name="LogfileName">The name of the logfile.</param>
        /// <param name="LogfileCreator">A delegate for creating the name of the logfile for this API.</param>
        /// <param name="DNSClient">The DNS client of the API.</param>
        public OpenADRAPI(HTTPHostname?                                              HTTPHostname                 = null,
                          String?                                                    ExternalDNSName              = null,
                          IPPort?                                                    HTTPServerPort               = null,
                          HTTPPath?                                                  BasePath                     = null,
                          String                                                     HTTPServerName               = DefaultHTTPServerName,

                          HTTPPath?                                                  URLPathPrefix                = null,
                          String                                                     HTTPServiceName              = DefaultHTTPServiceName,
                          String?                                                    HTMLTemplate                 = null,
                          JObject?                                                   APIVersionHashes             = null,

                          ServerCertificateSelectorDelegate?                         ServerCertificateSelector    = null,
                          RemoteTLSClientCertificateValidationHandler<IHTTPServer>?  ClientCertificateValidator   = null,
                          LocalCertificateSelectionHandler?                          LocalCertificateSelector     = null,
                          SslProtocols?                                              AllowedTLSProtocols          = null,
                          Boolean?                                                   ClientCertificateRequired    = null,
                          Boolean?                                                   CheckCertificateRevocation   = null,

                          ServerThreadNameCreatorDelegate?                           ServerThreadNameCreator      = null,
                          ServerThreadPriorityDelegate?                              ServerThreadPrioritySetter   = null,
                          Boolean?                                                   ServerThreadIsBackground     = null,
                          ConnectionIdBuilder?                                       ConnectionIdBuilder          = null,
                          TimeSpan?                                                  ConnectionTimeout            = null,
                          UInt32?                                                    MaxClientConnections         = null,

                          IPPort?                                                    TCPPort                      = null,
                          IPPort?                                                    UDPPort                      = null,

                          Organization_Id?                                           AdminOrganizationId          = null,
                          EMailAddress?                                              APIRobotEMailAddress         = null,
                          String?                                                    APIRobotGPGPassphrase        = null,
                          ISMTPClient?                                               SMTPClient                   = null,

                          PasswordQualityCheckDelegate?                              PasswordQualityCheck         = null,
                          HTTPCookieName?                                            CookieName                   = null,
                          Boolean                                                    UseSecureCookies             = true,
                          Languages?                                                 DefaultLanguage              = null,

                          Boolean?                                                   DisableMaintenanceTasks      = null,
                          TimeSpan?                                                  MaintenanceInitialDelay      = null,
                          TimeSpan?                                                  MaintenanceEvery             = null,

                          Boolean?                                                   DisableWardenTasks           = null,
                          TimeSpan?                                                  WardenInitialDelay           = null,
                          TimeSpan?                                                  WardenCheckEvery             = null,

                          IEnumerable<URLWithAPIKey>?                                RemoteAuthServers            = null,
                          IEnumerable<APIKey_Id>?                                    RemoteAuthAPIKeys            = null,

                          Boolean?                                                   AllowsAnonymousReadAccesss   = true,

                          Boolean?                                                   IsDevelopment                = null,
                          IEnumerable<String>?                                       DevelopmentServers           = null,
                          Boolean                                                    SkipURLTemplates             = false,
                          String                                                     DatabaseFileName             = DefaultOpenADRAPI_DatabaseFileName,
                          Boolean                                                    DisableNotifications         = false,
                          Boolean                                                    DisableLogging               = false,
                          String?                                                    LoggingPath                  = null,
                          String                                                     LogfileName                  = DefaultOpenADRAPI_LogfileName,
                          LogfileCreatorDelegate?                                    LogfileCreator               = null,
                          DNSClient?                                                 DNSClient                    = null)

            : base(HTTPHostname,
                   ExternalDNSName,
                   HTTPServerPort,
                   BasePath,
                   HTTPServerName,

                   URLPathPrefix,
                   HTTPServiceName,
                   HTMLTemplate,
                   APIVersionHashes,

                   ServerCertificateSelector,
                   ClientCertificateValidator,
                   LocalCertificateSelector,
                   AllowedTLSProtocols,
                   ClientCertificateRequired,
                   CheckCertificateRevocation,

                   ServerThreadNameCreator,
                   ServerThreadPrioritySetter,
                   ServerThreadIsBackground,
                   ConnectionIdBuilder,
                   ConnectionTimeout,
                   MaxClientConnections,

                   AdminOrganizationId,
                   APIRobotEMailAddress,
                   APIRobotGPGPassphrase,
                   SMTPClient,

                   PasswordQualityCheck,
                   CookieName           ?? HTTPCookieName.Parse(nameof(OpenADRAPI)),
                   UseSecureCookies,
                   TimeSpan.FromDays(30),
                   DefaultLanguage      ?? Languages.en,
                   4,
                   4,
                   4,
                   5,
                   20,
                   8,
                   4,
                   4,
                   8,
                   8,
                   8,
                   8,

                   DisableMaintenanceTasks,
                   MaintenanceInitialDelay,
                   MaintenanceEvery,

                   DisableWardenTasks,
                   WardenInitialDelay,
                   WardenCheckEvery,

                   RemoteAuthServers,
                   RemoteAuthAPIKeys,

                   IsDevelopment,
                   DevelopmentServers,
                   SkipURLTemplates,
                   DatabaseFileName     ?? DefaultOpenADRAPI_DatabaseFileName,
                   DisableNotifications,
                   DisableLogging,
                   LoggingPath,
                   LogfileName          ?? DefaultOpenADRAPI_LogfileName,
                   LogfileCreator,
                   DNSClient,
                   false) // AutoStart

        {

            this.APIVersionHash  = APIVersionHashes?[nameof(OpenADRAPI)]?.Value<String>()?.Trim() ?? "";

            this.OpenADRAPIPath  = Path.Combine(this.LoggingPath, "OpenADRAPI");

            if (!DisableLogging)
            {
                Directory.CreateDirectory(OpenADRAPIPath);
            }

         //   DebugLog     = HTTPServer.AddJSONEventSource(EventIdentification:      DebugLogId,
         //                                                HTTPAPI:                  this,
         //                                                URLTemplate:              this.URLPathPrefix + DebugLogId.ToString(),
         //                                                MaxNumberOfCachedEvents:  1000,
         //                                                RetryInterval :           TimeSpan.FromSeconds(5),
         //                                                EnableLogging:            true,
         //                                                LogfilePath:              this.OpenADRAPIPath);

            RegisterURLTemplates();

            //this.HTMLTemplate = HTMLTemplate ?? GetResourceString("template.html");

            DebugX.Log(nameof(OpenADRAPI) + " version '" + APIVersionHash + "' initialized...");

        }

        #endregion


        Boolean? AllowDowngrades = false;

        #region Programs

        private readonly ConcurrentDictionary<Program_Id, Program> programs = [];

        #region Events

        public delegate Task OnProgramAddedDelegate(Program Program);

        public event OnProgramAddedDelegate?    OnProgramAdded;


        public delegate Task OnProgramChangedDelegate(Program Program);

        public event OnProgramChangedDelegate?  OnProgramChanged;

        #endregion


        #region AddProgram            (Program, ...)

        public async Task<AddResult<Program>>

            AddProgram(Program            Program,
                       Boolean            SkipNotifications   = false,
                       EventTracking_Id?  EventTrackingId     = null,
                       User_Id?           CurrentUserId       = null,
                       CancellationToken  CancellationToken   = default)

        {

            EventTrackingId ??= EventTracking_Id.New;

            var programId = Program_Id.Parse(Program.Id.ToString());

            if (programs.TryGetValue(programId, out var party))
            {

                if (programs.TryAdd(programId, Program))
                {

                    DebugX.Log($"OCPI {Version.String} Program '{Program.Id}': '{Program}' added...");

                    //Program.CommonAPI = this;

                    //await LogAsset(
                    //          CommonBaseAPI.addProgram,
                    //          Program.ToJSON(
                    //              true,
                    //              true,
                    //              CustomProgramSerializer,
                    //              CustomDisplayTextSerializer,
                    //              CustomPriceSerializer,
                    //              CustomProgramElementSerializer,
                    //              CustomPriceComponentSerializer,
                    //              CustomProgramRestrictionsSerializer,
                    //              CustomEnergyMixSerializer,
                    //              CustomEnergySourceSerializer,
                    //              CustomEnvironmentalImpactSerializer
                    //          ),
                    //          EventTrackingId,
                    //          CurrentUserId,
                    //          CancellationToken
                    //      );

                    if (!SkipNotifications)
                    {

                        var OnProgramAddedLocal = OnProgramAdded;
                        if (OnProgramAddedLocal is not null)
                        {
                            try
                            {
                                await OnProgramAddedLocal(Program);
                            }
                            catch (Exception e)
                            {
                                DebugX.LogT($"OpenADR {Version.String} {nameof(OpenADRAPI)} ", nameof(AddProgram), " ", nameof(OnProgramAdded), ": ",
                                            Environment.NewLine, e.Message,
                                            Environment.NewLine, e.StackTrace ?? "");
                            }
                        }

                    }

                    return AddResult<Program>.Success(
                               EventTrackingId,
                               Program
                           );

                }

                return AddResult<Program>.Failed(
                           EventTrackingId,
                           Program,
                           "TryAdd(Program.Id, Program) failed!"
                       );

            }

            return AddResult<Program>.Failed(
                       EventTrackingId,
                       Program,
                       "The party identification of the program is unknown!"
                   );

        }

        #endregion

        #region AddProgramIfNotExists (Program, ...)

        public async Task<AddResult<Program>>

            AddProgramIfNotExists(Program            Program,
                                  Boolean            SkipNotifications   = false,
                                  EventTracking_Id?  EventTrackingId     = null,
                                  User_Id?           CurrentUserId       = null,
                                  CancellationToken  CancellationToken   = default)

        {

            EventTrackingId ??= EventTracking_Id.New;

            var programId = Program_Id.Parse(Program.Id.ToString());

            if (programs.TryGetValue(programId, out var party))
            {

                if (programs.TryAdd(programId, Program))
                {

                    //Program.CommonAPI = this;

                    //await LogAsset(
                    //          CommonBaseAPI.addProgramIfNotExists,
                    //          Program.ToJSON(
                    //              true,
                    //              true,
                    //              CustomProgramSerializer,
                    //              CustomDisplayTextSerializer,
                    //              CustomPriceSerializer,
                    //              CustomProgramElementSerializer,
                    //              CustomPriceComponentSerializer,
                    //              CustomProgramRestrictionsSerializer,
                    //              CustomEnergyMixSerializer,
                    //              CustomEnergySourceSerializer,
                    //              CustomEnvironmentalImpactSerializer
                    //          ),
                    //          EventTrackingId,
                    //          CurrentUserId,
                    //          CancellationToken
                    //      );

                    if (!SkipNotifications)
                    {

                        var OnProgramAddedLocal = OnProgramAdded;
                        if (OnProgramAddedLocal is not null)
                        {
                            try
                            {
                                await OnProgramAddedLocal(Program);
                            }
                            catch (Exception e)
                            {
                                DebugX.LogT($"OpenADR {Version.String} {nameof(OpenADRAPI)} ", nameof(AddProgramIfNotExists), " ", nameof(OnProgramAdded), ": ",
                                            Environment.NewLine, e.Message,
                                            Environment.NewLine, e.StackTrace ?? "");
                            }
                        }

                    }

                    return AddResult<Program>.Success(
                               EventTrackingId,
                               Program
                           );

                }

                return AddResult<Program>.NoOperation(
                           EventTrackingId,
                           Program,
                           "The given program already exists."
                       );

            }

            return AddResult<Program>.Failed(
                       EventTrackingId,
                       Program,
                       "The party identification of the program is unknown!"
                   );

        }

        #endregion

        #region AddOrUpdateProgram    (Program, AllowDowngrades = false, ...)

        public async Task<AddOrUpdateResult<Program>>

            AddOrUpdateProgram(Program            Program,
                               Boolean?           AllowDowngrades     = false,
                               Boolean            SkipNotifications   = false,
                               EventTracking_Id?  EventTrackingId     = null,
                               User_Id?           CurrentUserId       = null,
                               CancellationToken  CancellationToken   = default)

        {

            EventTrackingId ??= EventTracking_Id.New;

            var programId = Program_Id.Parse(Program.Id.ToString());

            if (programs.TryGetValue(programId, out var party))
            {

                #region Update an existing program

                if (programs.TryGetValue(programId,
                                         out var existingProgram))
                {

                    if ((AllowDowngrades ?? this.AllowDowngrades) == false &&
                        Program.LastModification <= existingProgram.LastModification)
                    {
                        return AddOrUpdateResult<Program>.Failed(
                                   EventTrackingId,
                                   Program,
                                   "The 'lastUpdated' timestamp of the new program must be newer then the timestamp of the existing program!"
                               );
                    }

                    if (programs.TryUpdate(programId,
                                           Program,
                                           existingProgram))
                    {

                      //  Program.CommonAPI = this;

                      //  await LogAsset(
                      //            CommonBaseAPI.addOrUpdateProgram,
                      //            Program.ToJSON(
                      //                true,
                      //                true,
                      //                CustomProgramSerializer,
                      //                CustomDisplayTextSerializer,
                      //                CustomPriceSerializer,
                      //                CustomProgramElementSerializer,
                      //                CustomPriceComponentSerializer,
                      //                CustomProgramRestrictionsSerializer,
                      //                CustomEnergyMixSerializer,
                      //                CustomEnergySourceSerializer,
                      //                CustomEnvironmentalImpactSerializer
                      //            ),
                      //            EventTrackingId,
                      //            CurrentUserId,
                      //            CancellationToken
                      //        );

                        if (!SkipNotifications)
                        {

                            var OnProgramChangedLocal = OnProgramChanged;
                            if (OnProgramChangedLocal is not null)
                            {
                                try
                                {
                                    await OnProgramChangedLocal(Program);
                                }
                                catch (Exception e)
                                {
                                    DebugX.LogT($"OpenADR {Version.String} {nameof(OpenADRAPI)} ", nameof(AddOrUpdateProgram), " ", nameof(OnProgramChanged), ": ",
                                                Environment.NewLine, e.Message,
                                                Environment.NewLine, e.StackTrace ?? "");
                                }
                            }

                        }

                        return AddOrUpdateResult<Program>.Updated(
                                   EventTrackingId,
                                   Program
                               );

                    }

                    return AddOrUpdateResult<Program>.Failed(
                               EventTrackingId,
                               Program,
                               "Updating the given program failed!"
                           );

                }

                #endregion

                #region Add a new program

                if (programs.TryAdd(programId, Program))
                {

                  //  Program.CommonAPI = this;

                  //  await LogAsset(
                  //            CommonBaseAPI.addOrUpdateProgram,
                  //            Program.ToJSON(
                  //                true,
                  //                true,
                  //                CustomProgramSerializer,
                  //                CustomDisplayTextSerializer,
                  //                CustomPriceSerializer,
                  //                CustomProgramElementSerializer,
                  //                CustomPriceComponentSerializer,
                  //                CustomProgramRestrictionsSerializer,
                  //                CustomEnergyMixSerializer,
                  //                CustomEnergySourceSerializer,
                  //                CustomEnvironmentalImpactSerializer
                  //            ),
                  //            EventTrackingId,
                  //            CurrentUserId,
                  //            CancellationToken
                  //        );

                    if (!SkipNotifications)
                    {

                        var OnProgramAddedLocal = OnProgramAdded;
                        if (OnProgramAddedLocal is not null)
                        {
                            try
                            {
                                await OnProgramAddedLocal(Program);
                            }
                            catch (Exception e)
                            {
                                DebugX.LogT($"OpenADR {Version.String} {nameof(OpenADRAPI)} ", nameof(AddOrUpdateProgram), " ", nameof(OnProgramAdded), ": ",
                                            Environment.NewLine, e.Message,
                                            Environment.NewLine, e.StackTrace ?? "");
                            }
                        }

                    }

                    return AddOrUpdateResult<Program>.Created(
                               EventTrackingId,
                               Program
                           );

                }

                #endregion

                return AddOrUpdateResult<Program>.Failed(
                           EventTrackingId,
                           Program,
                           "Adding the given program failed because of concurrency issues!"
                       );

            }

            return AddOrUpdateResult<Program>.Failed(
                       EventTrackingId,
                       Program,
                       "The party identification of the program is unknown!"
                   );

        }

        #endregion

        #region UpdateProgram         (Program, AllowDowngrades = false, ...)

        public async Task<UpdateResult<Program>>

            UpdateProgram(Program            Program,
                          Boolean?           AllowDowngrades     = false,
                          Boolean            SkipNotifications   = false,
                          EventTracking_Id?  EventTrackingId     = null,
                          User_Id?           CurrentUserId       = null,
                          CancellationToken  CancellationToken   = default)

        {

            EventTrackingId ??= EventTracking_Id.New;

            var programId = Program_Id.Parse(Program.Id.ToString());

            if (programs.TryGetValue(programId, out var party))
            {

                if (!programs.TryGetValue(programId, out var existingProgram))
                    return UpdateResult<Program>.Failed(
                               EventTrackingId,
                               Program,
                               $"The given program identification '{Program.Id}' is unknown!"
                           );

                #region Validate AllowDowngrades

                if ((AllowDowngrades ?? this.AllowDowngrades) == false &&
                    Program.LastModification <= existingProgram.LastModification)
                {

                    return UpdateResult<Program>.Failed(
                               EventTrackingId,
                               Program,
                               "The 'lastUpdated' timestamp of the new program must be newer then the timestamp of the existing program!"
                           );

                }

                #endregion


                if (programs.TryUpdate(programId,
                                       Program,
                                       existingProgram))
                {

                    //Program.CommonAPI = this;

                    //await LogAsset(
                    //          CommonBaseAPI.updateProgram,
                    //          Program.ToJSON(
                    //              true,
                    //              true,
                    //              CustomProgramSerializer,
                    //              CustomDisplayTextSerializer,
                    //              CustomPriceSerializer,
                    //              CustomProgramElementSerializer,
                    //              CustomPriceComponentSerializer,
                    //              CustomProgramRestrictionsSerializer,
                    //              CustomEnergyMixSerializer,
                    //              CustomEnergySourceSerializer,
                    //              CustomEnvironmentalImpactSerializer
                    //          ),
                    //          EventTrackingId,
                    //          CurrentUserId,
                    //          CancellationToken
                    //      );

                    if (!SkipNotifications)
                    {

                        var OnProgramChangedLocal = OnProgramChanged;
                        if (OnProgramChangedLocal is not null)
                        {
                            try
                            {
                                await OnProgramChangedLocal(Program);
                            }
                            catch (Exception e)
                            {
                                DebugX.LogT($"OpenADR {Version.String} {nameof(OpenADRAPI)} ", nameof(UpdateProgram), " ", nameof(OnProgramChanged), ": ",
                                            Environment.NewLine, e.Message,
                                            Environment.NewLine, e.StackTrace ?? "");
                            }
                        }

                    }

                    return UpdateResult<Program>.Success(
                               EventTrackingId,
                               Program
                           );

                }

                return UpdateResult<Program>.Failed(
                           EventTrackingId,
                           Program,
                           "Programs.TryUpdate(Program.Id, Program, Program) failed!"
                       );

            }

            return UpdateResult<Program>.Failed(
                       EventTrackingId,
                       Program,
                       "The party identification of the program is unknown!"
                   );

        }

        #endregion

        #region RemoveProgram         (Program, ...)

        /// <summary>
        /// Remove the given program.
        /// </summary>
        /// <param name="Program">A program.</param>
        /// <param name="SkipNotifications">Skip sending notifications.</param>
        public async Task<RemoveResult<Program>>

            RemoveProgram(Program            Program,
                          Boolean            SkipNotifications   = false,
                          EventTracking_Id?  EventTrackingId     = null,
                          User_Id?           CurrentUserId       = null,
                          CancellationToken  CancellationToken   = default)

        {

            EventTrackingId ??= EventTracking_Id.New;

            var programId = Program_Id.Parse(Program.Id.ToString());

            if (programs.TryRemove(programId, out var programVersions))
            {

                //await LogAsset(
                //          CommonBaseAPI.removeProgram,
                //          new JArray(
                //              programVersions.Select(program =>
                //                  program.ToJSON(
                //                      true,
                //                      true,
                //                      CustomProgramSerializer,
                //                      CustomDisplayTextSerializer,
                //                      CustomPriceSerializer,
                //                      CustomProgramElementSerializer,
                //                      CustomPriceComponentSerializer,
                //                      CustomProgramRestrictionsSerializer,
                //                      CustomEnergyMixSerializer,
                //                      CustomEnergySourceSerializer,
                //                      CustomEnvironmentalImpactSerializer
                //                  )
                //              )
                //          ),
                //          EventTrackingId,
                //          CurrentUserId,
                //          CancellationToken
                //      );

                return RemoveResult<Program>.Success(
                           EventTrackingId,
                           programVersions
                       );

            }

            return RemoveResult<Program>.Failed(
                       EventTrackingId,
                       Program,
                       "The identification of the program is unknown!"
                   );

        }

        #endregion

        #region RemoveProgram         (ProgramId, ...)

        /// <summary>
        /// Remove the given program.
        /// </summary>
        /// <param name="ProgramId">An unique program identification.</param>
        /// <param name="SkipNotifications">Skip sending notifications.</param>
        public async Task<RemoveResult<Program>>

            RemoveProgram(Program_Id         ProgramId,
                          Boolean            SkipNotifications   = false,
                          EventTracking_Id?  EventTrackingId     = null,
                          User_Id?           CurrentUserId       = null,
                          CancellationToken  CancellationToken   = default)

        {

            EventTrackingId ??= EventTracking_Id.New;

            if (programs.TryRemove(ProgramId, out var programVersions))
            {

                //await LogAsset(
                //          CommonBaseAPI.removeProgram,
                //          new JArray(
                //              programVersions.Select(
                //                  program => program.ToJSON(
                //                                true,
                //                                true,
                //                                CustomProgramSerializer,
                //                                CustomDisplayTextSerializer,
                //                                CustomPriceSerializer,
                //                                CustomProgramElementSerializer,
                //                                CustomPriceComponentSerializer,
                //                                CustomProgramRestrictionsSerializer,
                //                                CustomEnergyMixSerializer,
                //                                CustomEnergySourceSerializer,
                //                                CustomEnvironmentalImpactSerializer
                //                            )
                //                  )
                //          ),
                //          EventTrackingId,
                //          CurrentUserId,
                //          CancellationToken
                //      );

                return RemoveResult<Program>.Success(
                           EventTrackingId,
                           programVersions
                       );

            }

            return RemoveResult<Program>.Failed(
                       EventTrackingId,
                       "The identification of the program is unknown!"
                   );

        }

        #endregion

        #region RemoveAllPrograms     (IncludePrograms = null, ...)

        /// <summary>
        /// Remove all matching programs.
        /// </summary>
        /// <param name="IncludePrograms">A program filter.</param>
        /// <param name="SkipNotifications">Skip sending notifications.</param>
        public async Task<RemoveResult<IEnumerable<Program>>>

            RemoveAllPrograms(Func<Program, Boolean>?  IncludePrograms     = null,
                              Boolean                  SkipNotifications   = false,
                              EventTracking_Id?        EventTrackingId     = null,
                              User_Id?                 CurrentUserId       = null,
                              CancellationToken        CancellationToken   = default)

        {

            EventTrackingId ??= EventTracking_Id.New;

            var removedPrograms = new List<Program>();

            if (IncludePrograms is null)
            {
                removedPrograms.AddRange(programs.Values);
                programs.Clear();
            }

            else
            {

                foreach (var program in programs.Values)
                {
                    if (IncludePrograms(program))
                        removedPrograms.Add(program);
                }

                foreach (var program in removedPrograms)
                    programs.TryRemove(Program_Id.Parse(program.Id.ToString()), out _);

            }

            //await LogAsset(
            //          CommonBaseAPI.removeAllPrograms,
            //          new JArray(
            //              removedPrograms.Select(
            //                  program => program.ToJSON(
            //                                true,
            //                                true,
            //                                CustomProgramSerializer,
            //                                CustomDisplayTextSerializer,
            //                                CustomPriceSerializer,
            //                                CustomProgramElementSerializer,
            //                                CustomPriceComponentSerializer,
            //                                CustomProgramRestrictionsSerializer,
            //                                CustomEnergyMixSerializer,
            //                                CustomEnergySourceSerializer,
            //                                CustomEnvironmentalImpactSerializer
            //                            )
            //                  )
            //          ),
            //          EventTrackingId,
            //          CurrentUserId,
            //          CancellationToken
            //      );

            return RemoveResult<IEnumerable<Program>>.Success(
                       EventTrackingId,
                       removedPrograms
                   );

        }

        #endregion

        #region RemoveAllPrograms     (IncludeProgramIds, ...)

        /// <summary>
        /// Remove all matching programs.
        /// </summary>
        /// <param name="IncludeProgramIds">The program identification filter.</param>
        /// <param name="SkipNotifications">Skip sending notifications.</param>
        public async Task<RemoveResult<IEnumerable<Program>>>

            RemoveAllPrograms(Func<Program_Id, Boolean>  IncludeProgramIds,
                              Boolean                    SkipNotifications   = false,
                              EventTracking_Id?          EventTrackingId     = null,
                              User_Id?                   CurrentUserId       = null,
                              CancellationToken          CancellationToken   = default)
        {

            EventTrackingId ??= EventTracking_Id.New;

            var removedPrograms = new List<Program>();

            foreach (var program in programs.Values)
            {
                if (IncludeProgramIds(Program_Id.Parse(program.Id.ToString())))
                    removedPrograms.Add(program);
            }

            foreach (var program in removedPrograms)
                programs.TryRemove(Program_Id.Parse(program.Id.ToString()), out _);


            //await LogAsset(
            //          CommonBaseAPI.removeAllPrograms,
            //          new JArray(
            //              removedPrograms.Select(
            //                  program => program.ToJSON(
            //                                true,
            //                                true,
            //                                CustomProgramSerializer,
            //                                CustomDisplayTextSerializer,
            //                                CustomPriceSerializer,
            //                                CustomProgramElementSerializer,
            //                                CustomPriceComponentSerializer,
            //                                CustomProgramRestrictionsSerializer,
            //                                CustomEnergyMixSerializer,
            //                                CustomEnergySourceSerializer,
            //                                CustomEnvironmentalImpactSerializer
            //                            )
            //                  )
            //          ),
            //          EventTrackingId,
            //          CurrentUserId,
            //          CancellationToken
            //      );

            return RemoveResult<IEnumerable<Program>>.Success(
                       EventTrackingId,
                       removedPrograms
                   );

        }

        #endregion


        #region ProgramExists (ProgramId)

        public Boolean ProgramExists(Program_Id ProgramId)
            => programs.ContainsKey(ProgramId);

        #endregion

        #region GetProgram    (ProgramId)

        public Program? GetProgram(Program_Id ProgramId)
        {

            if (programs.TryGetValue(ProgramId, out var program))
                return program;

            return null;

        }

        #endregion

        #region TryGetProgram (ProgramId, out Program)

        public Boolean TryGetProgram(Program_Id                        ProgramId,
                                     [NotNullWhen(true)] out Program?  Program)
        {

            if (programs.TryGetValue(ProgramId, out var program))
            {
                Program = program;
                return true;
            }

            Program = null;
            return false;

        }

        #endregion

        #region GetPrograms   (IncludeProgram = null)

        public IEnumerable<Program> GetPrograms(Func<Program, Boolean>? IncludeProgram = null)
        {

            if (IncludeProgram is null)
                return programs.Values;


            var selectedPrograms = new List<Program>();

            foreach (var program in programs.Values)
            {
                if (IncludeProgram(program))
                    selectedPrograms.Add(program);
            }

            return selectedPrograms;

        }

        #endregion

        #endregion


        #region (private) RegisterURLTemplates()

        #region Manage HTTP Resources

        #region (protected override) GetResourceStream      (ResourceName)

        protected override Stream? GetResourceStream(String ResourceName)

            => GetResourceStream(ResourceName,
                                 new Tuple<String, Assembly>(OpenADRAPI.HTTPRoot, typeof(OpenADRAPI).Assembly),
                                 new Tuple<String, Assembly>(HTTPAPI.   HTTPRoot, typeof(HTTPAPI).   Assembly));

        #endregion

        #region (protected override) GetResourceMemoryStream(ResourceName)

        protected override MemoryStream? GetResourceMemoryStream(String ResourceName)

            => GetResourceMemoryStream(ResourceName,
                                       new Tuple<String, Assembly>(OpenADRAPI.HTTPRoot, typeof(OpenADRAPI).Assembly),
                                       new Tuple<String, Assembly>(HTTPAPI.   HTTPRoot, typeof(HTTPAPI).   Assembly));

        #endregion

        #region (protected override) GetResourceString      (ResourceName)

        protected override String GetResourceString(String ResourceName)

            => GetResourceString(ResourceName,
                                 new Tuple<String, Assembly>(OpenADRAPI.HTTPRoot, typeof(OpenADRAPI).Assembly),
                                 new Tuple<String, Assembly>(HTTPAPI.   HTTPRoot, typeof(HTTPAPI).   Assembly));

        #endregion

        #region (protected override) GetResourceBytes       (ResourceName)

        protected override Byte[] GetResourceBytes(String ResourceName)

            => GetResourceBytes(ResourceName,
                                new Tuple<String, Assembly>(OpenADRAPI.HTTPRoot, typeof(OpenADRAPI).Assembly),
                                new Tuple<String, Assembly>(HTTPAPI.   HTTPRoot, typeof(HTTPAPI).   Assembly));

        #endregion

        #region (protected override) MixWithHTMLTemplate    (ResourceName)

        protected override String MixWithHTMLTemplate(String ResourceName)

            => MixWithHTMLTemplate(ResourceName,
                                   new Tuple<String, Assembly>(OpenADRAPI.HTTPRoot, typeof(OpenADRAPI).Assembly),
                                   new Tuple<String, Assembly>(HTTPAPI.   HTTPRoot, typeof(HTTPAPI).   Assembly));

        #endregion

        #region (protected override) MixWithHTMLTemplate    (Template, ResourceName, Content = null)

        protected override String MixWithHTMLTemplate(String   Template,
                                                      String   ResourceName,
                                                      String?  Content   = null)

            => MixWithHTMLTemplate(Template,
                                   ResourceName,
                                   [
                                       new Tuple<String, Assembly>(OpenADRAPI.HTTPRoot, typeof(OpenADRAPI).Assembly),
                                       new Tuple<String, Assembly>(HTTPAPI.   HTTPRoot, typeof(HTTPAPI).   Assembly)
                                   ],
                                   Content);

        #endregion

        #endregion

        private void RegisterURLTemplates()
        {

            HTTPServer.AddAuth(request => {

                #region Allow some URLs for anonymous access...

                if (request.Path.Equals(URLPathPrefix))
                {
                    return Anonymous;
                }

                #endregion

                return null;

            });


            #region ~/programs

            #region OPTIONS  ~/programs

            AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "programs",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServer.DefaultServerName,
                            Date                        = Timestamp.Now,
                            Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.POST ],
                            AccessControlAllowOrigin    = "*",
                            AccessControlAllowMethods   = [ "OPTIONS", "GET", "POST" ],
                            AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                            AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ],
                            Connection                  = ConnectionType.Close
                        }.AsImmutable
                    )

            );

            #endregion

            #region GET      ~/programs

            AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.GET,
                URLPathPrefix + "programs",
                HTTPContentType.Application.JSON_UTF8,
                HTTPDelegate: request => {

                    #region Check access token

                    //if (request.LocalAccessInfo.IsNot(Role.EMSP) == true ||
                    //    request.LocalAccessInfo?.Status != AccessStatus.ALLOWED)
                    //{

                    //    return Task.FromResult(
                    //        new HTTPResponse.Builder(request) {
                    //            HTTPStatusCode              = HTTPStatusCode.Forbidden,
                    //            AccessControlAllowOrigin    = "*",
                    //            AccessControlAllowMethods   = [ "OPTIONS", "GET", "POST" ],
                    //            AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                    //            AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                    //        });

                    //}

                    #endregion


                    var targetType        = request.QueryString.GetString  ("targetType");
                    var targetValues      = request.QueryString.GetStrings ("targetValues");
                    var skip              = request.QueryString.GetUInt32  ("skip");
                    var limit             = request.QueryString.GetUInt32  ("limit");
                    var from              = request.QueryString.GetDateTime("from");
                    var to                = request.QueryString.GetDateTime("to");

                    var matchFilter       = request.QueryString.CreateStringFilter<Program>(
                                                "match",
                                                (program, pattern) => program.ProgramName.     Contains(pattern) ||
                                                                      program.ProgramLongName?.Contains(pattern) == true
                                            );

                                                                      //ToDo: Filter to NOT show all locations to everyone!
                    var allPrograms       = GetPrograms().//location => Request.AccessInfo.Value.Roles.Any(role => role.CountryCode == location.CountryCode &&
                                                                      //                                                       role.PartyId     == location.PartyId)).
                                                       ToArray();

                    var filteredPrograms  = allPrograms.Where(matchFilter).
                                                        Where(program => !from.HasValue || program.LastModification >  from.Value).
                                                        Where(program => !to.  HasValue || program.LastModification <= to.  Value).
                                                        ToArray();


                    return Task.FromResult(
                               new HTTPResponse.Builder(request) {
                                   HTTPStatusCode              = HTTPStatusCode.OK,
                                   Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.POST ],
                                   Content                     = new JArray(
                                                                     filteredPrograms.
                                                                         OrderBy(program => program.Created).
                                                                         SkipTakeFilter(skip, limit).
                                                                         Select (program => program.ToJSON(CustomProgramSerializer,
                                                                                                           CustomIntervalPeriodSerializer,
                                                                                                           CustomEventPayloadDescriptorSerializer,
                                                                                                           CustomReportPayloadDescriptorSerializer,
                                                                                                           CustomValuesMapSerializer))
                                                                 ).ToUTF8Bytes(),
                                   AccessControlAllowOrigin    = "*",
                                   AccessControlAllowMethods   = [ "OPTIONS", "GET", "POST" ],
                                   AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                                   AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ],
                                   Connection                  = ConnectionType.Close
                               }.

                               // The overall number of locations
                               Set("X-Total-Count",     allPrograms.     Length).

                               // The number of locations matching search filters
                               Set("X-Filtered-Count",  filteredPrograms.Length).

                               // The maximum number of locations that the server WILL return within a single request
                               Set("X-Limit",           allPrograms.     Length).AsImmutable

                           );

                }

            );

            #endregion

            #region POST     ~/programs

            AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.POST,
                URLPathPrefix + "programs",
                HTTPContentType.Application.JSON_UTF8,
                HTTPDelegate: async request => {

                    #region Check access token

                    //if (request.LocalAccessInfo.IsNot(Role.EMSP) == true ||
                    //    request.LocalAccessInfo?.Status != AccessStatus.ALLOWED)
                    //{

                    //    return Task.FromResult(
                    //        new OCPIResponse.Builder(request) {
                    //            StatusCode           = 2000,
                    //            StatusMessage        = "Invalid or blocked access token!",
                    //            HTTPResponseBuilder  = new HTTPResponse.Builder(request.HTTPRequest) {
                    //                HTTPStatusCode             = HTTPStatusCode.Forbidden,
                    //                AccessControlAllowMethods  = [ "OPTIONS", "GET" ],
                    //                AccessControlAllowHeaders  = [ "Authorization" ]
                    //            }
                    //        });

                    //}

                    #endregion

                    #region Parse new or updated tariff

                    if (!request.TryParseJSONObjectRequestBody(out var programJSON, out var httpResponseBuilder))
                        return httpResponseBuilder;

                    if (!Program.TryParse(programJSON,
                                          out var newProgram,
                                          out var errorResponse))
                    {

                        return new HTTPResponse.Builder(request) {
                                   HTTPStatusCode              = HTTPStatusCode.BadRequest,
                                   Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET ],
                                   AccessControlAllowOrigin    = "*",
                                   AccessControlAllowMethods   = [ "OPTIONS", "GET" ],
                                   AccessControlAllowHeaders   = [ "Authorization" ],
                                   AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ],
                                   Connection                  = ConnectionType.Close
                               };

                    }

                    #endregion


                    var result = await AddProgram(
                                           newProgram,
                                           SkipNotifications:  true,
                                           EventTrackingId:    request.EventTrackingId,
                                           //CurrentUserId:      request.UserId,
                                           CancellationToken:  request.CancellationToken
                                       );


                    return result.IsSuccessAndDataNotNull(out var data)

                               ? new HTTPResponse.Builder(request) {
                                     HTTPStatusCode              = HTTPStatusCode.OK,
                                     Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET ],
                                     AccessControlAllowOrigin    = "*",
                                     AccessControlAllowMethods   = [ "OPTIONS", "GET" ],
                                     AccessControlAllowHeaders   = [ "Authorization" ],
                                     AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ],
                                     Connection                  = ConnectionType.Close
                                 }.AsImmutable

                               : new HTTPResponse.Builder(request) {
                                     HTTPStatusCode              = HTTPStatusCode.BadRequest,
                                     Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET ],
                                     AccessControlAllowOrigin    = "*",
                                     AccessControlAllowMethods   = [ "OPTIONS", "GET" ],
                                     AccessControlAllowHeaders   = [ "Authorization" ],
                                     AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ],
                                     Connection                  = ConnectionType.Close
                                 }.AsImmutable;

                }

            );

            #endregion

            #endregion

            #region ~/programs/{programId}

            #region OPTIONS  ~/programs/{programId}

            // ------------------------------------------------------------
            // curl -v -X OPTIONS http://127.0.0.1:5500/programs/program1
            // ------------------------------------------------------------
            AddMethodCallback(

                Hostname,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "programs/{programId}",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServer.DefaultServerName,
                            Date                        = Timestamp.Now,
                            Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.PUT, HTTPMethod.DELETE],
                            AccessControlAllowOrigin    = "*",
                            AccessControlAllowMethods   = [ "OPTIONS", "GET", "PUT", "DELETE" ],
                            AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                            AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ],
                            Connection                  = ConnectionType.Close
                        }.AsImmutable)

            );

            #endregion

            #region GET      ~/programs/{programId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/programs/program1
            // -------------------------------------------------------------------------------
            AddMethodCallback(

                Hostname,
                HTTPMethod.GET,
                URLPathPrefix + "programs/{programId}",
                HTTPContentType.Application.JSON_UTF8,
                HTTPDelegate: async request => {

                    #region Check access token

                    //if (request.LocalAccessInfo.IsNot(Role.EMSP) == true ||
                    //    request.LocalAccessInfo?.Status != AccessStatus.ALLOWED)
                    //{

                    //    return Task.FromResult(
                    //        new HTTPResponse.Builder(request) {
                    //            HTTPStatusCode              = HTTPStatusCode.Forbidden,
                    //            AccessControlAllowOrigin    = "*",
                    //            AccessControlAllowMethods   = [ "OPTIONS", "GET", "POST" ],
                    //            AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                    //            AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                    //        });

                    //}

                    #endregion

                    #region Try to get the requested program

                    if (!request.ParseProgramId(out var programId,
                                                out var httpResponseBuilder))
                    {
                        return httpResponseBuilder;
                    }

                    if (!programs.TryGetValue(programId.Value, out var program))
                    {

                        return new HTTPResponse.Builder(request) {
                                   HTTPStatusCode             = HTTPStatusCode.NotFound,
                                   AccessControlAllowHeaders  = [ "Authorization" ],
                                   Connection                 = ConnectionType.Close
                               };

                    }

                    #endregion


                    return new HTTPResponse.Builder(request) {
                               HTTPStatusCode             = HTTPStatusCode.OK,
                               Server                     = HTTPServer.DefaultServerName,
                               Date                       = Timestamp.Now,
                               Allow                      = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.PUT, HTTPMethod.DELETE],
                               AccessControlAllowOrigin   = "*",
                               AccessControlAllowMethods  = [ "OPTIONS", "GET", "PUT", "DELETE" ],
                               ContentType                = HTTPContentType.Application.JSON_UTF8,
                               Content                    = new JArray().ToUTF8Bytes(),
                               Connection                 = ConnectionType.Close
                           }.AsImmutable;

                }

            );

            #endregion

            #region PUT      ~/programs/{programId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/programs/program1
            // -------------------------------------------------------------------------------
            AddMethodCallback(

                Hostname,
                HTTPMethod.PUT,
                URLPathPrefix + "programs/{programId}",
                HTTPContentType.Application.JSON_UTF8,
                HTTPDelegate: async request => {

                    #region Check access token

                    //if (request.LocalAccessInfo.IsNot(Role.EMSP) == true ||
                    //    request.LocalAccessInfo?.Status != AccessStatus.ALLOWED)
                    //{

                    //    return Task.FromResult(
                    //        new HTTPResponse.Builder(request) {
                    //            HTTPStatusCode              = HTTPStatusCode.Forbidden,
                    //            AccessControlAllowOrigin    = "*",
                    //            AccessControlAllowMethods   = [ "OPTIONS", "GET", "POST" ],
                    //            AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                    //            AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                    //        });

                    //}

                    #endregion

                    #region Try to get the requested program identification

                    if (!request.ParseProgramId(out var programIdURL,
                                                out var httpResponseBuilder))
                    {
                        return httpResponseBuilder;
                    }

                    #endregion

                    #region Try to parse the new program

                    if (!request.TryParseJSONObjectRequestBody(out var programJSON, out httpResponseBuilder))
                        return httpResponseBuilder;

                    if (!Program.TryParse(programJSON,
                                          out var newOrUpdatedProgram,
                                          out var errorResponse))
                    {

                        return new HTTPResponse.Builder(request) {
                                   HTTPStatusCode             = HTTPStatusCode.BadRequest,
                                   AccessControlAllowHeaders  = [ "Authorization" ],
                                   Content                    = new Problem(
                                                                    nameof(Program),
                                                                    errorResponse
                                                                ).ToJSON().ToUTF8Bytes(),
                                   ContentType                = HTTPContentType.Application.JSON_UTF8,
                                   Connection                 = ConnectionType.Close
                               };

                    }

                    if (newOrUpdatedProgram.Id.ToString() != programIdURL.ToString())
                    {

                        return new HTTPResponse.Builder(request) {
                                   HTTPStatusCode             = HTTPStatusCode.BadRequest,
                                   AccessControlAllowHeaders  = [ "Authorization" ],
                                   Content                    = new Problem(
                                                                    nameof(Program),
                                                                    $"The programId '{programIdURL}' within the URL does not match the programId '{newOrUpdatedProgram.Id}' in the parsed JSON request body!"
                                                                ).ToJSON().ToUTF8Bytes(),
                                   ContentType                = HTTPContentType.Application.JSON_UTF8,
                                   Connection                 = ConnectionType.Close
                               };

                    }

                    #endregion


                    var result = await AddOrUpdateProgram(
                                           newOrUpdatedProgram,
                                           EventTrackingId:    request.EventTrackingId,
                                           CurrentUserId:      null, //request.LocalAccessInfo?.UserId,
                                           CancellationToken:  request.CancellationToken
                                       );


                    return result.IsSuccessAndDataNotNull(out var data)

                        ? new HTTPResponse.Builder(request) {
                                   HTTPStatusCode             = HTTPStatusCode.OK,
                                   Server                     = HTTPServer.DefaultServerName,
                                   Date                       = Timestamp.Now,
                                   Allow                      = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.PUT, HTTPMethod.DELETE],
                                   AccessControlAllowOrigin   = "*",
                                   AccessControlAllowMethods  = [ "OPTIONS", "GET", "PUT", "DELETE" ],
                                   ContentType                = HTTPContentType.Application.JSON_UTF8,
                                   Content                    = newOrUpdatedProgram.ToJSON().ToUTF8Bytes(),
                                   Connection                 = ConnectionType.Close
                               }.AsImmutable

                        : new HTTPResponse.Builder(request) {
                                   HTTPStatusCode             = HTTPStatusCode.Conflict,
                                   Server                     = HTTPServer.DefaultServerName,
                                   Date                       = Timestamp.Now,
                                   Allow                      = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.PUT, HTTPMethod.DELETE],
                                   AccessControlAllowOrigin   = "*",
                                   AccessControlAllowMethods  = [ "OPTIONS", "GET", "PUT", "DELETE" ],
                                   ContentType                = HTTPContentType.Application.JSON_UTF8,
                                   Content                    = new Problem(
                                                                    nameof(Program),
                                                                    "conflict!"
                                                                ).ToJSON().ToUTF8Bytes(),
                                   Connection                 = ConnectionType.Close
                               }.AsImmutable;

                }

            );

            #endregion

            #region DELETE   ~/programs/{programId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/programs/program1
            // -------------------------------------------------------------------------------
            AddMethodCallback(

                Hostname,
                HTTPMethod.DELETE,
                URLPathPrefix + "programs/{programId}",
                HTTPContentType.Application.JSON_UTF8,
                HTTPDelegate: async request => {

                    #region Check access token

                    //if (request.LocalAccessInfo.IsNot(Role.EMSP) == true ||
                    //    request.LocalAccessInfo?.Status != AccessStatus.ALLOWED)
                    //{

                    //    return Task.FromResult(
                    //        new HTTPResponse.Builder(request) {
                    //            HTTPStatusCode              = HTTPStatusCode.Forbidden,
                    //            AccessControlAllowOrigin    = "*",
                    //            AccessControlAllowMethods   = [ "OPTIONS", "GET", "POST" ],
                    //            AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                    //            AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                    //        });

                    //}

                    #endregion

                    #region Try to get the requested program

                    if (!request.ParseProgramId(out var programId,
                                                out var httpResponseBuilder))
                    {
                        return httpResponseBuilder;
                    }

                    #endregion


                    var result = await RemoveProgram(
                                           programId.Value,
                                           EventTrackingId:    request.EventTrackingId,
                                           CurrentUserId:      null, //request.LocalAccessInfo?.UserId,
                                           CancellationToken:  request.CancellationToken
                                       );


                    return result.IsSuccessAndDataNotNull(out var data)

                        ? new HTTPResponse.Builder(request) {
                              HTTPStatusCode             = HTTPStatusCode.OK,
                              Server                     = HTTPServer.DefaultServerName,
                              Date                       = Timestamp.Now,
                              Allow                      = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.PUT, HTTPMethod.DELETE],
                              AccessControlAllowOrigin   = "*",
                              AccessControlAllowMethods  = [ "OPTIONS", "GET", "PUT", "DELETE" ],
                              ContentType                = HTTPContentType.Application.JSON_UTF8,
                              Content                    = new JArray().ToUTF8Bytes(),
                              Connection                 = ConnectionType.Close
                          }.AsImmutable

                        : new HTTPResponse.Builder(request) {
                              HTTPStatusCode             = HTTPStatusCode.NotFound,
                              Server                     = HTTPServer.DefaultServerName,
                              Date                       = Timestamp.Now,
                              Allow                      = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.PUT, HTTPMethod.DELETE],
                              AccessControlAllowOrigin   = "*",
                              AccessControlAllowMethods  = [ "OPTIONS", "GET", "PUT", "DELETE" ],
                              Connection                 = ConnectionType.Close
                          }.AsImmutable;

                }

            );

            #endregion

            #endregion


            #region ~/reports

            #region OPTIONS  ~/reports

            AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "reports",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServer.DefaultServerName,
                            Date                        = Timestamp.Now,
                            Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.POST ],
                            AccessControlAllowOrigin    = "*",
                            AccessControlAllowMethods   = [ "OPTIONS", "GET", "POST" ],
                            AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                            AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                        }.AsImmutable
                    )

            );

            #endregion

            #region GET      ~/reports

            AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.GET,
                URLPathPrefix + "reports",
                HTTPContentType.Application.JSON_UTF8,
                HTTPDelegate: request => {

                    #region Check access token

                    //if (request.LocalAccessInfo.IsNot(Role.EMSP) == true ||
                    //    request.LocalAccessInfo?.Status != AccessStatus.ALLOWED)
                    //{

                    //    return Task.FromResult(
                    //        new OCPIResponse.Builder(request) {
                    //            StatusCode           = 2000,
                    //            StatusMessage        = "Invalid or blocked access token!",
                    //            HTTPResponseBuilder  = new HTTPResponse.Builder(request.HTTPRequest) {
                    //                HTTPStatusCode             = HTTPStatusCode.Forbidden,
                    //                AccessControlAllowMethods  = [ "OPTIONS", "GET" ],
                    //                AccessControlAllowHeaders  = [ "Authorization" ]
                    //            }
                    //        });

                    //}

                    #endregion



                    return Task.FromResult(
                               new HTTPResponse.Builder(request) {
                                   HTTPStatusCode              = HTTPStatusCode.OK,
                                   Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET ],
                                   AccessControlAllowMethods   = [ "OPTIONS", "GET" ],
                                   AccessControlAllowHeaders   = [ "Authorization" ],
                                   AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                               }.AsImmutable
                           );

            });

            #endregion

            #region POST     ~/reports

            AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.POST,
                URLPathPrefix + "reports",
                HTTPContentType.Application.JSON_UTF8,
                HTTPDelegate: request => {

                    #region Check access token

                    //if (request.LocalAccessInfo.IsNot(Role.EMSP) == true ||
                    //    request.LocalAccessInfo?.Status != AccessStatus.ALLOWED)
                    //{

                    //    return Task.FromResult(
                    //        new OCPIResponse.Builder(request) {
                    //            StatusCode           = 2000,
                    //            StatusMessage        = "Invalid or blocked access token!",
                    //            HTTPResponseBuilder  = new HTTPResponse.Builder(request.HTTPRequest) {
                    //                HTTPStatusCode             = HTTPStatusCode.Forbidden,
                    //                AccessControlAllowMethods  = [ "OPTIONS", "GET" ],
                    //                AccessControlAllowHeaders  = [ "Authorization" ]
                    //            }
                    //        });

                    //}

                    #endregion



                    return Task.FromResult(
                               new HTTPResponse.Builder(request) {
                                   HTTPStatusCode              = HTTPStatusCode.OK,
                                   Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET ],
                                   AccessControlAllowMethods   = [ "OPTIONS", "GET" ],
                                   AccessControlAllowHeaders   = [ "Authorization" ],
                                   AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                               }.AsImmutable
                           );

            });

            #endregion

            #endregion

            #region ~/reports/{reportId}

            #region OPTIONS  ~/reports/{reportId}

            // ----------------------------------------------------------
            // curl -v -X OPTIONS http://127.0.0.1:5500/reports/versions
            // ----------------------------------------------------------
            AddMethodCallback(

                Hostname,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "reports/{reportId}",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServer.DefaultServerName,
                            Date                        = Timestamp.Now,
                            Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.PUT, HTTPMethod.DELETE],
                            AccessControlAllowOrigin    = "*",
                            AccessControlAllowMethods   = [ "OPTIONS", "GET", "PUT", "DELETE" ],
                            AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                            AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                        }.AsImmutable)

            );

            #endregion

            #region GET      ~/reports/{reportId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/reports/versions
            // -------------------------------------------------------------------------------
            AddMethodCallback(Hostname,
                              HTTPMethod.GET,
                              URLPathPrefix + "reports/{reportId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServer.DefaultServerName,
                                          Date                          = Timestamp.Now,
                                          AccessControlAllowOrigin      = "*",
                                          AccessControlAllowMethods     = [ "GET", "OPTIONS" ],
                                          AccessControlAllowHeaders     = [ "Content-Type", "Accept", "Authorization" ],
                                          ETag                          = "1",
                                          ContentType                   = HTTPContentType.Application.JSON_UTF8,
                                          Content                       = new JArray().
                                                                              ToUTF8Bytes(),
                                          X_ExpectedTotalNumberOfItems  = 0
                                      }.AsImmutable);

                              });

            #endregion

            #region PUT      ~/reports/{reportId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/reports/versions
            // -------------------------------------------------------------------------------
            AddMethodCallback(Hostname,
                              HTTPMethod.PUT,
                              URLPathPrefix + "reports/{reportId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServer.DefaultServerName,
                                          Date                          = Timestamp.Now,
                                          AccessControlAllowOrigin      = "*",
                                          AccessControlAllowMethods     = [ "GET", "OPTIONS" ],
                                          AccessControlAllowHeaders     = [ "Content-Type", "Accept", "Authorization" ],
                                          ETag                          = "1",
                                          ContentType                   = HTTPContentType.Application.JSON_UTF8,
                                          Content                       = new JArray().
                                                                              ToUTF8Bytes(),
                                          X_ExpectedTotalNumberOfItems  = 0
                                      }.AsImmutable);

                              });

            #endregion

            #region DELETE   ~/reports/{reportId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/reports/versions
            // -------------------------------------------------------------------------------
            AddMethodCallback(Hostname,
                              HTTPMethod.DELETE,
                              URLPathPrefix + "reports/{reportId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServer.DefaultServerName,
                                          Date                          = Timestamp.Now,
                                          AccessControlAllowOrigin      = "*",
                                          AccessControlAllowMethods     = [ "GET", "OPTIONS" ],
                                          AccessControlAllowHeaders     = [ "Content-Type", "Accept", "Authorization" ],
                                          ETag                          = "1",
                                          ContentType                   = HTTPContentType.Application.JSON_UTF8,
                                          Content                       = new JArray().
                                                                              ToUTF8Bytes(),
                                          X_ExpectedTotalNumberOfItems  = 0
                                      }.AsImmutable);

                              });

            #endregion

            #endregion


            #region ~/events

            #region OPTIONS  ~/events

            AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "events",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServer.DefaultServerName,
                            Date                        = Timestamp.Now,
                            Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.POST ],
                            AccessControlAllowOrigin    = "*",
                            AccessControlAllowMethods   = [ "OPTIONS", "GET", "POST" ],
                            AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                            AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                        }.AsImmutable
                    )

            );

            #endregion

            #region GET      ~/events

            AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.GET,
                URLPathPrefix + "events",
                HTTPContentType.Application.JSON_UTF8,
                HTTPDelegate: request => {

                    #region Check access token

                    //if (request.LocalAccessInfo.IsNot(Role.EMSP) == true ||
                    //    request.LocalAccessInfo?.Status != AccessStatus.ALLOWED)
                    //{

                    //    return Task.FromResult(
                    //        new OCPIResponse.Builder(request) {
                    //            StatusCode           = 2000,
                    //            StatusMessage        = "Invalid or blocked access token!",
                    //            HTTPResponseBuilder  = new HTTPResponse.Builder(request.HTTPRequest) {
                    //                HTTPStatusCode             = HTTPStatusCode.Forbidden,
                    //                AccessControlAllowMethods  = [ "OPTIONS", "GET" ],
                    //                AccessControlAllowHeaders  = [ "Authorization" ]
                    //            }
                    //        });

                    //}

                    #endregion



                    return Task.FromResult(
                               new HTTPResponse.Builder(request) {
                                   HTTPStatusCode              = HTTPStatusCode.OK,
                                   Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET ],
                                   AccessControlAllowMethods   = [ "OPTIONS", "GET" ],
                                   AccessControlAllowHeaders   = [ "Authorization" ],
                                   AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                               }.AsImmutable
                           );

            });

            #endregion

            #region POST     ~/events

            AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.POST,
                URLPathPrefix + "events",
                HTTPContentType.Application.JSON_UTF8,
                HTTPDelegate: request => {

                    #region Check access token

                    //if (request.LocalAccessInfo.IsNot(Role.EMSP) == true ||
                    //    request.LocalAccessInfo?.Status != AccessStatus.ALLOWED)
                    //{

                    //    return Task.FromResult(
                    //        new OCPIResponse.Builder(request) {
                    //            StatusCode           = 2000,
                    //            StatusMessage        = "Invalid or blocked access token!",
                    //            HTTPResponseBuilder  = new HTTPResponse.Builder(request.HTTPRequest) {
                    //                HTTPStatusCode             = HTTPStatusCode.Forbidden,
                    //                AccessControlAllowMethods  = [ "OPTIONS", "GET" ],
                    //                AccessControlAllowHeaders  = [ "Authorization" ]
                    //            }
                    //        });

                    //}

                    #endregion



                    return Task.FromResult(
                               new HTTPResponse.Builder(request) {
                                   HTTPStatusCode              = HTTPStatusCode.OK,
                                   Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET ],
                                   AccessControlAllowMethods   = [ "OPTIONS", "GET" ],
                                   AccessControlAllowHeaders   = [ "Authorization" ],
                                   AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                               }.AsImmutable
                           );

            });

            #endregion

            #endregion

            #region ~/events/{eventId}

            #region OPTIONS  ~/events/{eventId}

            // ----------------------------------------------------------
            // curl -v -X OPTIONS http://127.0.0.1:5500/events/versions
            // ----------------------------------------------------------
            AddMethodCallback(

                Hostname,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "events/{eventId}",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServer.DefaultServerName,
                            Date                        = Timestamp.Now,
                            Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.PUT, HTTPMethod.DELETE],
                            AccessControlAllowOrigin    = "*",
                            AccessControlAllowMethods   = [ "OPTIONS", "GET", "PUT", "DELETE" ],
                            AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                            AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                        }.AsImmutable)

            );

            #endregion

            #region GET      ~/events/{eventId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/events/versions
            // -------------------------------------------------------------------------------
            AddMethodCallback(Hostname,
                              HTTPMethod.GET,
                              URLPathPrefix + "events/{eventId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServer.DefaultServerName,
                                          Date                          = Timestamp.Now,
                                          AccessControlAllowOrigin      = "*",
                                          AccessControlAllowMethods     = [ "GET", "OPTIONS" ],
                                          AccessControlAllowHeaders     = [ "Content-Type", "Accept", "Authorization" ],
                                          ETag                          = "1",
                                          ContentType                   = HTTPContentType.Application.JSON_UTF8,
                                          Content                       = new JArray().
                                                                              ToUTF8Bytes(),
                                          X_ExpectedTotalNumberOfItems  = 0
                                      }.AsImmutable);

                              });

            #endregion

            #region PUT      ~/events/{eventId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/events/versions
            // -------------------------------------------------------------------------------
            AddMethodCallback(Hostname,
                              HTTPMethod.PUT,
                              URLPathPrefix + "events/{eventId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServer.DefaultServerName,
                                          Date                          = Timestamp.Now,
                                          AccessControlAllowOrigin      = "*",
                                          AccessControlAllowMethods     = [ "GET", "OPTIONS" ],
                                          AccessControlAllowHeaders     = [ "Content-Type", "Accept", "Authorization" ],
                                          ETag                          = "1",
                                          ContentType                   = HTTPContentType.Application.JSON_UTF8,
                                          Content                       = new JArray().
                                                                              ToUTF8Bytes(),
                                          X_ExpectedTotalNumberOfItems  = 0
                                      }.AsImmutable);

                              });

            #endregion

            #region DELETE   ~/events/{eventId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/events/versions
            // -------------------------------------------------------------------------------
            AddMethodCallback(Hostname,
                              HTTPMethod.DELETE,
                              URLPathPrefix + "events/{eventId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServer.DefaultServerName,
                                          Date                          = Timestamp.Now,
                                          AccessControlAllowOrigin      = "*",
                                          AccessControlAllowMethods     = [ "GET", "OPTIONS" ],
                                          AccessControlAllowHeaders     = [ "Content-Type", "Accept", "Authorization" ],
                                          ETag                          = "1",
                                          ContentType                   = HTTPContentType.Application.JSON_UTF8,
                                          Content                       = new JArray().
                                                                              ToUTF8Bytes(),
                                          X_ExpectedTotalNumberOfItems  = 0
                                      }.AsImmutable);

                              });

            #endregion

            #endregion


            #region ~/subscriptions

            #region OPTIONS  ~/subscriptions

            AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "subscriptions",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServer.DefaultServerName,
                            Date                        = Timestamp.Now,
                            Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.POST ],
                            AccessControlAllowOrigin    = "*",
                            AccessControlAllowMethods   = [ "OPTIONS", "GET", "POST" ],
                            AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                            AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                        }.AsImmutable
                    )

            );

            #endregion

            #region GET      ~/subscriptions

            AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.GET,
                URLPathPrefix + "subscriptions",
                HTTPContentType.Application.JSON_UTF8,
                HTTPDelegate: request => {

                    #region Check access token

                    //if (request.LocalAccessInfo.IsNot(Role.EMSP) == true ||
                    //    request.LocalAccessInfo?.Status != AccessStatus.ALLOWED)
                    //{

                    //    return Task.FromResult(
                    //        new OCPIResponse.Builder(request) {
                    //            StatusCode           = 2000,
                    //            StatusMessage        = "Invalid or blocked access token!",
                    //            HTTPResponseBuilder  = new HTTPResponse.Builder(request.HTTPRequest) {
                    //                HTTPStatusCode             = HTTPStatusCode.Forbidden,
                    //                AccessControlAllowMethods  = [ "OPTIONS", "GET" ],
                    //                AccessControlAllowHeaders  = [ "Authorization" ]
                    //            }
                    //        });

                    //}

                    #endregion



                    return Task.FromResult(
                               new HTTPResponse.Builder(request) {
                                   HTTPStatusCode              = HTTPStatusCode.OK,
                                   Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET ],
                                   AccessControlAllowMethods   = [ "OPTIONS", "GET" ],
                                   AccessControlAllowHeaders   = [ "Authorization" ],
                                   AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                               }.AsImmutable
                           );

            });

            #endregion

            #region POST     ~/subscriptions

            AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.POST,
                URLPathPrefix + "subscriptions",
                HTTPContentType.Application.JSON_UTF8,
                HTTPDelegate: request => {

                    #region Check access token

                    //if (request.LocalAccessInfo.IsNot(Role.EMSP) == true ||
                    //    request.LocalAccessInfo?.Status != AccessStatus.ALLOWED)
                    //{

                    //    return Task.FromResult(
                    //        new OCPIResponse.Builder(request) {
                    //            StatusCode           = 2000,
                    //            StatusMessage        = "Invalid or blocked access token!",
                    //            HTTPResponseBuilder  = new HTTPResponse.Builder(request.HTTPRequest) {
                    //                HTTPStatusCode             = HTTPStatusCode.Forbidden,
                    //                AccessControlAllowMethods  = [ "OPTIONS", "GET" ],
                    //                AccessControlAllowHeaders  = [ "Authorization" ]
                    //            }
                    //        });

                    //}

                    #endregion



                    return Task.FromResult(
                               new HTTPResponse.Builder(request) {
                                   HTTPStatusCode              = HTTPStatusCode.OK,
                                   Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET ],
                                   AccessControlAllowMethods   = [ "OPTIONS", "GET" ],
                                   AccessControlAllowHeaders   = [ "Authorization" ],
                                   AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                               }.AsImmutable
                           );

            });

            #endregion

            #endregion

            #region ~/subscriptions/{subscriptionId}

            #region OPTIONS  ~/subscriptions/{subscriptionId}

            // ----------------------------------------------------------
            // curl -v -X OPTIONS http://127.0.0.1:5500/subscriptions/versions
            // ----------------------------------------------------------
            AddMethodCallback(

                Hostname,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "subscriptions/{subscriptionId}",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServer.DefaultServerName,
                            Date                        = Timestamp.Now,
                            Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.PUT, HTTPMethod.DELETE],
                            AccessControlAllowOrigin    = "*",
                            AccessControlAllowMethods   = [ "OPTIONS", "GET", "PUT", "DELETE" ],
                            AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                            AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                        }.AsImmutable)

            );

            #endregion

            #region GET      ~/subscriptions/{subscriptionId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/subscriptions/versions
            // -------------------------------------------------------------------------------
            AddMethodCallback(Hostname,
                              HTTPMethod.GET,
                              URLPathPrefix + "subscriptions/{subscriptionId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServer.DefaultServerName,
                                          Date                          = Timestamp.Now,
                                          AccessControlAllowOrigin      = "*",
                                          AccessControlAllowMethods     = [ "GET", "OPTIONS" ],
                                          AccessControlAllowHeaders     = [ "Content-Type", "Accept", "Authorization" ],
                                          ETag                          = "1",
                                          ContentType                   = HTTPContentType.Application.JSON_UTF8,
                                          Content                       = new JArray().
                                                                              ToUTF8Bytes(),
                                          X_ExpectedTotalNumberOfItems  = 0
                                      }.AsImmutable);

                              });

            #endregion

            #region PUT      ~/subscriptions/{subscriptionId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/subscriptions/versions
            // -------------------------------------------------------------------------------
            AddMethodCallback(Hostname,
                              HTTPMethod.PUT,
                              URLPathPrefix + "subscriptions/{subscriptionId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServer.DefaultServerName,
                                          Date                          = Timestamp.Now,
                                          AccessControlAllowOrigin      = "*",
                                          AccessControlAllowMethods     = [ "GET", "OPTIONS" ],
                                          AccessControlAllowHeaders     = [ "Content-Type", "Accept", "Authorization" ],
                                          ETag                          = "1",
                                          ContentType                   = HTTPContentType.Application.JSON_UTF8,
                                          Content                       = new JArray().
                                                                              ToUTF8Bytes(),
                                          X_ExpectedTotalNumberOfItems  = 0
                                      }.AsImmutable);

                              });

            #endregion

            #region DELETE   ~/subscriptions/{subscriptionId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/subscriptions/versions
            // -------------------------------------------------------------------------------
            AddMethodCallback(Hostname,
                              HTTPMethod.DELETE,
                              URLPathPrefix + "subscriptions/{subscriptionId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServer.DefaultServerName,
                                          Date                          = Timestamp.Now,
                                          AccessControlAllowOrigin      = "*",
                                          AccessControlAllowMethods     = [ "GET", "OPTIONS" ],
                                          AccessControlAllowHeaders     = [ "Content-Type", "Accept", "Authorization" ],
                                          ETag                          = "1",
                                          ContentType                   = HTTPContentType.Application.JSON_UTF8,
                                          Content                       = new JArray().
                                                                              ToUTF8Bytes(),
                                          X_ExpectedTotalNumberOfItems  = 0
                                      }.AsImmutable);

                              });

            #endregion

            #endregion


            #region ~/vens

            #region OPTIONS  ~/vens

            AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "vens",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServer.DefaultServerName,
                            Date                        = Timestamp.Now,
                            Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.POST ],
                            AccessControlAllowOrigin    = "*",
                            AccessControlAllowMethods   = [ "OPTIONS", "GET", "POST" ],
                            AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                            AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                        }.AsImmutable
                    )

            );

            #endregion

            #region GET      ~/vens

            AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.GET,
                URLPathPrefix + "vens",
                HTTPContentType.Application.JSON_UTF8,
                HTTPDelegate: request => {

                    #region Check access token

                    //if (request.LocalAccessInfo.IsNot(Role.EMSP) == true ||
                    //    request.LocalAccessInfo?.Status != AccessStatus.ALLOWED)
                    //{

                    //    return Task.FromResult(
                    //        new OCPIResponse.Builder(request) {
                    //            StatusCode           = 2000,
                    //            StatusMessage        = "Invalid or blocked access token!",
                    //            HTTPResponseBuilder  = new HTTPResponse.Builder(request.HTTPRequest) {
                    //                HTTPStatusCode             = HTTPStatusCode.Forbidden,
                    //                AccessControlAllowMethods  = [ "OPTIONS", "GET" ],
                    //                AccessControlAllowHeaders  = [ "Authorization" ]
                    //            }
                    //        });

                    //}

                    #endregion



                    return Task.FromResult(
                               new HTTPResponse.Builder(request) {
                                   HTTPStatusCode              = HTTPStatusCode.OK,
                                   Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET ],
                                   AccessControlAllowMethods   = [ "OPTIONS", "GET" ],
                                   AccessControlAllowHeaders   = [ "Authorization" ],
                                   AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                               }.AsImmutable
                           );

            });

            #endregion

            #region POST     ~/vens

            AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.POST,
                URLPathPrefix + "vens",
                HTTPContentType.Application.JSON_UTF8,
                HTTPDelegate: request => {

                    #region Check access token

                    //if (request.LocalAccessInfo.IsNot(Role.EMSP) == true ||
                    //    request.LocalAccessInfo?.Status != AccessStatus.ALLOWED)
                    //{

                    //    return Task.FromResult(
                    //        new OCPIResponse.Builder(request) {
                    //            StatusCode           = 2000,
                    //            StatusMessage        = "Invalid or blocked access token!",
                    //            HTTPResponseBuilder  = new HTTPResponse.Builder(request.HTTPRequest) {
                    //                HTTPStatusCode             = HTTPStatusCode.Forbidden,
                    //                AccessControlAllowMethods  = [ "OPTIONS", "GET" ],
                    //                AccessControlAllowHeaders  = [ "Authorization" ]
                    //            }
                    //        });

                    //}

                    #endregion



                    return Task.FromResult(
                               new HTTPResponse.Builder(request) {
                                   HTTPStatusCode              = HTTPStatusCode.OK,
                                   Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET ],
                                   AccessControlAllowMethods   = [ "OPTIONS", "GET" ],
                                   AccessControlAllowHeaders   = [ "Authorization" ],
                                   AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                               }.AsImmutable
                           );

            });

            #endregion

            #endregion

            #region ~/vens/{venId}

            #region OPTIONS  ~/vens/{venId}

            // ----------------------------------------------------------
            // curl -v -X OPTIONS http://127.0.0.1:5500/vens/versions
            // ----------------------------------------------------------
            AddMethodCallback(

                Hostname,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "vens/{venId}",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServer.DefaultServerName,
                            Date                        = Timestamp.Now,
                            Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.PUT, HTTPMethod.DELETE],
                            AccessControlAllowOrigin    = "*",
                            AccessControlAllowMethods   = [ "OPTIONS", "GET", "PUT", "DELETE" ],
                            AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                            AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                        }.AsImmutable)

            );

            #endregion

            #region GET      ~/vens/{venId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/vens/versions
            // -------------------------------------------------------------------------------
            AddMethodCallback(Hostname,
                              HTTPMethod.GET,
                              URLPathPrefix + "vens/{venId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServer.DefaultServerName,
                                          Date                          = Timestamp.Now,
                                          AccessControlAllowOrigin      = "*",
                                          AccessControlAllowMethods     = [ "GET", "OPTIONS" ],
                                          AccessControlAllowHeaders     = [ "Content-Type", "Accept", "Authorization" ],
                                          ETag                          = "1",
                                          ContentType                   = HTTPContentType.Application.JSON_UTF8,
                                          Content                       = new JArray().
                                                                              ToUTF8Bytes(),
                                          X_ExpectedTotalNumberOfItems  = 0
                                      }.AsImmutable);

                              });

            #endregion

            #region PUT      ~/vens/{venId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/vens/versions
            // -------------------------------------------------------------------------------
            AddMethodCallback(Hostname,
                              HTTPMethod.PUT,
                              URLPathPrefix + "vens/{venId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServer.DefaultServerName,
                                          Date                          = Timestamp.Now,
                                          AccessControlAllowOrigin      = "*",
                                          AccessControlAllowMethods     = [ "GET", "OPTIONS" ],
                                          AccessControlAllowHeaders     = [ "Content-Type", "Accept", "Authorization" ],
                                          ETag                          = "1",
                                          ContentType                   = HTTPContentType.Application.JSON_UTF8,
                                          Content                       = new JArray().
                                                                              ToUTF8Bytes(),
                                          X_ExpectedTotalNumberOfItems  = 0
                                      }.AsImmutable);

                              });

            #endregion

            #region DELETE   ~/vens/{venId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/vens/versions
            // -------------------------------------------------------------------------------
            AddMethodCallback(Hostname,
                              HTTPMethod.DELETE,
                              URLPathPrefix + "vens/{venId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServer.DefaultServerName,
                                          Date                          = Timestamp.Now,
                                          AccessControlAllowOrigin      = "*",
                                          AccessControlAllowMethods     = [ "GET", "OPTIONS" ],
                                          AccessControlAllowHeaders     = [ "Content-Type", "Accept", "Authorization" ],
                                          ETag                          = "1",
                                          ContentType                   = HTTPContentType.Application.JSON_UTF8,
                                          Content                       = new JArray().
                                                                              ToUTF8Bytes(),
                                          X_ExpectedTotalNumberOfItems  = 0
                                      }.AsImmutable);

                              });

            #endregion

            #endregion

            #region ~/vens/{venId}/resources

            #region OPTIONS  ~/vens/{venId}/resources

            AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "vens/{venId}/resources",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServer.DefaultServerName,
                            Date                        = Timestamp.Now,
                            Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.POST ],
                            AccessControlAllowOrigin    = "*",
                            AccessControlAllowMethods   = [ "OPTIONS", "GET", "POST" ],
                            AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                            AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                        }.AsImmutable
                    )

            );

            #endregion

            #region GET      ~/vens/{venId}/resources

            AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.GET,
                URLPathPrefix + "vens/{venId}/resources",
                HTTPContentType.Application.JSON_UTF8,
                HTTPDelegate: request => {

                    #region Check access token

                    //if (request.LocalAccessInfo.IsNot(Role.EMSP) == true ||
                    //    request.LocalAccessInfo?.Status != AccessStatus.ALLOWED)
                    //{

                    //    return Task.FromResult(
                    //        new OCPIResponse.Builder(request) {
                    //            StatusCode           = 2000,
                    //            StatusMessage        = "Invalid or blocked access token!",
                    //            HTTPResponseBuilder  = new HTTPResponse.Builder(request.HTTPRequest) {
                    //                HTTPStatusCode             = HTTPStatusCode.Forbidden,
                    //                AccessControlAllowMethods  = [ "OPTIONS", "GET" ],
                    //                AccessControlAllowHeaders  = [ "Authorization" ]
                    //            }
                    //        });

                    //}

                    #endregion



                    return Task.FromResult(
                               new HTTPResponse.Builder(request) {
                                   HTTPStatusCode              = HTTPStatusCode.OK,
                                   Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET ],
                                   AccessControlAllowMethods   = [ "OPTIONS", "GET" ],
                                   AccessControlAllowHeaders   = [ "Authorization" ],
                                   AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                               }.AsImmutable
                           );

            });

            #endregion

            #region POST     ~/vens/{venId}/resources

            AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.POST,
                URLPathPrefix + "vens/{venId}/resources",
                HTTPContentType.Application.JSON_UTF8,
                HTTPDelegate: request => {

                    #region Check access token

                    //if (request.LocalAccessInfo.IsNot(Role.EMSP) == true ||
                    //    request.LocalAccessInfo?.Status != AccessStatus.ALLOWED)
                    //{

                    //    return Task.FromResult(
                    //        new OCPIResponse.Builder(request) {
                    //            StatusCode           = 2000,
                    //            StatusMessage        = "Invalid or blocked access token!",
                    //            HTTPResponseBuilder  = new HTTPResponse.Builder(request.HTTPRequest) {
                    //                HTTPStatusCode             = HTTPStatusCode.Forbidden,
                    //                AccessControlAllowMethods  = [ "OPTIONS", "GET" ],
                    //                AccessControlAllowHeaders  = [ "Authorization" ]
                    //            }
                    //        });

                    //}

                    #endregion



                    return Task.FromResult(
                               new HTTPResponse.Builder(request) {
                                   HTTPStatusCode              = HTTPStatusCode.OK,
                                   Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET ],
                                   AccessControlAllowMethods   = [ "OPTIONS", "GET" ],
                                   AccessControlAllowHeaders   = [ "Authorization" ],
                                   AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                               }.AsImmutable
                           );

            });

            #endregion

            #endregion

            #region ~/vens/{venId}/resources/{resourceId}

            #region OPTIONS  ~/vens/{venId}/resources/{resourceId}

            // ----------------------------------------------------------
            // curl -v -X OPTIONS http://127.0.0.1:5500/vens/ven1/resources/res1
            // ----------------------------------------------------------
            AddMethodCallback(

                Hostname,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "vens/{venId}/resources/{resourceId}",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServer.DefaultServerName,
                            Date                        = Timestamp.Now,
                            Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.PUT, HTTPMethod.DELETE],
                            AccessControlAllowOrigin    = "*",
                            AccessControlAllowMethods   = [ "OPTIONS", "GET", "PUT", "DELETE" ],
                            AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                            AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ]
                        }.AsImmutable)

            );

            #endregion

            #region GET      ~/vens/{venId}/resources/{resourceId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/vens/ven1/resources/res1
            // -------------------------------------------------------------------------------
            AddMethodCallback(Hostname,
                              HTTPMethod.GET,
                              URLPathPrefix + "vens/{venId}/resources/{resourceId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServer.DefaultServerName,
                                          Date                          = Timestamp.Now,
                                          AccessControlAllowOrigin      = "*",
                                          AccessControlAllowMethods     = [ "GET", "OPTIONS" ],
                                          AccessControlAllowHeaders     = [ "Content-Type", "Accept", "Authorization" ],
                                          ETag                          = "1",
                                          ContentType                   = HTTPContentType.Application.JSON_UTF8,
                                          Content                       = new JArray().
                                                                              ToUTF8Bytes(),
                                          X_ExpectedTotalNumberOfItems  = 0
                                      }.AsImmutable);

                              });

            #endregion

            #region PUT      ~/vens/{venId}/resources/{resourceId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/vens/ven1/resources/res1
            // -------------------------------------------------------------------------------
            AddMethodCallback(Hostname,
                              HTTPMethod.PUT,
                              URLPathPrefix + "vens/{venId}/resources/{resourceId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServer.DefaultServerName,
                                          Date                          = Timestamp.Now,
                                          AccessControlAllowOrigin      = "*",
                                          AccessControlAllowMethods     = [ "GET", "OPTIONS" ],
                                          AccessControlAllowHeaders     = [ "Content-Type", "Accept", "Authorization" ],
                                          ETag                          = "1",
                                          ContentType                   = HTTPContentType.Application.JSON_UTF8,
                                          Content                       = new JArray().
                                                                              ToUTF8Bytes(),
                                          X_ExpectedTotalNumberOfItems  = 0
                                      }.AsImmutable);

                              });

            #endregion

            #region DELETE   ~/vens/{venId}/resources/{resourceId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/vens/ven1/resources/res1
            // -------------------------------------------------------------------------------
            AddMethodCallback(Hostname,
                              HTTPMethod.DELETE,
                              URLPathPrefix + "vens/{venId}/resources/{resourceId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServer.DefaultServerName,
                                          Date                          = Timestamp.Now,
                                          AccessControlAllowOrigin      = "*",
                                          AccessControlAllowMethods     = [ "GET", "OPTIONS" ],
                                          AccessControlAllowHeaders     = [ "Content-Type", "Accept", "Authorization" ],
                                          ETag                          = "1",
                                          ContentType                   = HTTPContentType.Application.JSON_UTF8,
                                          Content                       = new JArray().
                                                                              ToUTF8Bytes(),
                                          X_ExpectedTotalNumberOfItems  = 0
                                      }.AsImmutable);

                              });

            #endregion

            #endregion


        }

        #endregion


    }

}
