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
    /// Extension methods for event identifications.
    /// </summary>
    public static class EventIdExtensions
    {

        /// <summary>
        /// Indicates whether this event identification is null or empty.
        /// </summary>
        /// <param name="EventId">An event identification.</param>
        public static Boolean IsNullOrEmpty(this Event_Id? EventId)
            => !EventId.HasValue || EventId.Value.IsNullOrEmpty;

        /// <summary>
        /// Indicates whether this event identification is null or empty.
        /// </summary>
        /// <param name="EventId">An event identification.</param>
        public static Boolean IsNotNullOrEmpty(this Event_Id? EventId)
            => EventId.HasValue && EventId.Value.IsNotNullOrEmpty;

    }


    /// <summary>
    /// The event identification.
    /// </summary>
    public readonly struct Event_Id : IId,
                                      IEquatable<Event_Id>,
                                      IComparable<Event_Id>
    {

        #region Data

        /// <summary>
        /// The numeric value of the event identification.
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
        /// The length of the event identification.
        /// </summary>
        public readonly UInt64 Length
            => (UInt64) Value.Length;

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new event identification based on the given text.
        /// </summary>
        /// <param name="Text">A text representation of an event identification.</param>
        private Event_Id(String Text)
        {
            this.Value = Text;
        }

        #endregion


        #region Documentation

        // eventID:
        //     type: string
        //     pattern: "^[a-zA-Z0-9_-]*$"
        //     minLength: 1
        //     maxLength: 128
        //     description: URL safe VTN assigned event ID.
        //     example: event-999

        #endregion

        #region (static) NewRandom

        /// <summary>
        /// Create a new random event identification.
        /// </summary>
        public static Event_Id NewRandom

            => new (RandomExtensions.RandomString(36));

        #endregion

        #region (static) Parse    (Text)

        /// <summary>
        /// Parse the given string as an event identification.
        /// </summary>
        /// <param name="Text">A text representation of an event identification.</param>
        public static Event_Id Parse(String Text)
        {

            if (TryParse(Text, out var eventId))
                return eventId;

            throw new ArgumentException($"Invalid text representation of an event identification: '{Text}'!",
                                        nameof(Text));

        }

        #endregion

        #region (static) TryParse (Text)

        /// <summary>
        /// Try to parse the given text as an event identification.
        /// </summary>
        /// <param name="Text">A text representation of an event identification.</param>
        public static Event_Id? TryParse(String Text)
        {

            if (TryParse(Text, out var eventId))
                return eventId;

            return null;

        }

        #endregion

        #region (static) TryParse (Text, out EventId)

        /// <summary>
        /// Try to parse the given text as an event identification.
        /// </summary>
        /// <param name="Text">A text representation of an event identification.</param>
        /// <param name="EventId">The parsed event identification.</param>
        public static Boolean TryParse(String                             Text,
                                       [NotNullWhen(true)] out Event_Id  EventId)
        {

            Text = Text.Trim();

            if (Text.IsNotNullOrEmpty())
            {
                EventId = new Event_Id(Text);
                return true;
            }

            EventId = default;
            return false;

        }

        #endregion

        #region Clone()

        /// <summary>
        /// Clone this event identification.
        /// </summary>
        public Event_Id Clone()

            => new (Value);

        #endregion


        #region Operator overloading

        #region Operator == (EventId1, EventId2)

        /// <summary>
        /// Compares two instances of this event.
        /// </summary>
        /// <param name="EventId1">An event identification.</param>
        /// <param name="EventId2">Another event identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator == (Event_Id EventId1,
                                           Event_Id EventId2)

            => EventId1.Equals(EventId2);

        #endregion

        #region Operator != (EventId1, EventId2)

        /// <summary>
        /// Compares two instances of this event.
        /// </summary>
        /// <param name="EventId1">An event identification.</param>
        /// <param name="EventId2">Another event identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator != (Event_Id EventId1,
                                           Event_Id EventId2)

            => !EventId1.Equals(EventId2);

        #endregion

        #region Operator <  (EventId1, EventId2)

        /// <summary>
        /// Compares two instances of this event.
        /// </summary>
        /// <param name="EventId1">An event identification.</param>
        /// <param name="EventId2">Another event identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator < (Event_Id EventId1,
                                          Event_Id EventId2)

            => EventId1.CompareTo(EventId2) < 0;

        #endregion

        #region Operator <= (EventId1, EventId2)

        /// <summary>
        /// Compares two instances of this event.
        /// </summary>
        /// <param name="EventId1">An event identification.</param>
        /// <param name="EventId2">Another event identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator <= (Event_Id EventId1,
                                           Event_Id EventId2)

            => EventId1.CompareTo(EventId2) <= 0;

        #endregion

        #region Operator >  (EventId1, EventId2)

        /// <summary>
        /// Compares two instances of this event.
        /// </summary>
        /// <param name="EventId1">An event identification.</param>
        /// <param name="EventId2">Another event identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator > (Event_Id EventId1,
                                          Event_Id EventId2)

            => EventId1.CompareTo(EventId2) > 0;

        #endregion

        #region Operator >= (EventId1, EventId2)

        /// <summary>
        /// Compares two instances of this event.
        /// </summary>
        /// <param name="EventId1">An event identification.</param>
        /// <param name="EventId2">Another event identification.</param>
        /// <returns>true|false</returns>
        public static Boolean operator >= (Event_Id EventId1,
                                           Event_Id EventId2)

            => EventId1.CompareTo(EventId2) >= 0;

        #endregion

        #endregion

        #region IComparable<EventId> Members

        #region CompareTo(Event)

        /// <summary>
        /// Compares two event identifications.
        /// </summary>
        /// <param name="Event">An event identification to compare with.</param>
        public Int32 CompareTo(Object? Event)

            => Event is Event_Id eventId
                   ? CompareTo(eventId)
                   : throw new ArgumentException("The given event is not an event identification!",
                                                 nameof(Event));

        #endregion

        #region CompareTo(EventId)

        /// <summary>
        /// Compares two event identifications.
        /// </summary>
        /// <param name="EventId">An event identification to compare with.</param>
        public Int32 CompareTo(Event_Id EventId)

            => String.Compare(Value,
                              EventId.Value,
                              StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region IEquatable<EventId> Members

        #region Equals(Event)

        /// <summary>
        /// Compares two event identifications for equality.
        /// </summary>
        /// <param name="Event">An event identification to compare with.</param>
        public override Boolean Equals(Object? Event)

            => Event is Event_Id eventId &&
                   Equals(eventId);

        #endregion

        #region Equals(EventId)

        /// <summary>
        /// Compares two event identifications for equality.
        /// </summary>
        /// <param name="EventId">An event identification to compare with.</param>
        public Boolean Equals(Event_Id EventId)

            => String.Equals(Value,
                             EventId.Value,
                             StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        #region (override) GetHashCode()

        /// <summary>
        /// Return the HashCode of this event.
        /// </summary>
        public override Int32 GetHashCode()

            => Value.GetHashCode();

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Return a text representation of this event.
        /// </summary>
        public override String ToString()

            => Value.ToString();

        #endregion

    }

}
