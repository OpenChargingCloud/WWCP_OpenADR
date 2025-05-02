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
    /// A resource is an energy device or system subject to control
    /// by a Virtual End Node (VEN).
    /// </summary>
    public class Resource : AOpenADRObject
    {

        #region Properties

        /// <summary>
        /// The name of the resource, which is expected
        /// to be unique within the scope of a Virtual Top Node (VTN)
        /// An user generated identifier which may also be a
        /// VEN identifier provisioned out-of-band.
        /// </summary>
        [Mandatory]
        public String                  ResourceName        { get; }

        /// <summary>
        /// The optional identification of the Virtual End Node (VEN) this resource is associated with.
        /// May be provisioned by the Virtual End Node (VEN) on object creation based on the path,
        /// e.g. POST <>/ven/{venID}/resources.
        /// </summary>
        [Optional]
        public Object_Id?              VirtualEndNodeId    { get; }

        /// <summary>
        /// The optional enumeration of valuesMap objects describing attributes.
        /// </summary>
        [Optional]
        public IEnumerable<ValuesMap>  Attributes          { get; }

        /// <summary>
        /// The optional enumeration of valuesMap objects describing target criteria.
        /// </summary>
        [Optional]
        public IEnumerable<ValuesMap>  Targets             { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new resource.
        /// </summary>
        /// <param name="ResourceName">The name of the resource, which is expected to be unique within the scope of a Virtual Top Node (VTN). An user generated identifier which may also be a VEN identifier provisioned out-of-band.</param>
        /// <param name="VirtualEndNodeId">An optional identification of the Virtual End Node (VEN) this resource is associated with. May be provisioned by the Virtual End Node (VEN) on object creation based on the path, e.g. POST <>/ven/{venID}/resources.</param>
        /// <param name="Attributes">An optional enumeration of valuesMap objects describing attributes.</param>
        /// <param name="Targets">An optional enumeration of valuesMap objects describing target criteria.</param>
        /// 
        /// <param name="Id">The optional unique identification of this resource.</param>
        /// <param name="Created">The optional date and time when this resource was created.</param>
        /// <param name="LastModification">The optional date and time when this resource was last modified.</param>
        public Resource(String                        ResourceName,
                        Object_Id?                    VirtualEndNodeId   = null,
                        IEnumerable<ValuesMap>?       Attributes         = null,
                        IEnumerable<ValuesMap>?       Targets            = null,

                        Object_Id?                    Id                 = null,
                        DateTimeOffset?               Created            = null,
                        DateTimeOffset?               LastModification   = null)

            : base(ObjectType.SUBSCRIPTION,
                   Id,
                   Created,
                   LastModification)

        {

            this.ResourceName      = ResourceName;
            this.VirtualEndNodeId  = VirtualEndNodeId;
            this.Attributes        = Attributes?.Distinct() ?? [];
            this.Targets           = Targets?.   Distinct() ?? [];

            unchecked
            {

                hashCode = this.ResourceName.     GetHashCode()       * 7 +
                          (this.VirtualEndNodeId?.GetHashCode() ?? 0) * 5 +
                           this.Attributes.       CalcHashCode()      * 3 +
                           this.Targets.          CalcHashCode();

            }

        }

        #endregion


        #region Documentation

        // resource:
        //   type: object
        //   description: |
        //     A resource is an energy device or system subject to control by a VEN.
        //   required:
        //     - resourceName
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
        //       enum: [RESOURCE]
        //       # VTN provisioned on object creation.
        //     resourceName:
        //       type: string
        //       description: |
        //         User generated identifier, resource may be configured with identifier out-of-band.
        //         resourceName is expected to be unique within the scope of the associated VEN.
        //       minLength: 1
        //       maxLength: 128
        //       example: RESOURCE-999
        //     venID:
        //       $ref: '#/components/schemas/objectID'
        //       # VTN provisioned on object creation based on path, e.g. POST <>/ven/{venID}/resources.
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

        #endregion

        #region (static) TryParse(JSON, out Resource, CustomResourceParser = null)

        // Note: The following is needed to satisfy pattern matching delegates! Do not refactor it!

        /// <summary>
        /// Try to parse the given JSON representation of a resource.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="Resource">The parsed resource.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        public static Boolean TryParse(JObject                             JSON,
                                       [NotNullWhen(true)]  out Resource?  Resource,
                                       [NotNullWhen(false)] out String?    ErrorResponse)

            => TryParse(JSON,
                        out Resource,
                        out ErrorResponse,
                        null);


        /// <summary>
        /// Try to parse the given JSON representation of a resource.
        /// </summary>
        /// <param name="JSON">The JSON to be parsed.</param>
        /// <param name="Resource">The parsed resource.</param>
        /// <param name="ErrorResponse">An optional error response.</param>
        /// <param name="CustomResourceParser">A delegate to parse custom resources.</param>
        public static Boolean TryParse(JObject                                 JSON,
                                       [NotNullWhen(true)]  out Resource?      Resource,
                                       [NotNullWhen(false)] out String?        ErrorResponse,
                                       CustomJObjectParserDelegate<Resource>?  CustomResourceParser)
        {

            try
            {

                Resource = null;

                #region ResourceName        [mandatory]

                if (!JSON.ParseMandatoryText("resourceName",
                                             "resource name",
                                             out String? resourceName,
                                             out ErrorResponse))
                {
                    return false;
                }

                #endregion

                #region VirtualEndNodeId    [optional]

                if (JSON.ParseOptional("venID",
                                       "virtuel end node identification",
                                       Object_Id.TryParse,
                                       out Object_Id? virtuelEndNodeId,
                                       out ErrorResponse))
                {
                    if (ErrorResponse is not null)
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


                Resource = new Resource(

                               resourceName,
                               virtuelEndNodeId,
                               attributes,
                               targets,

                               id,
                               created,
                               lastModification

                           );

                if (CustomResourceParser is not null)
                    Resource = CustomResourceParser(JSON,
                                                    Resource);

                return true;

            }
            catch (Exception e)
            {
                Resource       = default;
                ErrorResponse  = "The given JSON representation of a resource is invalid: " + e.Message;
                return false;
            }

        }

        #endregion

        #region ToJSON(CustomResourceSerializer = null, CustomValuesMapSerializer = null, ...)

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        /// <param name="CustomResourceSerializer">A delegate to serialize custom resources.</param>
        /// <param name="CustomValuesMapSerializer">A delegate to serialize custom values maps.</param>
        public JObject ToJSON(CustomJObjectSerializerDelegate<Resource>?   CustomResourceSerializer    = null,
                              CustomJObjectSerializerDelegate<ValuesMap>?  CustomValuesMapSerializer   = null)
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


                                 new JProperty("resourceName",           ResourceName),
                                 new JProperty("venID",                  VirtualEndNodeId.      ToString()),

                            Attributes.Any()
                               ? new JProperty("attributes",             new JArray(Attributes.Select(attribute => attribute.ToJSON(CustomValuesMapSerializer))))
                               : null,

                            Targets.Any()
                               ? new JProperty("targets",                new JArray(Targets.   Select(target    => target.   ToJSON(CustomValuesMapSerializer))))
                               : null

                       );

            return CustomResourceSerializer is not null
                       ? CustomResourceSerializer(this, json)
                       : json;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this resource.
        /// </summary>
        public Resource Clone()

            => new (

                   ResourceName.      CloneString(),
                   VirtualEndNodeId?. Clone(),
                   Attributes.Select(attribute => attribute.Clone()),
                   Targets.   Select(taraget   => taraget.  Clone()),

                   Id?.               Clone(),
                   Created,
                   LastModification

                );

        #endregion


        #region Operator overloading

        #region Operator == (Resource1, Resource2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Resource1">A resource.</param>
        /// <param name="Resource2">Another resource.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Resource? Resource1,
                                           Resource? Resource2)
        {

            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(Resource1, Resource2))
                return true;

            // If one is null, but not both, return false.
            if (Resource1 is null || Resource2 is null)
                return false;

            return Resource1.Equals(Resource2);

        }

        #endregion

        #region Operator != (Resource1, Resource2)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Resource1">A resource.</param>
        /// <param name="Resource2">Another resource.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Resource? Resource1,
                                           Resource? Resource2)

            => !(Resource1 == Resource2);

        #endregion

        #endregion

        #region IEquatable<Resource> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two resources for equality.
        /// </summary>
        /// <param name="Object">A resource to compare with.</param>
        public override Boolean Equals(Object? Object)

            => Object is Resource resource &&
                   Equals(resource);

        #endregion

        #region Equals(Resource)

        /// <summary>
        /// Compares two resources for equality.
        /// </summary>
        /// <param name="Resource">A resource to compare with.</param>
        public Boolean Equals(Resource? Resource)

            => Resource is not null &&

               ResourceName.            Equals (Resource.ResourceName)       &&
               VirtualEndNodeId.        Equals (Resource.VirtualEndNodeId)   &&
               Attributes.Order().SequenceEqual(Resource.Attributes.Order()) &&
               Targets.   Order().SequenceEqual(Resource.Targets.   Order()) &&

            ((!Id.              HasValue && !Resource.Id.              HasValue) ||
              (Id.              HasValue &&  Resource.Id.              HasValue && Id.              Value.Equals(Resource.Id.              Value))) &&

            ((!Created.         HasValue && !Resource.Created.         HasValue) ||
              (Created.         HasValue &&  Resource.Created.         HasValue && Created.         Value.Equals(Resource.Created.         Value))) &&

            ((!LastModification.HasValue && !Resource.LastModification.HasValue) ||
              (LastModification.HasValue &&  Resource.LastModification.HasValue && LastModification.Value.Equals(Resource.LastModification.Value)));

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

                   $"'{ResourceName}' /  '{VirtualEndNodeId}'",

                   Attributes.Any()
                       ? $" with '{Attributes.AggregateWith(", ")}'"
                       : "",

                   Targets.Any()
                       ? $" at '{Targets.AggregateWith(", ")}'"
                       : ""

               );

        #endregion

    }

}
