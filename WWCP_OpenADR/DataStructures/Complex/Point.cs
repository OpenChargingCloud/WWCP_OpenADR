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
    /// A pair of floats typically used as a point on a 2 dimensional grid.
    /// Can be used within a values map.
    /// </summary>
    /// <param name="X">The value on an x axis.</param>
    /// <param name="Y">The value on a y axis.</param>
    public readonly struct Point(Single  X,
                                 Single  Y)
    {

        #region Properties

        /// <summary>
        /// A value on an x axis.
        /// </summary>
        public Single  X    { get; } = X;

        /// <summary>
        /// A value on a y axis.
        /// </summary>
        public Single  Y    { get; } = Y;

        #endregion


        #region Documentation

        // point:
        //   type: object
        //   description: A pair of floats typically used as a point on a 2 dimensional grid.
        //   required:
        //     - x
        //     - y
        //   properties:
        //     x:
        //       type: number
        //       format: float
        //       description: A value on an x axis.
        //       example: 1.0
        //     y:
        //       type: number
        //       format: float
        //       description: A value on a y axis.
        //       example: 2.0

        #endregion

        #region (static) TryParse(JSON, out Point, CustomPointParser = null)

        // Note: The following is needed to satisfy pattern matching delegates! Do not refactor it!

        /// <summary>
        /// Try to parse the given JSON representation of a point.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="Point">The parsed point.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParse(JObject                           JSON,
                                       [NotNullWhen(true)]  out Point    Point,
                                       [NotNullWhen(false)] out String?  ErrorResponse)

            => TryParse(JSON,
                        out Point,
                        out ErrorResponse,
                        null);


        /// <summary>
        /// Try to parse the given JSON representation of a point.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="Point">The parsed point.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="CustomPointParser">A delegate to parse custom points.</param>
        public static Boolean TryParse(JObject                              JSON,
                                       [NotNullWhen(true)]  out Point       Point,
                                       [NotNullWhen(false)] out String?     ErrorResponse,
                                       CustomJObjectParserDelegate<Point>?  CustomPointParser)
        {

            try
            {

                Point = default;

                #region X    [mandatory]

                if (!JSON.ParseMandatory("x",
                                         "x coordinate",
                                         out Single x,
                                         out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region Y    [mandatory]

                if (!JSON.ParseMandatory("y",
                                         "y coordinate",
                                         out Single y,
                                         out ErrorResponse))
                {
                    return false;
                }

                #endregion


                Point = new Point(
                            x,
                            y
                        );

                if (CustomPointParser is not null)
                    Point = CustomPointParser(JSON,
                                              Point);

                return true;

            }
            catch (Exception e)
            {
                Point          = default;
                ErrorResponse  = "The given JSON representation of a point is invalid: " + e.Message;
                return false;
            }

        }

        #endregion

        #region ToJSON(CustomPointSerializer = null)

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        /// <param name="CustomPointSerializer">A delegate to serialize custom points.</param>
        public JObject ToJSON(CustomJObjectSerializerDelegate<Point>? CustomPointSerializer = null)
        {

            var json = JSONObject.Create(
                           new JProperty("x",  X),
                           new JProperty("y",  Y)
                       );

            return CustomPointSerializer is not null
                       ? CustomPointSerializer(this, json)
                       : json;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this point.
        /// </summary>
        public Point Clone()

            => new (X, Y);

        #endregion


        #region Operator overloading

        #region Operator == (Point1, Point2)

        /// <summary>
        /// Compares two instances of this report.
        /// </summary>
        /// <param name="Point1">A point.</param>
        /// <param name="Point2">Another point.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Point Point1,
                                           Point Point2)

            => Point1.Equals(Point2);

        #endregion

        #region Operator != (Point1, Point2)

        /// <summary>
        /// Compares two instances of this report.
        /// </summary>
        /// <param name="Point1">A point.</param>
        /// <param name="Point2">Another point.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Point Point1,
                                           Point Point2)

            => !Point1.Equals(Point2);

        #endregion

        #endregion

        #region IEquatable<Point> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two points for equality.
        /// </summary>
        /// <param name="Object">A point to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is Point point &&
                   Equals(point);

        #endregion

        #region Equals(Point)

        /// <summary>
        /// Compares two points for equality.
        /// </summary>
        /// <param name="Point">A point to compare with.</param>
        public Boolean Equals(Point Point)

            => X.Equals(Point.X) &&
               Y.Equals(Point.Y);

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the hash code of this object.
        /// </summary>
        public override Int32 GetHashCode()

            => X.GetHashCode() ^
               Y.GetHashCode();

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this object.
        /// </summary>
        public override String ToString()

            => $"{X} / {Y}";

        #endregion

    }

}
