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

using System.Diagnostics.CodeAnalysis;

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.OpenADRv3
{

    /// <summary>
    /// Extension methods for report identifications.
    /// </summary>
    public static class ReportIdExtensions
    {

        /// <summary>
        /// Indicates whether this report identification is null or empty.
        /// </summary>
        /// <param name="ReportId">A report identification.</param>
        public static Boolean IsNullOrEmpty(this Report_Id? ReportId)
            => !ReportId.HasValue || ReportId.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this report identification is null or empty.
        /// </summary>
        /// <param name="ReportId">A report identification.</param>
        public static Boolean IsNotNullOrEmpty(this Report_Id? ReportId)
            => ReportId.HasValue && ReportId.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// The report identification.
    /// </summary>
    public readonly struct Report_Id : IId,
                                       IEquatable<Report_Id>,
                                       IComparable<Report_Id>
    {

        #region Data

        /// <summary>
        /// The numeric value of the report identification.
        /// </summary>
        public readonly String Value;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this identification is null or empty.
        /// </summary>
        public readonly Boolean IsNullOrEmpty
            => Value.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this identification is NOT null or empty.
        /// </summary>
        public readonly Boolean IsNotNullOrEmpty
            => Value.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the report identification.
        /// </summary>
        public readonly UInt64 Length
            => (UInt64) Value.Length;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new report identification based on the given text.
        /// </summary>
        /// <param name="Text">A text representation of a report identification.</param>
        private Report_Id(String Text)
        {
            this.Value = Text;
        }

        #endregion


        #region Documentation

        // reportID:
        //     type: string
        //     pattern: "^[a-zA-Z0-9_-]*$"
        //     minLength: 1
        //     maxLength: 128
        //     description: URL safe VTN assigned report ID.
        //     example: report-999

        #endregion

        #region (static) NewRandom

        /// <summary>
        /// Create a new random report identification.
        /// </summary>
        public static Report_Id NewRandom

            => new (RandomExtensions.RandomString(36));

        #endregion

        #region (static) Parse    (Text)

        /// <summary>
        /// Parse the given string as a report identification.
        /// </summary>
        /// <param name="Text">A text representation of a report identification.</param>
        public static Report_Id Parse(String Text)
        {

            if (TryParse(Text, out var reportId))
                return reportId;

            throw new ArgumentException($"Invalid text representation of a report identification: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse (Text)

        /// <summary>
        /// Try to parse the given text as a report identification.
        /// </summary>
        /// <param name="Text">A text representation of a report identification.</param>
        public static Report_Id? TryParse(String Text)
        {

            if (TryParse(Text, out var reportId))
                return reportId;

            return null;

        }

        #endregion

        #region (static) TryParse (Text, out ReportId)

        /// <summary>
        /// Try to parse the given text as a report identification.
        /// </summary>
        /// <param name="Text">A text representation of a report identification.</param>
        /// <param name="ReportId">The parsed report identification.</param>
        public static Boolean TryParse(String                             Text,
                                       [NotNullWhen(true)] out Report_Id  ReportId)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {
                ReportId = new Report_Id(Text);
                return true;
            }

            ReportId = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this report identification.
        /// </summary>
        public Report_Id Clone()

            => new (Value);

        #endregion


        #region Operator overloading

        #region Operator == (ReportId1, ReportId2)

        /// <summary>
        /// Compares two instances of this report.
        /// </summary>
        /// <param name="ReportId1">A report identification.</param>
        /// <param name="ReportId2">Another report identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Report_Id ReportId1,
                                           Report_Id ReportId2)

            => ReportId1.Equals(ReportId2);

        #endregion

        #region Operator != (ReportId1, ReportId2)

        /// <summary>
        /// Compares two instances of this report.
        /// </summary>
        /// <param name="ReportId1">A report identification.</param>
        /// <param name="ReportId2">Another report identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Report_Id ReportId1,
                                           Report_Id ReportId2)

            => !ReportId1.Equals(ReportId2);

        #endregion

        #region Operator <  (ReportId1, ReportId2)

        /// <summary>
        /// Compares two instances of this report.
        /// </summary>
        /// <param name="ReportId1">A report identification.</param>
        /// <param name="ReportId2">Another report identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (Report_Id ReportId1,
                                          Report_Id ReportId2)

            => ReportId1.CompareTo(ReportId2) < 0;

        #endregion

        #region Operator <= (ReportId1, ReportId2)

        /// <summary>
        /// Compares two instances of this report.
        /// </summary>
        /// <param name="ReportId1">A report identification.</param>
        /// <param name="ReportId2">Another report identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (Report_Id ReportId1,
                                           Report_Id ReportId2)

            => ReportId1.CompareTo(ReportId2) <= 0;

        #endregion

        #region Operator >  (ReportId1, ReportId2)

        /// <summary>
        /// Compares two instances of this report.
        /// </summary>
        /// <param name="ReportId1">A report identification.</param>
        /// <param name="ReportId2">Another report identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (Report_Id ReportId1,
                                          Report_Id ReportId2)

            => ReportId1.CompareTo(ReportId2) > 0;

        #endregion

        #region Operator >= (ReportId1, ReportId2)

        /// <summary>
        /// Compares two instances of this report.
        /// </summary>
        /// <param name="ReportId1">A report identification.</param>
        /// <param name="ReportId2">Another report identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (Report_Id ReportId1,
                                           Report_Id ReportId2)

            => ReportId1.CompareTo(ReportId2) >= 0;

        #endregion

        #endregion

        #region IComparable<ReportId> Members

        #region CompareTo(Report)

        /// <summary>
        /// Compares two report identifications.
        /// </summary>
        /// <param name="Report">A report identification to compare with.</param>
        public Int32 CompareTo(Object? Report)

            => Report is Report_Id reportId
                   ? CompareTo(reportId)
                   : throw new ArgumentException("The given report is not a report identification!",
                                                 nameof(Report));

        #endregion

        #region CompareTo(ReportId)

        /// <summary>
        /// Compares two report identifications.
        /// </summary>
        /// <param name="ReportId">A report identification to compare with.</param>
        public Int32 CompareTo(Report_Id ReportId)

            => String.Compare(Value,
                              ReportId.Value,
                              StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region IEquatable<ReportId> Members

        #region Equals(Report)

        /// <summary>
        /// Compares two report identifications for equality.
        /// </summary>
        /// <param name="Report">A report identification to compare with.</param>
        public override Boolean Equals(Object? Report)

            => Report is Report_Id reportId &&
                   Equals(reportId);

        #endregion

        #region Equals(ReportId)

        /// <summary>
        /// Compares two report identifications for equality.
        /// </summary>
        /// <param name="ReportId">A report identification to compare with.</param>
        public Boolean Equals(Report_Id ReportId)

            => String.Equals(Value,
                             ReportId.Value,
                             StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the HashCode of this report.
        /// </summary>
        public override Int32 GetHashCode()

            => Value.GetHashCode();

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this report.
        /// </summary>
        public override String ToString()

            => Value.ToString();

        #endregion

    }

}
