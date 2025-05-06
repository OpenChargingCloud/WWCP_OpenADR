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

using System.Security.Cryptography;

using Newtonsoft.Json.Linq;

using BCx509 = Org.BouncyCastle.X509;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Utilities;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Hermod;
using org.GraphDefined.Vanaheimr.Hermod.DNS;
using org.GraphDefined.Vanaheimr.Hermod.Mail;
using org.GraphDefined.Vanaheimr.Hermod.SMTP;
using org.GraphDefined.Vanaheimr.Hermod.HTTP;
using org.GraphDefined.Vanaheimr.Hermod.WebSocket;
using org.GraphDefined.Vanaheimr.Norn.NTS;

using cloud.charging.open.protocols.WWCP;
using cloud.charging.open.protocols.WWCP.NetworkingNode;

#endregion

namespace cloud.charging.open.protocols.OpenADRv3.Node
{

    /// <summary>
    /// A OpenADR for testing
    /// </summary>
    public partial class TestOpenADRNode : AOpenADRNode,
                                           IOpenADRNode
    {

        #region Constructor(s)

        /// <summary>
        /// Create a new charging station management system for testing.
        /// </summary>
        /// <param name="Id">The unique identification of this charging station management system.</param>
        public TestOpenADRNode(NetworkingNode_Id                 Id,
                               String                            VendorName,
                               String                            Model,
                               String?                           SerialNumber                            = null,
                               String?                           SoftwareVersion                         = null,
                               I18NString?                       Description                             = null,
                               CustomData?                       CustomData                              = null,

                               AsymmetricCipherKeyPair?          ClientCAKeyPair                         = null,
                               BCx509.X509Certificate?           ClientCACertificate                     = null,

                               SignaturePolicy?                  SignaturePolicy                         = null,
                               SignaturePolicy?                  ForwardingSignaturePolicy               = null,

                               Func<AOpenADRNode, HTTPAPI>?      HTTPAPI                                 = null,
                               Boolean                           HTTPAPI_Disabled                        = false,
                               IPPort?                           HTTPAPI_Port                            = null,
                               String?                           HTTPAPI_ServerName                      = null,
                               String?                           HTTPAPI_ServiceName                     = null,
                               EMailAddress?                     HTTPAPI_RobotEMailAddress               = null,
                               String?                           HTTPAPI_RobotGPGPassphrase              = null,
                               Boolean                           HTTPAPI_EventLoggingDisabled            = false,

                               Func<AOpenADRNode, OpenADRAPI>?   OpenADRAPI                              = null,
                               Boolean                           OpenADRAPI_Disabled                     = false,
                               HTTPPath?                         OpenADRAPI_Path                         = null,
                               String?                           OpenADRAPI_FileSystemPath               = null,

                               Func<AOpenADRNode, WebAPI>?       WebAPI                                  = null,
                               Boolean                           WebAPI_Disabled                         = false,
                               HTTPPath?                         WebAPI_Path                             = null,

                               Func<AOpenADRNode, NTSServer>?    NTSServer                               = null,
                               Boolean                           NTSServer_Disabled                      = true,

                               WebSocketServer?                  ControlWebSocketServer                  = null,

                               TimeSpan?                         DefaultRequestTimeout                   = null,

                               Boolean                           DisableSendHeartbeats                   = false,
                               TimeSpan?                         SendHeartbeatsEvery                     = null,

                               Boolean                           DisableMaintenanceTasks                 = false,
                               TimeSpan?                         MaintenanceEvery                        = null,

                               ISMTPClient?                      SMTPClient                              = null,
                               DNSClient?                        DNSClient                               = null)

            : base(Id,
                   VendorName,
                   Model,
                   SerialNumber,
                   SoftwareVersion,
                   Description,
                   CustomData,

                   ClientCAKeyPair,
                   ClientCACertificate,

                   SignaturePolicy,
                   ForwardingSignaturePolicy,

                   HTTPAPI,
                   HTTPAPI_Disabled,
                   HTTPAPI_Port,
                   HTTPAPI_ServerName,
                   HTTPAPI_ServiceName,
                   HTTPAPI_RobotEMailAddress,
                   HTTPAPI_RobotGPGPassphrase,
                   HTTPAPI_EventLoggingDisabled,

                   OpenADRAPI,
                   OpenADRAPI_Disabled,
                   OpenADRAPI_Path,
                   OpenADRAPI_FileSystemPath,

                   WebAPI,
                   WebAPI_Disabled,
                   WebAPI_Path,

                   NTSServer,
                   NTSServer_Disabled,

                   ControlWebSocketServer,

                   DefaultRequestTimeout,

                   DisableSendHeartbeats,
                   SendHeartbeatsEvery,

                   DisableMaintenanceTasks,
                   MaintenanceEvery,

                   SMTPClient,
                   DNSClient)

        {

            //Directory.CreateDirectory(Path.Combine(AppContext.BaseDirectory, "HTTPSSEs"));

        }

        #endregion


        public void EnableLogging()
        {



        }

    }

}
