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

using NUnit.Framework;

using Newtonsoft.Json.Linq;

using org.GraphDefined.Vanaheimr.Illias;
using org.GraphDefined.Vanaheimr.Hermod;
using org.GraphDefined.Vanaheimr.Hermod.DNS;
using nts = org.GraphDefined.Vanaheimr.Norn.NTS;


using cloud.charging.open.protocols.WWCP.NetworkingNode;
using cloud.charging.open.protocols.OpenADRv3.Node;

#endregion

namespace cloud.charging.open.protocols.OpenADRv3.tests.HTTP
{

    /// <summary>
    /// OpenADR HTTP test defaults.
    /// </summary>
    public abstract class AHTTPTests
    {

        #region Data

        protected DNSClient?                    dnsClient;

        #endregion

        #region OpenADR #1 Data

        protected TestOpenADRNode?                 testOpenADR1;

        #endregion



        #region SetupOnce()

        [OneTimeSetUp]
        public virtual Task SetupOnce()
        {

            dnsClient = new DNSClient();

            #region OpenADR #1

            testOpenADR1 = new TestOpenADRNode(

                            Id:                                      NetworkingNode_Id.Parse("OpenADRTest01"),
                            VendorName:                              "GraphDefined",
                            Model:                                   "OpenADR-Testing-Model1",
                            SerialNumber:                            null,
                            SoftwareVersion:                         null,
                            Description:                             null,
                            CustomData:                              null,

                            ClientCAKeyPair:                         null,
                            ClientCACertificate:                     null,

                            SignaturePolicy:                         null,
                            ForwardingSignaturePolicy:               null,

                            HTTPAPI:                                 null,
                            HTTPAPI_Disabled:                        false,
                            HTTPAPI_Port:                            null,
                            HTTPAPI_ServerName:                      null,
                            HTTPAPI_ServiceName:                     null,
                            HTTPAPI_RobotEMailAddress:               null,
                            HTTPAPI_RobotGPGPassphrase:              null,
                            HTTPAPI_EventLoggingDisabled:            true,

                            OpenADRAPI:                              null,
                            OpenADRAPI_Disabled:                     false,
                            OpenADRAPI_Path:                         null,
                            OpenADRAPI_FileSystemPath:               null,

                            //WebAPI:                                  null,
                            WebAPI_Disabled:                         true,
                            WebAPI_Path:                             null,

                            NTSServer:                               (csmsNode) => new nts.NTSServer(
                                                                                       Description:   I18NString.Create("Secure Time Server"),
                                                                                       NTSKEPort:     IPPort.Parse(7777),
                                                                                       NTSPort:       IPPort.Parse(1234),
                                                                                       KeyPair:       nts.KeyPair.GenerateECKeys(1)
                                                                                   ),
                            NTSServer_Disabled:                      false,

                            DefaultRequestTimeout:                   null,

                            DisableSendHeartbeats:                   true,
                            SendHeartbeatsEvery:                     null,

                            DisableMaintenanceTasks:                 false,
                            MaintenanceEvery:                        null,

                            SMTPClient:                              null,
                            DNSClient:                               dnsClient

                        );

            Assert.That(testOpenADR1, Is.Not.Null);

            #endregion

            return Task.CompletedTask;

        }

        #endregion

        #region SetupEachTest()

        [SetUp]
        public virtual Task SetupEachTest()
        {

            Timestamp.Reset();

            return Task.CompletedTask;

        }

        #endregion

        #region ShutdownEachTest()

        [TearDown]
        public virtual Task ShutdownEachTest()
        {
            return Task.CompletedTask;
        }

        #endregion

        #region ShutdownOnce()

        [OneTimeTearDown]
        public virtual async Task ShutdownOnce()
        {

            if (testOpenADR1 is not null)
                await testOpenADR1.Shutdown();

            testOpenADR1               = null;

        }

        #endregion

    }

}
