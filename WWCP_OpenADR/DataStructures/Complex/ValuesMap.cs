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

using Newtonsoft.Json.Linq;

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace cloud.charging.open.protocols.OpenADRv3
{

    /// <summary>
    /// Represents one or more values associated with a type,
    /// e.g. a type of PRICE contains a single float value.
    /// </summary>
    public class ValuesMap
    {

        #region Properties

        /// <summary>
        /// Specifies the type of the values,
        /// e.g. "PRICE" indicates value is to be interpreted as a currency.
        /// </summary>
        [Mandatory]
        public ValueType              Type      { get; }

        /// <summary>
        /// A list of data points.
        /// Most often a singular value such as a price.
        /// </summary>
        [Mandatory]
        public IReadOnlyList<Object>  Values    { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new values map.
        /// </summary>
        /// <param name="Type">Specifies the type of the values, e.g. "PRICE" indicates value is to be interpreted as a currency.</param>
        /// <param name="Values">A list of data points. Most often a singular value such as a price.</param>
        public ValuesMap(ValueType              Type,
                         IReadOnlyList<Object>  Values)
        {

            this.Type    = Type;
            this.Values  = Values;

            unchecked
            {
                hashCode = this.Type.  GetHashCode() * 3 ^
                           this.Values.CalcHashCode();
            }

        }


        /// <summary>
        /// Create a new values map.
        /// </summary>
        /// <param name="Type">Specifies the type of the values, e.g. "PRICE" indicates value is to be interpreted as a currency.</param>
        /// <param name="Values">A list of data points. Most often a singular value such as a price.</param>
        public ValuesMap(ValueType            Type,
                         IEnumerable<Object>  Values)

            : this(Type,
                   [.. Values])

        { }

        #endregion


        #region Documentation

        // valuesMap:
        //   type: object
        //   description: |
        //     Represents one or more values associated with a type.
        //     E.g. a type of PRICE contains a single float value.
        //   required:
        //     - type
        //     - values
        //   properties:
        //     type:
        //       type: string
        //       minLength: 1
        //       maxLength: 128
        //       description: |
        //         Enumerated or private string signifying the nature of values.
        //         E.G. "PRICE" indicates value is to be interpreted as a currency.
        //       example: PRICE
        //     values:
        //         type: array
        //         description: A list of data points. Most often a singular value such as a price.
        //         example: [0.17]
        //         items:
        //           anyOf:
        //             - type: number
        //             - type: integer
        //             - type: string
        //             - type: boolean
        //             - $ref: '#/components/schemas/point'

        #endregion

        #region (static) TryParse(JSON, out ValuesMap, CustomValuesMapParser = null)

        // Note: The following is needed to satisfy pattern matching delegates! Do not refactor it!

        /// <summary>
        /// Try to parse the given JSON representation of a values map.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="ValuesMap">The parsed values map.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParse(JObject                              JSON,
                                       [NotNullWhen(true)]  out ValuesMap?  ValuesMap,
                                       [NotNullWhen(false)] out String?     ErrorResponse)

            => TryParse(JSON,
                        out ValuesMap,
                        out ErrorResponse,
                        null);


        /// <summary>
        /// Try to parse the given JSON representation of a values map.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="ValuesMap">The parsed values map.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="CustomValuesMapParser">A delegate to parse custom values maps.</param>
        public static Boolean TryParse(JObject                                  JSON,
                                       [NotNullWhen(true)]  out ValuesMap?      ValuesMap,
                                       [NotNullWhen(false)] out String?         ErrorResponse,
                                       CustomJObjectParserDelegate<ValuesMap>?  CustomValuesMapParser)
        {

            try
            {

                ValuesMap = null;

                #region Type      [mandatory]

                if (!JSON.ParseMandatory("type",
                                         "type",
                                         ValueType.TryParse,
                                         out ValueType type,
                                         out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region Values    [mandatory]

                if (!JSON.ParseMandatoryObjectList("values",
                                                   "values",
                                                   out List<Object> values,
                                                   out ErrorResponse))
                {
                    return false;
                }

                #endregion


                ValuesMap = new ValuesMap(
                                type,
                                values
                            );

                if (CustomValuesMapParser is not null)
                    ValuesMap = CustomValuesMapParser(JSON,
                                                      ValuesMap);

                return true;

            }
            catch (Exception e)
            {
                ValuesMap  = default;
                ErrorResponse    = "The given JSON representation of a values map is invalid: " + e.Message;
                return false;
            }

        }

        #endregion

        #region ToJSON(CustomValuesMapSerializer = null)

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        /// <param name="CustomValuesMapSerializer">A delegate to serialize custom values maps.</param>
        public JObject ToJSON(CustomJObjectSerializerDelegate<ValuesMap>? CustomValuesMapSerializer = null)
        {

            var json = JSONObject.Create(
                            new JProperty("type",     Type.ToString()),
                            new JProperty("values",   new JArray(Values))
                       );

            return CustomValuesMapSerializer is not null
                       ? CustomValuesMapSerializer(this, json)
                       : json;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this values map.
        /// </summary>
        public ValuesMap Clone()

            => new (Type.Clone(),
                    [.. Values]);

        #endregion


        #region Operator overloading

        #region Operator == (ValuesMap1, ValuesMap2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ValuesMap1">A values map.</param>
        /// <param name="ValuesMap2">Another values map.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ValuesMap? ValuesMap1,
                                           ValuesMap? ValuesMap2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(ValuesMap1, ValuesMap2))
                return true;

            // If one is null, but not both, return false.
            if (ValuesMap1 is null || ValuesMap2 is null)
                return false;

            return ValuesMap1.Equals(ValuesMap2);

        }

        #endregion

        #region Operator != (ValuesMap1, ValuesMap2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ValuesMap1">A values map.</param>
        /// <param name="ValuesMap2">Another values map.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ValuesMap? ValuesMap1,
                                           ValuesMap? ValuesMap2)

            => !(ValuesMap1 == ValuesMap2);

        #endregion

        #endregion

        #region IEquatable<ValuesMap> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two values maps for equality.
        /// </summary>
        /// <param name="Object">A values map to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is ValuesMap valuesMap &&
                   Equals(valuesMap);

        #endregion

        #region Equals(ValuesMap)

        /// <summary>
        /// Compares two values maps for equality.
        /// </summary>
        /// <param name="ValuesMap">A values map to compare with.</param>
        public Boolean Equals(ValuesMap? ValuesMap)

            => ValuesMap is not null &&
               Type.  Equals       (ValuesMap.Type) &&
               Values.SequenceEqual(ValuesMap.Values);

        #endregion

        #endregion

        #region (override) GetHashCode()

        private readonly Int32 hashCode;

        /// <summary>
        /// Return the hash code of this object.
        /// </summary>
        public override Int32 GetHashCode()
            => hashCode;

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this object.
        /// </summary>
        public override String ToString()

            => $"{Type}: ({Values.AggregateWith(", ")})";

        #endregion

    }

}
