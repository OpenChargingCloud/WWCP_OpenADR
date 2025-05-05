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
    /// Extension methods for Object Types
    /// </summary>
    public static class ObjectTypeExtensions
    {

        /// <summary>
        /// Indicates whether this object type is null or empty.
        /// </summary>
        /// <param name="ObjectType">An object type.</param>
        public static Boolean IsNullOrEmpty(this ObjectType? ObjectType)
            => !ObjectType.HasValue || ObjectType.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this object type is null or empty.
        /// </summary>
        /// <param name="ObjectType">An object type.</param>
        public static Boolean IsNotNullOrEmpty(this ObjectType? ObjectType)
            => ObjectType.HasValue && ObjectType.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// An Object Type
    /// </summary>
    public readonly struct ObjectType : IId,
                                        IEquatable<ObjectType>,
                                        IComparable<ObjectType>
    {

        #region (class) Defaults

        /// <summary>
        /// Default object types.
        /// </summary>
        public static class Defaults
        {
            public const String EVENT         = "EVENT";
            public const String PROGRAM       = "PROGRAM";
            public const String REPORT        = "REPORT";
            public const String RESOURCE      = "RESOURCE";
            public const String SUBSCRIPTION  = "SUBSCRIPTION";
            public const String VEN           = "VEN";
        }

        #endregion


        #region Data

        private readonly static Dictionary<String, ObjectType>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this object type is null or empty.
        /// </summary>
        public readonly  Boolean                         IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this object type is NOT null or empty.
        /// </summary>
        public readonly  Boolean                         IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the object type.
        /// </summary>
        public readonly  UInt64                          Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered log types.
        /// </summary>
        public static    IEnumerable<ObjectType>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new object type based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an object type.</param>
        private ObjectType(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static ObjectType Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new ObjectType(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an object type.
        /// </summary>
        /// <param name="Text">A text representation of an object type.</param>
        public static ObjectType Parse(String Text)
        {

            if (TryParse(Text, out var objectType))
                return objectType;

            throw new ArgumentException($"Invalid text representation of an object type: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an object type.
        /// </summary>
        /// <param name="Text">A text representation of an object type.</param>
        public static ObjectType? TryParse(String Text)
        {

            if (TryParse(Text, out var objectType))
                return objectType;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out ObjectType)

        /// <summary>
        /// Try to parse the given text as an object type.
        /// </summary>
        /// <param name="Text">A text representation of an object type.</param>
        /// <param name="ObjectType">The parsed object type.</param>
        public static Boolean TryParse(String Text, out ObjectType ObjectType)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out ObjectType))
                    ObjectType = Register(Text);

                return true;

            }

            ObjectType = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this object type.
        /// </summary>
        public ObjectType Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// PROGRAM
        /// </summary>
        public static ObjectType  PROGRAM                      { get; }
            = Register(Defaults.PROGRAM);

        /// <summary>
        /// EVENT
        /// </summary>
        public static ObjectType  EVENT                        { get; }
            = Register(Defaults.EVENT);

        /// <summary>
        /// REPORT
        /// </summary>
        public static ObjectType  REPORT                       { get; }
            = Register(Defaults.REPORT);

        /// <summary>
        /// SUBSCRIPTION
        /// </summary>
        public static ObjectType  SUBSCRIPTION                 { get; }
            = Register(Defaults.SUBSCRIPTION);

        /// <summary>
        /// VEN
        /// </summary>
        public static ObjectType  VEN                          { get; }
            = Register(Defaults.VEN);

        /// <summary>
        /// RESOURCE
        /// </summary>
        public static ObjectType  RESOURCE                     { get; }
            = Register(Defaults.RESOURCE);




        /// <summary>
        /// EVENT_PAYLOAD_DESCRIPTOR
        /// </summary>
        public static ObjectType  EVENT_PAYLOAD_DESCRIPTOR     { get; }
            = Register("EVENT_PAYLOAD_DESCRIPTOR");

        /// <summary>
        /// REPORT_PAYLOAD_DESCRIPTOR
        /// </summary>
        public static ObjectType  REPORT_PAYLOAD_DESCRIPTOR    { get; }
            = Register("REPORT_PAYLOAD_DESCRIPTOR");

        #endregion


        #region Operator overloading

        #region Operator == (ObjectType1, ObjectType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ObjectType1">An object type.</param>
        /// <param name="ObjectType2">Another object type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ObjectType ObjectType1,
                                           ObjectType ObjectType2)

            => ObjectType1.Equals(ObjectType2);

        #endregion

        #region Operator != (ObjectType1, ObjectType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ObjectType1">An object type.</param>
        /// <param name="ObjectType2">Another object type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ObjectType ObjectType1,
                                           ObjectType ObjectType2)

            => !ObjectType1.Equals(ObjectType2);

        #endregion

        #region Operator <  (ObjectType1, ObjectType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ObjectType1">An object type.</param>
        /// <param name="ObjectType2">Another object type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (ObjectType ObjectType1,
                                          ObjectType ObjectType2)

            => ObjectType1.CompareTo(ObjectType2) < 0;

        #endregion

        #region Operator <= (ObjectType1, ObjectType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ObjectType1">An object type.</param>
        /// <param name="ObjectType2">Another object type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (ObjectType ObjectType1,
                                           ObjectType ObjectType2)

            => ObjectType1.CompareTo(ObjectType2) <= 0;

        #endregion

        #region Operator >  (ObjectType1, ObjectType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ObjectType1">An object type.</param>
        /// <param name="ObjectType2">Another object type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (ObjectType ObjectType1,
                                          ObjectType ObjectType2)

            => ObjectType1.CompareTo(ObjectType2) > 0;

        #endregion

        #region Operator >= (ObjectType1, ObjectType2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ObjectType1">An object type.</param>
        /// <param name="ObjectType2">Another object type.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (ObjectType ObjectType1,
                                           ObjectType ObjectType2)

            => ObjectType1.CompareTo(ObjectType2) >= 0;

        #endregion

        #endregion

        #region IComparable<ObjectType> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two object type.
        /// </summary>
        /// <param name="Object">An object type to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is ObjectType objectType
                   ? CompareTo(objectType)
                   : throw new ArgumentException("The given object is not an object type!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(ObjectType)

        /// <summary>
        /// Compares two object type.
        /// </summary>
        /// <param name="ObjectType">An object type to compare with.</param>
        public Int32 CompareTo(ObjectType ObjectType)

            => String.Compare(InternalId,
                              ObjectType.InternalId,
                              StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region IEquatable<ObjectType> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two object type for equality.
        /// </summary>
        /// <param name="Object">An object type to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is ObjectType objectType &&
                   Equals(objectType);

        #endregion

        #region Equals(ObjectType)

        /// <summary>
        /// Compares two object type for equality.
        /// </summary>
        /// <param name="ObjectType">An object type to compare with.</param>
        public Boolean Equals(ObjectType ObjectType)

            => String.Equals(InternalId,
                             ObjectType.InternalId,
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
