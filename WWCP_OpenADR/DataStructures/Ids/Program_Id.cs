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
    /// Extension methods for program identifications.
    /// </summary>
    public static class ProgramIdExtensions
    {

        /// <summary>
        /// Indicates whether this program identification is null or empty.
        /// </summary>
        /// <param name="ProgramId">A program identification.</param>
        public static Boolean IsNullOrEmpty(this Program_Id? ProgramId)
            => !ProgramId.HasValue || ProgramId.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this program identification is null or empty.
        /// </summary>
        /// <param name="ProgramId">A program identification.</param>
        public static Boolean IsNotNullOrEmpty(this Program_Id? ProgramId)
            => ProgramId.HasValue && ProgramId.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// The program identification.
    /// </summary>
    public readonly struct Program_Id : IId,
                                        IEquatable<Program_Id>,
                                        IComparable<Program_Id>
    {

        #region Data

        /// <summary>
        /// The numeric value of the program identification.
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
        /// The length of the program identification.
        /// </summary>
        public readonly UInt64 Length
            => (UInt64) Value.Length;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new program identification based on the given text.
        /// </summary>
        /// <param name="Text">A text representation of a program identification.</param>
        private Program_Id(String Text)
        {
            this.Value = Text;
        }

        #endregion


        #region Documentation

        // programID:
        //     type: string
        //     pattern: "^[a-zA-Z0-9_-]*$"
        //     minLength: 1
        //     maxLength: 128
        //     description: URL safe VTN assigned program ID.
        //     example: program-999

        #endregion

        #region (static) NewRandom

        /// <summary>
        /// Create a new random program identification.
        /// </summary>
        public static Program_Id NewRandom

            => new (RandomExtensions.RandomString(36));

        #endregion

        #region (static) Parse    (Text)

        /// <summary>
        /// Parse the given string as a program identification.
        /// </summary>
        /// <param name="Text">A text representation of a program identification.</param>
        public static Program_Id Parse(String Text)
        {

            if (TryParse(Text, out var programId))
                return programId;

            throw new ArgumentException($"Invalid text representation of a program identification: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse (Text)

        /// <summary>
        /// Try to parse the given text as a program identification.
        /// </summary>
        /// <param name="Text">A text representation of a program identification.</param>
        public static Program_Id? TryParse(String Text)
        {

            if (TryParse(Text, out var programId))
                return programId;

            return null;

        }

        #endregion

        #region (static) TryParse (Text, out ProgramId)

        /// <summary>
        /// Try to parse the given text as a program identification.
        /// </summary>
        /// <param name="Text">A text representation of a program identification.</param>
        /// <param name="ProgramId">The parsed program identification.</param>
        public static Boolean TryParse(String                             Text,
                                       [NotNullWhen(true)] out Program_Id  ProgramId)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {
                ProgramId = new Program_Id(Text);
                return true;
            }

            ProgramId = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this program identification.
        /// </summary>
        public Program_Id Clone()

            => new (Value);

        #endregion


        #region Operator overloading

        #region Operator == (ProgramId1, ProgramId2)

        /// <summary>
        /// Compares two instances of this program.
        /// </summary>
        /// <param name="ProgramId1">A program identification.</param>
        /// <param name="ProgramId2">Another program identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Program_Id ProgramId1,
                                           Program_Id ProgramId2)

            => ProgramId1.Equals(ProgramId2);

        #endregion

        #region Operator != (ProgramId1, ProgramId2)

        /// <summary>
        /// Compares two instances of this program.
        /// </summary>
        /// <param name="ProgramId1">A program identification.</param>
        /// <param name="ProgramId2">Another program identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Program_Id ProgramId1,
                                           Program_Id ProgramId2)

            => !ProgramId1.Equals(ProgramId2);

        #endregion

        #region Operator <  (ProgramId1, ProgramId2)

        /// <summary>
        /// Compares two instances of this program.
        /// </summary>
        /// <param name="ProgramId1">A program identification.</param>
        /// <param name="ProgramId2">Another program identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (Program_Id ProgramId1,
                                          Program_Id ProgramId2)

            => ProgramId1.CompareTo(ProgramId2) < 0;

        #endregion

        #region Operator <= (ProgramId1, ProgramId2)

        /// <summary>
        /// Compares two instances of this program.
        /// </summary>
        /// <param name="ProgramId1">A program identification.</param>
        /// <param name="ProgramId2">Another program identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (Program_Id ProgramId1,
                                           Program_Id ProgramId2)

            => ProgramId1.CompareTo(ProgramId2) <= 0;

        #endregion

        #region Operator >  (ProgramId1, ProgramId2)

        /// <summary>
        /// Compares two instances of this program.
        /// </summary>
        /// <param name="ProgramId1">A program identification.</param>
        /// <param name="ProgramId2">Another program identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (Program_Id ProgramId1,
                                          Program_Id ProgramId2)

            => ProgramId1.CompareTo(ProgramId2) > 0;

        #endregion

        #region Operator >= (ProgramId1, ProgramId2)

        /// <summary>
        /// Compares two instances of this program.
        /// </summary>
        /// <param name="ProgramId1">A program identification.</param>
        /// <param name="ProgramId2">Another program identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (Program_Id ProgramId1,
                                           Program_Id ProgramId2)

            => ProgramId1.CompareTo(ProgramId2) >= 0;

        #endregion

        #endregion

        #region IComparable<ProgramId> Members

        #region CompareTo(Program)

        /// <summary>
        /// Compares two program identifications.
        /// </summary>
        /// <param name="Program">A program identification to compare with.</param>
        public Int32 CompareTo(Object? Program)

            => Program is Program_Id programId
                   ? CompareTo(programId)
                   : throw new ArgumentException("The given program is not a program identification!",
                                                 nameof(Program));

        #endregion

        #region CompareTo(ProgramId)

        /// <summary>
        /// Compares two program identifications.
        /// </summary>
        /// <param name="ProgramId">A program identification to compare with.</param>
        public Int32 CompareTo(Program_Id ProgramId)

            => String.Compare(Value,
                              ProgramId.Value,
                              StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region IEquatable<ProgramId> Members

        #region Equals(Program)

        /// <summary>
        /// Compares two program identifications for equality.
        /// </summary>
        /// <param name="Program">A program identification to compare with.</param>
        public override Boolean Equals(Object? Program)

            => Program is Program_Id programId &&
                   Equals(programId);

        #endregion

        #region Equals(ProgramId)

        /// <summary>
        /// Compares two program identifications for equality.
        /// </summary>
        /// <param name="ProgramId">A program identification to compare with.</param>
        public Boolean Equals(Program_Id ProgramId)

            => String.Equals(Value,
                             ProgramId.Value,
                             StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the HashCode of this program.
        /// </summary>
        public override Int32 GetHashCode()

            => Value.GetHashCode();

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this program.
        /// </summary>
        public override String ToString()

            => Value.ToString();

        #endregion

    }

}
