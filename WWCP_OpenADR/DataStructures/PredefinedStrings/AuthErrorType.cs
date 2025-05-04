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
    /// Extension methods for AuthError Types
    /// </summary>
    public static class AuthErrorTypeExtensions
    {

        /// <summary>
        /// Indicates whether this Auth Error Type is null or empty.
        /// </summary>
        /// <param name="AuthErrorType">An Auth Error Type.</param>
        public static Boolean IsNullOrEmpty(this AuthErrorType? AuthErrorType)
            => !AuthErrorType.HasValue || AuthErrorType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this Auth Error Type is null or empty.
        /// </summary>
        /// <param name="AuthErrorType">An Auth Error Type.</param>
        public static Boolean IsNotNullOrEmpty(this AuthErrorType? AuthErrorType)
            => AuthErrorType.HasValue && AuthErrorType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An AuthError Type
    /// </summary>
    public readonly struct AuthErrorType : IId,
                                           IEquatable<AuthErrorType>,
                                           IComparable<AuthErrorType>
    {

        #region Data

        private readonly static Dictionary<String, AuthErrorType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this Auth Error Type is null or empty.
        /// </summary>
        public readonly  Boolean                         IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this Auth Error Type is NOT null or empty.
        /// </summary>
        public readonly  Boolean                         IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the Auth Error Type.
        /// </summary>
        public readonly  UInt64                          Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered log types.
        /// </summary>
        public static    IEnumerable<AuthErrorType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new Auth Error Type based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an Auth Error Type.</param>
        private AuthErrorType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static AuthErrorType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new AuthErrorType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an Auth Error Type.
        /// </summary>
        /// <param name="Text">A text representation of an Auth Error Type.</param>
        public static AuthErrorType Parse(String Text)
        {

            if (TryParse(Text, out var authErrorType))
                return authErrorType;

            throw new ArgumentException($"Invalid text representation of an Auth Error Type: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an Auth Error Type.
        /// </summary>
        /// <param name="Text">A text representation of an Auth Error Type.</param>
        public static AuthErrorType? TryParse(String Text)
        {

            if (TryParse(Text, out var authErrorType))
                return authErrorType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out AuthErrorType)

        /// <summary>
        /// Try to parse the given text as an Auth Error Type.
        /// </summary>
        /// <param name="Text">A text representation of an Auth Error Type.</param>
        /// <param name="AuthErrorType">The parsed Auth Error Type.</param>
        public static Boolean TryParse(String Text, out AuthErrorType AuthErrorType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out AuthErrorType))
                    AuthErrorType = Register(Text);

                return true;

            }

            AuthErrorType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this Auth Error Type.
        /// </summary>
        public AuthErrorType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// invalid_request
        /// </summary>
        public static AuthErrorType  InvalidRequest          { get; }
            = Register("invalid_request");

        /// <summary>
        /// invalid_client
        /// </summary>
        public static AuthErrorType  InvalidClient           { get; }
            = Register("invalid_client");

        /// <summary>
        /// invalid_grant
        /// </summary>
        public static AuthErrorType  InvalidGrant            { get; }
            = Register("invalid_grant");

        /// <summary>
        /// invalid_scope
        /// </summary>
        public static AuthErrorType  InvalidScope            { get; }
            = Register("invalid_scope");

        /// <summary>
        /// unauthorized_client
        /// </summary>
        public static AuthErrorType  UnauthorizedClient      { get; }
            = Register("unauthorized_client");

        /// <summary>
        /// unsupported_grant_type
        /// </summary>
        public static AuthErrorType  UnsupportedGrantType    { get; }
            = Register("unsupported_grant_type");

        #endregion


        #region Operator overloading

        #region Operator == (AuthErrorType1, AuthErrorType2)

        /// <summary>
        /// Compares two instances of this authError.
        /// </summary>
        /// <param name="AuthErrorType1">An Auth Error Type.</param>
        /// <param name="AuthErrorType2">Another Auth Error Type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (AuthErrorType AuthErrorType1,
                                           AuthErrorType AuthErrorType2)

            => AuthErrorType1.Equals(AuthErrorType2);

        #endregion

        #region Operator != (AuthErrorType1, AuthErrorType2)

        /// <summary>
        /// Compares two instances of this authError.
        /// </summary>
        /// <param name="AuthErrorType1">An Auth Error Type.</param>
        /// <param name="AuthErrorType2">Another Auth Error Type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (AuthErrorType AuthErrorType1,
                                           AuthErrorType AuthErrorType2)

            => !AuthErrorType1.Equals(AuthErrorType2);

        #endregion

        #region Operator <  (AuthErrorType1, AuthErrorType2)

        /// <summary>
        /// Compares two instances of this authError.
        /// </summary>
        /// <param name="AuthErrorType1">An Auth Error Type.</param>
        /// <param name="AuthErrorType2">Another Auth Error Type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (AuthErrorType AuthErrorType1,
                                          AuthErrorType AuthErrorType2)

            => AuthErrorType1.CompareTo(AuthErrorType2) < 0;

        #endregion

        #region Operator <= (AuthErrorType1, AuthErrorType2)

        /// <summary>
        /// Compares two instances of this authError.
        /// </summary>
        /// <param name="AuthErrorType1">An Auth Error Type.</param>
        /// <param name="AuthErrorType2">Another Auth Error Type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (AuthErrorType AuthErrorType1,
                                           AuthErrorType AuthErrorType2)

            => AuthErrorType1.CompareTo(AuthErrorType2) <= 0;

        #endregion

        #region Operator >  (AuthErrorType1, AuthErrorType2)

        /// <summary>
        /// Compares two instances of this authError.
        /// </summary>
        /// <param name="AuthErrorType1">An Auth Error Type.</param>
        /// <param name="AuthErrorType2">Another Auth Error Type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (AuthErrorType AuthErrorType1,
                                          AuthErrorType AuthErrorType2)

            => AuthErrorType1.CompareTo(AuthErrorType2) > 0;

        #endregion

        #region Operator >= (AuthErrorType1, AuthErrorType2)

        /// <summary>
        /// Compares two instances of this authError.
        /// </summary>
        /// <param name="AuthErrorType1">An Auth Error Type.</param>
        /// <param name="AuthErrorType2">Another Auth Error Type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (AuthErrorType AuthErrorType1,
                                           AuthErrorType AuthErrorType2)

            => AuthErrorType1.CompareTo(AuthErrorType2) >= 0;

        #endregion

        #endregion

        #region IComparable<AuthErrorType> Members

        #region CompareTo(AuthError)

        /// <summary>
        /// Compares two Auth Error Type.
        /// </summary>
        /// <param name="AuthError">An Auth Error Type to compare with.</param>
        public Int32 CompareTo(Object? AuthErrorType)

            => AuthErrorType is AuthErrorType authErrorType
                   ? CompareTo(authErrorType)
                   : throw new ArgumentException("The given authError is not an AuthErrorType!",
                                                 nameof(AuthErrorType));

        #endregion

        #region CompareTo(AuthErrorType)

        /// <summary>
        /// Compares two Auth Error Type.
        /// </summary>
        /// <param name="AuthErrorType">An Auth Error Type to compare with.</param>
        public Int32 CompareTo(AuthErrorType AuthErrorType)

            => String.Compare(InternalId,
                              AuthErrorType.InternalId,
                              StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region IEquatable<AuthErrorType> Members

        #region Equals(AuthError)

        /// <summary>
        /// Compares two Auth Error Type for equality.
        /// </summary>
        /// <param name="AuthError">An Auth Error Type to compare with.</param>
        public override Boolean Equals(Object? AuthErrorType)

            => AuthErrorType is AuthErrorType authErrorType &&
                   Equals(authErrorType);

        #endregion

        #region Equals(AuthErrorType)

        /// <summary>
        /// Compares two Auth Error Type for equality.
        /// </summary>
        /// <param name="AuthErrorType">An Auth Error Type to compare with.</param>
        public Boolean Equals(AuthErrorType AuthErrorType)

            => String.Equals(InternalId,
                             AuthErrorType.InternalId,
                             StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the hash code of this authError.
        /// </summary>
        public override Int32 GetHashCode()

            => InternalId?.ToLower().GetHashCode() ?? 0;

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this authError.
        /// </summary>
        public override String ToString()

            => InternalId ?? "";

        #endregion

    }

}
