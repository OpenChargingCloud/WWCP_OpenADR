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
    /// Extension methods for operations.
    /// </summary>
    public static class OperationExtensions
    {

        /// <summary>
        /// Indicates whether this operation is null or empty.
        /// </summary>
        /// <param name="Operation">An operation.</param>
        public static Boolean IsNullOrEmpty(this Operation? Operation)
            => !Operation.HasValue || Operation.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this operation is null or empty.
        /// </summary>
        /// <param name="Operation">An operation.</param>
        public static Boolean IsNotNullOrEmpty(this Operation? Operation)
            => Operation.HasValue && Operation.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// Types of objects addressable through API.
    /// </summary>
    public readonly struct Operation : IId,
                                       IEquatable<Operation>,
                                       IComparable<Operation>
    {

        #region Data

        private readonly static Dictionary<String, Operation>  lookup = new (StringComparer.OrdinalIgnoreCase);
        private readonly        String                         InternalId;

        #endregion

        #region Properties

        /// <summary>
        /// Indicates whether this operation is null or empty.
        /// </summary>
        public readonly  Boolean                         IsNullOrEmpty
            => InternalId.IsNullOrEmpty();

        /// <summary>
        /// Indicates whether this operation is NOT null or empty.
        /// </summary>
        public readonly  Boolean                         IsNotNullOrEmpty
            => InternalId.IsNotNullOrEmpty();

        /// <summary>
        /// The length of the operation.
        /// </summary>
        public readonly  UInt64                          Length
            => (UInt64) (InternalId?.Length ?? 0);

        /// <summary>
        /// All registered log types.
        /// </summary>
        public static    IEnumerable<Operation>  All
            => lookup.Values;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new operation based on the given text.
        /// </summary>
        /// <param name="Text">The text representation of an operation.</param>
        private Operation(String Text)
        {
            this.InternalId = Text;
        }

        #endregion


        #region (private static) Register(Text)

        private static Operation Register(String Text)

            => lookup.AddAndReturnValue(
                   Text,
                   new Operation(Text)
               );

        #endregion


        #region (static) Parse   (Text)

        /// <summary>
        /// Parse the given string as an operation.
        /// </summary>
        /// <param name="Text">A text representation of an operation.</param>
        public static Operation Parse(String Text)
        {

            if (TryParse(Text, out var operation))
                return operation;

            throw new ArgumentException($"Invalid text representation of an operation: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse(Text)

        /// <summary>
        /// Try to parse the given text as an operation.
        /// </summary>
        /// <param name="Text">A text representation of an operation.</param>
        public static Operation? TryParse(String Text)
        {

            if (TryParse(Text, out var operation))
                return operation;

            return null;

        }

        #endregion

        #region (static) TryParse(Text, out Operation)

        /// <summary>
        /// Try to parse the given text as an operation.
        /// </summary>
        /// <param name="Text">A text representation of an operation.</param>
        /// <param name="Operation">The parsed operation.</param>
        public static Boolean TryParse(String Text, out Operation Operation)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {

                if (!lookup.TryGetValue(Text, out Operation))
                    Operation = Register(Text);

                return true;

            }

            Operation = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this operation.
        /// </summary>
        public Operation Clone()

            => new (
                   InternalId.CloneString()
               );

        #endregion


        #region Static definitions

        /// <summary>
        /// GET
        /// </summary>
        public static Operation  GET       { get; }
            = Register("GET");

        /// <summary>
        /// POST
        /// </summary>
        public static Operation  POST      { get; }
            = Register("POST");

        /// <summary>
        /// PUT
        /// </summary>
        public static Operation  PUT       { get; }
            = Register("PUT");

        /// <summary>
        /// DELETE
        /// </summary>
        public static Operation  DELETE    { get; }
            = Register("DELETE");

        #endregion


        #region Operator overloading

        #region Operator == (Operation1, Operation2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Operation1">An operation.</param>
        /// <param name="Operation2">Another operation.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Operation Operation1,
                                           Operation Operation2)

            => Operation1.Equals(Operation2);

        #endregion

        #region Operator != (Operation1, Operation2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Operation1">An operation.</param>
        /// <param name="Operation2">Another operation.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Operation Operation1,
                                           Operation Operation2)

            => !Operation1.Equals(Operation2);

        #endregion

        #region Operator <  (Operation1, Operation2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Operation1">An operation.</param>
        /// <param name="Operation2">Another operation.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (Operation Operation1,
                                          Operation Operation2)

            => Operation1.CompareTo(Operation2) < 0;

        #endregion

        #region Operator <= (Operation1, Operation2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Operation1">An operation.</param>
        /// <param name="Operation2">Another operation.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (Operation Operation1,
                                           Operation Operation2)

            => Operation1.CompareTo(Operation2) <= 0;

        #endregion

        #region Operator >  (Operation1, Operation2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Operation1">An operation.</param>
        /// <param name="Operation2">Another operation.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (Operation Operation1,
                                          Operation Operation2)

            => Operation1.CompareTo(Operation2) > 0;

        #endregion

        #region Operator >= (Operation1, Operation2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Operation1">An operation.</param>
        /// <param name="Operation2">Another operation.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (Operation Operation1,
                                           Operation Operation2)

            => Operation1.CompareTo(Operation2) >= 0;

        #endregion

        #endregion

        #region IComparable<Operation> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two operation.
        /// </summary>
        /// <param name="Object">An operation to compare with.</param>
        public Int32 CompareTo(Object? Object)

            => Object is Operation operation
                   ? CompareTo(operation)
                   : throw new ArgumentException("The given object is not an operation!",
                                                 nameof(Object));

        #endregion

        #region CompareTo(Operation)

        /// <summary>
        /// Compares two operation.
        /// </summary>
        /// <param name="Operation">An operation to compare with.</param>
        public Int32 CompareTo(Operation Operation)

            => String.Compare(InternalId,
                              Operation.InternalId,
                              StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region IEquatable<Operation> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two operation for equality.
        /// </summary>
        /// <param name="Object">An operation to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is Operation operation &&
                   Equals(operation);

        #endregion

        #region Equals(Operation)

        /// <summary>
        /// Compares two operation for equality.
        /// </summary>
        /// <param name="Operation">An operation to compare with.</param>
        public Boolean Equals(Operation Operation)

            => String.Equals(InternalId,
                             Operation.InternalId,
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
