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
    /// Extension methods for object identifications.
    /// </summary>
    public static class ObjectIdExtensions
    {

        /// <summary>
        /// Indicates whether this object identification is null or empty.
        /// </summary>
        /// <param name="ObjectId">An object identification.</param>
        public static Boolean IsNullOrEmpty(this Object_Id? ObjectId)
            => !ObjectId.HasValue || ObjectId.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this object identification is null or empty.
        /// </summary>
        /// <param name="ObjectId">An object identification.</param>
        public static Boolean IsNotNullOrEmpty(this Object_Id? ObjectId)
            => ObjectId.HasValue && ObjectId.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// The object identification.
    /// </summary>
    public readonly struct Object_Id : IId,
                                       IEquatable<Object_Id>,
                                       IComparable<Object_Id>
    {

        #region Data

        /// <summary>
        /// The numeric value of the object identification.
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
        /// The length of the object identification.
        /// </summary>
        public readonly UInt64 Length
            => (UInt64) Value.Length;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new object identification based on the given text.
        /// </summary>
        /// <param name="Text">A text representation of an object identification.</param>
        private Object_Id(String Text)
        {
            this.Value = Text;
        }

        #endregion


        #region Documentation

        // objectID:
        //     type: string
        //     pattern: "^[a-zA-Z0-9_-]*$"
        //     minLength: 1
        //     maxLength: 128
        //     description: URL safe VTN assigned object ID.
        //     example: object-999

        #endregion

        #region (static) NewRandom

        /// <summary>
        /// Create a new random object identification.
        /// </summary>
        public static Object_Id NewRandom

            => new (RandomExtensions.RandomString(36));

        #endregion

        #region (static) Parse    (Text)

        /// <summary>
        /// Parse the given string as an object identification.
        /// </summary>
        /// <param name="Text">A text representation of an object identification.</param>
        public static Object_Id Parse(String Text)
        {

            if (TryParse(Text, out var objectId))
                return objectId;

            throw new ArgumentException($"Invalid text representation of an object identification: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse (Text)

        /// <summary>
        /// Try to parse the given text as an object identification.
        /// </summary>
        /// <param name="Text">A text representation of an object identification.</param>
        public static Object_Id? TryParse(String Text)
        {

            if (TryParse(Text, out var objectId))
                return objectId;

            return null;

        }

        #endregion

        #region (static) TryParse (Text, out ObjectId)

        /// <summary>
        /// Try to parse the given text as an object identification.
        /// </summary>
        /// <param name="Text">A text representation of an object identification.</param>
        /// <param name="ObjectId">The parsed object identification.</param>
        public static Boolean TryParse(String                             Text,
                                       [NotNullWhen(true)] out Object_Id  ObjectId)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {
                ObjectId = new Object_Id(Text);
                return true;
            }

            ObjectId = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this object identification.
        /// </summary>
        public Object_Id Clone()

            => new (Value);

        #endregion


        #region Operator overloading

        #region Operator == (ObjectId1, ObjectId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ObjectId1">An object identification.</param>
        /// <param name="ObjectId2">Another object identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Object_Id ObjectId1,
                                           Object_Id ObjectId2)

            => ObjectId1.Equals(ObjectId2);

        #endregion

        #region Operator != (ObjectId1, ObjectId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ObjectId1">An object identification.</param>
        /// <param name="ObjectId2">Another object identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Object_Id ObjectId1,
                                           Object_Id ObjectId2)

            => !ObjectId1.Equals(ObjectId2);

        #endregion

        #region Operator <  (ObjectId1, ObjectId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ObjectId1">An object identification.</param>
        /// <param name="ObjectId2">Another object identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (Object_Id ObjectId1,
                                          Object_Id ObjectId2)

            => ObjectId1.CompareTo(ObjectId2) < 0;

        #endregion

        #region Operator <= (ObjectId1, ObjectId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ObjectId1">An object identification.</param>
        /// <param name="ObjectId2">Another object identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (Object_Id ObjectId1,
                                           Object_Id ObjectId2)

            => ObjectId1.CompareTo(ObjectId2) <= 0;

        #endregion

        #region Operator >  (ObjectId1, ObjectId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ObjectId1">An object identification.</param>
        /// <param name="ObjectId2">Another object identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (Object_Id ObjectId1,
                                          Object_Id ObjectId2)

            => ObjectId1.CompareTo(ObjectId2) > 0;

        #endregion

        #region Operator >= (ObjectId1, ObjectId2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ObjectId1">An object identification.</param>
        /// <param name="ObjectId2">Another object identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (Object_Id ObjectId1,
                                           Object_Id ObjectId2)

            => ObjectId1.CompareTo(ObjectId2) >= 0;

        #endregion

        #endregion

        #region IComparable<ObjectId> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two object identifications.
        /// </summary>
        /// <param name="Object">An object identification to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is Object_Id objectId
                   ? CompareTo(objectId)
                   : throw new ArgumentException("The given object is not an object identification!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(ObjectId)

        /// <summary>
        /// Compares two object identifications.
        /// </summary>
        /// <param name="ObjectId">An object identification to compare with.</param>
        public Int32 CompareTo(Object_Id ObjectId)

            => String.Compare(Value,
                              ObjectId.Value,
                              StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region IEquatable<ObjectId> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two object identifications for equality.
        /// </summary>
        /// <param name="Object">An object identification to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is Object_Id objectId &&
                   Equals(objectId);

        #endregion

        #region Equals(ObjectId)

        /// <summary>
        /// Compares two object identifications for equality.
        /// </summary>
        /// <param name="ObjectId">An object identification to compare with.</param>
        public Boolean Equals(Object_Id ObjectId)

            => String.Equals(Value,
                             ObjectId.Value,
                             StringComparison.OrdinalIgnoreCase);

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
