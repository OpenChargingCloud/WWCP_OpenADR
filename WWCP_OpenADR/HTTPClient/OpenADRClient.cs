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

using System.Security.Authentication;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Hermod;
using org.GraphDefined.Vanaheimr.Hermod.DNS;
using org.GraphDefined.Vanaheimr.Hermod.HTTP;
using org.GraphDefined.Vanaheimr.Hermod.Mail;
using org.GraphDefined.Vanaheimr.Hermod.Logging;

using cloud.charging.open.protocols.WWCP;

#endregion

namespace cloud.charging.open.protocols.OpenADRv3
{

    /// <summary>
    /// The OpenADR HTTP Client.
    /// </summary>
    public partial class OpenADRClient : AHTTPClient,
                                         IHTTPClient
    {

        #region (class) APICounters

        public class APICounters
        {

            #region Properties

            public APICounterValues  GetPrograms          { get; }
            public APICounterValues  PostPrograms         { get; }

            public APICounterValues  GetProgram           { get; }


            #endregion

            #region Constructor(s)

            public APICounters(APICounterValues?  GetPrograms          = null,
                               APICounterValues?  PostVersions         = null,

                               APICounterValues?  GetProgram           = null)

            {

                this.GetPrograms        = GetPrograms       ?? new APICounterValues();
                this.PostPrograms       = PostPrograms      ?? new APICounterValues();

                this.GetProgram         = GetProgram        ?? new APICounterValues();

            }

            #endregion


            #region ToJSON()

            public JObject ToJSON()

                => JSONObject.Create(

                       new JProperty("getPrograms",    GetPrograms. ToJSON()),
                       new JProperty("postPrograms",   PostPrograms.ToJSON()),

                       new JProperty("getProgram",     GetProgram.  ToJSON())

                   );

            #endregion

        }

        #endregion

        #region DateAndPaginationFilters

        /// <summary>
        /// Date and pagination filters.
        /// </summary>
        /// <param name="From">An optional 'from' timestamp (inclusive).</param>
        /// <param name="To">An optional 'to' timestamp (exclusive).</param>
        /// <param name="Offset">An optional 'offset' within the result set.</param>
        /// <param name="Limit">An optional 'limit' of the result set.</param>
        public readonly struct DateAndPaginationFilters(DateTime?  From,
                                                        DateTime?  To,
                                                        UInt64?    Offset,
                                                        UInt64?    Limit)
        {

            #region Properties

            /// <summary>
            /// The optional 'from' timestamp (inclusive).
            /// </summary>
            public DateTime?  From      { get; } = From;

            /// <summary>
            /// The optional 'to' timestamp (exclusive).
            /// </summary>
            public DateTime?  To        { get; } = To;

            /// <summary>
            /// The optional 'offset' within the result set.
            /// </summary>
            public UInt64?    Offset    { get; } = Offset;

            /// <summary>
            /// The optional 'limit' of the result set.
            /// </summary>
            public UInt64?    Limit     { get; } = Limit;

            #endregion


            #region ToHTTPQueryString()

            /// <summary>
            /// Return a HTTP QueryString representation of this object.
            /// </summary>
            public String ToHTTPQueryString()

                => (From.  HasValue ||
                    To.    HasValue ||
                    Offset.HasValue ||
                    Limit. HasValue)

                    ? "?" + new String[] {
                          From.  HasValue ? "date_from=" + From.  Value.ToISO8601() : "",
                          To.    HasValue ? "date_to="   + To.    Value.ToISO8601() : "",
                          Offset.HasValue ? "offset="    + Offset.Value.ToString()  : "",
                          Limit. HasValue ? "limit="     + Limit. Value.ToString()  : ""
                      }.Where(text => text.IsNotNullOrEmpty()).
                        AggregateWith("&")

                    : "";

            #endregion

            #region (override) ToString()

            /// <summary>
            /// Return a text representation of this object.
            /// </summary>
            public override String ToString()

                => (From.  HasValue ||
                    To.    HasValue ||
                    Offset.HasValue ||
                    Limit. HasValue)

                    ? new String[] {
                          From.  HasValue ? "from: "   + From.  Value.ToString() : "",
                          To.    HasValue ? "to: "     + To.    Value.ToString() : "",
                          Offset.HasValue ? "offset: " + Offset.Value.ToString() : "",
                          Limit. HasValue ? "limit: "  + Limit. Value.ToString() : ""
                      }.Where(text => text.IsNotNullOrEmpty()).
                        AggregateWith(", ")

                    : "";

            #endregion


        }

        public static DateAndPaginationFilters GetDateAndPaginationFilters(HTTPRequest HTTPRequest)

            => new (HTTPRequest.QueryString.GetDateTime("date_from"),
                    HTTPRequest.QueryString.GetDateTime("date_to"),
                    HTTPRequest.QueryString.GetUInt64  ("offset"),
                    HTTPRequest.QueryString.GetUInt64  ("limit"));

        #endregion



        #region Data

        /// <summary>
        /// The default HTTPS user agent.
        /// </summary>
        public new const String  DefaultHTTPUserAgent  = "GraphDefined OpenADR HTTP Client";


        protected readonly AcceptTypes ocpiAcceptTypes = AcceptTypes.FromHTTPContentTypes(HTTPContentType.Application.JSON_UTF8);

        #endregion

        #region Properties

        /// <summary>
        /// HTTP client event counters.
        /// </summary>
        public APICounters  Counters      { get; }

        /// <summary>
        /// The HTTP client (HTTP client) logger.
        /// </summary>
        public new Logger?  HTTPLogger    { get; set; }

        #endregion

        #region Events

        #region ~/program

        #region OnGetProgramsRequest/-Response

        /// <summary>
        /// An event fired whenever a GET ~/programs request will be send.
        /// </summary>
        public event OnGetProgramsRequestDelegate?   OnGetProgramsRequest;

        /// <summary>
        /// An event fired whenever a GET ~/programs HTTP request will be send.
        /// </summary>
        public event ClientRequestLogHandler?        OnGetProgramsHTTPRequest;

        /// <summary>
        /// An event fired whenever a response to a GET ~/programs HTTP request had been received.
        /// </summary>
        public event ClientResponseLogHandler?       OnGetProgramsHTTPResponse;

        /// <summary>
        /// An event fired whenever a response to a GET ~/programs request had been received.
        /// </summary>
        public event OnGetProgramsResponseDelegate?  OnGetProgramsResponse;

        #endregion

        #region OnPostProgramsRequest/-Response

        /// <summary>
        /// An event fired whenever a POST ~/programs request will be send.
        /// </summary>
        public event OnPostProgramsRequestDelegate?   OnPostProgramsRequest;

        /// <summary>
        /// An event fired whenever a POST ~/programs HTTP request will be send.
        /// </summary>
        public event ClientRequestLogHandler?         OnPostProgramsHTTPRequest;

        /// <summary>
        /// An event fired whenever a response to a POST ~/programs HTTP request had been received.
        /// </summary>
        public event ClientResponseLogHandler?        OnPostProgramsHTTPResponse;

        /// <summary>
        /// An event fired whenever a response to a POST ~/programs request had been received.
        /// </summary>
        public event OnPostProgramsResponseDelegate?  OnPostProgramsResponse;

        #endregion


        #region OnGetProgramRequest/-Response

        /// <summary>
        /// An event fired whenever a GET ~/program request will be send.
        /// </summary>
        public event OnGetProgramRequestDelegate?   OnGetProgramRequest;

        /// <summary>
        /// An event fired whenever a GET ~/program HTTP request will be send.
        /// </summary>
        public event ClientRequestLogHandler?       OnGetProgramHTTPRequest;

        /// <summary>
        /// An event fired whenever a response to a GET ~/program HTTP request had been received.
        /// </summary>
        public event ClientResponseLogHandler?      OnGetProgramHTTPResponse;

        /// <summary>
        /// An event fired whenever a response to a GET ~/program request had been received.
        /// </summary>
        public event OnGetProgramResponseDelegate?  OnGetProgramResponse;

        #endregion

        #region OnPutProgramRequest/-Response

        /// <summary>
        /// An event fired whenever a PUT ~/program request will be send.
        /// </summary>
        public event OnPutProgramRequestDelegate?   OnPutProgramRequest;

        /// <summary>
        /// An event fired whenever a PUT ~/program HTTP request will be send.
        /// </summary>
        public event ClientRequestLogHandler?       OnPutProgramHTTPRequest;

        /// <summary>
        /// An event fired whenever a response to a PUT ~/program HTTP request had been received.
        /// </summary>
        public event ClientResponseLogHandler?      OnPutProgramHTTPResponse;

        /// <summary>
        /// An event fired whenever a response to a PUT ~/program request had been received.
        /// </summary>
        public event OnPutProgramResponseDelegate?  OnPutProgramResponse;

        #endregion

        #region OnDeleteProgramRequest/-Response

        /// <summary>
        /// An event fired whenever a DELETE ~/program request will be send.
        /// </summary>
        public event OnDeleteProgramRequestDelegate?   OnDeleteProgramRequest;

        /// <summary>
        /// An event fired whenever a DELETE ~/program HTTP request will be send.
        /// </summary>
        public event ClientRequestLogHandler?          OnDeleteProgramHTTPRequest;

        /// <summary>
        /// An event fired whenever a response to a DELETE ~/program HTTP request had been received.
        /// </summary>
        public event ClientResponseLogHandler?         OnDeleteProgramHTTPResponse;

        /// <summary>
        /// An event fired whenever a response to a DELETE ~/program request had been received.
        /// </summary>
        public event OnDeleteProgramResponseDelegate?  OnDeleteProgramResponse;

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
        /// Create a new OpenADR HTTP client.
        /// </summary>
        /// <param name="RemoteURL">The remote URL of the OICP HTTP endpoint to connect to.</param>
        /// <param name="VirtualHostname">An optional HTTP virtual hostname.</param>
        /// <param name="Description">An optional description of this CPO client.</param>
        /// <param name="PreferIPv4">Prefer IPv4 instead of IPv6.</param>
        /// <param name="ContentType">An optional HTTP content type.</param>
        /// <param name="Accept">The optional HTTP accept header.</param>
        /// <param name="HTTPAuthentication">The optional HTTP authentication to use, e.g. HTTP Basic Auth.</param>
        /// <param name="HTTPUserAgent">The HTTP user agent identification.</param>
        /// <param name="Connection">An optional connection type.</param>
        /// <param name="RequestTimeout">An optional request timeout.</param>
        /// <param name="TransmissionRetryDelay">The delay between transmission retries.</param>
        /// <param name="MaxNumberOfRetries">The maximum number of transmission retries for HTTP request.</param>
        /// <param name="InternalBufferSize">An optional size of the internal buffers.</param>
        /// <param name="UseHTTPPipelining">Whether to pipeline multiple HTTP request through a single HTTP/TCP connection.</param>
        /// <param name="DisableLogging">Disable logging.</param>
        /// <param name="HTTPLogger">A HTTP logger.</param>
        /// <param name="DNSClient">The DNS client to use.</param>
        public OpenADRClient(URL                                                        RemoteURL,
                             String?                                                    AccessToken                  = null,

                             HTTPHostname?                                              VirtualHostname              = null,
                             I18NString?                                                Description                  = null,
                             Boolean?                                                   PreferIPv4                   = null,
                             RemoteTLSServerCertificateValidationHandler<IHTTPClient>?  RemoteCertificateValidator   = null,
                             LocalCertificateSelectionHandler?                          LocalCertificateSelector     = null,
                             X509Certificate?                                           ClientCert                   = null,
                             SslProtocols?                                              TLSProtocol                  = null,
                             HTTPContentType?                                           ContentType                  = null,
                             AcceptTypes?                                               Accept                       = null,
                             IHTTPAuthentication?                                       HTTPAuthentication           = null,
                             String?                                                    HTTPUserAgent                = DefaultHTTPUserAgent,
                             ConnectionType?                                            Connection                   = null,
                             TimeSpan?                                                  RequestTimeout               = null,
                             TransmissionRetryDelayDelegate?                            TransmissionRetryDelay       = null,
                             UInt16?                                                    MaxNumberOfRetries           = DefaultMaxNumberOfRetries,
                             UInt32?                                                    InternalBufferSize           = DefaultInternalBufferSize,
                             Boolean?                                                   UseHTTPPipelining            = null,
                             Boolean?                                                   DisableLogging               = false,
                             String?                                                    LoggingPath                  = null,
                             String?                                                    LoggingContext               = null,
                             LogfileCreatorDelegate?                                    LogfileCreator               = null,
                             HTTPClientLogger?                                          HTTPLogger                   = null,
                             DNSClient?                                                 DNSClient                    = null)

            : base(RemoteURL,
                   VirtualHostname,
                   Description,
                   PreferIPv4,
                   RemoteCertificateValidator,
                   LocalCertificateSelector,
                   ClientCert,
                   TLSProtocol,
                   ContentType        ?? HTTPContentType.Application.JSON_UTF8,
                   Accept             ?? AcceptTypes.FromHTTPContentTypes(HTTPContentType.Application.JSON_UTF8),
                   HTTPAuthentication ?? (AccessToken.IsNotNullOrEmpty()
                                              ? HTTPTokenAuthentication.Parse(AccessToken)
                                              : null),
                   HTTPUserAgent      ?? DefaultHTTPUserAgent,
                   Connection         ?? ConnectionType.Close,
                   RequestTimeout     ?? DefaultRequestTimeout,
                   TransmissionRetryDelay,
                   MaxNumberOfRetries,
                   InternalBufferSize,
                   UseHTTPPipelining,
                   DisableLogging,
                   HTTPLogger,
                   DNSClient)

        {



            //this.APIVersionHash  = APIVersionHashes?[nameof(OpenADRAPI)]?.Value<String>()?.Trim() ?? "";

            //this.OpenADRAPIPath  = Path.Combine(this.LoggingPath, "OpenADRAPI");

            //if (!DisableLogging)
            //{
            //    Directory.CreateDirectory(OpenADRAPIPath);
            //}

            this.Counters    = new APICounters();

            this.HTTPLogger  = this.DisableLogging == false
                                   ? new Logger(
                                         this,
                                         LoggingPath,
                                         LoggingContext,
                                         LogfileCreator
                                     )
                                   : null;

            //DebugX.Log(nameof(OpenADRClient) + " version '" + APIVersionHash + "' initialized...");

        }

        #endregion


        public URL remoteURL { get; set; }



        #region GetPrograms      (...)

        /// <summary>
        /// Get all programs from the remote API.
        /// </summary>
        /// <param name="From">An optional 'from' timestamp (inclusive).</param>
        /// <param name="To">An optional 'to' timestamp (exclusive).</param>
        /// <param name="Offset">An optional 'offset' within the result set.</param>
        /// <param name="Limit">An optional 'limit' of the result set.</param>
        /// 
        /// <param name="EventTrackingId">An optional event tracking identification for correlating this request with other events.</param>
        /// <param name="RequestTimeout">An optional timeout for this request.</param>
        /// <param name="CancellationToken">An optional cancellation token to cancel this request.</param>
        public async Task<IEnumerable<Program>>

            GetPrograms(DateTime?           From                = null,
                        DateTime?           To                  = null,
                        UInt64?             Offset              = null,
                        UInt64?             Limit               = null,

                        EventTracking_Id?   EventTrackingId     = null,
                        TimeSpan?           RequestTimeout      = null,
                        CancellationToken   CancellationToken   = default)

        {

            #region Init

            var startTime        = Timestamp.Now;
            var eventTrackingId  = EventTrackingId ?? EventTracking_Id.New;
            var requestTimeout   = RequestTimeout  ?? this.RequestTimeout;

            Counters.GetPrograms.IncRequests_OK();

            var programs         = new List<Program>();

            #endregion

            #region Send OnGetProgramsRequest event

            await LogEvent(
                      OnGetProgramsRequest,
                      loggingDelegate => loggingDelegate.Invoke(
                          startTime,
                          this,
                          eventTrackingId,
                          requestTimeout,
                          CancellationToken
                      )
                  );

            #endregion


            try
            {

                #region Upstream HTTP request...

                var dateAndPaginationFilters  = new DateAndPaginationFilters(From, To, Offset, Limit);

                var httpResponse              = await HTTPClientFactory.Create(
                                                          remoteURL,
                                                          VirtualHostname,
                                                          Description,
                                                          PreferIPv4,
                                                          RemoteCertificateValidator,
                                                          LocalCertificateSelector,
                                                          ClientCert,
                                                          TLSProtocol,
                                                          ContentType,
                                                          Accept,
                                                          Authentication,
                                                          HTTPUserAgent,
                                                          Connection,
                                                          RequestTimeout,
                                                          TransmissionRetryDelay,
                                                          MaxNumberOfRetries,
                                                          InternalBufferSize,
                                                          UseHTTPPipelining,
                                                          DisableLogging,
                                                          HTTPLogger,
                                                          DNSClient
                                                      ).

                                                      GET(remoteURL.Path + dateAndPaginationFilters.ToHTTPQueryString(),
                                                          Accept:                ocpiAcceptTypes,
                                                          //Authentication:        TokenAuth,
                                                          Connection:            ConnectionType.Close,
                                                          //RequestBuilder:        requestBuilder => {
                                                          //                            requestBuilder.Set("X-Request-ID",     requestId);
                                                          //                            requestBuilder.Set("X-Correlation-ID", correlationId);
                                                          //                        },
                                                          RequestLogDelegate:    OnGetProgramsHTTPRequest,
                                                          ResponseLogDelegate:   OnGetProgramsHTTPResponse,
                                                          EventTrackingId:       eventTrackingId,
                                                          //NumberOfRetry:         transmissionRetry,
                                                          RequestTimeout:        RequestTimeout ?? this.RequestTimeout,
                                                          CancellationToken:     CancellationToken).

                                                      ConfigureAwait(false);

                #endregion


                foreach (var programJSONToken in httpResponse.HTTPBodyAsJSONArray ?? [])
                {
                    if (programJSONToken is JObject programJSON &&
                        Program.TryParse(programJSON, out var program, out var errorResponse))
                    {
                        programs.Add(program);
                    }
                    //else
                    //{
                    //    response = OCPIResponse<String, IEnumerable<Program>>.Error(
                    //        new ErrorResponse(
                    //            httpResponse,
                    //            requestId,
                    //            correlationId,
                    //            errorResponse
                    //        )
                    //    );
                    //    Counters.GetPrograms.IncResponses_Error();
                    //    return response;
                    //}
                }

                Counters.GetPrograms.IncResponses_OK();

            }

            catch (Exception e)
            {
                //response = OCPIResponse<String, IEnumerable<Program>>.Exception(e);
                programs.Clear();
                Counters.GetPrograms.IncResponses_Error();
            }


            #region Send OnGetProgramsResponse event

            var endtime = Timestamp.Now;

            await LogEvent(
                      OnGetProgramsResponse,
                      loggingDelegate => loggingDelegate.Invoke(
                          endtime,
                          this,
                          eventTrackingId,
                          requestTimeout,

                          programs,
                          endtime - startTime,

                          CancellationToken
                      )
                  );

            #endregion

            return programs;

        }

        #endregion


        #region GetProgram         (ProgramId, ...)

        /// <summary>
        /// Get the program specified by the given program identification from the remote API.
        /// </summary>
        /// <param name="ProgramId">The identification of the requested program.</param>
        /// 
        /// <param name="VersionId">An optional OCPI version identification.</param>
        /// <param name="RequestId">An optional request identification.</param>
        /// <param name="CorrelationId">An optional request correlation identification.</param>
        /// 
        /// <param name="EventTrackingId">An optional event tracking identification for correlating this request with other events.</param>
        /// <param name="RequestTimeout">An optional timeout for this request.</param>
        /// <param name="CancellationToken">An optional cancellation token to cancel this request.</param>
        public async Task<Program>

            GetProgram(Program_Id          ProgramId,

                       EventTracking_Id?   EventTrackingId     = null,
                       TimeSpan?           RequestTimeout      = null,
                       CancellationToken   CancellationToken   = default)

        {

            #region Init

            var startTime        = Timestamp.Now;
            var eventTrackingId  = EventTrackingId ?? EventTracking_Id.New;
            var requestTimeout   = RequestTimeout  ?? this.RequestTimeout;

            Counters.GetProgram.IncRequests_OK();

            Program? response = null;

            #endregion

            #region Send OnGetProgramRequest event

            await LogEvent(
                      OnGetProgramRequest,
                      loggingDelegate => loggingDelegate.Invoke(
                          startTime,
                          this,
                          ProgramId,
                          eventTrackingId,
                          requestTimeout,
                          CancellationToken
                      )
                  );

            #endregion


            try
            {

                #region Upstream HTTP request...

                var httpResponse = await HTTPClientFactory.Create(
                                             remoteURL,
                                             VirtualHostname,
                                             Description,
                                             PreferIPv4,
                                             RemoteCertificateValidator,
                                             LocalCertificateSelector,
                                             ClientCert,
                                             TLSProtocol,
                                             ContentType,
                                             Accept,
                                             Authentication,
                                             HTTPUserAgent,
                                             Connection,
                                             RequestTimeout,
                                             TransmissionRetryDelay,
                                             MaxNumberOfRetries,
                                             InternalBufferSize,
                                             UseHTTPPipelining,
                                             DisableLogging,
                                             HTTPLogger,
                                             DNSClient
                                         ).

                                         GET(remoteURL.Path + ProgramId.ToString(),
                                             Accept:                ocpiAcceptTypes,
                                             //Authentication:        TokenAuth,
                                             Connection:            ConnectionType.Close,
                                             //RequestBuilder:        requestBuilder => {
                                             //                           requestBuilder.Set("X-Request-ID",     requestId);
                                             //                           requestBuilder.Set("X-Correlation-ID", correlationId);
                                             //                       },
                                             RequestLogDelegate:    OnGetProgramHTTPRequest,
                                             ResponseLogDelegate:   OnGetProgramHTTPResponse,
                                             EventTrackingId:       eventTrackingId,
                                             //NumberOfRetry:         transmissionRetry,
                                             RequestTimeout:        RequestTimeout ?? this.RequestTimeout,
                                             CancellationToken:     CancellationToken).

                                         ConfigureAwait(false);

                #endregion

                //response = OCPIResponse<Program>.ParseJObject(
                //               httpResponse,
                //               requestId,
                //               correlationId,
                //               json => Program.Parse(json)
                //           );

                Counters.GetProgram.IncResponses_OK();

            }

            catch (Exception e)
            {
                //response = OCPIResponse<String, Program>.Exception(e);
                Counters.GetProgram.IncResponses_Error();
            }


            #region Send OnGetProgramResponse event

            var endtime = Timestamp.Now;

            await LogEvent(
                      OnGetProgramResponse,
                      loggingDelegate => loggingDelegate.Invoke(
                          endtime,
                          this,
                          ProgramId,

                          eventTrackingId,
                          requestTimeout,

                          response,
                          endtime - startTime,
                          CancellationToken
                      )
                  );

            #endregion

            return response;

        }

        #endregion







        #region (private)   LogEvent     (Logger, LogHandler, ...)

        private Task LogEvent<TDelegate>(TDelegate?                                         Logger,
                                         Func<TDelegate, Task>                              LogHandler,
                                         [CallerArgumentExpression(nameof(Logger))] String  EventName     = "",
                                         [CallerMemberName()]                       String  OCPICommand   = "")

            where TDelegate : Delegate

            => LogEvent(
                   nameof(OpenADRClient),
                   Logger,
                   LogHandler,
                   EventName,
                   OCPICommand
               );

        #endregion

        #region (protected) LogEvent     (OCPIIO, Logger, LogHandler, ...)

        protected async Task LogEvent<TDelegate>(String                                             OCPIIO,
                                                 TDelegate?                                         Logger,
                                                 Func<TDelegate, Task>                              LogHandler,
                                                 [CallerArgumentExpression(nameof(Logger))] String  EventName     = "",
                                                 [CallerMemberName()]                       String  OCPICommand   = "")

            where TDelegate : Delegate

        {
            if (Logger is not null)
            {
                try
                {

                    await Task.WhenAll(
                              Logger.GetInvocationList().
                                     OfType<TDelegate>().
                                     Select(LogHandler)
                          );

                }
                catch (Exception e)
                {
                    await HandleErrors(OCPIIO, $"{OCPICommand}.{EventName}", e);
                }
            }
        }

        #endregion

        #region (virtual)   HandleErrors (Module, Caller, ErrorResponse)

        public virtual Task HandleErrors(String  Module,
                                         String  Caller,
                                         String  ErrorResponse)
        {

            DebugX.Log($"{Module}.{Caller}: {ErrorResponse}");

            return Task.CompletedTask;

        }

        #endregion

        #region (virtual)   HandleErrors (Module, Caller, ExceptionOccured)

        public virtual Task HandleErrors(String     Module,
                                         String     Caller,
                                         Exception  ExceptionOccured)
        {

            DebugX.LogException(ExceptionOccured, $"{Module}.{Caller}");

            return Task.CompletedTask;

        }

        #endregion


        #region Dispose()

        /// <summary>
        /// Dispose this object.
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion


    }

}
