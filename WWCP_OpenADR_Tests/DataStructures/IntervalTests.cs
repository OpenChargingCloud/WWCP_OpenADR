/*
 * Copyright (c) 2014-2026 GraphDefined GmbH <achim.friedland@graphdefined.com>
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

using OpenADRValueType = cloud.charging.open.protocols.OpenADRv3.ValueType;

#endregion

namespace cloud.charging.open.protocols.OpenADRv3.tests.DataStructures
{

    /// <summary>
    /// An interval, as OpenADR 3 writes it: its own period is "intervalPeriod",
    /// the name the schema gives it - not "interval", which a VTN or VEN that
    /// follows the schema neither writes nor reads.
    /// </summary>
    [TestFixture]
    public class IntervalTests
    {

        #region AnIntervalReadsItsPeriodFromIntervalPeriod()

        /// <summary>
        /// The interval of a report as a VEN following the schema sends it.
        /// </summary>
        [Test]
        public void AnIntervalReadsItsPeriodFromIntervalPeriod()
        {

            var json = JObject.Parse("""
                {
                    "id":              0,
                    "intervalPeriod":  { "start": "2026-11-02T18:00:00.000Z", "duration": "PT15M" },
                    "payloads":        [ { "type": "IMPORT_CAPACITY_LIMIT", "values": [ 11 ] } ]
                }
                """);

            Assert.That(Interval.TryParse(json, out var interval, out var error), Is.True, error);

            Assert.Multiple(() => {
                Assert.That(interval!.IntervalPeriod,                   Is.Not.Null, "the period of the interval");
                Assert.That(interval.IntervalPeriod!.Start,             Is.EqualTo(new DateTimeOffset(2026, 11, 2, 18, 0, 0, TimeSpan.Zero)));
                Assert.That(interval.IntervalPeriod!.Duration,          Is.EqualTo(TimeSpan.FromMinutes(15)));
            });

        }

        #endregion

        #region AnIntervalWritesItsPeriodAsIntervalPeriod()

        /// <summary>
        /// What is written is read back the same, under the schema's name.
        /// </summary>
        [Test]
        public void AnIntervalWritesItsPeriodAsIntervalPeriod()
        {

            var interval = new Interval(Interval_Id.Parse(0),
                                        [ new ValuesMap(OpenADRValueType.Parse("IMPORT_CAPACITY_LIMIT"), [ 11 ]) ],
                                        new IntervalPeriod(new DateTimeOffset(2026, 11, 2, 18, 0, 0, TimeSpan.Zero),
                                                           TimeSpan.FromMinutes(15)));

            var json     = interval.ToJSON();

            Assert.Multiple(() => {
                Assert.That(json["intervalPeriod"],                                  Is.Not.Null);
                Assert.That(json["interval"],                                        Is.Null, "a name the schema does not know");
                Assert.That(Interval.TryParse(json, out var again, out var error),   Is.True, error);
                Assert.That(again!.IntervalPeriod?.Start,                            Is.EqualTo(interval.IntervalPeriod!.Start));
                Assert.That(again.IntervalPeriod?.Duration,                          Is.EqualTo(interval.IntervalPeriod!.Duration));
            });

        }

        #endregion

    }

}
