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
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Hermod.HTTP;

#endregion

namespace cloud.charging.open.protocols.OpenADRv3
{

    /// <summary>
    /// OpenADR API extension methods.
    /// </summary>
    public static class OpenADRAPIExtensions
    {

        #region ParseProgramId                  (this Request, out ProgramId, ...)

        /// <summary>
        /// Parse the program identification from the given HTTP request
        /// or return a HTTP error.
        /// </summary>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="ProgramId">The parsed program identification.</param>
        /// <param name="HTTPResponseBuilder">An HTTP response builder.</param>
        public static Boolean ParseProgramId(this HTTPRequest                                Request,
                                             [NotNullWhen(true)]  out Program_Id             ProgramId,
                                             [NotNullWhen(false)] out HTTPResponse.Builder?  HTTPResponseBuilder)
        {

            ProgramId            = default;
            HTTPResponseBuilder  = default;

            if (Request.ParsedURLParameters.Length < 1 ||
                !Program_Id.TryParse(Request.ParsedURLParameters[0], out var programId))
            {

                HTTPResponseBuilder = new HTTPResponse.Builder(Request) {
                    HTTPStatusCode              = HTTPStatusCode.BadRequest,
                    Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.PUT, HTTPMethod.DELETE],
                    AccessControlAllowOrigin    = "*",
                    AccessControlAllowMethods   = [ "OPTIONS", "GET", "PUT", "DELETE" ],
                    AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                    AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ],
                    Connection                  = ConnectionType.Close
                };

                return false;

            }

            ProgramId = programId;
            return true;

        }

        #endregion

        #region ParseReportId                   (this Request, out ReportId, ...)

        /// <summary>
        /// Parse the report identification from the given HTTP request
        /// or return a HTTP error.
        /// </summary>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="ReportId">The parsed report identification.</param>
        /// <param name="HTTPResponseBuilder">An HTTP response builder.</param>
        public static Boolean ParseReportId(this HTTPRequest                                Request,
                                            [NotNullWhen(true)]  out Report_Id              ReportId,
                                            [NotNullWhen(false)] out HTTPResponse.Builder?  HTTPResponseBuilder)
        {

            ReportId             = default;
            HTTPResponseBuilder  = default;

            if (Request.ParsedURLParameters.Length < 1 ||
                !Report_Id.TryParse(Request.ParsedURLParameters[0], out var reportId))
            {

                HTTPResponseBuilder = new HTTPResponse.Builder(Request) {
                    HTTPStatusCode              = HTTPStatusCode.BadRequest,
                    Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.PUT, HTTPMethod.DELETE],
                    AccessControlAllowOrigin    = "*",
                    AccessControlAllowMethods   = [ "OPTIONS", "GET", "PUT", "DELETE" ],
                    AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                    AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ],
                    Connection                  = ConnectionType.Close
                };

                return false;

            }

            ReportId = reportId;
            return true;

        }

        #endregion

        #region ParseEventId                    (this Request, out EventId, ...)

        /// <summary>
        /// Parse the event identification from the given HTTP request
        /// or return a HTTP error.
        /// </summary>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="EventId">The parsed event identification.</param>
        /// <param name="HTTPResponseBuilder">An HTTP response builder.</param>
        public static Boolean ParseEventId(this HTTPRequest                                Request,
                                           [NotNullWhen(true)]  out Event_Id               EventId,
                                           [NotNullWhen(false)] out HTTPResponse.Builder?  HTTPResponseBuilder)
        {

            EventId              = default;
            HTTPResponseBuilder  = default;

            if (Request.ParsedURLParameters.Length < 1 ||
                !Event_Id.TryParse(Request.ParsedURLParameters[0], out var eventId))
            {

                HTTPResponseBuilder = new HTTPResponse.Builder(Request) {
                    HTTPStatusCode              = HTTPStatusCode.BadRequest,
                    Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.PUT, HTTPMethod.DELETE],
                    AccessControlAllowOrigin    = "*",
                    AccessControlAllowMethods   = [ "OPTIONS", "GET", "PUT", "DELETE" ],
                    AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                    AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ],
                    Connection                  = ConnectionType.Close
                };

                return false;

            }

            EventId = eventId;
            return true;

        }

        #endregion

        #region ParseSubscriptionId             (this Request, out SubscriptionId, ...)

        /// <summary>
        /// Parse the subscription identification from the given HTTP request
        /// or return a HTTP error.
        /// </summary>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="SubscriptionId">The parsed subscription identification.</param>
        /// <param name="HTTPResponseBuilder">An HTTP response builder.</param>
        public static Boolean ParseSubscriptionId(this HTTPRequest                                Request,
                                                  [NotNullWhen(true)]  out Subscription_Id        SubscriptionId,
                                                  [NotNullWhen(false)] out HTTPResponse.Builder?  HTTPResponseBuilder)
        {

            SubscriptionId       = default;
            HTTPResponseBuilder  = default;

            if (Request.ParsedURLParameters.Length < 1 ||
                !Subscription_Id.TryParse(Request.ParsedURLParameters[0], out var subscriptionId))
            {

                HTTPResponseBuilder = new HTTPResponse.Builder(Request) {
                    HTTPStatusCode              = HTTPStatusCode.BadRequest,
                    Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.PUT, HTTPMethod.DELETE],
                    AccessControlAllowOrigin    = "*",
                    AccessControlAllowMethods   = [ "OPTIONS", "GET", "PUT", "DELETE" ],
                    AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                    AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ],
                    Connection                  = ConnectionType.Close
                };

                return false;

            }

            SubscriptionId = subscriptionId;
            return true;

        }

        #endregion

        #region ParseVirtualEndNodeId           (this Request, out VirtualEndNodeId, ...)

        /// <summary>
        /// Parse the ven identification from the given HTTP request
        /// or return a HTTP error.
        /// </summary>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="VirtualEndNodeId">The parsed ven identification.</param>
        /// <param name="HTTPResponseBuilder">An HTTP response builder.</param>
        public static Boolean ParseVirtualEndNodeId(this HTTPRequest                                Request,
                                                    [NotNullWhen(true)]  out VirtualEndNode_Id      VirtualEndNodeId,
                                                    [NotNullWhen(false)] out HTTPResponse.Builder?  HTTPResponseBuilder)
        {

            VirtualEndNodeId     = default;
            HTTPResponseBuilder  = default;

            if (Request.ParsedURLParameters.Length < 1 ||
                !VirtualEndNode_Id.TryParse(Request.ParsedURLParameters[0], out var venId))
            {

                HTTPResponseBuilder = new HTTPResponse.Builder(Request) {
                    HTTPStatusCode              = HTTPStatusCode.BadRequest,
                    Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.PUT, HTTPMethod.DELETE],
                    AccessControlAllowOrigin    = "*",
                    AccessControlAllowMethods   = [ "OPTIONS", "GET", "PUT", "DELETE" ],
                    AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                    AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ],
                    Connection                  = ConnectionType.Close
                };

                return false;

            }

            VirtualEndNodeId = venId;
            return true;

        }

        #endregion

        #region ParseVirtualEndNodeIdResourceId (this Request, out VirtualEndNodeId, out ResourceId, ...)

        /// <summary>
        /// Parse the virtual end node and resource identification from the given HTTP request
        /// or return a HTTP error.
        /// </summary>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="VirtualEndNodeId">The parsed virtual end node identification.</param>
        /// <param name="ResourceId">The parsed resource identification.</param>
        /// <param name="HTTPResponseBuilder">An HTTP response builder.</param>
        public static Boolean ParseVirtualEndNodeIdResourceId(this HTTPRequest                                Request,
                                                              [NotNullWhen(true)]  out VirtualEndNode_Id      VirtualEndNodeId,
                                                              [NotNullWhen(true)]  out Resource_Id            ResourceId,
                                                              [NotNullWhen(false)] out HTTPResponse.Builder?  HTTPResponseBuilder)
        {

            VirtualEndNodeId     = default;
            ResourceId           = default;
            HTTPResponseBuilder  = default;

            if (Request.ParsedURLParameters.Length == 2 &&
                VirtualEndNode_Id.TryParse(Request.ParsedURLParameters[0], out var virtualEndNodeId) &&
                Resource_Id.      TryParse(Request.ParsedURLParameters[1], out var resourceId))
            {

                VirtualEndNodeId  = virtualEndNodeId;
                ResourceId        = resourceId;
                return true;

            }

            HTTPResponseBuilder = new HTTPResponse.Builder(Request) {
                HTTPStatusCode              = HTTPStatusCode.BadRequest,
                Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.PUT, HTTPMethod.DELETE],
                AccessControlAllowOrigin    = "*",
                AccessControlAllowMethods   = [ "OPTIONS", "GET", "PUT", "DELETE" ],
                AccessControlAllowHeaders   = [ "Content-Type", "Accept", "Authorization"],
                AccessControlExposeHeaders  = [ "X-Request-ID", "X-Correlation-ID", "Link", "X-Total-Count", "X-Filtered-Count" ],
                Connection                  = ConnectionType.Close
            };

            return false;

        }

        #endregion

    }


    /// <summary>
    /// The OpenADR HTTP API.
    /// </summary>
    public class OpenADRHTTPAPI : AHTTPAPIExtension<HTTPExtAPI>,
                                  IHTTPAPIExtension<HTTPExtAPI>
    {

        #region Data

        /// <summary>
        /// The default HTTP server name.
        /// </summary>
        public const           String              DefaultHTTPServerName                = "OpenADR API";

        /// <summary>
        /// The default HTTP service name.
        /// </summary>
        public const           String              DefaultHTTPServiceName               = "OpenADR API";

        /// <summary>
        /// The default HTTP realm, if HTTP Basic Authentication is used.
        /// </summary>
        public const           String              DefaultHTTPRealm                     = "Open Charging Cloud OpenADR HTTP API";

        /// <summary>
        /// The HTTP root for embedded resources.
        /// </summary>
        public const           String              HTTPRoot                             = "cloud.charging.open.API.HTTPRoot.";

        public const           String              DefaultOpenADRAPI_DatabaseFileName   = "OpenADRAPI.db";
        public const           String              DefaultOpenADRAPI_LogfileName        = "OpenADRAPI.log";

        public static readonly HTTPEventSource_Id  DebugLogId                           = HTTPEventSource_Id.Parse("DebugLog");

        public                 WWWAuthenticate     WWWAuthenticateDefaults              = WWWAuthenticate.Basic("OpenADR");

        #endregion

        #region Properties

        public String    OpenADRAPIPath     { get; }

        public Boolean?  AllowDowngrades    { get; } = false;

        #endregion

        #region Events

        // HTTP Events

        #region On ~/programs

        #region (protected internal) On GET    ~/programs              HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a GET ~/programs HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnGET_programs__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a GET ~/programs HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnGET_programs__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a GET ~/programs HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task GET_programs__HTTPRequest(DateTime     Timestamp,
                                                          HTTPAPI      API,
                                                          HTTPRequest  Request)

            => OnGET_programs__HTTPRequest?.WhenAll(Timestamp,
                                                    API,
                                                    Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a GET ~/programs HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task GET_programs__HTTPResponse(DateTime      Timestamp,
                                                           HTTPAPI       API,
                                                           HTTPRequest   Request,
                                                           HTTPResponse  Response)

            => OnGET_programs__HTTPResponse?.WhenAll(Timestamp,
                                                     API,
                                                     Request,
                                                     Response) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) On POST   ~/programs              HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a POST ~/programs HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnPOST_programs__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a POST ~/programs HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnPOST_programs__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a POST ~/programs HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task POST_programs__HTTPRequest(DateTime     Timestamp,
                                                           HTTPAPI      API,
                                                           HTTPRequest  Request)

            => OnPOST_programs__HTTPRequest?.WhenAll(Timestamp,
                                                     API,
                                                     Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a POST ~/programs HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task POST_programs__HTTPResponse(DateTime      Timestamp,
                                                            HTTPAPI       API,
                                                            HTTPRequest   Request,
                                                            HTTPResponse  Response)

            => OnPOST_programs__HTTPResponse?.WhenAll(Timestamp,
                                                      API,
                                                      Request,
                                                      Response) ?? Task.CompletedTask;

        #endregion


        #region (protected internal) On GET    ~/programs/{programId}  HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a GET ~/programs/{programId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnGET_program__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a GET ~/programs/{programId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnGET_program__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a GET ~/programs/{programId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task GET_program__HTTPRequest(DateTime     Timestamp,
                                                         HTTPAPI      API,
                                                         HTTPRequest  Request)

            => OnGET_program__HTTPRequest?.WhenAll(Timestamp,
                                                   API,
                                                   Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a GET ~/programs/{programId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task GET_program__HTTPResponse(DateTime      Timestamp,
                                                          HTTPAPI       API,
                                                          HTTPRequest   Request,
                                                          HTTPResponse  Response)

            => OnGET_program__HTTPResponse?.WhenAll(Timestamp,
                                                    API,
                                                    Request,
                                                    Response) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) On PUT    ~/programs/{programId}  HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a PUT ~/programs/{programId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnPUT_program__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a PUT ~/programs/{programId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnPUT_program__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a PUT ~/programs/{programId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task PUT_program__HTTPRequest(DateTime     Timestamp,
                                                         HTTPAPI      API,
                                                         HTTPRequest  Request)

            => OnPUT_program__HTTPRequest?.WhenAll(Timestamp,
                                                   API,
                                                   Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a PUT ~/programs/{programId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task PUT_program__HTTPResponse(DateTime      Timestamp,
                                                          HTTPAPI       API,
                                                          HTTPRequest   Request,
                                                          HTTPResponse  Response)

            => OnPUT_program__HTTPResponse?.WhenAll(Timestamp,
                                                    API,
                                                    Request,
                                                    Response) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) On DELETE ~/programs/{programId}  HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a DELETE ~/programs/{programId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnDELETE_program__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a DELETE ~/programs/{programId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnDELETE_program__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a DELETE ~/programs/{programId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task DELETE_program__HTTPRequest(DateTime     Timestamp,
                                                            HTTPAPI      API,
                                                            HTTPRequest  Request)

            => OnDELETE_program__HTTPRequest?.WhenAll(Timestamp,
                                                      API,
                                                      Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a DELETE ~/programs/{programId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task DELETE_program__HTTPResponse(DateTime      Timestamp,
                                                             HTTPAPI       API,
                                                             HTTPRequest   Request,
                                                             HTTPResponse  Response)

            => OnDELETE_program__HTTPResponse?.WhenAll(Timestamp,
                                                       API,
                                                       Request,
                                                       Response) ?? Task.CompletedTask;

        #endregion

        #endregion

        #region On ~/reports

        #region (protected internal) On GET    ~/reports             HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a GET ~/reports HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnGET_reports__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a GET ~/reports HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnGET_reports__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a GET ~/reports HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task GET_reports__HTTPRequest(DateTime     Timestamp,
                                                         HTTPAPI      API,
                                                         HTTPRequest  Request)

            => OnGET_reports__HTTPRequest?.WhenAll(Timestamp,
                                                   API,
                                                   Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a GET ~/reports HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task GET_reports__HTTPResponse(DateTime      Timestamp,
                                                          HTTPAPI       API,
                                                          HTTPRequest   Request,
                                                          HTTPResponse  Response)

            => OnGET_reports__HTTPResponse?.WhenAll(Timestamp,
                                                    API,
                                                    Request,
                                                    Response) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) On POST   ~/reports             HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a POST ~/reports HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnPOST_reports__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a POST ~/reports HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnPOST_reports__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a POST ~/reports HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task POST_reports__HTTPRequest(DateTime     Timestamp,
                                                          HTTPAPI      API,
                                                          HTTPRequest  Request)

            => OnPOST_reports__HTTPRequest?.WhenAll(Timestamp,
                                                    API,
                                                    Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a POST ~/reports HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task POST_reports__HTTPResponse(DateTime      Timestamp,
                                                           HTTPAPI       API,
                                                           HTTPRequest   Request,
                                                           HTTPResponse  Response)

            => OnPOST_reports__HTTPResponse?.WhenAll(Timestamp,
                                                     API,
                                                     Request,
                                                     Response) ?? Task.CompletedTask;

        #endregion


        #region (protected internal) On GET    ~/reports/{reportId}  HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a GET ~/reports/{reportId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnGET_report__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a GET ~/reports/{reportId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnGET_report__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a GET ~/reports/{reportId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task GET_report__HTTPRequest(DateTime     Timestamp,
                                                        HTTPAPI      API,
                                                        HTTPRequest  Request)

            => OnGET_report__HTTPRequest?.WhenAll(Timestamp,
                                                  API,
                                                  Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a GET ~/reports/{reportId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task GET_report__HTTPResponse(DateTime      Timestamp,
                                                         HTTPAPI       API,
                                                         HTTPRequest   Request,
                                                         HTTPResponse  Response)

            => OnGET_report__HTTPResponse?.WhenAll(Timestamp,
                                                   API,
                                                   Request,
                                                   Response) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) On PUT    ~/reports/{reportId}  HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a PUT ~/reports/{reportId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnPUT_report__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a PUT ~/reports/{reportId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnPUT_report__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a PUT ~/reports/{reportId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task PUT_report__HTTPRequest(DateTime     Timestamp,
                                                        HTTPAPI      API,
                                                        HTTPRequest  Request)

            => OnPUT_report__HTTPRequest?.WhenAll(Timestamp,
                                                  API,
                                                  Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a PUT ~/reports/{reportId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task PUT_report__HTTPResponse(DateTime      Timestamp,
                                                         HTTPAPI       API,
                                                         HTTPRequest   Request,
                                                         HTTPResponse  Response)

            => OnPUT_report__HTTPResponse?.WhenAll(Timestamp,
                                                   API,
                                                   Request,
                                                   Response) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) On DELETE ~/reports/{reportId}  HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a DELETE ~/reports/{reportId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnDELETE_report__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a DELETE ~/reports/{reportId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnDELETE_report__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a DELETE ~/reports/{reportId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task DELETE_report__HTTPRequest(DateTime     Timestamp,
                                                           HTTPAPI      API,
                                                           HTTPRequest  Request)

            => OnDELETE_report__HTTPRequest?.WhenAll(Timestamp,
                                                     API,
                                                     Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a DELETE ~/reports/{reportId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task DELETE_report__HTTPResponse(DateTime      Timestamp,
                                                            HTTPAPI       API,
                                                            HTTPRequest   Request,
                                                            HTTPResponse  Response)

            => OnDELETE_report__HTTPResponse?.WhenAll(Timestamp,
                                                      API,
                                                      Request,
                                                      Response) ?? Task.CompletedTask;

        #endregion

        #endregion

        #region On ~/events

        #region (protected internal) On GET    ~/events            HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a GET ~/events HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnGET_events__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a GET ~/events HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnGET_events__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a GET ~/events HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task GET_events__HTTPRequest(DateTime     Timestamp,
                                                        HTTPAPI      API,
                                                        HTTPRequest  Request)

            => OnGET_events__HTTPRequest?.WhenAll(Timestamp,
                                                  API,
                                                  Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a GET ~/events HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task GET_events__HTTPResponse(DateTime      Timestamp,
                                                         HTTPAPI       API,
                                                         HTTPRequest   Request,
                                                         HTTPResponse  Response)

            => OnGET_events__HTTPResponse?.WhenAll(Timestamp,
                                                   API,
                                                   Request,
                                                   Response) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) On POST   ~/events            HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a POST ~/events HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnPOST_events__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a POST ~/events HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnPOST_events__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a POST ~/events HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task POST_events__HTTPRequest(DateTime     Timestamp,
                                                         HTTPAPI      API,
                                                         HTTPRequest  Request)

            => OnPOST_events__HTTPRequest?.WhenAll(Timestamp,
                                                   API,
                                                   Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a POST ~/events HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task POST_events__HTTPResponse(DateTime      Timestamp,
                                                          HTTPAPI       API,
                                                          HTTPRequest   Request,
                                                          HTTPResponse  Response)

            => OnPOST_events__HTTPResponse?.WhenAll(Timestamp,
                                                    API,
                                                    Request,
                                                    Response) ?? Task.CompletedTask;

        #endregion


        #region (protected internal) On GET    ~/events/{eventId}  HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a GET ~/events/{eventId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnGET_event__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a GET ~/events/{eventId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnGET_event__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a GET ~/events/{eventId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task GET_event__HTTPRequest(DateTime     Timestamp,
                                                       HTTPAPI      API,
                                                       HTTPRequest  Request)

            => OnGET_event__HTTPRequest?.WhenAll(Timestamp,
                                                 API,
                                                 Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a GET ~/events/{eventId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task GET_event__HTTPResponse(DateTime      Timestamp,
                                                        HTTPAPI       API,
                                                        HTTPRequest   Request,
                                                        HTTPResponse  Response)

            => OnGET_event__HTTPResponse?.WhenAll(Timestamp,
                                                  API,
                                                  Request,
                                                  Response) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) On PUT    ~/events/{eventId}  HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a PUT ~/events/{eventId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnPUT_event__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a PUT ~/events/{eventId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnPUT_event__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a PUT ~/events/{eventId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task PUT_event__HTTPRequest(DateTime     Timestamp,
                                                       HTTPAPI      API,
                                                       HTTPRequest  Request)

            => OnPUT_event__HTTPRequest?.WhenAll(Timestamp,
                                                 API,
                                                 Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a PUT ~/events/{eventId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task PUT_event__HTTPResponse(DateTime      Timestamp,
                                                        HTTPAPI       API,
                                                        HTTPRequest   Request,
                                                        HTTPResponse  Response)

            => OnPUT_event__HTTPResponse?.WhenAll(Timestamp,
                                                  API,
                                                  Request,
                                                  Response) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) On DELETE ~/events/{eventId}  HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a DELETE ~/events/{eventId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnDELETE_event__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a DELETE ~/events/{eventId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnDELETE_event__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a DELETE ~/events/{eventId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task DELETE_event__HTTPRequest(DateTime     Timestamp,
                                                          HTTPAPI      API,
                                                          HTTPRequest  Request)

            => OnDELETE_event__HTTPRequest?.WhenAll(Timestamp,
                                                    API,
                                                    Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a DELETE ~/events/{eventId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task DELETE_event__HTTPResponse(DateTime      Timestamp,
                                                           HTTPAPI       API,
                                                           HTTPRequest   Request,
                                                           HTTPResponse  Response)

            => OnDELETE_event__HTTPResponse?.WhenAll(Timestamp,
                                                     API,
                                                     Request,
                                                     Response) ?? Task.CompletedTask;

        #endregion

        #endregion

        #region On ~/subscriptions

        #region (protected internal) On GET    ~/subscriptions                   HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a GET ~/subscriptions HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnGET_subscriptions__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a GET ~/subscriptions HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnGET_subscriptions__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a GET ~/subscriptions HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task GET_subscriptions__HTTPRequest(DateTime     Timestamp,
                                                               HTTPAPI      API,
                                                               HTTPRequest  Request)

            => OnGET_subscriptions__HTTPRequest?.WhenAll(Timestamp,
                                                         API,
                                                         Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a GET ~/subscriptions HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task GET_subscriptions__HTTPResponse(DateTime      Timestamp,
                                                                HTTPAPI       API,
                                                                HTTPRequest   Request,
                                                                HTTPResponse  Response)

            => OnGET_subscriptions__HTTPResponse?.WhenAll(Timestamp,
                                                          API,
                                                          Request,
                                                          Response) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) On POST   ~/subscriptions                   HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a POST ~/subscriptions HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnPOST_subscriptions__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a POST ~/subscriptions HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnPOST_subscriptions__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a POST ~/subscriptions HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task POST_subscriptions__HTTPRequest(DateTime     Timestamp,
                                                                HTTPAPI      API,
                                                                HTTPRequest  Request)

            => OnPOST_subscriptions__HTTPRequest?.WhenAll(Timestamp,
                                                          API,
                                                          Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a POST ~/subscriptions HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task POST_subscriptions__HTTPResponse(DateTime      Timestamp,
                                                                 HTTPAPI       API,
                                                                 HTTPRequest   Request,
                                                                 HTTPResponse  Response)

            => OnPOST_subscriptions__HTTPResponse?.WhenAll(Timestamp,
                                                           API,
                                                           Request,
                                                           Response) ?? Task.CompletedTask;

        #endregion


        #region (protected internal) On GET    ~/subscriptions/{subscriptionId}  HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a GET ~/subscriptions/{subscriptionId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnGET_subscription__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a GET ~/subscriptions/{subscriptionId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnGET_subscription__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a GET ~/subscriptions/{subscriptionId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task GET_subscription__HTTPRequest(DateTime     Timestamp,
                                                              HTTPAPI      API,
                                                              HTTPRequest  Request)

            => OnGET_subscription__HTTPRequest?.WhenAll(Timestamp,
                                                        API,
                                                        Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a GET ~/subscriptions/{subscriptionId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task GET_subscription__HTTPResponse(DateTime      Timestamp,
                                                               HTTPAPI       API,
                                                               HTTPRequest   Request,
                                                               HTTPResponse  Response)

            => OnGET_subscription__HTTPResponse?.WhenAll(Timestamp,
                                                         API,
                                                         Request,
                                                         Response) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) On PUT    ~/subscriptions/{subscriptionId}  HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a PUT ~/subscriptions/{subscriptionId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnPUT_subscription__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a PUT ~/subscriptions/{subscriptionId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnPUT_subscription__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a PUT ~/subscriptions/{subscriptionId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task PUT_subscription__HTTPRequest(DateTime     Timestamp,
                                                              HTTPAPI      API,
                                                              HTTPRequest  Request)

            => OnPUT_subscription__HTTPRequest?.WhenAll(Timestamp,
                                                        API,
                                                        Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a PUT ~/subscriptions/{subscriptionId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task PUT_subscription__HTTPResponse(DateTime      Timestamp,
                                                               HTTPAPI       API,
                                                               HTTPRequest   Request,
                                                               HTTPResponse  Response)

            => OnPUT_subscription__HTTPResponse?.WhenAll(Timestamp,
                                                         API,
                                                         Request,
                                                         Response) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) On DELETE ~/subscriptions/{subscriptionId}  HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a DELETE ~/subscriptions/{subscriptionId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnDELETE_subscription__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a DELETE ~/subscriptions/{subscriptionId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnDELETE_subscription__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a DELETE ~/subscriptions/{subscriptionId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task DELETE_subscription__HTTPRequest(DateTime     Timestamp,
                                                                 HTTPAPI      API,
                                                                 HTTPRequest  Request)

            => OnDELETE_subscription__HTTPRequest?.WhenAll(Timestamp,
                                                           API,
                                                           Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a DELETE ~/subscriptions/{subscriptionId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task DELETE_subscription__HTTPResponse(DateTime      Timestamp,
                                                                  HTTPAPI       API,
                                                                  HTTPRequest   Request,
                                                                  HTTPResponse  Response)

            => OnDELETE_subscription__HTTPResponse?.WhenAll(Timestamp,
                                                            API,
                                                            Request,
                                                            Response) ?? Task.CompletedTask;

        #endregion

        #endregion

        #region On ~/vens

        #region (protected internal) On GET    ~/vens                                 HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a GET ~/vens HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnGET_vens__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a GET ~/vens HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnGET_vens__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a GET ~/vens HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task GET_vens__HTTPRequest(DateTime     Timestamp,
                                                      HTTPAPI      API,
                                                      HTTPRequest  Request)

            => OnGET_vens__HTTPRequest?.WhenAll(Timestamp,
                                                API,
                                                Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a GET ~/vens HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task GET_vens__HTTPResponse(DateTime      Timestamp,
                                                       HTTPAPI       API,
                                                       HTTPRequest   Request,
                                                       HTTPResponse  Response)

            => OnGET_vens__HTTPResponse?.WhenAll(Timestamp,
                                                 API,
                                                 Request,
                                                 Response) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) On POST   ~/vens                                 HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a POST ~/vens HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnPOST_vens__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a POST ~/vens HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnPOST_vens__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a POST ~/vens HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task POST_vens__HTTPRequest(DateTime     Timestamp,
                                                       HTTPAPI      API,
                                                       HTTPRequest  Request)

            => OnPOST_vens__HTTPRequest?.WhenAll(Timestamp,
                                                 API,
                                                 Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a POST ~/vens HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task POST_vens__HTTPResponse(DateTime      Timestamp,
                                                        HTTPAPI       API,
                                                        HTTPRequest   Request,
                                                        HTTPResponse  Response)

            => OnPOST_vens__HTTPResponse?.WhenAll(Timestamp,
                                                  API,
                                                  Request,
                                                  Response) ?? Task.CompletedTask;

        #endregion


        #region (protected internal) On GET    ~/vens/{venId}                         HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a GET ~/vens/{venId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnGET_ven__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a GET ~/vens/{venId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnGET_ven__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a GET ~/vens/{venId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task GET_ven__HTTPRequest(DateTime     Timestamp,
                                                     HTTPAPI      API,
                                                     HTTPRequest  Request)

            => OnGET_ven__HTTPRequest?.WhenAll(Timestamp,
                                               API,
                                               Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a GET ~/vens/{venId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task GET_ven__HTTPResponse(DateTime      Timestamp,
                                                      HTTPAPI       API,
                                                      HTTPRequest   Request,
                                                      HTTPResponse  Response)

            => OnGET_ven__HTTPResponse?.WhenAll(Timestamp,
                                                API,
                                                Request,
                                                Response) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) On PUT    ~/vens/{venId}                         HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a PUT ~/vens/{venId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnPUT_ven__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a PUT ~/vens/{venId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnPUT_ven__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a PUT ~/vens/{venId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task PUT_ven__HTTPRequest(DateTime     Timestamp,
                                                     HTTPAPI      API,
                                                     HTTPRequest  Request)

            => OnPUT_ven__HTTPRequest?.WhenAll(Timestamp,
                                               API,
                                               Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a PUT ~/vens/{venId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task PUT_ven__HTTPResponse(DateTime      Timestamp,
                                                      HTTPAPI       API,
                                                      HTTPRequest   Request,
                                                      HTTPResponse  Response)

            => OnPUT_ven__HTTPResponse?.WhenAll(Timestamp,
                                                API,
                                                Request,
                                                Response) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) On DELETE ~/vens/{venId}                         HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a DELETE ~/vens/{venId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnDELETE_ven__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a DELETE ~/vens/{venId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnDELETE_ven__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a DELETE ~/vens/{venId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task DELETE_ven__HTTPRequest(DateTime     Timestamp,
                                                        HTTPAPI      API,
                                                        HTTPRequest  Request)

            => OnDELETE_ven__HTTPRequest?.WhenAll(Timestamp,
                                                  API,
                                                  Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a DELETE ~/vens/{venId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task DELETE_ven__HTTPResponse(DateTime      Timestamp,
                                                         HTTPAPI       API,
                                                         HTTPRequest   Request,
                                                         HTTPResponse  Response)

            => OnDELETE_ven__HTTPResponse?.WhenAll(Timestamp,
                                                   API,
                                                   Request,
                                                   Response) ?? Task.CompletedTask;

        #endregion


        #region (protected internal) On GET    ~/vens/{venId}/resources               HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a GET ~/vens/{venId}/resources/{resourceId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnGET_ven_resources__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a GET ~/vens/{venId}/resources/{resourceId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnGET_ven_resources__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a GET ~/vens/{venId}/resources/{resourceId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task GET_ven_resources__HTTPRequest(DateTime     Timestamp,
                                                               HTTPAPI      API,
                                                               HTTPRequest  Request)

            => OnGET_vens__HTTPRequest?.WhenAll(Timestamp,
                                                API,
                                                Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a GET ~/vens/{venId}/resources/{resourceId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task GET_ven_resources__HTTPResponse(DateTime      Timestamp,
                                                                HTTPAPI       API,
                                                                HTTPRequest   Request,
                                                                HTTPResponse  Response)

            => OnGET_vens__HTTPResponse?.WhenAll(Timestamp,
                                                 API,
                                                 Request,
                                                 Response) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) On POST   ~/vens/{venId}/resources               HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a POST ~/vens/{venId}/resources/{resourceId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnPOST_ven_resources__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a POST ~/vens/{venId}/resources/{resourceId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnPOST_ven_resources__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a POST ~/vens/{venId}/resources/{resourceId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task POST_ven_resources__HTTPRequest(DateTime     Timestamp,
                                                                HTTPAPI      API,
                                                                HTTPRequest  Request)

            => OnPOST_vens__HTTPRequest?.WhenAll(Timestamp,
                                                 API,
                                                 Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a POST ~/vens/{venId}/resources/{resourceId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task POST_ven_resources__HTTPResponse(DateTime      Timestamp,
                                                                 HTTPAPI       API,
                                                                 HTTPRequest   Request,
                                                                 HTTPResponse  Response)

            => OnPOST_vens__HTTPResponse?.WhenAll(Timestamp,
                                                  API,
                                                  Request,
                                                  Response) ?? Task.CompletedTask;

        #endregion


        #region (protected internal) On GET    ~/vens/{venId}/resources/{resourceId}  HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a GET ~/vens/{venId}/resources/{resourceId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnGET_ven_resource__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a GET ~/vens/{venId}/resources/{resourceId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnGET_ven_resource__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a GET ~/vens/{venId}/resources/{resourceId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task GET_ven_resource__HTTPRequest(DateTime     Timestamp,
                                                              HTTPAPI      API,
                                                              HTTPRequest  Request)

            => OnGET_ven_resource__HTTPRequest?.WhenAll(Timestamp,
                                                        API,
                                                        Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a GET ~/vens/{venId}/resources/{resourceId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task GET_ven_resource__HTTPResponse(DateTime      Timestamp,
                                                               HTTPAPI       API,
                                                               HTTPRequest   Request,
                                                               HTTPResponse  Response)

            => OnGET_ven_resource__HTTPResponse?.WhenAll(Timestamp,
                                                         API,
                                                         Request,
                                                         Response) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) On PUT    ~/vens/{venId}/resources/{resourceId}  HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a PUT ~/vens/{venId}/resources/{resourceId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnPUT_ven_resource__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a PUT ~/vens/{venId}/resources/{resourceId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnPUT_ven_resource__HTTPResponse  = new ();



        /// <summary>
        /// An event sent whenever a PUT ~/vens/{venId}/resources/{resourceId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task PUT_ven_resource__HTTPRequest(DateTime     Timestamp,
                                                              HTTPAPI      API,
                                                              HTTPRequest  Request)

            => OnPUT_ven_resource__HTTPRequest?.WhenAll(Timestamp,
                                                        API,
                                                        Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a PUT ~/vens/{venId}/resources/{resourceId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task PUT_ven_resource__HTTPResponse(DateTime      Timestamp,
                                                               HTTPAPI       API,
                                                               HTTPRequest   Request,
                                                               HTTPResponse  Response)

            => OnPUT_ven_resource__HTTPResponse?.WhenAll(Timestamp,
                                                         API,
                                                         Request,
                                                         Response) ?? Task.CompletedTask;

        #endregion

        #region (protected internal) On DELETE ~/vens/{venId}/resources/{resourceId}  HTTP(Request/Response)

        /// <summary>
        /// An event sent whenever a DELETE ~/vens/{venId}/resources/{resourceId} HTTP request was received.
        /// </summary>
        public HTTPRequestLogEvent  OnDELETE_ven_resource__HTTPRequest   = new ();

        /// <summary>
        /// An event sent whenever a DELETE ~/vens/{venId}/resources/{resourceId} HTTP response was sent.
        /// </summary>
        public HTTPResponseLogEvent OnDELETE_ven_resource__HTTPResponse = new ();



        /// <summary>
        /// An event sent whenever a DELETE ~/vens/{venId}/resources/{resourceId} HTTP request was received.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        protected internal Task DELETE_ven_resource__HTTPRequest(DateTime     Timestamp,
                                                                 HTTPAPI      API,
                                                                 HTTPRequest  Request)

            => OnDELETE_ven_resource__HTTPRequest?.WhenAll(Timestamp,
                                                           API,
                                                           Request) ?? Task.CompletedTask;

        /// <summary>
        /// An event sent whenever a DELETE ~/vens/{venId}/resources/{resourceId} HTTP response was sent.
        /// </summary>
        /// <param name="Timestamp">The timestamp of the request.</param>
        /// <param name="API">The HTTP API.</param>
        /// <param name="Request">A HTTP request.</param>
        /// <param name="Response">A HTTP response.</param>
        protected internal Task DELETE_ven_resource__HTTPResponse(DateTime      Timestamp,
                                                                  HTTPAPI       API,
                                                                  HTTPRequest   Request,
                                                                  HTTPResponse  Response)

            => OnDELETE_ven_resource__HTTPResponse?.WhenAll(Timestamp,
                                                            API,
                                                            Request,
                                                            Response) ?? Task.CompletedTask;

        #endregion

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
        /// <param name="BasePath">When the API is served from an optional subdirectory path.</param>
        /// <param name="HTTPServerName">The default HTTP server name, used whenever no HTTP Host-header has been given.</param>
        /// 
        /// <param name="URLPathPrefix">A common prefix for all URLs.</param>

        public OpenADRHTTPAPI(HTTPExtAPI                                  HTTPAPI,
                              String?                                     HTTPServerName         = null,
                              HTTPPath?                                   URLPathPrefix          = null,
                              HTTPPath?                                   BasePath               = null,

                              Boolean                                     EventLoggingDisabled   = true,

                              String                                      HTTPRealm              = DefaultHTTPRealm,
                              IEnumerable<KeyValuePair<String, String>>?  HTTPLogins             = null,
                              Formatting                                  JSONFormatting         = Formatting.None,
                              JObject?                                    APIVersionHashes       = null)

            : base(HTTPAPI,
                   HTTPServerName ?? DefaultHTTPServerName,
                   URLPathPrefix,
                   BasePath,
                   APIVersionHash: APIVersionHashes?[nameof(OpenADRHTTPAPI)]?.Value<String>()?.Trim())

        {

            this.OpenADRAPIPath  = Path.Combine(this.LoggingPath, "OpenADRAPI");

            if (!DisableLogging)
            {
                Directory.CreateDirectory(OpenADRAPIPath);
            }

            RegisterURLTemplates();

            DebugX.Log($"OpenADR {Version.String} HTTP API {(APIVersionHash.IsNullOrEmpty() ? $" ({APIVersionHash})" : "")} initialized...");

        }

        #endregion


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

            var program = Program.FillMetadata(Program);

            if (programs.TryAdd(program.Id!.Value, program))
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

                    var onProgramAdded = OnProgramAdded;
                    if (onProgramAdded is not null)
                    {
                        try
                        {
                            await onProgramAdded(Program);
                        }
                        catch (Exception e)
                        {
                            DebugX.LogT($"OpenADR {Version.String} {nameof(OpenADRHTTPAPI)} ", nameof(AddProgram), " ", nameof(OnProgramAdded), ": ",
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

            var program = Program.FillMetadata(Program);

            if (programs.TryAdd(program.Id!.Value, program))
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

                    var onProgramAdded = OnProgramAdded;
                    if (onProgramAdded is not null)
                    {
                        try
                        {
                            await onProgramAdded(Program);
                        }
                        catch (Exception e)
                        {
                            DebugX.LogT($"OpenADR {Version.String} {nameof(OpenADRHTTPAPI)} ", nameof(AddProgramIfNotExists), " ", nameof(OnProgramAdded), ": ",
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

            var program = Program.FillMetadata(Program);

            #region Update an existing program

            if (programs.TryGetValue(program.Id!.Value,
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

                if (programs.TryUpdate(program.Id!.Value,
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

                    var onProgramChanged = OnProgramChanged;
                    if (onProgramChanged is not null)
                    {
                        try
                        {
                            await onProgramChanged(Program);
                        }
                        catch (Exception e)
                        {
                            DebugX.LogT($"OpenADR {Version.String} {nameof(OpenADRHTTPAPI)} ", nameof(AddOrUpdateProgram), " ", nameof(OnProgramChanged), ": ",
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

            if (programs.TryAdd(program.Id!.Value, Program))
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

                    var onProgramAdded = OnProgramAdded;
                    if (onProgramAdded is not null)
                    {
                        try
                        {
                            await onProgramAdded(Program);
                        }
                        catch (Exception e)
                        {
                            DebugX.LogT($"OpenADR {Version.String} {nameof(OpenADRHTTPAPI)} ", nameof(AddOrUpdateProgram), " ", nameof(OnProgramAdded), ": ",
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

            #region Initial checks

            if (!Program.Id.HasValue)
                return UpdateResult<Program>.Failed(
                            EventTrackingId,
                            Program,
                            $"The given program identification is mandatory!"
                        );

            if (!programs.TryGetValue(Program.Id!.Value, out var existingProgram))
                return UpdateResult<Program>.Failed(
                            EventTrackingId,
                            Program,
                            $"The given program identification '{Program.Id}' is unknown!"
                        );

            #endregion

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


            if (programs.TryUpdate(Program.Id!.Value,
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

                    var onProgramChanged = OnProgramChanged;
                    if (onProgramChanged is not null)
                    {
                        try
                        {
                            await onProgramChanged(Program);
                        }
                        catch (Exception e)
                        {
                            DebugX.LogT($"OpenADR {Version.String} {nameof(OpenADRHTTPAPI)} ", nameof(UpdateProgram), " ", nameof(OnProgramChanged), ": ",
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

            #region Initial checks

            if (!Program.Id.HasValue)
                return RemoveResult<Program>.Failed(
                            EventTrackingId,
                            Program,
                            $"The given program identification is mandatory!"
                        );

            #endregion

            if (programs.TryRemove(Program.Id!.Value, out var programVersions))
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
                       $"The given program identification '{Program.Id}' is unknown!"
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
                       $"The given program identification '{ProgramId}' is unknown!"
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
                    programs.TryRemove(Program_Id.Parse(program.Id!.Value.ToString()), out _);

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
                if (IncludeProgramIds(Program_Id.Parse(program.Id!.Value.ToString())))
                    removedPrograms.Add(program);
            }

            foreach (var program in removedPrograms)
                programs.TryRemove(Program_Id.Parse(program.Id!.Value.ToString()), out _);


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
                                 new Tuple<String, Assembly>(OpenADRHTTPAPI.HTTPRoot, typeof(OpenADRHTTPAPI).Assembly),
                                 new Tuple<String, Assembly>(HTTPAPI.   HTTPRoot, typeof(HTTPAPI).   Assembly));

        #endregion

        #region (protected override) GetResourceMemoryStream(ResourceName)

        protected override MemoryStream? GetResourceMemoryStream(String ResourceName)

            => GetResourceMemoryStream(ResourceName,
                                       new Tuple<String, Assembly>(OpenADRHTTPAPI.HTTPRoot, typeof(OpenADRHTTPAPI).Assembly),
                                       new Tuple<String, Assembly>(HTTPAPI.   HTTPRoot, typeof(HTTPAPI).   Assembly));

        #endregion

        #region (protected override) GetResourceString      (ResourceName)

        protected override String GetResourceString(String ResourceName)

            => GetResourceString(ResourceName,
                                 new Tuple<String, Assembly>(OpenADRHTTPAPI.HTTPRoot, typeof(OpenADRHTTPAPI).Assembly),
                                 new Tuple<String, Assembly>(HTTPAPI.   HTTPRoot, typeof(HTTPAPI).   Assembly));

        #endregion

        #region (protected override) GetResourceBytes       (ResourceName)

        protected override Byte[] GetResourceBytes(String ResourceName)

            => GetResourceBytes(ResourceName,
                                new Tuple<String, Assembly>(OpenADRHTTPAPI.HTTPRoot, typeof(OpenADRHTTPAPI).Assembly),
                                new Tuple<String, Assembly>(HTTPAPI.   HTTPRoot, typeof(HTTPAPI).   Assembly));

        #endregion

        #region (protected override) MixWithHTMLTemplate    (ResourceName)

        protected override String MixWithHTMLTemplate(String ResourceName)

            => MixWithHTMLTemplate(ResourceName,
                                   new Tuple<String, Assembly>(OpenADRHTTPAPI.HTTPRoot, typeof(OpenADRHTTPAPI).Assembly),
                                   new Tuple<String, Assembly>(HTTPAPI.   HTTPRoot, typeof(HTTPAPI).   Assembly));

        #endregion

        #region (protected override) MixWithHTMLTemplate    (Template, ResourceName, Content = null)

        protected override String MixWithHTMLTemplate(String   Template,
                                                      String   ResourceName,
                                                      String?  Content   = null)

            => MixWithHTMLTemplate(Template,
                                   ResourceName,
                                   [
                                       new Tuple<String, Assembly>(OpenADRHTTPAPI.HTTPRoot, typeof(OpenADRHTTPAPI).Assembly),
                                       new Tuple<String, Assembly>(HTTPAPI.   HTTPRoot, typeof(HTTPAPI).   Assembly)
                                   ],
                                   Content);

        #endregion

        #endregion

        private void RegisterURLTemplates()
        {

            HTTPBaseAPI.HTTPServer.AddAuth(request => {

                #region Allow some URLs for anonymous access...

                if (request.Path.Equals(URLPathPrefix))
                {
                    return HTTPExtAPI.Anonymous;
                }

                #endregion

                return null;

            });


            #region ~/programs

            #region OPTIONS  ~/programs

            HTTPBaseAPI.AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "programs",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServiceName,
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

            HTTPBaseAPI.AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.GET,
                URLPathPrefix + "programs",
                HTTPContentType.Application.JSON_UTF8,
                HTTPRequestLogger:   GET_programs__HTTPRequest,
                HTTPResponseLogger:  GET_programs__HTTPResponse,
                HTTPDelegate:        request => {

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
                                                (program, pattern) => program.ProgramName.      Contains(pattern)         ||
                                                                      program.ProgramLongName?. Contains(pattern) == true ||
                                                                      program.RetailerName?.    Contains(pattern) == true ||
                                                                      program.RetailerLongName?.Contains(pattern) == true
                                            );

                                            //ToDo: Filter to NOT show all programs to everyone!
                    var allPrograms       = GetPrograms().ToArray();

                    var filteredPrograms  = allPrograms.Where(matchFilter).
                                                        Where(program => !from.HasValue || program.LastModification >  from.Value).
                                                        Where(program => !to.  HasValue || program.LastModification <= to.  Value).
                                                        ToArray();


                    return Task.FromResult(
                               new HTTPResponse.Builder(request) {
                                   HTTPStatusCode              = HTTPStatusCode.OK,
                                   Content                     = new JArray(
                                                                     filteredPrograms.
                                                                         OrderBy(program => program.Created).
                                                                         SkipTakeFilter(skip, limit).
                                                                         Select (program => program.ToJSON(
                                                                                                CustomProgramSerializer,
                                                                                                CustomIntervalPeriodSerializer,
                                                                                                CustomEventPayloadDescriptorSerializer,
                                                                                                CustomReportPayloadDescriptorSerializer,
                                                                                                CustomValuesMapSerializer
                                                                                            ))
                                                                 ).ToUTF8Bytes(),
                                   Allow                       = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.POST ],
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

            HTTPBaseAPI.AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.POST,
                URLPathPrefix + "programs",
                HTTPContentType.Application.JSON_UTF8,
                HTTPRequestLogger:   POST_programs__HTTPRequest,
                HTTPResponseLogger:  POST_programs__HTTPResponse,
                HTTPDelegate:        async request => {

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
            HTTPBaseAPI.AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "programs/{programId}",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServiceName,
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
            HTTPBaseAPI.AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.GET,
                URLPathPrefix + "programs/{programId}",
                HTTPContentType.Application.JSON_UTF8,
                HTTPRequestLogger:   GET_program__HTTPRequest,
                HTTPResponseLogger:  GET_program__HTTPResponse,
                HTTPDelegate:        request => {

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
                        return Task.FromResult(httpResponseBuilder.AsImmutable);
                    }

                    if (!TryGetProgram(programId, out var program))
                    {

                        return Task.FromResult(
                                   new HTTPResponse.Builder(request) {
                                       HTTPStatusCode             = HTTPStatusCode.NotFound,
                                       AccessControlAllowHeaders  = [ "Authorization" ],
                                       Connection                 = ConnectionType.Close
                                   }.AsImmutable
                               );

                    }

                    #endregion


                    return Task.FromResult(
                               new HTTPResponse.Builder(request) {
                                   HTTPStatusCode             = HTTPStatusCode.OK,
                                   Server                     = HTTPServiceName,
                                   Date                       = Timestamp.Now,
                                   Allow                      = [ HTTPMethod.OPTIONS, HTTPMethod.GET, HTTPMethod.PUT, HTTPMethod.DELETE],
                                   AccessControlAllowOrigin   = "*",
                                   AccessControlAllowMethods  = [ "OPTIONS", "GET", "PUT", "DELETE" ],
                                   ContentType                = HTTPContentType.Application.JSON_UTF8,
                                   Content                    = program.ToJSON(
                                                                    CustomProgramSerializer,
                                                                    CustomIntervalPeriodSerializer,
                                                                    CustomEventPayloadDescriptorSerializer,
                                                                    CustomReportPayloadDescriptorSerializer,
                                                                    CustomValuesMapSerializer
                                                                ).ToUTF8Bytes(),
                                   Connection                 = ConnectionType.Close
                               }.AsImmutable
                           );

                }

            );

            #endregion

            #region PUT      ~/programs/{programId}

            // -------------------------------------------------------------------------------
            // curl -v -H "Accept: application/json" http://127.0.0.1:5500/programs/program1
            // -------------------------------------------------------------------------------
            HTTPBaseAPI.AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.PUT,
                URLPathPrefix + "programs/{programId}",
                HTTPContentType.Application.JSON_UTF8,
                HTTPRequestLogger:   PUT_program__HTTPRequest,
                HTTPResponseLogger:  PUT_program__HTTPResponse,
                HTTPDelegate:        async request => {

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


                    var result = await UpdateProgram(
                                           newOrUpdatedProgram,
                                           EventTrackingId:    request.EventTrackingId,
                                           CurrentUserId:      null, //request.LocalAccessInfo?.UserId,
                                           CancellationToken:  request.CancellationToken
                                       );


                    return result.IsSuccessAndDataNotNull(out var data)

                        ? new HTTPResponse.Builder(request) {
                                   HTTPStatusCode             = HTTPStatusCode.OK,
                                   Server                     = HTTPServiceName,
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
                                   Server                     = HTTPServiceName,
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
            HTTPBaseAPI.AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.DELETE,
                URLPathPrefix + "programs/{programId}",
                HTTPContentType.Application.JSON_UTF8,
                HTTPRequestLogger:   DELETE_program__HTTPRequest,
                HTTPResponseLogger:  DELETE_program__HTTPResponse,
                HTTPDelegate:        async request => {

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
                                           programId,
                                           EventTrackingId:    request.EventTrackingId,
                                           CurrentUserId:      null, //request.LocalAccessInfo?.UserId,
                                           CancellationToken:  request.CancellationToken
                                       );


                    return result.IsSuccessAndDataNotNull(out var data)

                        ? new HTTPResponse.Builder(request) {
                              HTTPStatusCode             = HTTPStatusCode.OK,
                              Server                     = HTTPServiceName,
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
                              Server                     = HTTPServiceName,
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

            HTTPBaseAPI.AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "reports",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServiceName,
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

            HTTPBaseAPI.AddMethodCallback(

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

            HTTPBaseAPI.AddMethodCallback(

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
            HTTPBaseAPI.AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "reports/{reportId}",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServiceName,
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
            HTTPBaseAPI.AddMethodCallback(HTTPHostname.Any,
                              HTTPMethod.GET,
                              URLPathPrefix + "reports/{reportId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServiceName,
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
            HTTPBaseAPI.AddMethodCallback(HTTPHostname.Any,
                              HTTPMethod.PUT,
                              URLPathPrefix + "reports/{reportId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServiceName,
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
            HTTPBaseAPI.AddMethodCallback(HTTPHostname.Any,
                              HTTPMethod.DELETE,
                              URLPathPrefix + "reports/{reportId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServiceName,
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

            HTTPBaseAPI.AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "events",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServiceName,
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

            HTTPBaseAPI.AddMethodCallback(

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

            HTTPBaseAPI.AddMethodCallback(

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
            HTTPBaseAPI.AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "events/{eventId}",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServiceName,
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
            HTTPBaseAPI.AddMethodCallback(HTTPHostname.Any,
                              HTTPMethod.GET,
                              URLPathPrefix + "events/{eventId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServiceName,
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
            HTTPBaseAPI.AddMethodCallback(HTTPHostname.Any,
                              HTTPMethod.PUT,
                              URLPathPrefix + "events/{eventId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServiceName,
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
            HTTPBaseAPI.AddMethodCallback(HTTPHostname.Any,
                              HTTPMethod.DELETE,
                              URLPathPrefix + "events/{eventId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServiceName,
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

            HTTPBaseAPI.AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "subscriptions",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServiceName,
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

            HTTPBaseAPI.AddMethodCallback(

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

            HTTPBaseAPI.AddMethodCallback(

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
            HTTPBaseAPI.AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "subscriptions/{subscriptionId}",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServiceName,
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
            HTTPBaseAPI.AddMethodCallback(HTTPHostname.Any,
                              HTTPMethod.GET,
                              URLPathPrefix + "subscriptions/{subscriptionId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServiceName,
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
            HTTPBaseAPI.AddMethodCallback(HTTPHostname.Any,
                              HTTPMethod.PUT,
                              URLPathPrefix + "subscriptions/{subscriptionId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServiceName,
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
            HTTPBaseAPI.AddMethodCallback(HTTPHostname.Any,
                              HTTPMethod.DELETE,
                              URLPathPrefix + "subscriptions/{subscriptionId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServiceName,
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

            HTTPBaseAPI.AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "vens",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServiceName,
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

            HTTPBaseAPI.AddMethodCallback(

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

            HTTPBaseAPI.AddMethodCallback(

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
            HTTPBaseAPI.AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "vens/{venId}",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServiceName,
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
            HTTPBaseAPI.AddMethodCallback(HTTPHostname.Any,
                              HTTPMethod.GET,
                              URLPathPrefix + "vens/{venId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServiceName,
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
            HTTPBaseAPI.AddMethodCallback(HTTPHostname.Any,
                              HTTPMethod.PUT,
                              URLPathPrefix + "vens/{venId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServiceName,
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
            HTTPBaseAPI.AddMethodCallback(HTTPHostname.Any,
                              HTTPMethod.DELETE,
                              URLPathPrefix + "vens/{venId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServiceName,
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

            HTTPBaseAPI.AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "vens/{venId}/resources",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServiceName,
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

            HTTPBaseAPI.AddMethodCallback(

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

            HTTPBaseAPI.AddMethodCallback(

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
            HTTPBaseAPI.AddMethodCallback(

                HTTPHostname.Any,
                HTTPMethod.OPTIONS,
                URLPathPrefix + "vens/{venId}/resources/{resourceId}",
                HTTPDelegate: request =>

                    Task.FromResult(
                        new HTTPResponse.Builder(request) {
                            HTTPStatusCode              = HTTPStatusCode.OK,
                            Server                      = HTTPServiceName,
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
            HTTPBaseAPI.AddMethodCallback(HTTPHostname.Any,
                              HTTPMethod.GET,
                              URLPathPrefix + "vens/{venId}/resources/{resourceId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServiceName,
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
            HTTPBaseAPI.AddMethodCallback(HTTPHostname.Any,
                              HTTPMethod.PUT,
                              URLPathPrefix + "vens/{venId}/resources/{resourceId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServiceName,
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
            HTTPBaseAPI.AddMethodCallback(HTTPHostname.Any,
                              HTTPMethod.DELETE,
                              URLPathPrefix + "vens/{venId}/resources/{resourceId}",
                              HTTPContentType.Application.JSON_UTF8,
                              HTTPDelegate: request => {

                                  var skip                    = request.QueryString.GetUInt64("skip");
                                  var take                    = request.QueryString.GetUInt64("take");

                                  return Task.FromResult(
                                      new HTTPResponse.Builder(request) {
                                          HTTPStatusCode                = HTTPStatusCode.OK,
                                          Server                        = HTTPServiceName,
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
