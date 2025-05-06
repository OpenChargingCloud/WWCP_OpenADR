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

using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

using BCx509 = Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Hermod;
using org.GraphDefined.Vanaheimr.Hermod.DNS;
using org.GraphDefined.Vanaheimr.Hermod.Mail;
using org.GraphDefined.Vanaheimr.Hermod.HTTP;
using org.GraphDefined.Vanaheimr.Hermod.SMTP;
using org.GraphDefined.Vanaheimr.Hermod.Sockets;
using org.GraphDefined.Vanaheimr.Hermod.WebSocket;
using org.GraphDefined.Vanaheimr.Norn.NTS;

using cloud.charging.open.protocols.WWCP;
using cloud.charging.open.protocols.WWCP.NetworkingNode;

#endregion

namespace cloud.charging.open.protocols.OpenADRv3.Node
{

    /// <summary>
    /// An abstract OpenADR node.
    /// </summary>
    public abstract class AOpenADRNode : AWWCPNetworkingNode,
                                         INetworkingNode
    {

        #region Data


        private   readonly HashSet<SignaturePolicy>  signaturePolicies       = [];

        private   readonly TimeSpan                  defaultRequestTimeout   = TimeSpan.FromSeconds(30);

        #endregion

        #region Properties

        /// <summary>
        /// The OpenADR vendor identification.
        /// </summary>
        [Mandatory]
        public String                       VendorName                               { get; }      = "";

        /// <summary>
        ///  The OpenADR model identification.
        /// </summary>
        [Mandatory]
        public String                       Model                                    { get; }      = "";

        /// <summary>
        /// The optional serial number of the OpenADR.
        /// </summary>
        [Optional]
        public String?                      SerialNumber                             { get; }

        /// <summary>
        /// The optional firmware version of the OpenADR.
        /// </summary>
        [Optional]
        public String?                      SoftwareVersion                          { get; }


        public AsymmetricCipherKeyPair?     ClientCAKeyPair                          { get; }
        public BCx509.X509Certificate?      ClientCACertificate                      { get; }



        /// <summary>
        /// The time at the OpenADR.
        /// </summary>
        public DateTime?                    OpenADRTime                              { get; set; } = Timestamp.Now;


        public HTTPAPI?                     HTTPAPI                                  { get; }

        public OpenADRHTTPAPI?                  OpenADRAPI                               { get; }
        public HTTPPath?                    OpenADRAPI_Path                          { get; }

        public WebAPI?                      WebAPI                                   { get; }
        public HTTPPath?                    WebAPI_Path                              { get; }


        public NTSServer?                   NTSServer                                { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new abstract OpenADR node.
        /// </summary>
        /// <param name="Id">The unique identification of this OpenADR node.</param>
        /// <param name="Description">An optional multi-language description of the OpenADR node.</param>
        public AOpenADRNode(NetworkingNode_Id                    Id,
                            String                               VendorName,
                            String                               Model,
                            String?                              SerialNumber                            = null,
                            String?                              SoftwareVersion                         = null,
                            I18NString?                          Description                             = null,
                            CustomData?                          CustomData                              = null,

                            AsymmetricCipherKeyPair?             ClientCAKeyPair                         = null,
                            BCx509.X509Certificate?              ClientCACertificate                     = null,

                            SignaturePolicy?                     SignaturePolicy                         = null,
                            SignaturePolicy?                     ForwardingSignaturePolicy               = null,

                            Func<AOpenADRNode, HTTPAPI>?         HTTPAPI                                 = null,
                            Boolean                              HTTPAPI_Disabled                        = false,
                            IPPort?                              HTTPAPI_Port                            = null,
                            String?                              HTTPAPI_ServerName                      = null,
                            String?                              HTTPAPI_ServiceName                     = null,
                            EMailAddress?                        HTTPAPI_RobotEMailAddress               = null,
                            String?                              HTTPAPI_RobotGPGPassphrase              = null,
                            Boolean                              HTTPAPI_EventLoggingDisabled            = false,

                            Func<AOpenADRNode, OpenADRHTTPAPI>?  OpenADRAPI                              = null,
                            Boolean                              OpenADRAPI_Disabled                     = false,
                            HTTPPath?                            OpenADRAPI_Path                         = null,
                            String?                              OpenADRAPI_FileSystemPath               = null,

                            Func<AOpenADRNode, WebAPI>?          WebAPI                                  = null,
                            Boolean                              WebAPI_Disabled                         = false,
                            HTTPPath?                            WebAPI_Path                             = null,

                            Func<AOpenADRNode, NTSServer>?       NTSServerBuilder                        = null,
                            Boolean                              NTSServer_Disabled                      = true,

                            WebSocketServer?                     ControlWebSocketServer                  = null,

                            TimeSpan?                            DefaultRequestTimeout                   = null,

                            Boolean                              DisableSendHeartbeats                   = false,
                            TimeSpan?                            SendHeartbeatsEvery                     = null,

                            Boolean                              DisableMaintenanceTasks                 = false,
                            TimeSpan?                            MaintenanceEvery                        = null,

                            ISMTPClient?                         SMTPClient                              = null,
                            DNSClient?                           DNSClient                               = null)

            : base(Id,
                   Description,
                   CustomData,

                   SignaturePolicy,
                   ForwardingSignaturePolicy,

                   !HTTPAPI_Disabled
                       ? new HTTPExtAPI(
                             HTTPServerPort:         HTTPAPI_Port               ?? IPPort.Auto,
                             HTTPServerName:         HTTPAPI_ServerName         ?? "GraphDefined OpenADR Test",
                             HTTPServiceName:        HTTPAPI_ServiceName        ?? "GraphDefined OpenADR Test Service",
                             APIRobotEMailAddress:   HTTPAPI_RobotEMailAddress  ?? EMailAddress.Parse("GraphDefined OpenADR Test Robot <robot@charging.cloud>"),
                             APIRobotGPGPassphrase:  HTTPAPI_RobotGPGPassphrase ?? "test123",
                             SMTPClient:             SMTPClient                 ?? new NullMailer(),
                             DNSClient:              DNSClient,
                             AutoStart:              true
                         )
                       : null,
                   ControlWebSocketServer,

                   DisableSendHeartbeats,
                   SendHeartbeatsEvery,

                   DefaultRequestTimeout ?? TimeSpan.FromMinutes(1),

                   DisableMaintenanceTasks,
                   MaintenanceEvery,

                   DNSClient)

        {

            if (VendorName.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(VendorName),  "The given vendor name must not be null or empty!");

            if (Model.     IsNullOrEmpty())
                throw new ArgumentNullException(nameof(Model),       "The given model must not be null or empty!");

            this.VendorName                             = VendorName;
            this.Model                                  = Model;
            this.SerialNumber                           = SerialNumber;
            this.SoftwareVersion                        = SoftwareVersion;

            this.ClientCAKeyPair                        = ClientCAKeyPair;
            this.ClientCACertificate                    = ClientCACertificate;

            #region Setup OpenADR API

            this.OpenADRAPI_Path                        = OpenADRAPI_Path ?? HTTPPath.Parse("OpenADR");

            if (this.HTTPExtAPI is not null)
            {

                this.HTTPAPI                            = !HTTPAPI_Disabled
                                                              ? HTTPAPI?.Invoke(this)    ?? new HTTPAPI(
                                                                                                this,
                                                                                                HTTPExtAPI
                                                                                                //EventLoggingDisabled: HTTPAPI_EventLoggingDisabled
                                                                                            )
                                                              : null;

                if (this.HTTPAPI is not null)
                {

                    #region HTTP API Security Settings

                    this.HTTPAPI.HTTPBaseAPI.HTTPServer.AddAuth(request => {

                        // Allow some URLs for anonymous access...
                        if (request.Path.StartsWith(this.HTTPAPI.URLPathPrefix + this.WebAPI_Path))
                        {
                            return HTTPExtAPI.Anonymous;
                        }

                        return null;

                    });

                    #endregion


                    if (!OpenADRAPI_Disabled)
                    {

                        this.OpenADRAPI = OpenADRAPI?.Invoke(this) ?? new OpenADRHTTPAPI(
                                                                          HTTPExtAPI,
                                                                          URLPathPrefix: this.OpenADRAPI_Path
                                                                      );

                    }

                    if (!WebAPI_Disabled)
                    {

                        this.WebAPI                     = WebAPI?.Invoke(this)          ?? new WebAPI(
                                                                                               this,
                                                                                               this.HTTPAPI.HTTPBaseAPI,
                                                                                               URLPathPrefix:   this.WebAPI_Path
                                                                                           );

                    }

                }

            }

            #endregion

            #region Setup NTS Server

            if (!NTSServer_Disabled && NTSServerBuilder is not null)
            {

                this.NTSServer                         = NTSServerBuilder.Invoke(this);

            }

            #endregion

        }

        #endregion




        #region (protected internal) WriteToDatabaseFileAndNotify(ChargingStation,                      MessageType,    OldChargingStation = null, ...)

        ///// <summary>
        ///// Write the given chargingStation to the database and send out notifications.
        ///// </summary>
        ///// <param name="ChargingStation">The charging station.</param>
        ///// <param name="MessageType">The chargingStation notification.</param>
        ///// <param name="OldChargingStation">The old/updated charging station.</param>
        ///// <param name="EventTrackingId">An optional unique event tracking identification for correlating this request with other events.</param>
        ///// <param name="CurrentUserId">An optional user identification initiating this command/request.</param>
        //protected internal async Task WriteToDatabaseFileAndNotify(ChargingStation             ChargingStation,
        //                                                           NotificationMessageType  MessageType,
        //                                                           ChargingStation             OldChargingStation   = null,
        //                                                           EventTracking_Id         EventTrackingId   = null,
        //                                                           User_Id?                 CurrentUserId     = null)
        //{

        //    if (ChargingStation is null)
        //        throw new ArgumentNullException(nameof(ChargingStation),  "The given chargingStation must not be null or empty!");

        //    if (MessageType.IsNullOrEmpty)
        //        throw new ArgumentNullException(nameof(MessageType),   "The given message type must not be null or empty!");


        //    var eventTrackingId = EventTrackingId ?? EventTracking_Id.New;

        //    await WriteToDatabaseFile(MessageType,
        //                              ChargingStation.ToJSON(false, true),
        //                              eventTrackingId,
        //                              CurrentUserId);

        //    await SendNotifications(ChargingStation,
        //                            MessageType,
        //                            OldChargingStation,
        //                            eventTrackingId,
        //                            CurrentUserId);

        //}

        #endregion

        #region (protected internal) SendNotifications           (ChargingStation,                      MessageType(s), OldChargingStation = null, ...)

        //protected virtual String ChargingStationHTMLInfo(ChargingStation ChargingStation)

        //    => String.Concat(ChargingStation.Name.IsNeitherNullNorEmpty()
        //                         ? String.Concat("<a href=\"https://", ExternalDNSName, BasePath, "/chargingStations/", ChargingStation.Id, "\">", ChargingStation.Name.FirstText(), "</a> ",
        //                                        "(<a href=\"https://", ExternalDNSName, BasePath, "/chargingStations/", ChargingStation.Id, "\">", ChargingStation.Id, "</a>)")
        //                         : String.Concat("<a href=\"https://", ExternalDNSName, BasePath, "/chargingStations/", ChargingStation.Id, "\">", ChargingStation.Id, "</a>"));

        //protected virtual String ChargingStationTextInfo(ChargingStation ChargingStation)

        //    => String.Concat(ChargingStation.Name.IsNeitherNullNorEmpty()
        //                         ? String.Concat("'", ChargingStation.Name.FirstText(), "' (", ChargingStation.Id, ")")
        //                         : String.Concat("'", ChargingStation.Id.ToString(), "'"));


        ///// <summary>
        ///// Send chargingStation notifications.
        ///// </summary>
        ///// <param name="ChargingStation">The charging station.</param>
        ///// <param name="MessageType">The chargingStation notification.</param>
        ///// <param name="OldChargingStation">The old/updated charging station.</param>
        ///// <param name="EventTrackingId">An optional unique event tracking identification for correlating this request with other events.</param>
        ///// <param name="CurrentUserId">The invoking chargingStation identification</param>
        //protected internal virtual Task SendNotifications(ChargingStation             ChargingStation,
        //                                                  NotificationMessageType  MessageType,
        //                                                  ChargingStation             OldChargingStation   = null,
        //                                                  EventTracking_Id         EventTrackingId   = null,
        //                                                  User_Id?                 CurrentUserId     = null)

        //    => SendNotifications(ChargingStation,
        //                         new NotificationMessageType[] { MessageType },
        //                         OldChargingStation,
        //                         EventTrackingId,
        //                         CurrentUserId);


        ///// <summary>
        ///// Send chargingStation notifications.
        ///// </summary>
        ///// <param name="ChargingStation">The charging station.</param>
        ///// <param name="MessageTypes">The chargingStation notifications.</param>
        ///// <param name="OldChargingStation">The old/updated charging station.</param>
        ///// <param name="EventTrackingId">An optional unique event tracking identification for correlating this request with other events.</param>
        ///// <param name="CurrentUserId">The invoking chargingStation identification</param>
        //protected internal async virtual Task SendNotifications(ChargingStation                          ChargingStation,
        //                                                        IEnumerable<NotificationMessageType>  MessageTypes,
        //                                                        ChargingStation                          OldChargingStation   = null,
        //                                                        EventTracking_Id                      EventTrackingId   = null,
        //                                                        User_Id?                              CurrentUserId     = null)
        //{

        //    if (ChargingStation is null)
        //        throw new ArgumentNullException(nameof(ChargingStation),  "The given chargingStation must not be null or empty!");

        //    var messageTypesHash = new HashSet<NotificationMessageType>(MessageTypes.Where(messageType => !messageType.IsNullOrEmpty));

        //    if (messageTypesHash.IsNullOrEmpty())
        //        throw new ArgumentNullException(nameof(MessageTypes),  "The given enumeration of message types must not be null or empty!");

        //    if (messageTypesHash.Contains(addChargingStationIfNotExists_MessageType))
        //        messageTypesHash.Add(addChargingStation_MessageType);

        //    if (messageTypesHash.Contains(addOrUpdateChargingStation_MessageType))
        //        messageTypesHash.Add(OldChargingStation == null
        //                               ? addChargingStation_MessageType
        //                               : updateChargingStation_MessageType);

        //    var messageTypes = messageTypesHash.ToArray();


        //    ComparizionResult? comparizionResult = null;

        //    if (messageTypes.Contains(updateChargingStation_MessageType))
        //        comparizionResult = ChargingStation.CompareWith(OldChargingStation);


        //    if (!DisableNotifications)
        //    {

        //        #region Telegram Notifications

        //        if (TelegramClient != null)
        //        {
        //            try
        //            {

        //                var AllTelegramNotifications  = ChargingStation.GetNotificationsOf<TelegramNotification>(messageTypes).
        //                                                     ToSafeHashSet();

        //                if (AllTelegramNotifications.SafeAny())
        //                {

        //                    if (messageTypes.Contains(addChargingStation_MessageType))
        //                        await TelegramClient.SendTelegrams(ChargingStationHTMLInfo(ChargingStation) + " was successfully created.",
        //                                                           AllTelegramNotifications.Select(TelegramNotification => TelegramNotification.Username),
        //                                                           Telegram.Bot.Types.Enums.ParseMode.Html);

        //                    if (messageTypes.Contains(updateChargingStation_MessageType))
        //                        await TelegramClient.SendTelegrams(ChargingStationHTMLInfo(ChargingStation) + " information had been successfully updated.\n" + comparizionResult?.ToTelegram(),
        //                                                           AllTelegramNotifications.Select(TelegramNotification => TelegramNotification.Username),
        //                                                           Telegram.Bot.Types.Enums.ParseMode.Html);

        //                }

        //            }
        //            catch (Exception e)
        //            {
        //                DebugX.LogException(e);
        //            }
        //        }

        //        #endregion

        //        #region SMS Notifications

        //        try
        //        {

        //            var AllSMSNotifications  = ChargingStation.GetNotificationsOf<SMSNotification>(messageTypes).
        //                                                    ToSafeHashSet();

        //            if (AllSMSNotifications.SafeAny())
        //            {

        //                if (messageTypes.Contains(addChargingStation_MessageType))
        //                    SendSMS(String.Concat("ChargingStation '", ChargingStation.Name.FirstText(), "' was successfully created. ",
        //                                          "https://", ExternalDNSName, BasePath, "/chargingStations/", ChargingStation.Id),
        //                            AllSMSNotifications.Select(smsPhoneNumber => smsPhoneNumber.PhoneNumber.ToString()).ToArray(),
        //                            SMSSenderName);

        //                if (messageTypes.Contains(updateChargingStation_MessageType))
        //                    SendSMS(String.Concat("ChargingStation '", ChargingStation.Name.FirstText(), "' information had been successfully updated. ",
        //                                          "https://", ExternalDNSName, BasePath, "/chargingStations/", ChargingStation.Id),
        //                                          // + {Updated information}
        //                            AllSMSNotifications.Select(smsPhoneNumber => smsPhoneNumber.PhoneNumber.ToString()).ToArray(),
        //                            SMSSenderName);

        //            }

        //        }
        //        catch (Exception e)
        //        {
        //            DebugX.LogException(e);
        //        }

        //        #endregion

        //        #region HTTPS Notifications

        //        try
        //        {

        //            var AllHTTPSNotifications  = ChargingStation.GetNotificationsOf<HTTPSNotification>(messageTypes).
        //                                                      ToSafeHashSet();

        //            if (AllHTTPSNotifications.SafeAny())
        //            {

        //                if (messageTypes.Contains(addChargingStation_MessageType))
        //                    await SendHTTPSNotifications(AllHTTPSNotifications,
        //                                                 new JObject(
        //                                                     new JProperty("chargingStationCreated",
        //                                                         ChargingStation.ToJSON()
        //                                                     ),
        //                                                     new JProperty("timestamp", Timestamp.Now.ToIso8601())
        //                                                 ));

        //                if (messageTypes.Contains(updateChargingStation_MessageType))
        //                    await SendHTTPSNotifications(AllHTTPSNotifications,
        //                                                 new JObject(
        //                                                     new JProperty("chargingStationUpdated",
        //                                                         ChargingStation.ToJSON()
        //                                                     ),
        //                                                     new JProperty("timestamp", Timestamp.Now.ToIso8601())
        //                                                 ));

        //            }

        //        }
        //        catch (Exception e)
        //        {
        //            DebugX.LogException(e);
        //        }

        //        #endregion

        //        #region EMailNotifications

        //        if (SMTPClient != null)
        //        {
        //            try
        //            {

        //                var AllEMailNotifications  = ChargingStation.GetNotificationsOf<EMailNotification>(messageTypes).
        //                                                          ToSafeHashSet();

        //                if (AllEMailNotifications.SafeAny())
        //                {

        //                    if (messageTypes.Contains(addChargingStation_MessageType))
        //                        await SMTPClient.Send(
        //                                 new HTMLEMailBuilder() {

        //                                     From           = Robot.EMail,
        //                                     To             = EMailAddressListBuilder.Create(EMailAddressList.Create(AllEMailNotifications.Select(emailnotification => emailnotification.EMailAddress))),
        //                                     Passphrase     = APIPassphrase,
        //                                     Subject        = ChargingStationTextInfo(ChargingStation) + " was successfully created",

        //                                     HTMLText       = String.Concat(HTMLEMailHeader(ExternalDNSName, BasePath, EMailType.Notification),
        //                                                                    ChargingStationHTMLInfo(ChargingStation) + " was successfully created.",
        //                                                                    HTMLEMailFooter(ExternalDNSName, BasePath, EMailType.Notification)),

        //                                     PlainText      = String.Concat(TextEMailHeader(ExternalDNSName, BasePath, EMailType.Notification),
        //                                                                    ChargingStationTextInfo(ChargingStation) + " was successfully created.\r\n",
        //                                                                    "https://", ExternalDNSName, BasePath, "/chargingStations/", ChargingStation.Id, "\r\r\r\r",
        //                                                                    TextEMailFooter(ExternalDNSName, BasePath, EMailType.Notification)),

        //                                     SecurityLevel  = EMailSecurity.autosign

        //                                 });

        //                    if (messageTypes.Contains(updateChargingStation_MessageType))
        //                        await SMTPClient.Send(
        //                                 new HTMLEMailBuilder() {

        //                                     From           = Robot.EMail,
        //                                     To             = EMailAddressListBuilder.Create(EMailAddressList.Create(AllEMailNotifications.Select(emailnotification => emailnotification.EMailAddress))),
        //                                     Passphrase     = APIPassphrase,
        //                                     Subject        = ChargingStationTextInfo(ChargingStation) + " information had been successfully updated",

        //                                     HTMLText       = String.Concat(HTMLEMailHeader(ExternalDNSName, BasePath, EMailType.Notification),
        //                                                                    ChargingStationHTMLInfo(ChargingStation) + " information had been successfully updated.<br /><br />",
        //                                                                    comparizionResult?.ToHTML() ?? "",
        //                                                                    HTMLEMailFooter(ExternalDNSName, BasePath, EMailType.Notification)),

        //                                     PlainText      = String.Concat(TextEMailHeader(ExternalDNSName, BasePath, EMailType.Notification),
        //                                                                    ChargingStationTextInfo(ChargingStation) + " information had been successfully updated.\r\r\r\r",
        //                                                                    comparizionResult?.ToText() ?? "",
        //                                                                    "\r\r\r\r",
        //                                                                    "https://", ExternalDNSName, BasePath, "/chargingStations/", ChargingStation.Id, "\r\r\r\r",
        //                                                                    TextEMailFooter(ExternalDNSName, BasePath, EMailType.Notification)),

        //                                     SecurityLevel  = EMailSecurity.autosign

        //                                 });

        //                }

        //            }
        //            catch (Exception e)
        //            {
        //                DebugX.LogException(e);
        //            }
        //        }

        //        #endregion

        //    }

        //}

        #endregion

        #region (protected internal) SendNotifications           (ChargingStation, ParentChargingStationes, MessageType(s), ...)

        ///// <summary>
        ///// Send chargingStation notifications.
        ///// </summary>
        ///// <param name="ChargingStation">The charging station.</param>
        ///// <param name="ParentChargingStationes">The enumeration of parent charging stationes.</param>
        ///// <param name="MessageType">The chargingStation notification.</param>
        ///// <param name="EventTrackingId">An optional unique event tracking identification for correlating this request with other events.</param>
        ///// <param name="CurrentUserId">The invoking chargingStation identification</param>
        //protected internal virtual Task SendNotifications(ChargingStation               ChargingStation,
        //                                                  IEnumerable<ChargingStation>  ParentChargingStationes,
        //                                                  NotificationMessageType    MessageType,
        //                                                  EventTracking_Id           EventTrackingId   = null,
        //                                                  User_Id?                   CurrentUserId     = null)

        //    => SendNotifications(ChargingStation,
        //                         ParentChargingStationes,
        //                         new NotificationMessageType[] { MessageType },
        //                         EventTrackingId,
        //                         CurrentUserId);


        ///// <summary>
        ///// Send chargingStation notifications.
        ///// </summary>
        ///// <param name="ChargingStation">The charging station.</param>
        ///// <param name="ParentChargingStationes">The enumeration of parent charging stationes.</param>
        ///// <param name="MessageTypes">The user notifications.</param>
        ///// <param name="EventTrackingId">An optional unique event tracking identification for correlating this request with other events.</param>
        ///// <param name="CurrentUserId">An optional user identification initiating this command/request.</param>
        //protected internal async virtual Task SendNotifications(ChargingStation                          ChargingStation,
        //                                                        IEnumerable<ChargingStation>             ParentChargingStationes,
        //                                                        IEnumerable<NotificationMessageType>  MessageTypes,
        //                                                        EventTracking_Id                      EventTrackingId   = null,
        //                                                        User_Id?                              CurrentUserId     = null)
        //{

        //    if (ChargingStation is null)
        //        throw new ArgumentNullException(nameof(ChargingStation),         "The given chargingStation must not be null or empty!");

        //    if (ParentChargingStationes is null)
        //        ParentChargingStationes = new ChargingStation[0];

        //    var messageTypesHash = new HashSet<NotificationMessageType>(MessageTypes.Where(messageType => !messageType.IsNullOrEmpty));

        //    if (messageTypesHash.IsNullOrEmpty())
        //        throw new ArgumentNullException(nameof(MessageTypes),         "The given enumeration of message types must not be null or empty!");

        //    //if (messageTypesHash.Contains(addUserIfNotExists_MessageType))
        //    //    messageTypesHash.Add(addUser_MessageType);

        //    //if (messageTypesHash.Contains(addOrUpdateUser_MessageType))
        //    //    messageTypesHash.Add(OldChargingStation == null
        //    //                           ? addUser_MessageType
        //    //                           : updateUser_MessageType);

        //    var messageTypes = messageTypesHash.ToArray();


        //    if (!DisableNotifications)
        //    {

        //        #region Telegram Notifications

        //        if (TelegramClient != null)
        //        {
        //            try
        //            {

        //                var AllTelegramNotifications  = ParentChargingStationes.
        //                                                    SelectMany(parent => parent.User2ChargingStationEdges).
        //                                                    SelectMany(edge   => edge.Source.GetNotificationsOf<TelegramNotification>(deleteChargingStation_MessageType)).
        //                                                    ToSafeHashSet();

        //                if (AllTelegramNotifications.SafeAny())
        //                {

        //                    if (messageTypes.Contains(deleteChargingStation_MessageType))
        //                        await TelegramClient.SendTelegrams(ChargingStationHTMLInfo(ChargingStation) + " has been deleted.",
        //                                                           AllTelegramNotifications.Select(TelegramNotification => TelegramNotification.Username),
        //                                                           Telegram.Bot.Types.Enums.ParseMode.Html);

        //                }

        //            }
        //            catch (Exception e)
        //            {
        //                DebugX.LogException(e);
        //            }
        //        }

        //        #endregion

        //        #region SMS Notifications

        //        try
        //        {

        //            var AllSMSNotifications = ParentChargingStationes.
        //                                          SelectMany(parent => parent.User2ChargingStationEdges).
        //                                          SelectMany(edge   => edge.Source.GetNotificationsOf<SMSNotification>(deleteChargingStation_MessageType)).
        //                                          ToSafeHashSet();

        //            if (AllSMSNotifications.SafeAny())
        //            {

        //                if (messageTypes.Contains(deleteChargingStation_MessageType))
        //                    SendSMS(String.Concat("ChargingStation '", ChargingStation.Name.FirstText(), "' has been deleted."),
        //                            AllSMSNotifications.Select(smsPhoneNumber => smsPhoneNumber.PhoneNumber.ToString()).ToArray(),
        //                            SMSSenderName);

        //            }

        //        }
        //        catch (Exception e)
        //        {
        //            DebugX.LogException(e);
        //        }

        //        #endregion

        //        #region HTTPS Notifications

        //        try
        //        {

        //            var AllHTTPSNotifications = ParentChargingStationes.
        //                                            SelectMany(parent => parent.User2ChargingStationEdges).
        //                                            SelectMany(edge   => edge.Source.GetNotificationsOf<HTTPSNotification>(deleteChargingStation_MessageType)).
        //                                            ToSafeHashSet();

        //            if (AllHTTPSNotifications.SafeAny())
        //            {

        //                if (messageTypes.Contains(deleteChargingStation_MessageType))
        //                    await SendHTTPSNotifications(AllHTTPSNotifications,
        //                                                 new JObject(
        //                                                     new JProperty("chargingStationDeleted",
        //                                                         ChargingStation.ToJSON()
        //                                                     ),
        //                                                     new JProperty("timestamp", Timestamp.Now.ToIso8601())
        //                                                 ));

        //            }

        //        }
        //        catch (Exception e)
        //        {
        //            DebugX.LogException(e);
        //        }

        //        #endregion

        //        #region EMailNotifications

        //        if (SMTPClient != null)
        //        {
        //            try
        //            {

        //                var AllEMailNotifications = ParentChargingStationes.
        //                                                SelectMany(parent => parent.User2ChargingStationEdges).
        //                                                SelectMany(edge   => edge.Source.GetNotificationsOf<EMailNotification>(deleteChargingStation_MessageType)).
        //                                                ToSafeHashSet();

        //                if (AllEMailNotifications.SafeAny())
        //                {

        //                    if (messageTypes.Contains(deleteChargingStation_MessageType))
        //                        await SMTPClient.Send(
        //                             new HTMLEMailBuilder() {

        //                                 From           = Robot.EMail,
        //                                 To             = EMailAddressListBuilder.Create(EMailAddressList.Create(AllEMailNotifications.Select(emailnotification => emailnotification.EMailAddress))),
        //                                 Passphrase     = APIPassphrase,
        //                                 Subject        = ChargingStationTextInfo(ChargingStation) + " has been deleted",

        //                                 HTMLText       = String.Concat(HTMLEMailHeader(ExternalDNSName, BasePath, EMailType.Notification),
        //                                                                ChargingStationHTMLInfo(ChargingStation) + " has been deleted.<br />",
        //                                                                HTMLEMailFooter(ExternalDNSName, BasePath, EMailType.Notification)),

        //                                 PlainText      = String.Concat(TextEMailHeader(ExternalDNSName, BasePath, EMailType.Notification),
        //                                                                ChargingStationTextInfo(ChargingStation) + " has been deleted.\r\n",
        //                                                                TextEMailFooter(ExternalDNSName, BasePath, EMailType.Notification)),

        //                                 SecurityLevel  = EMailSecurity.autosign

        //                             });

        //                }

        //            }
        //            catch (Exception e)
        //            {
        //                DebugX.LogException(e);
        //            }
        //        }

        //        #endregion

        //    }

        //}

        #endregion



    }

}
