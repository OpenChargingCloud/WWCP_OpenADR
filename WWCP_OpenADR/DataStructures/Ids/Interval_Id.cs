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
    /// Extension methods for interval identifications.
    /// </summary>
    public static class IntervalIdExtensions
    {

        /// <summary>
        /// Indicates whether this interval identification is null or empty.
        /// </summary>
        /// <param name="IntervalId">An interval identification.</param>
        public static Boolean IsNullOrEmpty(this Interval_Id? IntervalId)
            => !IntervalId.HasValue || IntervalId.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this interval identification is null or empty.
        /// </summary>
        /// <param name="IntervalId">An interval identification.</param>
        public static Boolean IsNotNullOrEmpty(this Interval_Id? IntervalId)
            => IntervalId.HasValue && IntervalId.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An interval identification.
    /// </summary>
    public readonly struct Interval_Id : IId,
                                         IEquatable<Interval_Id>,
                                         IComparable<Interval_Id>
    {

        #region Data

        /// <summary>
        /// The numeric value of the interval identification.
        /// </summary>
        public readonly Int64 Value;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this identification is null or empty.
        /// </summary>
        public readonly Boolean  IsNullOrEmpty
            => false;

        /// <summary>
        /// Indicates whether this identification is NOT null or empty.
        /// </summary>
        public readonly Boolean  IsNotNullOrEmpty
            => true;

        /// <summary>
        /// The length of the interval identification.
        /// </summary>
        public readonly UInt64   Length
            => (UInt64) Value.ToString().Length;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new interval identification based on the given number.
        /// </summary>
        /// <param name="Number">A numeric representation of a display message identification.</param>
        private Interval_Id(Int64 Number)
        {
            this.Value = Number;
        }

        #endregion


        #region (static) NewRandom

        /// <summary>
        /// Create a new random interval identification.
        /// </summary>
        public static Interval_Id NewRandom

#pragma warning disable SCS0005 // Weak random number generator.
            => new (Random.Shared.Next(Int32.MaxValue));
#pragma warning restore SCS0005 // Weak random number generator.

        #endregion

        #region (static) Parse    (Text)

        /// <summary>
        /// Parse the given string as an interval identification.
        /// </summary>
        /// <param name="Text">A text representation of an interval identification.</param>
        public static Interval_Id Parse(String Text)
        {

            if (TryParse(Text, out var intervalId))
                return intervalId;

            throw new ArgumentException($"Invalid text representation of an interval identification: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) Parse    (Number)

        /// <summary>
        /// Parse the given number as an interval identification.
        /// </summary>
        /// <param name="Number">A numeric representation of an interval identification.</param>
        public static Interval_Id Parse(Int64 Number)

            => new (Number);

        #endregion

        #region (static) TryParse (Text)

        /// <summary>
        /// Try to parse the given text as an interval identification.
        /// </summary>
        /// <param name="Text">A text representation of an interval identification.</param>
        public static Interval_Id? TryParse(String Text)
        {

            if (TryParse(Text, out var intervalId))
                return intervalId;

            return null;

        }

        #endregion

        #region (static) TryParse (Number)

        /// <summary>
        /// Try to parse the given number as an interval identification.
        /// </summary>
        /// <param name="Number">A numeric representation of an interval identification.</param>
        public static Interval_Id? TryParse(Int64 Number)
        {

            if (TryParse(Number, out var intervalId))
                return intervalId;

            return null;

        }

        #endregion

        #region (static) TryParse (Text,   out IntervalId)

        /// <summary>
        /// Try to parse the given text as an interval identification.
        /// </summary>
        /// <param name="Text">A text representation of an interval identification.</param>
        /// <param name="IntervalId">The parsed interval identification.</param>
        public static Boolean TryParse(String                               Text,
                                       [NotNullWhen(true)] out Interval_Id  IntervalId)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty() &&
                Int64.TryParse(Text, out var number))
            {
                IntervalId = new Interval_Id(number);
                return true;
            }

            IntervalId = default;
            return false;

        }

        #endregion

        #region (static) TryParse (Number, out IntervalId)

        /// <summary>
        /// Try to parse the given number as an interval identification.
        /// </summary>
        /// <param name="Number">A numeric representation of an interval identification.</param>
        /// <param name="IntervalId">The parsed interval identification.</param>
        public static Boolean TryParse(Int64                                Number,
                                       [NotNullWhen(true)] out Interval_Id  IntervalId)
        {

            IntervalId = new Interval_Id(Number);

            return true;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this interval identification.
        /// </summary>
        public Interval_Id Clone()

            => new (Value);

        #endregion


        #region Operator overloading

        #region Operator == (IntervalId1, IntervalId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IntervalId1">An interval identification.</param>
        /// <param name="IntervalId2">Another interval identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Interval_Id IntervalId1,
                                           Interval_Id IntervalId2)

            => IntervalId1.Equals(IntervalId2);

        #endregion

        #region Operator != (IntervalId1, IntervalId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IntervalId1">An interval identification.</param>
        /// <param name="IntervalId2">Another interval identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Interval_Id IntervalId1,
                                           Interval_Id IntervalId2)

            => !IntervalId1.Equals(IntervalId2);

        #endregion

        #region Operator <  (IntervalId1, IntervalId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IntervalId1">An interval identification.</param>
        /// <param name="IntervalId2">Another interval identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (Interval_Id IntervalId1,
                                          Interval_Id IntervalId2)

            => IntervalId1.CompareTo(IntervalId2) < 0;

        #endregion

        #region Operator <= (IntervalId1, IntervalId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IntervalId1">An interval identification.</param>
        /// <param name="IntervalId2">Another interval identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (Interval_Id IntervalId1,
                                           Interval_Id IntervalId2)

            => IntervalId1.CompareTo(IntervalId2) <= 0;

        #endregion

        #region Operator >  (IntervalId1, IntervalId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IntervalId1">An interval identification.</param>
        /// <param name="IntervalId2">Another interval identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (Interval_Id IntervalId1,
                                          Interval_Id IntervalId2)

            => IntervalId1.CompareTo(IntervalId2) > 0;

        #endregion

        #region Operator >= (IntervalId1, IntervalId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="IntervalId1">An interval identification.</param>
        /// <param name="IntervalId2">Another interval identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (Interval_Id IntervalId1,
                                           Interval_Id IntervalId2)

            => IntervalId1.CompareTo(IntervalId2) >= 0;

        #endregion

        #endregion

        #region IComparable<IntervalId> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two interval identifications.
        /// </summary>
        /// <param name="Object">An interval identification to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is Interval_Id intervalId
                   ? CompareTo(intervalId)
                   : throw new ArgumentException("The given object is not an interval identification!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(IntervalId)

        /// <summary>
        /// Compares two interval identifications.
        /// </summary>
        /// <param name="IntervalId">An interval identification to compare with.</param>
        public Int32 CompareTo(Interval_Id IntervalId)

            => Value.CompareTo(IntervalId.Value);

        #endregion

        #endregion

        #region IEquatable<IntervalId> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two interval identifications for equality.
        /// </summary>
        /// <param name="Object">An interval identification to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is Interval_Id intervalId &&
                   Equals(intervalId);

        #endregion

        #region Equals(IntervalId)

        /// <summary>
        /// Compares two interval identifications for equality.
        /// </summary>
        /// <param name="IntervalId">An interval identification to compare with.</param>
        public Boolean Equals(Interval_Id IntervalId)

            => Value.Equals(IntervalId.Value);

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the HashCode of this object.
        /// </summary>
        public override Int32 GetHashCode()

            => Value.GetHashCode();

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this object.
        /// </summary>
        public override String ToString()

            => Value.ToString();

        #endregion

    }

}
