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
using org.GraphDefined.Vanaheimr.Hermod.HTTP;

#endregion

namespace cloud.charging.open.protocols.OpenADRv3
{

    /// <summary>
    /// Object type, operations, and callback URL.
    /// </summary>
    public class ObjectOperation
    {

        #region Properties

        /// <summary>
        /// The list of objects to subscribe to.
        /// </summary>
        [Mandatory]
        public IReadOnlyList<ObjectType>  Objects        { get; }

        /// <summary>
        /// The list of operations to subscribe to.
        /// </summary>
        [Mandatory]
        public IReadOnlyList<Operation>   Operations     { get; }

        /// <summary>
        /// User provided webhook URL.
        /// </summary>
        [Mandatory]
        public URL                        CallbackURL    { get; }

        /// <summary>
        /// The optional user provided HTTP Bearer authentication token.
        /// To avoid custom integrations, callback endpoints
        /// should accept the provided bearer token to authenticate VTN requests.
        /// </summary>
        [Optional]
        public String?                    BearerToken    { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new ObjectOperation.
        /// </summary>
        /// <param name="Objects">A list of objects to subscribe to.</param>
        /// <param name="Operations">A list of operations to subscribe to.</param>
        /// <param name="CallbackURL">A user provided webhook URL.</param>
        /// <param name="BearerToken">An optional user provided HTTP Bearer authentication token.</param>
        public ObjectOperation(IReadOnlyList<ObjectType>  Objects,
                               IReadOnlyList<Operation>   Operations,
                               URL                        CallbackURL,
                               String?                    BearerToken   = null)
        {

            this.Objects      = Objects;
            this.Operations   = Operations;
            this.CallbackURL  = CallbackURL;
            this.BearerToken  = BearerToken;

            unchecked
            {

                hashCode = this.Objects.     CalcHashCode() * 7 +
                           this.Operations.  CalcHashCode() * 5 +
                           this.CallbackURL. GetHashCode()  * 3 +
                          (this.BearerToken?.GetHashCode() ?? 0);

            }

        }

        #endregion


        #region Documentation

        // objectOperation:
        //   type: object
        //   description: object type, operations, and callbackUrl.
        //   required:
        //     - objects
        //     - operations
        //     - callbackUrl
        //   properties:
        //     objects:
        //       type: array
        //       description: list of objects to subscribe to.
        //       items:
        //         $ref: '#/components/schemas/objectTypes'
        //     operations:
        //       type: array
        //       description: list of operations to subscribe to.
        //       items:
        //         type: string
        //         description: object operation to subscribe to.
        //         example: POST
        //         enum: [GET, POST, PUT, DELETE]
        //     callbackUrl:
        //       type: string
        //       format: uri
        //       description: User provided webhook URL.
        //       example: https://myserver.com/send/callback/here
        //     bearerToken:
        //       type: string
        //       description: |
        //         User provided token.
        //         To avoid custom integrations, callback endpoints
        //         should accept the provided bearer token to authenticate VTN requests.
        //       example: NCEJGI9E8ER9802UT9HUG
        //       nullable: true
        //       default: null

        #endregion

        #region (static) TryParse(JSON, out ObjectOperation, CustomObjectOperationParser = null)

        // Note: The following is needed to satisfy pattern matching delegates! Do not refactor it!

        /// <summary>
        /// Try to parse the given JSON representation of an object operation.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="ObjectOperation">The parsed object operation.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParse(JObject                                    JSON,
                                       [NotNullWhen(true)]  out ObjectOperation?  ObjectOperation,
                                       [NotNullWhen(false)] out String?           ErrorResponse)

            => TryParse(JSON,
                        out ObjectOperation,
                        out ErrorResponse,
                        null);


        /// <summary>
        /// Try to parse the given JSON representation of an object operation.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="ObjectOperation">The parsed object operation.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="CustomObjectOperationParser">A delegate to parse custom object operations.</param>
        public static Boolean TryParse(JObject                                        JSON,
                                       [NotNullWhen(true)]  out ObjectOperation?      ObjectOperation,
                                       [NotNullWhen(false)] out String?               ErrorResponse,
                                       CustomJObjectParserDelegate<ObjectOperation>?  CustomObjectOperationParser)
        {

            try
            {

                ObjectOperation = null;

                #region Objects        [mandatory]

                if (!JSON.ParseMandatoryList("objects",
                                             "objects",
                                             ObjectType.TryParse,
                                             out List<ObjectType> objects,
                                             out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region Operations     [mandatory]

                if (!JSON.ParseMandatoryList("operations",
                                             "operations",
                                             Operation.TryParse,
                                             out List<Operation> operations,
                                             out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region CallbackURL    [mandatory]

                if (!JSON.ParseMandatory("callbackURL",
                                         "callback URL",
                                         URL.TryParse,
                                         out URL callbackURL,
                                         out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region BearerToken    [optional]

                if (JSON.ParseOptional("bearerToken",
                                       "HTTP Bearer Authentication Token",
                                       out String? bearerToken,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion


                ObjectOperation = new ObjectOperation(
                                     objects,
                                     operations,
                                     callbackURL,
                                     bearerToken
                                 );

                if (CustomObjectOperationParser is not null)
                    ObjectOperation = CustomObjectOperationParser(JSON,
                                                                  ObjectOperation);

                return true;

            }
            catch (Exception e)
            {
                ObjectOperation  = default;
                ErrorResponse    = "The given JSON representation of an object operation is invalid: " + e.Message;
                return false;
            }

        }

        #endregion

        #region ToJSON(CustomObjectOperationSerializer = null)

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        /// <param name="CustomObjectOperationSerializer">A delegate to serialize custom object operations.</param>
        public JObject ToJSON(CustomJObjectSerializerDelegate<ObjectOperation>? CustomObjectOperationSerializer = null)
        {

            var json = JSONObject.Create(

                                 new JProperty("objects",       new JArray(Objects.   Select(@object   => @object.  ToString()))),
                                 new JProperty("operations",    new JArray(Operations.Select(operation => operation.ToString()))),
                                 new JProperty("callbackUrl",   CallbackURL.ToString()),

                           BearerToken.IsNotNullOrEmpty()
                               ? new JProperty("bearerToken",   BearerToken)
                               : null

                       );

            return CustomObjectOperationSerializer is not null
                       ? CustomObjectOperationSerializer(this, json)
                       : json;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this object operation.
        /// </summary>
        public ObjectOperation Clone()

            => new (
                   [.. Objects.   Select(@object   => @object.  Clone())],
                   [.. Operations.Select(operation => operation.Clone())],
                   CallbackURL.Clone(),
                   BearerToken?.CloneString()
               );

        #endregion


        #region Operator overloading

        #region Operator == (ObjectOperation1, ObjectOperation2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ObjectOperation1">An object operation.</param>
        /// <param name="ObjectOperation2">Another object operation.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (ObjectOperation? ObjectOperation1,
                                           ObjectOperation? ObjectOperation2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(ObjectOperation1, ObjectOperation2))
                return true;

            // If one is null, but not both, return false.
            if (ObjectOperation1 is null || ObjectOperation2 is null)
                return false;

            return ObjectOperation1.Equals(ObjectOperation2);

        }

        #endregion

        #region Operator != (ObjectOperation1, ObjectOperation2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="ObjectOperation1">An object operation.</param>
        /// <param name="ObjectOperation2">Another object operation.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (ObjectOperation? ObjectOperation1,
                                           ObjectOperation? ObjectOperation2)

            => !(ObjectOperation1 == ObjectOperation2);

        #endregion

        #endregion

        #region IEquatable<ObjectOperation> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two object operations for equality.
        /// </summary>
        /// <param name="Object">An object operation to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is ObjectOperation objectOperation &&
                   Equals(objectOperation);

        #endregion

        #region Equals(ObjectOperation)

        /// <summary>
        /// Compares two object operations for equality.
        /// </summary>
        /// <param name="ObjectOperation">An object operation to compare with.</param>
        public Boolean Equals(ObjectOperation? ObjectOperation)

            => ObjectOperation is not null &&

               Objects.    SequenceEqual(ObjectOperation.Objects)     &&
               Operations. SequenceEqual(ObjectOperation.Operations)  &&
               CallbackURL.Equals       (ObjectOperation.CallbackURL) &&

             ((BearerToken.IsNullOrEmpty() &&  ObjectOperation.BearerToken.IsNullOrEmpty()) ||
             (!BearerToken.IsNullOrEmpty() && !ObjectOperation.BearerToken.IsNullOrEmpty() && BearerToken.Equals(ObjectOperation.BearerToken)));

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

            => String.Concat(

                   $"'{Objects.AggregateWith(", ")}', '{Operations.AggregateWith(", ")}' @ {CallbackURL}",

                   BearerToken.IsNotNullOrEmpty()
                       ? $" using '{BearerToken}'"
                       : ""

               );

        #endregion

    }

}
