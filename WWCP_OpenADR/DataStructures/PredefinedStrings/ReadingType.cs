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
    /// Extension methods for Reading Types
    /// </summary>
    public static class ReadingTypeExtensions
    {

        /// <summary>
        /// Indicates whether this reading type is null or empty.
        /// </summary>
        /// <param name="ReadingType">A reading type.</param>
        public static Boolean IsNullOrEmpty(this ReadingType? ReadingType)
            => !ReadingType.HasValue || ReadingType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this reading type is null or empty.
        /// </summary>
        /// <param name="ReadingType">A reading type.</param>
        public static Boolean IsNotNullOrEmpty(this ReadingType? ReadingType)
            => ReadingType.HasValue && ReadingType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A Reading Type
    /// </summary>
    public readonly struct ReadingType : IId,
                                         IEquatable<ReadingType>,
                                         IComparable<ReadingType>
    {

        #region Data

        private readonly static Dictionary<String, ReadingType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this reading type is null or empty.
        /// </summary>
        public readonly  Boolean                         IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this reading type is NOT null or empty.
        /// </summary>
        public readonly  Boolean                         IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the reading type.
        /// </summary>
        public readonly  UInt64                          Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered log types.
        /// </summary>
        public static    IEnumerable<ReadingType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new reading type based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a reading type.</param>
        private ReadingType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static ReadingType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new ReadingType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a reading type.
        /// </summary>
        /// <param name="Text">A text representation of a reading type.</param>
        public static ReadingType Parse(String Text)
        {

            if (TryParse(Text, out var readingType))
                return readingType;

            throw new ArgumentException($"Invalid text representation of a reading type: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a reading type.
        /// </summary>
        /// <param name="Text">A text representation of a reading type.</param>
        public static ReadingType? TryParse(String Text)
        {

            if (TryParse(Text, out var readingType))
                return readingType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out ReadingType)

        /// <summary>
        /// Try to parse the given text as a reading type.
        /// </summary>
        /// <param name="Text">A text representation of a reading type.</param>
        /// <param name="ReadingType">The parsed reading type.</param>
        public static Boolean TryParse(String Text, out ReadingType ReadingType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out ReadingType))
                    ReadingType = Register(Text);

                return true;

            }

            ReadingType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this reading type.
        /// </summary>
        public ReadingType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// DIRECT_READ
        /// </summary>
        public static ReadingType  DIRECT_READ    { get; }
            = Register("DIRECT_READ");

        #endregion


        #region Operator overloading

        #region Operator == (ReadingType1, ReadingType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReadingType1">A reading type.</param>
        /// <param name="ReadingType2">Another reading type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ReadingType ReadingType1,
                                           ReadingType ReadingType2)

            => ReadingType1.Equals(ReadingType2);

        #endregion

        #region Operator != (ReadingType1, ReadingType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReadingType1">A reading type.</param>
        /// <param name="ReadingType2">Another reading type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ReadingType ReadingType1,
                                           ReadingType ReadingType2)

            => !ReadingType1.Equals(ReadingType2);

        #endregion

        #region Operator <  (ReadingType1, ReadingType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReadingType1">A reading type.</param>
        /// <param name="ReadingType2">Another reading type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (ReadingType ReadingType1,
                                          ReadingType ReadingType2)

            => ReadingType1.CompareTo(ReadingType2) < 0;

        #endregion

        #region Operator <= (ReadingType1, ReadingType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReadingType1">A reading type.</param>
        /// <param name="ReadingType2">Another reading type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (ReadingType ReadingType1,
                                           ReadingType ReadingType2)

            => ReadingType1.CompareTo(ReadingType2) <= 0;

        #endregion

        #region Operator >  (ReadingType1, ReadingType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReadingType1">A reading type.</param>
        /// <param name="ReadingType2">Another reading type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (ReadingType ReadingType1,
                                          ReadingType ReadingType2)

            => ReadingType1.CompareTo(ReadingType2) > 0;

        #endregion

        #region Operator >= (ReadingType1, ReadingType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ReadingType1">A reading type.</param>
        /// <param name="ReadingType2">Another reading type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (ReadingType ReadingType1,
                                           ReadingType ReadingType2)

            => ReadingType1.CompareTo(ReadingType2) >= 0;

        #endregion

        #endregion

        #region IComparable<ReadingType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two reading type.
        /// </summary>
        /// <param name="Object">A reading type to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is ReadingType readingType
                   ? CompareTo(readingType)
                   : throw new ArgumentException("The given object is not a reading type!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(ReadingType)

        /// <summary>
        /// Compares two reading type.
        /// </summary>
        /// <param name="ReadingType">A reading type to compare with.</param>
        public Int32 CompareTo(ReadingType ReadingType)

            => String.Compare(InternalId,
                              ReadingType.InternalId,
                              StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region IEquatable<ReadingType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two reading type for equality.
        /// </summary>
        /// <param name="Object">A reading type to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is ReadingType readingType &&
                   Equals(readingType);

        #endregion

        #region Equals(ReadingType)

        /// <summary>
        /// Compares two reading type for equality.
        /// </summary>
        /// <param name="ReadingType">A reading type to compare with.</param>
        public Boolean Equals(ReadingType ReadingType)

            => String.Equals(InternalId,
                             ReadingType.InternalId,
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
