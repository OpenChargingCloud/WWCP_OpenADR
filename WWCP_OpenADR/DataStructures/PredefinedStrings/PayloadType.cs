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
    /// Extension methods for Payload Types
    /// </summary>
    public static class PayloadTypeExtensions
    {

        /// <summary>
        /// Indicates whether this payload type is null or empty.
        /// </summary>
        /// <param name="PayloadType">A payload type.</param>
        public static Boolean IsNullOrEmpty(this PayloadType? PayloadType)
            => !PayloadType.HasValue || PayloadType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this payload type is null or empty.
        /// </summary>
        /// <param name="PayloadType">A payload type.</param>
        public static Boolean IsNotNullOrEmpty(this PayloadType? PayloadType)
            => PayloadType.HasValue && PayloadType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// A Payload Type
    /// </summary>
    public readonly struct PayloadType : IId,
                                         IEquatable<PayloadType>,
                                         IComparable<PayloadType>
    {

        #region Data

        private readonly static Dictionary<String, PayloadType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this payload type is null or empty.
        /// </summary>
        public readonly  Boolean                         IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this payload type is NOT null or empty.
        /// </summary>
        public readonly  Boolean                         IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the payload type.
        /// </summary>
        public readonly  UInt64                          Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered log types.
        /// </summary>
        public static    IEnumerable<PayloadType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new payload type based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of a payload type.</param>
        private PayloadType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static PayloadType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new PayloadType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as a payload type.
        /// </summary>
        /// <param name="Text">A text representation of a payload type.</param>
        public static PayloadType Parse(String Text)
        {

            if (TryParse(Text, out var payloadType))
                return payloadType;

            throw new ArgumentException($"Invalid text representation of a payload type: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as a payload type.
        /// </summary>
        /// <param name="Text">A text representation of a payload type.</param>
        public static PayloadType? TryParse(String Text)
        {

            if (TryParse(Text, out var payloadType))
                return payloadType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out PayloadType)

        /// <summary>
        /// Try to parse the given text as a payload type.
        /// </summary>
        /// <param name="Text">A text representation of a payload type.</param>
        /// <param name="PayloadType">The parsed payload type.</param>
        public static Boolean TryParse(String Text, out PayloadType PayloadType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out PayloadType))
                    PayloadType = Register(Text);

                return true;

            }

            PayloadType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this payload type.
        /// </summary>
        public PayloadType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// USAGE
        /// </summary>
        public static PayloadType  USAGE    { get; }
            = Register("USAGE");

        #endregion


        #region Operator overloading

        #region Operator == (PayloadType1, PayloadType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PayloadType1">A payload type.</param>
        /// <param name="PayloadType2">Another payload type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (PayloadType PayloadType1,
                                           PayloadType PayloadType2)

            => PayloadType1.Equals(PayloadType2);

        #endregion

        #region Operator != (PayloadType1, PayloadType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PayloadType1">A payload type.</param>
        /// <param name="PayloadType2">Another payload type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (PayloadType PayloadType1,
                                           PayloadType PayloadType2)

            => !PayloadType1.Equals(PayloadType2);

        #endregion

        #region Operator <  (PayloadType1, PayloadType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PayloadType1">A payload type.</param>
        /// <param name="PayloadType2">Another payload type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (PayloadType PayloadType1,
                                          PayloadType PayloadType2)

            => PayloadType1.CompareTo(PayloadType2) < 0;

        #endregion

        #region Operator <= (PayloadType1, PayloadType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PayloadType1">A payload type.</param>
        /// <param name="PayloadType2">Another payload type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (PayloadType PayloadType1,
                                           PayloadType PayloadType2)

            => PayloadType1.CompareTo(PayloadType2) <= 0;

        #endregion

        #region Operator >  (PayloadType1, PayloadType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PayloadType1">A payload type.</param>
        /// <param name="PayloadType2">Another payload type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (PayloadType PayloadType1,
                                          PayloadType PayloadType2)

            => PayloadType1.CompareTo(PayloadType2) > 0;

        #endregion

        #region Operator >= (PayloadType1, PayloadType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="PayloadType1">A payload type.</param>
        /// <param name="PayloadType2">Another payload type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (PayloadType PayloadType1,
                                           PayloadType PayloadType2)

            => PayloadType1.CompareTo(PayloadType2) >= 0;

        #endregion

        #endregion

        #region IComparable<PayloadType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two payload type.
        /// </summary>
        /// <param name="Object">A payload type to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is PayloadType payloadType
                   ? CompareTo(payloadType)
                   : throw new ArgumentException("The given object is not a payload type!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(PayloadType)

        /// <summary>
        /// Compares two payload type.
        /// </summary>
        /// <param name="PayloadType">A payload type to compare with.</param>
        public Int32 CompareTo(PayloadType PayloadType)

            => String.Compare(InternalId,
                              PayloadType.InternalId,
                              StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region IEquatable<PayloadType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two payload type for equality.
        /// </summary>
        /// <param name="Object">A payload type to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is PayloadType payloadType &&
                   Equals(payloadType);

        #endregion

        #region Equals(PayloadType)

        /// <summary>
        /// Compares two payload type for equality.
        /// </summary>
        /// <param name="PayloadType">A payload type to compare with.</param>
        public Boolean Equals(PayloadType PayloadType)

            => String.Equals(InternalId,
                             PayloadType.InternalId,
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
