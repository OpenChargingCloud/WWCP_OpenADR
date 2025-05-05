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
    /// Extension methods for Virtual End Node identifications.
    /// </summary>
    public static class VirtualEndNodeIdExtensions
    {

        /// <summary>
        /// Indicates whether this Virtual End Node identification is null or empty.
        /// </summary>
        /// <param name="VirtualEndNodeId">A Virtual End Node identification.</param>
        public static Boolean IsNullOrEmpty(this VirtualEndNode_Id? VirtualEndNodeId)
            => !VirtualEndNodeId.HasValue || VirtualEndNodeId.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this Virtual End Node identification is null or empty.
        /// </summary>
        /// <param name="VirtualEndNodeId">A Virtual End Node identification.</param>
        public static Boolean IsNotNullOrEmpty(this VirtualEndNode_Id? VirtualEndNodeId)
            => VirtualEndNodeId.HasValue && VirtualEndNodeId.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// The Virtual End Node identification.
    /// </summary>
    public readonly struct VirtualEndNode_Id : IId,
                                               IEquatable<VirtualEndNode_Id>,
                                               IComparable<VirtualEndNode_Id>
    {

        #region Data

        /// <summary>
        /// The numeric value of the Virtual End Node identification.
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
        /// The length of the Virtual End Node identification.
        /// </summary>
        public readonly UInt64 Length
            => (UInt64) Value.Length;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new Virtual End Node identification based on the given text.
        /// </summary>
        /// <param name="Text">A text representation of a Virtual End Node identification.</param>
        private VirtualEndNode_Id(String Text)
        {
            this.Value = Text;
        }

        #endregion


        #region Documentation

        // Virtual End NodeID:
        //     type: string
        //     pattern: "^[a-zA-Z0-9_-]*$"
        //     minLength: 1
        //     maxLength: 128
        //     description: URL safe VTN assigned Virtual End Node ID.
        //     example: Virtual End Node-999

        #endregion

        #region (static) NewRandom

        /// <summary>
        /// Create a new random Virtual End Node identification.
        /// </summary>
        public static VirtualEndNode_Id NewRandom

            => new (RandomExtensions.RandomString(36));

        #endregion

        #region (static) Parse    (Text)

        /// <summary>
        /// Parse the given string as a Virtual End Node identification.
        /// </summary>
        /// <param name="Text">A text representation of a Virtual End Node identification.</param>
        public static VirtualEndNode_Id Parse(String Text)
        {

            if (TryParse(Text, out var virtualEndNodeId))
                return virtualEndNodeId;

            throw new ArgumentException($"Invalid text representation of a Virtual End Node identification: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse (Text)

        /// <summary>
        /// Try to parse the given text as a Virtual End Node identification.
        /// </summary>
        /// <param name="Text">A text representation of a Virtual End Node identification.</param>
        public static VirtualEndNode_Id? TryParse(String Text)
        {

            if (TryParse(Text, out var virtualEndNodeId))
                return virtualEndNodeId;

            return null;

        }

        #endregion

        #region (static) TryParse (Text, out VirtualEndNodeId)

        /// <summary>
        /// Try to parse the given text as a Virtual End Node identification.
        /// </summary>
        /// <param name="Text">A text representation of a Virtual End Node identification.</param>
        /// <param name="VirtualEndNodeId">The parsed Virtual End Node identification.</param>
        public static Boolean TryParse(String                                     Text,
                                       [NotNullWhen(true)] out VirtualEndNode_Id  VirtualEndNodeId)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {
                VirtualEndNodeId = new VirtualEndNode_Id(Text);
                return true;
            }

            VirtualEndNodeId = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this Virtual End Node identification.
        /// </summary>
        public VirtualEndNode_Id Clone()

            => new (Value);

        #endregion


        #region Operator overloading

        #region Operator == (VirtualEndNodeId1, VirtualEndNodeId2)

        /// <summary>
        /// Compares two instances of this Virtual End Node.
        /// </summary>
        /// <param name="VirtualEndNodeId1">A Virtual End Node identification.</param>
        /// <param name="VirtualEndNodeId2">Another Virtual End Node identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (VirtualEndNode_Id VirtualEndNodeId1,
                                           VirtualEndNode_Id VirtualEndNodeId2)

            => VirtualEndNodeId1.Equals(VirtualEndNodeId2);

        #endregion

        #region Operator != (VirtualEndNodeId1, VirtualEndNodeId2)

        /// <summary>
        /// Compares two instances of this Virtual End Node.
        /// </summary>
        /// <param name="VirtualEndNodeId1">A Virtual End Node identification.</param>
        /// <param name="VirtualEndNodeId2">Another Virtual End Node identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (VirtualEndNode_Id VirtualEndNodeId1,
                                           VirtualEndNode_Id VirtualEndNodeId2)

            => !VirtualEndNodeId1.Equals(VirtualEndNodeId2);

        #endregion

        #region Operator <  (VirtualEndNodeId1, VirtualEndNodeId2)

        /// <summary>
        /// Compares two instances of this Virtual End Node.
        /// </summary>
        /// <param name="VirtualEndNodeId1">A Virtual End Node identification.</param>
        /// <param name="VirtualEndNodeId2">Another Virtual End Node identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (VirtualEndNode_Id VirtualEndNodeId1,
                                          VirtualEndNode_Id VirtualEndNodeId2)

            => VirtualEndNodeId1.CompareTo(VirtualEndNodeId2) < 0;

        #endregion

        #region Operator <= (VirtualEndNodeId1, VirtualEndNodeId2)

        /// <summary>
        /// Compares two instances of this Virtual End Node.
        /// </summary>
        /// <param name="VirtualEndNodeId1">A Virtual End Node identification.</param>
        /// <param name="VirtualEndNodeId2">Another Virtual End Node identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (VirtualEndNode_Id VirtualEndNodeId1,
                                           VirtualEndNode_Id VirtualEndNodeId2)

            => VirtualEndNodeId1.CompareTo(VirtualEndNodeId2) <= 0;

        #endregion

        #region Operator >  (VirtualEndNodeId1, VirtualEndNodeId2)

        /// <summary>
        /// Compares two instances of this Virtual End Node.
        /// </summary>
        /// <param name="VirtualEndNodeId1">A Virtual End Node identification.</param>
        /// <param name="VirtualEndNodeId2">Another Virtual End Node identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (VirtualEndNode_Id VirtualEndNodeId1,
                                          VirtualEndNode_Id VirtualEndNodeId2)

            => VirtualEndNodeId1.CompareTo(VirtualEndNodeId2) > 0;

        #endregion

        #region Operator >= (VirtualEndNodeId1, VirtualEndNodeId2)

        /// <summary>
        /// Compares two instances of this Virtual End Node.
        /// </summary>
        /// <param name="VirtualEndNodeId1">A Virtual End Node identification.</param>
        /// <param name="VirtualEndNodeId2">Another Virtual End Node identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (VirtualEndNode_Id VirtualEndNodeId1,
                                           VirtualEndNode_Id VirtualEndNodeId2)

            => VirtualEndNodeId1.CompareTo(VirtualEndNodeId2) >= 0;

        #endregion

        #endregion

        #region IComparable<VirtualEndNodeId> Members

        #region CompareTo(VirtualEndNode)

        /// <summary>
        /// Compares two Virtual End Node identifications.
        /// </summary>
        /// <param name="VirtualEndNode">A Virtual End Node identification to compare with.</param>
        public Int32 CompareTo(Object? VirtualEndNode)

            => VirtualEndNode is VirtualEndNode_Id virtualEndNodeId
                   ? CompareTo(virtualEndNodeId)
                   : throw new ArgumentException("The given Virtual End Node is not a Virtual End Node identification!",
                                                 nameof(VirtualEndNode));

        #endregion

        #region CompareTo(VirtualEndNodeId)

        /// <summary>
        /// Compares two Virtual End Node identifications.
        /// </summary>
        /// <param name="VirtualEndNodeId">A Virtual End Node identification to compare with.</param>
        public Int32 CompareTo(VirtualEndNode_Id VirtualEndNodeId)

            => String.Compare(Value,
                              VirtualEndNodeId.Value,
                              StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region IEquatable<VirtualEndNodeId> Members

        #region Equals(VirtualEndNode)

        /// <summary>
        /// Compares two Virtual End Node identifications for equality.
        /// </summary>
        /// <param name="VirtualEndNode">A Virtual End Node identification to compare with.</param>
        public override Boolean Equals(Object? VirtualEndNode)

            => VirtualEndNode is VirtualEndNode_Id virtualEndNodeId &&
                   Equals(virtualEndNodeId);

        #endregion

        #region Equals(VirtualEndNodeId)

        /// <summary>
        /// Compares two Virtual End Node identifications for equality.
        /// </summary>
        /// <param name="VirtualEndNodeId">A Virtual End Node identification to compare with.</param>
        public Boolean Equals(VirtualEndNode_Id VirtualEndNodeId)

            => String.Equals(Value,
                             VirtualEndNodeId.Value,
                             StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the HashCode of this Virtual End Node.
        /// </summary>
        public override Int32 GetHashCode()

            => Value.GetHashCode();

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this Virtual End Node.
        /// </summary>
        public override String ToString()

            => Value.ToString();

        #endregion

    }

}
