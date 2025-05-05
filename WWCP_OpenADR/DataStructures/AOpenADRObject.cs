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
    /// The abstract base of all OpenADR top-level objects.
    /// </summary>
    public abstract class AOpenADRObject<T> : IOpenADRObject<T>
        where T: struct
    {

        #region Properties

        /// <summary>
        /// The type of this OpenADR object.
        /// </summary>
        [Mandatory]
        public ObjectType       ObjectType          { get; }

        /// <summary>
        /// The optional unique identification of this OpenADR object.
        /// </summary>
        [Optional]
        public T?               Id                  { get; }

        /// <summary>
        /// The optional unique identification of this OpenADR object.
        /// </summary>
        [Optional]
        Object_Id?              IOpenADRObject.Id
            => Id.HasValue
                   ? Object_Id.Parse(Id.Value.ToString() ?? "")
                   : null;

        /// <summary>
        /// The optional date and time when this OpenADR object was created.
        /// </summary>
        [Optional]
        public DateTimeOffset?  Created             { get; }

        /// <summary>
        /// The optional date and time when this OpenADR object was last modified.
        /// </summary>
        [Optional]
        public DateTimeOffset?  LastModification    { get; }

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new OpenADR object.
        /// </summary>
        /// <param name="ObjectType">The type of this OpenADR object.</param>
        /// <param name="Id">An optional unique identification of this OpenADR object.</param>
        /// <param name="Created">An optional date and time when this OpenADR object was created.</param>
        /// <param name="LastModification">An optional date and time when this OpenADR object was last modified.</param>
        public AOpenADRObject(ObjectType       ObjectType,
                              T?               Id                 = default,
                              DateTimeOffset?  Created            = null,
                              DateTimeOffset?  LastModification   = null)
        {

            this.ObjectType        = ObjectType;
            this.Id                = Id;
            this.Created           = Created;
            this.LastModification  = LastModification;

            unchecked
            {

                hashCode = this.ObjectType.       GetHashCode()       * 7 ^
                          (this.Id?.              GetHashCode() ?? 0) * 5 ^
                          (this.Created?.         GetHashCode() ?? 0) * 3 ^
                           this.LastModification?.GetHashCode() ?? 0;

            }

        }


        #endregion


        public static Boolean TryBaseParse<T>(JObject                       JSON,
                                              [NotNullWhen(true)]  T?       OpenADRObject,
                                              [NotNullWhen(false)] String?  ErrorResponse)

            where T: class, IOpenADRObject

        {

            OpenADRObject = null;
            ErrorResponse = null;

            if (typeof(T) == typeof(Program))
            {

                if (!Program.TryParse(JSON, out var program, out ErrorResponse))
                    return false;

                OpenADRObject = program as T;

            }

            return false;

        }


        public JObject ToJSON()
            => ToBaseJSON(null, null, null, null, null, null);

        public JObject ToBaseJSON(CustomJObjectSerializerDelegate<Program>?                  CustomProgramSerializer                   = null,
                                  CustomJObjectSerializerDelegate<IntervalPeriod>?           CustomIntervalPeriodSerializer            = null,
                                  CustomJObjectSerializerDelegate<EventPayloadDescriptor>?   CustomEventPayloadDescriptorSerializer    = null,
                                  CustomJObjectSerializerDelegate<ReportPayloadDescriptor>?  CustomReportPayloadDescriptorSerializer   = null,
                                  CustomJObjectSerializerDelegate<ValuesMap>?                CustomValuesMapSerializer                 = null,

                                  CustomJObjectSerializerDelegate<Resource>?   CustomResourceSerializer    = null
                                 // CustomJObjectSerializerDelegate<ValuesMap>?  CustomValuesMapSerializer   = null
            
            
            
            
            
            
            )
        {

            if (this is Program _program)
                return _program.       ToJSON(CustomProgramSerializer,
                                              CustomIntervalPeriodSerializer,
                                              CustomEventPayloadDescriptorSerializer,
                                              CustomReportPayloadDescriptorSerializer,
                                              CustomValuesMapSerializer);

            else if (this is Event _event)
                return _event.         ToJSON();

            else if (this is Report _report)
                return _report.        ToJSON();

            else if (this is Subscription _subscription)
                return _subscription.  ToJSON();

            else if (this is VirtualEndNode _virtualEndNode)
                return _virtualEndNode.ToJSON();

            else if (this is Resource _resource)
                return _resource.      ToJSON(CustomResourceSerializer,
                                              CustomValuesMapSerializer);

            return [];

        }


        IOpenADRObject IOpenADRObject.Clone()
            => Clone();

        public abstract IOpenADRObject<T> Clone();


        #region (override) GetHashCode()

        private readonly Int32 hashCode;

        /// <summary>
        /// Return the hash code of this object.
        /// </summary>
        public override Int32 GetHashCode()
            => hashCode;

        #endregion

        #region CommonInfo

        /// <summary>
        /// Return a text representation of this object.
        /// </summary>
        public String[] CommonInfo

            => [

                   Id.HasValue
                       ? $"Id: {Id.Value}"
                       : "",

                   Created.HasValue
                       ? $"created: {Created.Value.ToISO8601()}"
                       : "",

                   LastModification.HasValue
                       ? $"last modification: {LastModification.Value.ToISO8601()}"
                       : ""

               ];

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this object.
        /// </summary>
        public override String ToString()

            => CommonInfo.AggregateWith(", ");

        #endregion

    }

}
