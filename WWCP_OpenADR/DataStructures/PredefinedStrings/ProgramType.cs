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

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.OpenADRv3
{

    /// <summary>
    /// Extension methods for Program Types
    /// </summary>
    public static class ProgramTypeExtensions
    {

        /// <summary>
        /// Indicates whether this program type is null or empty.
        /// </summary>
        /// <param name="ProgramType">A program type.</param>
        public static Boolean IsNullOrEmpty(this ProgramType? ProgramType)
            => !ProgramType.HasValue || ProgramType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this program type is null or empty.
        /// </summary>
        /// <param name="ProgramType">A program type.</param>
        public static Boolean IsNotNullOrEmpty(this ProgramType? ProgramType)
            => ProgramType.HasValue && ProgramType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A Program Type
    /// </summary>
    public readonly struct ProgramType : IId,
                                         IEquatable<ProgramType>,
                                         IComparable<ProgramType>
    {

        #region Data

        private readonly static Dictionary<String, ProgramType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this program type is null or empty.
        /// </summary>
        public readonly  Boolean                         IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this program type is NOT null or empty.
        /// </summary>
        public readonly  Boolean                         IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the program type.
        /// </summary>
        public readonly  UInt64                          Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered log types.
        /// </summary>
        public static    IEnumerable<ProgramType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new program type based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a program type.</param>
        private ProgramType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static ProgramType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new ProgramType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a program type.
        /// </summary>
        /// <param name="Text">A text representation of a program type.</param>
        public static ProgramType Parse(String Text)
        {

            if (TryParse(Text, out var programType))
                return programType;

            throw new ArgumentException($"Invalid text representation of a program type: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a program type.
        /// </summary>
        /// <param name="Text">A text representation of a program type.</param>
        public static ProgramType? TryParse(String Text)
        {

            if (TryParse(Text, out var programType))
                return programType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out ProgramType)

        /// <summary>
        /// Try to parse the given text as a program type.
        /// </summary>
        /// <param name="Text">A text representation of a program type.</param>
        /// <param name="ProgramType">The parsed program type.</param>
        public static Boolean TryParse(String Text, out ProgramType ProgramType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out ProgramType))
                    ProgramType = Register(Text);

                return true;

            }

            ProgramType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this program type.
        /// </summary>
        public ProgramType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// PRICING_TARIFF
        /// </summary>
        public static ProgramType  PRICING_TARIFF    { get; }
            = Register("PRICING_TARIFF");

        #endregion


        #region Operator overloading

        #region Operator == (ProgramType1, ProgramType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ProgramType1">A program type.</param>
        /// <param name="ProgramType2">Another program type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ProgramType ProgramType1,
                                           ProgramType ProgramType2)

            => ProgramType1.Equals(ProgramType2);

        #endregion

        #region Operator != (ProgramType1, ProgramType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ProgramType1">A program type.</param>
        /// <param name="ProgramType2">Another program type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ProgramType ProgramType1,
                                           ProgramType ProgramType2)

            => !ProgramType1.Equals(ProgramType2);

        #endregion

        #region Operator <  (ProgramType1, ProgramType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ProgramType1">A program type.</param>
        /// <param name="ProgramType2">Another program type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (ProgramType ProgramType1,
                                          ProgramType ProgramType2)

            => ProgramType1.CompareTo(ProgramType2) < 0;

        #endregion

        #region Operator <= (ProgramType1, ProgramType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ProgramType1">A program type.</param>
        /// <param name="ProgramType2">Another program type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (ProgramType ProgramType1,
                                           ProgramType ProgramType2)

            => ProgramType1.CompareTo(ProgramType2) <= 0;

        #endregion

        #region Operator >  (ProgramType1, ProgramType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ProgramType1">A program type.</param>
        /// <param name="ProgramType2">Another program type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (ProgramType ProgramType1,
                                          ProgramType ProgramType2)

            => ProgramType1.CompareTo(ProgramType2) > 0;

        #endregion

        #region Operator >= (ProgramType1, ProgramType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ProgramType1">A program type.</param>
        /// <param name="ProgramType2">Another program type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (ProgramType ProgramType1,
                                           ProgramType ProgramType2)

            => ProgramType1.CompareTo(ProgramType2) >= 0;

        #endregion

        #endregion

        #region IComparable<ProgramType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two program type.
        /// </summary>
        /// <param name="Object">A program type to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is ProgramType programType
                   ? CompareTo(programType)
                   : throw new ArgumentException("The given object is not a program type!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(ProgramType)

        /// <summary>
        /// Compares two program type.
        /// </summary>
        /// <param name="ProgramType">A program type to compare with.</param>
        public Int32 CompareTo(ProgramType ProgramType)

            => String.Compare(InternalId,
                              ProgramType.InternalId,
                              StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region IEquatable<ProgramType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two program type for equality.
        /// </summary>
        /// <param name="Object">A program type to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is ProgramType programType &&
                   Equals(programType);

        #endregion

        #region Equals(ProgramType)

        /// <summary>
        /// Compares two program type for equality.
        /// </summary>
        /// <param name="ProgramType">A program type to compare with.</param>
        public Boolean Equals(ProgramType ProgramType)

            => String.Equals(InternalId,
                             ProgramType.InternalId,
                             StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the hash code of this object.
        /// </summary>
        public override Int32 GetHashCode()

            => InternalId?.ToLower().GetHashCode() ?? 0;

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this object.
        /// </summary>
        public override String ToString()

            => InternalId ?? "";

        #endregion

    }

}
