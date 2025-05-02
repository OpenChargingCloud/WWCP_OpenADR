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
    /// A Virtual End Node.
    /// </summary>
    public class VirtualEndNode : AOpenADRObject
    {

        #region Properties

        /// <summary>
        /// The name of the Virtual End Node, which is expected
        /// to be unique within the scope of a Virtual Top Node (VTN).
        /// An user generated identifier which may also be a
        /// VEN identifier provisioned out-of-band.
        /// </summary>
        [Mandatory]
        public String                  VenName       { get; }

        /// <summary>
        /// The optional enumeration of valuesMap objects describing attributes.
        /// </summary>
        [Optional]
        public IEnumerable<ValuesMap>  Attributes    { get; }

        /// <summary>
        /// The optional enumeration of valuesMap objects describing target criteria.
        /// </summary>
        [Optional]
        public IEnumerable<ValuesMap>  Targets       { get; }

        /// <summary>
        /// The optional enumeration of resource objects representing end-devices or systems.
        /// </summary>
        [Optional]
        public IEnumerable<Resource>   Resources     { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new Virtual End Node.
        /// </summary>
        /// <param name="VenName">The name of the Virtual End Node, which is expected to be unique within the scope of a Virtual Top Node (VTN). An user generated identifier which may also be a VEN identifier provisioned out-of-band.</param>
        /// <param name="Attributes">An optional enumeration of valuesMap objects describing attributes.</param>
        /// <param name="Targets">An optional enumeration of valuesMap objects describing target criteria.</param>
        /// <param name="Resources">An optional enumeration of resource objects representing end-devices or systems.</param>
        /// 
        /// <param name="Id">The optional unique identification of this Virtual End Node.</param>
        /// <param name="Created">The optional date and time when this Virtual End Node was created.</param>
        /// <param name="LastModification">The optional date and time when this Virtual End Node was last modified.</param>
        public VirtualEndNode(String                   VenName,
                              IEnumerable<ValuesMap>?  Attributes         = null,
                              IEnumerable<ValuesMap>?  Targets            = null,
                              IEnumerable<Resource>?   Resources          = null,

                              Object_Id?               Id                 = null,
                              DateTimeOffset?          Created            = null,
                              DateTimeOffset?          LastModification   = null)

            : base(ObjectType.SUBSCRIPTION,
                   Id,
                   Created,
                   LastModification)

        {

            this.VenName     = VenName;
            this.Attributes  = Attributes?.Distinct() ?? [];
            this.Targets     = Targets?.   Distinct() ?? [];
            this.Resources   = Resources?. Distinct() ?? [];

            unchecked
            {

                hashCode = this.VenName.   GetHashCode()  * 7 +
                           this.Attributes.CalcHashCode() * 5 +
                           this.Targets.   CalcHashCode() * 3 +
                           this.Resources. CalcHashCode();

            }

        }

        #endregion


        #region Documentation

        // ven:
        //   type: object
        //   description: Ven represents a client with the ven role.
        //   required:
        //     - venName
        //   properties:
        //     id:
        //       $ref: '#/components/schemas/objectID'
        //       # VTN provisioned on object creation.
        //     createdDateTime:
        //       $ref: '#/components/schemas/dateTime'
        //       #  VTN provisioned on object creation.
        //     modificationDateTime:
        //       $ref: '#/components/schemas/dateTime'
        //       #  VTN provisioned on object modification.
        //     objectType:
        //       type: string
        //       description: Used as discriminator.
        //       enum: [VEN]
        //       # VTN provisioned on object creation.
        //     venName:
        //       type: string
        //       description: |
        //         User generated identifier, may be VEN identifier provisioned out-of-band.
        //         venName is expected to be unique within the scope of a VTN
        //       minLength: 1
        //       maxLength: 128
        //       example: VEN-999
        //     attributes:
        //       type: array
        //       description: A list of valuesMap objects describing attributes.
        //       items:
        //         $ref: '#/components/schemas/valuesMap'
        //       nullable: true
        //       default: null
        //     targets:
        //       type: array
        //       description: A list of valuesMap objects describing target criteria.
        //       items:
        //         $ref: '#/components/schemas/valuesMap'
        //       nullable: true
        //       default: null
        //     resources:
        //       type: array
        //       description: A list of resource objects representing end-devices or systems.
        //       items:
        //         $ref: '#/components/schemas/resource'
        //       nullable: true
        //       default: null

        #endregion

        #region (static) TryParse(JSON, out VirtualEndNode, CustomVirtualEndNodeParser = null)

        // Note: The following is needed to satisfy pattern matching delegates! Do not refactor it!

        /// <summary>
        /// Try to parse the given JSON representation of a Virtual End Node.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="VirtualEndNode">The parsed Virtual End Node.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParse(JObject                                   JSON,
                                       [NotNullWhen(true)]  out VirtualEndNode?  VirtualEndNode,
                                       [NotNullWhen(false)] out String?          ErrorResponse)

            => TryParse(JSON,
                        out VirtualEndNode,
                        out ErrorResponse,
                        null);


        /// <summary>
        /// Try to parse the given JSON representation of a Virtual End Node.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="VirtualEndNode">The parsed Virtual End Node.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="CustomVirtualEndNodeParser">A delegate to parse custom Virtual End Nodes.</param>
        public static Boolean TryParse(JObject                                       JSON,
                                       [NotNullWhen(true)]  out VirtualEndNode?      VirtualEndNode,
                                       [NotNullWhen(false)] out String?              ErrorResponse,
                                       CustomJObjectParserDelegate<VirtualEndNode>?  CustomVirtualEndNodeParser)
        {

            try
            {

                VirtualEndNode = null;

                #region VenName             [mandatory]

                if (!JSON.ParseMandatoryText("venName",
                                             "ven name",
                                             out String? venName,
                                             out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region Attributes          [optional]

                if (JSON.ParseOptionalHashSet("attributes",
                                              "attributes",
                                              ValuesMap.TryParse,
                                              out HashSet<ValuesMap> attributes,
                                              out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region Targets             [optional]

                if (JSON.ParseOptionalHashSet("targets",
                                              "targets",
                                              ValuesMap.TryParse,
                                              out HashSet<ValuesMap> targets,
                                              out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region Resources           [optional]

                if (JSON.ParseOptionalHashSet("resources",
                                              "resources",
                                              Resource.TryParse,
                                              out HashSet<Resource> resources,
                                              out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion


                #region Id                  [optional]

                if (JSON.ParseOptional("id",
                                       "randomize start",
                                       Object_Id.TryParse,
                                       out Object_Id? id,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region Created             [optional]

                if (JSON.ParseOptional("createdDateTime",
                                       "randomize start",
                                       out DateTimeOffset? created,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion

                #region LastModification    [optional]

                if (JSON.ParseOptional("modificationDateTime",
                                       "randomize start",
                                       out DateTimeOffset? lastModification,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
                        return false;
                }

                #endregion


                VirtualEndNode = new VirtualEndNode(

                                     venName,
                                     attributes,
                                     targets,
                                     resources,

                                     id,
                                     created,
                                     lastModification

                                 );

                if (CustomVirtualEndNodeParser is not null)
                    VirtualEndNode = CustomVirtualEndNodeParser(JSON,
                                                                VirtualEndNode);

                return true;

            }
            catch (Exception e)
            {
                VirtualEndNode  = default;
                ErrorResponse   = "The given JSON representation of a Virtual End Node is invalid: " + e.Message;
                return false;
            }

        }

        #endregion

        #region ToJSON(CustomVirtualEndNodeSerializer = null, CustomValuesMapSerializer = null, ...)

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        /// <param name="CustomVirtualEndNodeSerializer">A delegate to serialize custom Virtual End Nodes.</param>
        /// <param name="CustomValuesMapSerializer">A delegate to serialize custom values maps.</param>
        /// <param name="CustomResourceSerializer">A delegate to serialize custom resource objects.</param>
        public JObject ToJSON(CustomJObjectSerializerDelegate<VirtualEndNode>? CustomVirtualEndNodeSerializer   = null,
                              CustomJObjectSerializerDelegate<ValuesMap>?      CustomValuesMapSerializer        = null,
                              CustomJObjectSerializerDelegate<Resource>?       CustomResourceSerializer         = null)
        {

            var json = JSONObject.Create(

                           Id.              HasValue
                               ? new JProperty("id",                     Id.                    ToString())
                               : null,

                           Created.         HasValue
                               ? new JProperty("createdDateTime",        Created.         Value.ToISO8601())
                               : null,

                           LastModification.HasValue
                               ? new JProperty("modificationDateTime",   LastModification.Value.ToISO8601())
                               : null,

                                 new JProperty("objectType",             ObjectType.            ToString()),


                                 new JProperty("venName",                VenName),

                            Attributes.Any()
                               ? new JProperty("attributes",             new JArray(Attributes.Select(attribute => attribute.ToJSON(CustomValuesMapSerializer))))
                               : null,

                            Targets.   Any()
                               ? new JProperty("targets",                new JArray(Targets.   Select(target    => target.   ToJSON(CustomValuesMapSerializer))))
                               : null,

                            Resources. Any()
                               ? new JProperty("resources",              new JArray(Resources. Select(resource  => resource. ToJSON(CustomResourceSerializer))))
                               : null

                       );

            return CustomVirtualEndNodeSerializer is not null
                       ? CustomVirtualEndNodeSerializer(this, json)
                       : json;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this Virtual End Node.
        /// </summary>
        public VirtualEndNode Clone()

            => new (

                   VenName.   CloneString(),
                   Attributes.Select(attribute => attribute.Clone()),
                   Targets.   Select(taraget   => taraget.  Clone()),
                   Resources. Select(resource  => resource. Clone()),

                   Id?.       Clone(),
                   Created,
                   LastModification

                );

        #endregion


        #region Operator overloading

        #region Operator == (VirtualEndNode1, VirtualEndNode2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VirtualEndNode1">A Virtual End Node.</param>
        /// <param name="VirtualEndNode2">Another Virtual End Node.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (VirtualEndNode? VirtualEndNode1,
                                           VirtualEndNode? VirtualEndNode2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(VirtualEndNode1, VirtualEndNode2))
                return true;

            // If one is null, but not both, return false.
            if (VirtualEndNode1 is null || VirtualEndNode2 is null)
                return false;

            return VirtualEndNode1.Equals(VirtualEndNode2);

        }

        #endregion

        #region Operator != (VirtualEndNode1, VirtualEndNode2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="VirtualEndNode1">A Virtual End Node.</param>
        /// <param name="VirtualEndNode2">Another Virtual End Node.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (VirtualEndNode? VirtualEndNode1,
                                           VirtualEndNode? VirtualEndNode2)

            => !(VirtualEndNode1 == VirtualEndNode2);

        #endregion

        #endregion

        #region IEquatable<VirtualEndNode> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two Virtual End Nodes for equality.
        /// </summary>
        /// <param name="Object">A Virtual End Node to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is VirtualEndNode virtualEndNode &&
                   Equals(virtualEndNode);

        #endregion

        #region Equals(VirtualEndNode)

        /// <summary>
        /// Compares two Virtual End Nodes for equality.
        /// </summary>
        /// <param name="VirtualEndNode">A Virtual End Node to compare with.</param>
        public Boolean Equals(VirtualEndNode? VirtualEndNode)

            => VirtualEndNode is not null &&

               VenName.           Equals       (VirtualEndNode.VenName)            &&
               Attributes.Order().SequenceEqual(VirtualEndNode.Attributes.Order()) &&
               Targets.   Order().SequenceEqual(VirtualEndNode.Targets.   Order()) &&
               Resources. Order().SequenceEqual(VirtualEndNode.Resources. Order()) &&

            ((!Id.              HasValue && !VirtualEndNode.Id.              HasValue) ||
              (Id.              HasValue &&  VirtualEndNode.Id.              HasValue && Id.              Value.Equals(VirtualEndNode.Id.              Value))) &&

            ((!Created.         HasValue && !VirtualEndNode.Created.         HasValue) ||
              (Created.         HasValue &&  VirtualEndNode.Created.         HasValue && Created.         Value.Equals(VirtualEndNode.Created.         Value))) &&

            ((!LastModification.HasValue && !VirtualEndNode.LastModification.HasValue) ||
              (LastModification.HasValue &&  VirtualEndNode.LastModification.HasValue && LastModification.Value.Equals(VirtualEndNode.LastModification.Value)));

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

                   CommonInfo.Length > 0
                       ? $"{CommonInfo.AggregateWith(", ")}, "
                       : "",

                   $"'{VenName}'",

                   Attributes.Any()
                       ? $" with '{Attributes.AggregateWith(", ")}'"
                       : "",

                   Targets.   Any()
                       ? $" at '{Targets.AggregateWith(", ")}'"
                       : "",

                   Resources. Any()
                       ? $" for '{Resources.AggregateWith(", ")}'"
                       : ""

               );

        #endregion

    }

}
