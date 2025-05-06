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
    /// OpenADR HTTP tests.
    /// </summary>
    [TestFixture]
    public class HTTPTests : AHTTPTests
    {

        #region Test1()

        /// <summary>
        /// Test1
        /// </summary>
        [Test]
        public async Task Test1()
        {

            if (testOpenADR1 is not null)
            {

                

            }

            else
                Assert.Fail($"{nameof(Test1)} preconditions failed!");

        }

        #endregion

    }

}
