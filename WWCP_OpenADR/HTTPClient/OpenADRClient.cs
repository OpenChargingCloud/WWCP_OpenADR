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
    /// The OpenADR HTTP Client.
    /// </summary>
    public class OpenADRClient
    {

        #region Data

        /// <summary>
        /// The default HTTPS user agent.
        /// </summary>
        public const String  DefaultHTTPUserAgent  = "GraphDefined OpenADR HTTP Client";

        #endregion

        #region Properties


        #endregion

        #region Events


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
        public OpenADRClient(URL                              RemoteURL,
                             HTTPHostname?                    VirtualHostname          = null,
                             I18NString?                      Description              = null,
                             Boolean?                         PreferIPv4               = null,
                             HTTPContentType?                 ContentType              = null,
                             AcceptTypes?                     Accept                   = null,
                             IHTTPAuthentication?             HTTPAuthentication       = null,
                             String?                          HTTPUserAgent            = DefaultHTTPUserAgent,
                             ConnectionType?                  Connection               = null,
                             TimeSpan?                        RequestTimeout           = null,
                             TransmissionRetryDelayDelegate?  TransmissionRetryDelay   = null,
                             UInt16?                          MaxNumberOfRetries       = null,
                             UInt32?                          InternalBufferSize       = null,
                             Boolean                          UseHTTPPipelining        = false,
                             Boolean?                         DisableLogging           = false,
                             HTTPClientLogger?                HTTPLogger               = null,
                             DNSClient?                       DNSClient                = null)

        {

            //this.APIVersionHash  = APIVersionHashes?[nameof(OpenADRAPI)]?.Value<String>()?.Trim() ?? "";

            //this.OpenADRAPIPath  = Path.Combine(this.LoggingPath, "OpenADRAPI");

            //if (!DisableLogging)
            //{
            //    Directory.CreateDirectory(OpenADRAPIPath);
            //}


            //DebugX.Log(nameof(OpenADRClient) + " version '" + APIVersionHash + "' initialized...");

        }

        #endregion


    }

}
