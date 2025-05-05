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
    /// Extension methods for subscription identifications.
    /// </summary>
    public static class SubscriptionIdExtensions
    {

        /// <summary>
        /// Indicates whether this subscription identification is null or empty.
        /// </summary>
        /// <param name="SubscriptionId">A subscription identification.</param>
        public static Boolean IsNullOrEmpty(this Subscription_Id? SubscriptionId)
            => !SubscriptionId.HasValue || SubscriptionId.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this subscription identification is null or empty.
        /// </summary>
        /// <param name="SubscriptionId">A subscription identification.</param>
        public static Boolean IsNotNullOrEmpty(this Subscription_Id? SubscriptionId)
            => SubscriptionId.HasValue && SubscriptionId.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// The subscription identification.
    /// </summary>
    public readonly struct Subscription_Id : IId,
                                             IEquatable<Subscription_Id>,
                                             IComparable<Subscription_Id>
    {

        #region Data

        /// <summary>
        /// The numeric value of the subscription identification.
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
        /// The length of the subscription identification.
        /// </summary>
        public readonly UInt64 Length
            => (UInt64) Value.Length;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new subscription identification based on the given text.
        /// </summary>
        /// <param name="Text">A text representation of a subscription identification.</param>
        private Subscription_Id(String Text)
        {
            this.Value = Text;
        }

        #endregion


        #region Documentation

        // subscriptionID:
        //     type: string
        //     pattern: "^[a-zA-Z0-9_-]*$"
        //     minLength: 1
        //     maxLength: 128
        //     description: URL safe VTN assigned subscription ID.
        //     example: subscription-999

        #endregion

        #region (static) NewRandom

        /// <summary>
        /// Create a new random subscription identification.
        /// </summary>
        public static Subscription_Id NewRandom

            => new (RandomExtensions.RandomString(36));

        #endregion

        #region (static) Parse    (Text)

        /// <summary>
        /// Parse the given string as a subscription identification.
        /// </summary>
        /// <param name="Text">A text representation of a subscription identification.</param>
        public static Subscription_Id Parse(String Text)
        {

            if (TryParse(Text, out var subscriptionId))
                return subscriptionId;

            throw new ArgumentException($"Invalid text representation of a subscription identification: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse (Text)

        /// <summary>
        /// Try to parse the given text as a subscription identification.
        /// </summary>
        /// <param name="Text">A text representation of a subscription identification.</param>
        public static Subscription_Id? TryParse(String Text)
        {

            if (TryParse(Text, out var subscriptionId))
                return subscriptionId;

            return null;

        }

        #endregion

        #region (static) TryParse (Text, out SubscriptionId)

        /// <summary>
        /// Try to parse the given text as a subscription identification.
        /// </summary>
        /// <param name="Text">A text representation of a subscription identification.</param>
        /// <param name="SubscriptionId">The parsed subscription identification.</param>
        public static Boolean TryParse(String                             Text,
                                       [NotNullWhen(true)] out Subscription_Id  SubscriptionId)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {
                SubscriptionId = new Subscription_Id(Text);
                return true;
            }

            SubscriptionId = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this subscription identification.
        /// </summary>
        public Subscription_Id Clone()

            => new (Value);

        #endregion


        #region Operator overloading

        #region Operator == (SubscriptionId1, SubscriptionId2)

        /// <summary>
        /// Compares two instances of this subscription.
        /// </summary>
        /// <param name="SubscriptionId1">A subscription identification.</param>
        /// <param name="SubscriptionId2">Another subscription identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Subscription_Id SubscriptionId1,
                                           Subscription_Id SubscriptionId2)

            => SubscriptionId1.Equals(SubscriptionId2);

        #endregion

        #region Operator != (SubscriptionId1, SubscriptionId2)

        /// <summary>
        /// Compares two instances of this subscription.
        /// </summary>
        /// <param name="SubscriptionId1">A subscription identification.</param>
        /// <param name="SubscriptionId2">Another subscription identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Subscription_Id SubscriptionId1,
                                           Subscription_Id SubscriptionId2)

            => !SubscriptionId1.Equals(SubscriptionId2);

        #endregion

        #region Operator <  (SubscriptionId1, SubscriptionId2)

        /// <summary>
        /// Compares two instances of this subscription.
        /// </summary>
        /// <param name="SubscriptionId1">A subscription identification.</param>
        /// <param name="SubscriptionId2">Another subscription identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (Subscription_Id SubscriptionId1,
                                          Subscription_Id SubscriptionId2)

            => SubscriptionId1.CompareTo(SubscriptionId2) < 0;

        #endregion

        #region Operator <= (SubscriptionId1, SubscriptionId2)

        /// <summary>
        /// Compares two instances of this subscription.
        /// </summary>
        /// <param name="SubscriptionId1">A subscription identification.</param>
        /// <param name="SubscriptionId2">Another subscription identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (Subscription_Id SubscriptionId1,
                                           Subscription_Id SubscriptionId2)

            => SubscriptionId1.CompareTo(SubscriptionId2) <= 0;

        #endregion

        #region Operator >  (SubscriptionId1, SubscriptionId2)

        /// <summary>
        /// Compares two instances of this subscription.
        /// </summary>
        /// <param name="SubscriptionId1">A subscription identification.</param>
        /// <param name="SubscriptionId2">Another subscription identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (Subscription_Id SubscriptionId1,
                                          Subscription_Id SubscriptionId2)

            => SubscriptionId1.CompareTo(SubscriptionId2) > 0;

        #endregion

        #region Operator >= (SubscriptionId1, SubscriptionId2)

        /// <summary>
        /// Compares two instances of this subscription.
        /// </summary>
        /// <param name="SubscriptionId1">A subscription identification.</param>
        /// <param name="SubscriptionId2">Another subscription identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (Subscription_Id SubscriptionId1,
                                           Subscription_Id SubscriptionId2)

            => SubscriptionId1.CompareTo(SubscriptionId2) >= 0;

        #endregion

        #endregion

        #region IComparable<SubscriptionId> Members

        #region CompareTo(Subscription)

        /// <summary>
        /// Compares two subscription identifications.
        /// </summary>
        /// <param name="Subscription">A subscription identification to compare with.</param>
        public Int32 CompareTo(Object? Subscription)

            => Subscription is Subscription_Id subscriptionId
                   ? CompareTo(subscriptionId)
                   : throw new ArgumentException("The given subscription is not a subscription identification!",
                                                 nameof(Subscription));

        #endregion

        #region CompareTo(SubscriptionId)

        /// <summary>
        /// Compares two subscription identifications.
        /// </summary>
        /// <param name="SubscriptionId">A subscription identification to compare with.</param>
        public Int32 CompareTo(Subscription_Id SubscriptionId)

            => String.Compare(Value,
                              SubscriptionId.Value,
                              StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region IEquatable<SubscriptionId> Members

        #region Equals(Subscription)

        /// <summary>
        /// Compares two subscription identifications for equality.
        /// </summary>
        /// <param name="Subscription">A subscription identification to compare with.</param>
        public override Boolean Equals(Object? Subscription)

            => Subscription is Subscription_Id subscriptionId &&
                   Equals(subscriptionId);

        #endregion

        #region Equals(SubscriptionId)

        /// <summary>
        /// Compares two subscription identifications for equality.
        /// </summary>
        /// <param name="SubscriptionId">A subscription identification to compare with.</param>
        public Boolean Equals(Subscription_Id SubscriptionId)

            => String.Equals(Value,
                             SubscriptionId.Value,
                             StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the HashCode of this subscription.
        /// </summary>
        public override Int32 GetHashCode()

            => Value.GetHashCode();

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this subscription.
        /// </summary>
        public override String ToString()

            => Value.ToString();

        #endregion

    }

}
