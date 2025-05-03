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
    /// Extension methods for Unit Types
    /// </summary>
    public static class UnitTypeExtensions
    {

        /// <summary>
        /// Indicates whether this unit type is null or empty.
        /// </summary>
        /// <param name="UnitType">An unit type.</param>
        public static Boolean IsNullOrEmpty(this UnitType? UnitType)
            => !UnitType.HasValue || UnitType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this unit type is null or empty.
        /// </summary>
        /// <param name="UnitType">An unit type.</param>
        public static Boolean IsNotNullOrEmpty(this UnitType? UnitType)
            => UnitType.HasValue && UnitType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An Unit Type
    /// </summary>
    public readonly struct UnitType : IId,
                                      IEquatable<UnitType>,
                                      IComparable<UnitType>
    {

        #region Data

        private readonly static Dictionary<String, UnitType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this unit type is null or empty.
        /// </summary>
        public readonly  Boolean                         IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this unit type is NOT null or empty.
        /// </summary>
        public readonly  Boolean                         IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the unit type.
        /// </summary>
        public readonly  UInt64                          Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered log types.
        /// </summary>
        public static    IEnumerable<UnitType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new unit type based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an unit type.</param>
        private UnitType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static UnitType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new UnitType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an unit type.
        /// </summary>
        /// <param name="Text">A text representation of an unit type.</param>
        public static UnitType Parse(String Text)
        {

            if (TryParse(Text, out var unitType))
                return unitType;

            throw new ArgumentException($"Invalid text representation of an unit type: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an unit type.
        /// </summary>
        /// <param name="Text">A text representation of an unit type.</param>
        public static UnitType? TryParse(String Text)
        {

            if (TryParse(Text, out var unitType))
                return unitType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out UnitType)

        /// <summary>
        /// Try to parse the given text as an unit type.
        /// </summary>
        /// <param name="Text">A text representation of an unit type.</param>
        /// <param name="UnitType">The parsed unit type.</param>
        public static Boolean TryParse(String Text, out UnitType UnitType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out UnitType))
                    UnitType = Register(Text);

                return true;

            }

            UnitType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this unit type.
        /// </summary>
        public UnitType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

#pragma warning disable IDE1006 // Naming Styles

        /// <summary>
        /// kWh
        /// </summary>
        public static UnitType  kWh    { get; }
            = Register("kWh");

#pragma warning restore IDE1006 // Naming Styles

        #endregion


        #region Operator overloading

        #region Operator == (UnitType1, UnitType2)

        /// <summary>
        /// Compares two instances of this unit.
        /// </summary>
        /// <param name="UnitType1">An unit type.</param>
        /// <param name="UnitType2">Another unit type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (UnitType UnitType1,
                                           UnitType UnitType2)

            => UnitType1.Equals(UnitType2);

        #endregion

        #region Operator != (UnitType1, UnitType2)

        /// <summary>
        /// Compares two instances of this unit.
        /// </summary>
        /// <param name="UnitType1">An unit type.</param>
        /// <param name="UnitType2">Another unit type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (UnitType UnitType1,
                                           UnitType UnitType2)

            => !UnitType1.Equals(UnitType2);

        #endregion

        #region Operator <  (UnitType1, UnitType2)

        /// <summary>
        /// Compares two instances of this unit.
        /// </summary>
        /// <param name="UnitType1">An unit type.</param>
        /// <param name="UnitType2">Another unit type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (UnitType UnitType1,
                                          UnitType UnitType2)

            => UnitType1.CompareTo(UnitType2) < 0;

        #endregion

        #region Operator <= (UnitType1, UnitType2)

        /// <summary>
        /// Compares two instances of this unit.
        /// </summary>
        /// <param name="UnitType1">An unit type.</param>
        /// <param name="UnitType2">Another unit type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (UnitType UnitType1,
                                           UnitType UnitType2)

            => UnitType1.CompareTo(UnitType2) <= 0;

        #endregion

        #region Operator >  (UnitType1, UnitType2)

        /// <summary>
        /// Compares two instances of this unit.
        /// </summary>
        /// <param name="UnitType1">An unit type.</param>
        /// <param name="UnitType2">Another unit type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (UnitType UnitType1,
                                          UnitType UnitType2)

            => UnitType1.CompareTo(UnitType2) > 0;

        #endregion

        #region Operator >= (UnitType1, UnitType2)

        /// <summary>
        /// Compares two instances of this unit.
        /// </summary>
        /// <param name="UnitType1">An unit type.</param>
        /// <param name="UnitType2">Another unit type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (UnitType UnitType1,
                                           UnitType UnitType2)

            => UnitType1.CompareTo(UnitType2) >= 0;

        #endregion

        #endregion

        #region IComparable<UnitType> Members

        #region CompareTo(Unit)

        /// <summary>
        /// Compares two unit type.
        /// </summary>
        /// <param name="Unit">An unit type to compare with.</param>
        public Int32 CompareTo(Object? Unit)

            => Unit is UnitType unitType
                   ? CompareTo(unitType)
                   : throw new ArgumentException("The given unit is not an unit type!",
                                                 nameof(Unit));

        #endregion

        #region CompareTo(UnitType)

        /// <summary>
        /// Compares two unit type.
        /// </summary>
        /// <param name="UnitType">An unit type to compare with.</param>
        public Int32 CompareTo(UnitType UnitType)

            => String.Compare(InternalId,
                              UnitType.InternalId,
                              StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region IEquatable<UnitType> Members

        #region Equals(Unit)

        /// <summary>
        /// Compares two unit type for equality.
        /// </summary>
        /// <param name="Unit">An unit type to compare with.</param>
        public override Boolean Equals(Object? Unit)

            => Unit is UnitType unitType &&
                   Equals(unitType);

        #endregion

        #region Equals(UnitType)

        /// <summary>
        /// Compares two unit type for equality.
        /// </summary>
        /// <param name="UnitType">An unit type to compare with.</param>
        public Boolean Equals(UnitType UnitType)

            => String.Equals(InternalId,
                             UnitType.InternalId,
                             StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the hash code of this unit.
        /// </summary>
        public override Int32 GetHashCode()

            => InternalId?.ToLower().GetHashCode() ?? 0;

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this unit.
        /// </summary>
        public override String ToString()

            => InternalId ?? "";

        #endregion

    }

}
