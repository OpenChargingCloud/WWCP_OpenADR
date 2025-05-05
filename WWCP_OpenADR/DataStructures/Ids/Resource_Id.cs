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
    /// Extension methods for resource identifications.
    /// </summary>
    public static class ResourceIdExtensions
    {

        /// <summary>
        /// Indicates whether this resource identification is null or empty.
        /// </summary>
        /// <param name="ResourceId">A resource identification.</param>
        public static Boolean IsNullOrEmpty(this Resource_Id? ResourceId)
            => !ResourceId.HasValue || ResourceId.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this resource identification is null or empty.
        /// </summary>
        /// <param name="ResourceId">A resource identification.</param>
        public static Boolean IsNotNullOrEmpty(this Resource_Id? ResourceId)
            => ResourceId.HasValue && ResourceId.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// The resource identification.
    /// </summary>
    public readonly struct Resource_Id : IId,
                                         IEquatable<Resource_Id>,
                                         IComparable<Resource_Id>
    {

        #region Data

        /// <summary>
        /// The numeric value of the resource identification.
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
        /// The length of the resource identification.
        /// </summary>
        public readonly UInt64 Length
            => (UInt64) Value.Length;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new resource identification based on the given text.
        /// </summary>
        /// <param name="Text">A text representation of a resource identification.</param>
        private Resource_Id(String Text)
        {
            this.Value = Text;
        }

        #endregion


        #region Documentation

        // resourceID:
        //     type: string
        //     pattern: "^[a-zA-Z0-9_-]*$"
        //     minLength: 1
        //     maxLength: 128
        //     description: URL safe VTN assigned resource ID.
        //     example: resource-999

        #endregion

        #region (static) NewRandom

        /// <summary>
        /// Create a new random resource identification.
        /// </summary>
        public static Resource_Id NewRandom

            => new (RandomExtensions.RandomString(36));

        #endregion

        #region (static) Parse    (Text)

        /// <summary>
        /// Parse the given string as a resource identification.
        /// </summary>
        /// <param name="Text">A text representation of a resource identification.</param>
        public static Resource_Id Parse(String Text)
        {

            if (TryParse(Text, out var resourceId))
                return resourceId;

            throw new ArgumentException($"Invalid text representation of a resource identification: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse (Text)

        /// <summary>
        /// Try to parse the given text as a resource identification.
        /// </summary>
        /// <param name="Text">A text representation of a resource identification.</param>
        public static Resource_Id? TryParse(String Text)
        {

            if (TryParse(Text, out var resourceId))
                return resourceId;

            return null;

        }

        #endregion

        #region (static) TryParse (Text, out ResourceId)

        /// <summary>
        /// Try to parse the given text as a resource identification.
        /// </summary>
        /// <param name="Text">A text representation of a resource identification.</param>
        /// <param name="ResourceId">The parsed resource identification.</param>
        public static Boolean TryParse(String                             Text,
                                       [NotNullWhen(true)] out Resource_Id  ResourceId)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {
                ResourceId = new Resource_Id(Text);
                return true;
            }

            ResourceId = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this resource identification.
        /// </summary>
        public Resource_Id Clone()

            => new (Value);

        #endregion


        #region Operator overloading

        #region Operator == (ResourceId1, ResourceId2)

        /// <summary>
        /// Compares two instances of this resource.
        /// </summary>
        /// <param name="ResourceId1">A resource identification.</param>
        /// <param name="ResourceId2">Another resource identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Resource_Id ResourceId1,
                                           Resource_Id ResourceId2)

            => ResourceId1.Equals(ResourceId2);

        #endregion

        #region Operator != (ResourceId1, ResourceId2)

        /// <summary>
        /// Compares two instances of this resource.
        /// </summary>
        /// <param name="ResourceId1">A resource identification.</param>
        /// <param name="ResourceId2">Another resource identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Resource_Id ResourceId1,
                                           Resource_Id ResourceId2)

            => !ResourceId1.Equals(ResourceId2);

        #endregion

        #region Operator <  (ResourceId1, ResourceId2)

        /// <summary>
        /// Compares two instances of this resource.
        /// </summary>
        /// <param name="ResourceId1">A resource identification.</param>
        /// <param name="ResourceId2">Another resource identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (Resource_Id ResourceId1,
                                          Resource_Id ResourceId2)

            => ResourceId1.CompareTo(ResourceId2) < 0;

        #endregion

        #region Operator <= (ResourceId1, ResourceId2)

        /// <summary>
        /// Compares two instances of this resource.
        /// </summary>
        /// <param name="ResourceId1">A resource identification.</param>
        /// <param name="ResourceId2">Another resource identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (Resource_Id ResourceId1,
                                           Resource_Id ResourceId2)

            => ResourceId1.CompareTo(ResourceId2) <= 0;

        #endregion

        #region Operator >  (ResourceId1, ResourceId2)

        /// <summary>
        /// Compares two instances of this resource.
        /// </summary>
        /// <param name="ResourceId1">A resource identification.</param>
        /// <param name="ResourceId2">Another resource identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (Resource_Id ResourceId1,
                                          Resource_Id ResourceId2)

            => ResourceId1.CompareTo(ResourceId2) > 0;

        #endregion

        #region Operator >= (ResourceId1, ResourceId2)

        /// <summary>
        /// Compares two instances of this resource.
        /// </summary>
        /// <param name="ResourceId1">A resource identification.</param>
        /// <param name="ResourceId2">Another resource identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (Resource_Id ResourceId1,
                                           Resource_Id ResourceId2)

            => ResourceId1.CompareTo(ResourceId2) >= 0;

        #endregion

        #endregion

        #region IComparable<ResourceId> Members

        #region CompareTo(Resource)

        /// <summary>
        /// Compares two resource identifications.
        /// </summary>
        /// <param name="Resource">A resource identification to compare with.</param>
        public Int32 CompareTo(Object? Resource)

            => Resource is Resource_Id resourceId
                   ? CompareTo(resourceId)
                   : throw new ArgumentException("The given resource is not a resource identification!",
                                                 nameof(Resource));

        #endregion

        #region CompareTo(ResourceId)

        /// <summary>
        /// Compares two resource identifications.
        /// </summary>
        /// <param name="ResourceId">A resource identification to compare with.</param>
        public Int32 CompareTo(Resource_Id ResourceId)

            => String.Compare(Value,
                              ResourceId.Value,
                              StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region IEquatable<ResourceId> Members

        #region Equals(Resource)

        /// <summary>
        /// Compares two resource identifications for equality.
        /// </summary>
        /// <param name="Resource">A resource identification to compare with.</param>
        public override Boolean Equals(Object? Resource)

            => Resource is Resource_Id resourceId &&
                   Equals(resourceId);

        #endregion

        #region Equals(ResourceId)

        /// <summary>
        /// Compares two resource identifications for equality.
        /// </summary>
        /// <param name="ResourceId">A resource identification to compare with.</param>
        public Boolean Equals(Resource_Id ResourceId)

            => String.Equals(Value,
                             ResourceId.Value,
                             StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the HashCode of this resource.
        /// </summary>
        public override Int32 GetHashCode()

            => Value.GetHashCode();

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this resource.
        /// </summary>
        public override String ToString()

            => Value.ToString();

        #endregion

    }

}
