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

    // ~/programs

    #region OnGetProgramsRequest/-Response

    /// <summary>
    /// A delegate called whenever a GET ~/programs HTTP request will be send.
    /// </summary>
    public delegate Task OnGetProgramsRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                      OpenADRClient                         Sender,
                                                      EventTracking_Id                      EventTrackingId,
                                                      TimeSpan                              RequestTimeout,
                                                      CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/programs HTTP request had been received.
    /// </summary>
    public delegate Task OnGetProgramsResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                       OpenADRClient                         Sender,
                                                       EventTracking_Id                      EventTrackingId,
                                                       TimeSpan                              RequestTimeout,
                                                       IEnumerable<Program>                  Response,
                                                       TimeSpan                              Runtime,
                                                       CancellationToken?                    CancellationToken);

    #endregion

    #region OnPostProgramsRequest/-Response

    /// <summary>
    /// A delegate called whenever a POST ~/programs HTTP request will be send.
    /// </summary>
    public delegate Task OnPostProgramsRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                       OpenADRClient                         Sender,
                                                       EventTracking_Id                      EventTrackingId,
                                                       TimeSpan                              RequestTimeout,
                                                       CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a POST ~/programs HTTP request had been received.
    /// </summary>
    public delegate Task OnPostProgramsResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                        OpenADRClient                         Sender,
                                                        Program                               Program,
                                                        EventTracking_Id                      EventTrackingId,
                                                        TimeSpan                              RequestTimeout,
                                                        IEnumerable<Program>                  Response,
                                                        TimeSpan                              Runtime,
                                                        CancellationToken?                    CancellationToken);

    #endregion


    #region OnGetProgramRequest/-Response

    /// <summary>
    /// A delegate called whenever a GET ~/programs/{programId} HTTP request will be send.
    /// </summary>
    public delegate Task OnGetProgramRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                     OpenADRClient                         Sender,
                                                     Program_Id                            ProgramId,
                                                     EventTracking_Id                      EventTrackingId,
                                                     TimeSpan                              RequestTimeout,
                                                     CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/programs/{programId} HTTP request had been received.
    /// </summary>
    public delegate Task OnGetProgramResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                      OpenADRClient                         Sender,
                                                      Program_Id                            ProgramId,
                                                      EventTracking_Id                      EventTrackingId,
                                                      TimeSpan                              RequestTimeout,
                                                      Program                               Response,
                                                      TimeSpan                              Runtime,
                                                      CancellationToken?                    CancellationToken);

    #endregion

    #region OnPutProgramRequest/-Response

    /// <summary>
    /// A delegate called whenever a PUT ~/programs/{programId} HTTP request will be send.
    /// </summary>
    public delegate Task OnPutProgramRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                     OpenADRClient                         Sender,
                                                     EventTracking_Id                      EventTrackingId,
                                                     TimeSpan                              RequestTimeout,
                                                     CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a PUT ~/programs/{programId} HTTP request had been received.
    /// </summary>
    public delegate Task OnPutProgramResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                      OpenADRClient                         Sender,
                                                      EventTracking_Id                      EventTrackingId,
                                                      TimeSpan                              RequestTimeout,
                                                      //IEnumerable<Program>                  Response,
                                                      TimeSpan                              Runtime,
                                                      CancellationToken?                    CancellationToken);

    #endregion

    #region OnDeleteProgramRequest/-Response

    /// <summary>
    /// A delegate called whenever a DELETE ~/programs/{programId} HTTP request will be send.
    /// </summary>
    public delegate Task OnDeleteProgramRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                        OpenADRClient                         Sender,
                                                        Program_Id                            ProgramId,
                                                        EventTracking_Id                      EventTrackingId,
                                                        TimeSpan                              RequestTimeout,
                                                        CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a DELETE ~/programs/{programId} HTTP request had been received.
    /// </summary>
    public delegate Task OnDeleteProgramResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                         OpenADRClient                         Sender,
                                                         Program_Id                            ProgramId,
                                                         EventTracking_Id                      EventTrackingId,
                                                         TimeSpan                              RequestTimeout,
                                                         //IEnumerable<Program>                  Response,
                                                         TimeSpan                              Runtime,
                                                         CancellationToken?                    CancellationToken);

    #endregion


    // ~/reports

    #region OnGetReportsRequest/-Response

    /// <summary>
    /// A delegate called whenever a GET ~/reports HTTP request will be send.
    /// </summary>
    public delegate Task OnGetReportsRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                     OpenADRClient                         Sender,
                                                     EventTracking_Id                      EventTrackingId,
                                                     TimeSpan                              RequestTimeout,
                                                     CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/reports HTTP request had been received.
    /// </summary>
    public delegate Task OnGetReportsResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                      OpenADRClient                         Sender,
                                                      EventTracking_Id                      EventTrackingId,
                                                      TimeSpan                              RequestTimeout,
                                                      IEnumerable<Report>                   Response,
                                                      TimeSpan                              Runtime,
                                                      CancellationToken?                    CancellationToken);

    #endregion

    #region OnPostReportsRequest/-Response

    /// <summary>
    /// A delegate called whenever a POST ~/reports HTTP request will be send.
    /// </summary>
    public delegate Task OnPostReportsRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                      OpenADRClient                         Sender,
                                                      EventTracking_Id                      EventTrackingId,
                                                      TimeSpan                              RequestTimeout,
                                                      CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a POST ~/reports HTTP request had been received.
    /// </summary>
    public delegate Task OnPostReportsResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                       OpenADRClient                         Sender,
                                                       Report                                Report,
                                                       EventTracking_Id                      EventTrackingId,
                                                       TimeSpan                              RequestTimeout,
                                                       IEnumerable<Report>                   Response,
                                                       TimeSpan                              Runtime,
                                                       CancellationToken?                    CancellationToken);

    #endregion


    #region OnGetReportRequest/-Response

    /// <summary>
    /// A delegate called whenever a GET ~/reports/{reportId} HTTP request will be send.
    /// </summary>
    public delegate Task OnGetReportRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                    OpenADRClient                         Sender,
                                                    Report_Id                             ReportId,
                                                    EventTracking_Id                      EventTrackingId,
                                                    TimeSpan                              RequestTimeout,
                                                    CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/reports/{reportId} HTTP request had been received.
    /// </summary>
    public delegate Task OnGetReportResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                     OpenADRClient                         Sender,
                                                     Report_Id                             ReportId,
                                                     EventTracking_Id                      EventTrackingId,
                                                     TimeSpan                              RequestTimeout,
                                                     Report                                Response,
                                                     TimeSpan                              Runtime,
                                                     CancellationToken?                    CancellationToken);

    #endregion

    #region OnPutReportRequest/-Response

    /// <summary>
    /// A delegate called whenever a PUT ~/reports/{reportId} HTTP request will be send.
    /// </summary>
    public delegate Task OnPutReportRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                    OpenADRClient                         Sender,
                                                    EventTracking_Id                      EventTrackingId,
                                                    TimeSpan                              RequestTimeout,
                                                    CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a PUT ~/reports/{reportId} HTTP request had been received.
    /// </summary>
    public delegate Task OnPutReportResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                     OpenADRClient                         Sender,
                                                     EventTracking_Id                      EventTrackingId,
                                                     TimeSpan                              RequestTimeout,
                                                     //IEnumerable<Report>                   Response,
                                                     TimeSpan                              Runtime,
                                                     CancellationToken?                    CancellationToken);

    #endregion

    #region OnDeleteReportRequest/-Response

    /// <summary>
    /// A delegate called whenever a DELETE ~/reports/{reportId} HTTP request will be send.
    /// </summary>
    public delegate Task OnDeleteReportRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                       OpenADRClient                         Sender,
                                                       Report_Id                             ReportId,
                                                       EventTracking_Id                      EventTrackingId,
                                                       TimeSpan                              RequestTimeout,
                                                       CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a DELETE ~/reports/{reportId} HTTP request had been received.
    /// </summary>
    public delegate Task OnDeleteReportResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                        OpenADRClient                         Sender,
                                                        Report_Id                             ReportId,
                                                        EventTracking_Id                      EventTrackingId,
                                                        TimeSpan                              RequestTimeout,
                                                        //IEnumerable<Report>                   Response,
                                                        TimeSpan                              Runtime,
                                                        CancellationToken?                    CancellationToken);

    #endregion


    // ~/events

    #region OnGetEventsRequest/-Response

    /// <summary>
    /// A delegate called whenever a GET ~/events HTTP request will be send.
    /// </summary>
    public delegate Task OnGetEventsRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                    OpenADRClient                         Sender,
                                                    EventTracking_Id                      EventTrackingId,
                                                    TimeSpan                              RequestTimeout,
                                                    CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/events HTTP request had been received.
    /// </summary>
    public delegate Task OnGetEventsResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                     OpenADRClient                         Sender,
                                                     EventTracking_Id                      EventTrackingId,
                                                     TimeSpan                              RequestTimeout,
                                                     IEnumerable<Event>                    Response,
                                                     TimeSpan                              Runtime,
                                                     CancellationToken?                    CancellationToken);

    #endregion

    #region OnPostEventsRequest/-Response

    /// <summary>
    /// A delegate called whenever a POST ~/events HTTP request will be send.
    /// </summary>
    public delegate Task OnPostEventsRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                     OpenADRClient                         Sender,
                                                     EventTracking_Id                      EventTrackingId,
                                                     TimeSpan                              RequestTimeout,
                                                     CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a POST ~/events HTTP request had been received.
    /// </summary>
    public delegate Task OnPostEventsResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                      OpenADRClient                         Sender,
                                                      Event                                 Event,
                                                      EventTracking_Id                      EventTrackingId,
                                                      TimeSpan                              RequestTimeout,
                                                      IEnumerable<Event>                    Response,
                                                      TimeSpan                              Runtime,
                                                      CancellationToken?                    CancellationToken);

    #endregion


    #region OnGetEventRequest/-Response

    /// <summary>
    /// A delegate called whenever a GET ~/events/{eventId} HTTP request will be send.
    /// </summary>
    public delegate Task OnGetEventRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                   OpenADRClient                         Sender,
                                                   Event_Id                              EventId,
                                                   EventTracking_Id                      EventTrackingId,
                                                   TimeSpan                              RequestTimeout,
                                                   CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/events/{eventId} HTTP request had been received.
    /// </summary>
    public delegate Task OnGetEventResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                    OpenADRClient                         Sender,
                                                    Event_Id                              EventId,
                                                    EventTracking_Id                      EventTrackingId,
                                                    TimeSpan                              RequestTimeout,
                                                    Event                                 Response,
                                                    TimeSpan                              Runtime,
                                                    CancellationToken?                    CancellationToken);

    #endregion

    #region OnPutEventRequest/-Response

    /// <summary>
    /// A delegate called whenever a PUT ~/events/{eventId} HTTP request will be send.
    /// </summary>
    public delegate Task OnPutEventRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                   OpenADRClient                         Sender,
                                                   EventTracking_Id                      EventTrackingId,
                                                   TimeSpan                              RequestTimeout,
                                                   CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a PUT ~/events/{eventId} HTTP request had been received.
    /// </summary>
    public delegate Task OnPutEventResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                    OpenADRClient                         Sender,
                                                    EventTracking_Id                      EventTrackingId,
                                                    TimeSpan                              RequestTimeout,
                                                    //IEnumerable<Event>                    Response,
                                                    TimeSpan                              Runtime,
                                                    CancellationToken?                    CancellationToken);

    #endregion

    #region OnDeleteEventRequest/-Response

    /// <summary>
    /// A delegate called whenever a DELETE ~/events/{eventId} HTTP request will be send.
    /// </summary>
    public delegate Task OnDeleteEventRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                      OpenADRClient                         Sender,
                                                      Event_Id                              EventId,
                                                      EventTracking_Id                      EventTrackingId,
                                                      TimeSpan                              RequestTimeout,
                                                      CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a DELETE ~/events/{eventId} HTTP request had been received.
    /// </summary>
    public delegate Task OnDeleteEventResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                       OpenADRClient                         Sender,
                                                       Event_Id                              EventId,
                                                       EventTracking_Id                      EventTrackingId,
                                                       TimeSpan                              RequestTimeout,
                                                       //IEnumerable<Event>                    Response,
                                                       TimeSpan                              Runtime,
                                                       CancellationToken?                    CancellationToken);

    #endregion


    // ~/subscriptions

    #region OnGetSubscriptionsRequest/-Response

    /// <summary>
    /// A delegate called whenever a GET ~/subscriptions HTTP request will be send.
    /// </summary>
    public delegate Task OnGetSubscriptionsRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                           OpenADRClient                         Sender,
                                                           EventTracking_Id                      EventTrackingId,
                                                           TimeSpan                              RequestTimeout,
                                                           CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/subscriptions HTTP request had been received.
    /// </summary>
    public delegate Task OnGetSubscriptionsResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                            OpenADRClient                         Sender,
                                                            EventTracking_Id                      EventTrackingId,
                                                            TimeSpan                              RequestTimeout,
                                                            IEnumerable<Subscription>             Response,
                                                            TimeSpan                              Runtime,
                                                            CancellationToken?                    CancellationToken);

    #endregion

    #region OnPostSubscriptionsRequest/-Response

    /// <summary>
    /// A delegate called whenever a POST ~/subscriptions HTTP request will be send.
    /// </summary>
    public delegate Task OnPostSubscriptionsRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                            OpenADRClient                         Sender,
                                                            EventTracking_Id                      EventTrackingId,
                                                            TimeSpan                              RequestTimeout,
                                                            CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a POST ~/subscriptions HTTP request had been received.
    /// </summary>
    public delegate Task OnPostSubscriptionsResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                             OpenADRClient                         Sender,
                                                             Subscription                          Subscription,
                                                             EventTracking_Id                      EventTrackingId,
                                                             TimeSpan                              RequestTimeout,
                                                             IEnumerable<Subscription>             Response,
                                                             TimeSpan                              Runtime,
                                                             CancellationToken?                    CancellationToken);

    #endregion


    #region OnGetSubscriptionRequest/-Response

    /// <summary>
    /// A delegate called whenever a GET ~/subscriptions/{subscriptionId} HTTP request will be send.
    /// </summary>
    public delegate Task OnGetSubscriptionRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                          OpenADRClient                         Sender,
                                                          Subscription_Id                       SubscriptionId,
                                                          EventTracking_Id                      EventTrackingId,
                                                          TimeSpan                              RequestTimeout,
                                                          CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/subscriptions/{subscriptionId} HTTP request had been received.
    /// </summary>
    public delegate Task OnGetSubscriptionResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                           OpenADRClient                         Sender,
                                                           Subscription_Id                       SubscriptionId,
                                                           EventTracking_Id                      EventTrackingId,
                                                           TimeSpan                              RequestTimeout,
                                                           Subscription                          Response,
                                                           TimeSpan                              Runtime,
                                                           CancellationToken?                    CancellationToken);

    #endregion

    #region OnPutSubscriptionRequest/-Response

    /// <summary>
    /// A delegate called whenever a PUT ~/subscriptions/{subscriptionId} HTTP request will be send.
    /// </summary>
    public delegate Task OnPutSubscriptionRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                          OpenADRClient                         Sender,
                                                          EventTracking_Id                      EventTrackingId,
                                                          TimeSpan                              RequestTimeout,
                                                          CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a PUT ~/subscriptions/{subscriptionId} HTTP request had been received.
    /// </summary>
    public delegate Task OnPutSubscriptionResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                           OpenADRClient                         Sender,
                                                           EventTracking_Id                      EventTrackingId,
                                                           TimeSpan                              RequestTimeout,
                                                           //IEnumerable<Subscription>                  Response,
                                                           TimeSpan                              Runtime,
                                                           CancellationToken?                    CancellationToken);

    #endregion

    #region OnDeleteSubscriptionRequest/-Response

    /// <summary>
    /// A delegate called whenever a DELETE ~/subscriptions/{subscriptionId} HTTP request will be send.
    /// </summary>
    public delegate Task OnDeleteSubscriptionRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                             OpenADRClient                         Sender,
                                                             Subscription_Id                       SubscriptionId,
                                                             EventTracking_Id                      EventTrackingId,
                                                             TimeSpan                              RequestTimeout,
                                                             CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a DELETE ~/subscriptions/{subscriptionId} HTTP request had been received.
    /// </summary>
    public delegate Task OnDeleteSubscriptionResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                              OpenADRClient                         Sender,
                                                              Subscription_Id                       SubscriptionId,
                                                              EventTracking_Id                      EventTrackingId,
                                                              TimeSpan                              RequestTimeout,
                                                              //IEnumerable<Subscription>                  Response,
                                                              TimeSpan                              Runtime,
                                                              CancellationToken?                    CancellationToken);

    #endregion


    // ~/vens

    #region OnGetVENsRequest/-Response

    /// <summary>
    /// A delegate called whenever a GET ~/vens HTTP request will be send.
    /// </summary>
    public delegate Task OnGetVENsRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                  OpenADRClient                         Sender,
                                                  EventTracking_Id                      EventTrackingId,
                                                  TimeSpan                              RequestTimeout,
                                                  CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/vens HTTP request had been received.
    /// </summary>
    public delegate Task OnGetVENsResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                   OpenADRClient                         Sender,
                                                   EventTracking_Id                      EventTrackingId,
                                                   TimeSpan                              RequestTimeout,
                                                   IEnumerable<VirtualEndNode>           Response,
                                                   TimeSpan                              Runtime,
                                                   CancellationToken?                    CancellationToken);

    #endregion

    #region OnPostVENsRequest/-Response

    /// <summary>
    /// A delegate called whenever a POST ~/vens HTTP request will be send.
    /// </summary>
    public delegate Task OnPostVENsRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                   OpenADRClient                         Sender,
                                                   EventTracking_Id                      EventTrackingId,
                                                   TimeSpan                              RequestTimeout,
                                                   CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a POST ~/vens HTTP request had been received.
    /// </summary>
    public delegate Task OnPostVENsResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                    OpenADRClient                         Sender,
                                                    VirtualEndNode                        VirtualEndNode,
                                                    EventTracking_Id                      EventTrackingId,
                                                    TimeSpan                              RequestTimeout,
                                                    IEnumerable<VirtualEndNode>           Response,
                                                    TimeSpan                              Runtime,
                                                    CancellationToken?                    CancellationToken);

    #endregion


    #region OnGetVENRequest/-Response

    /// <summary>
    /// A delegate called whenever a GET ~/vens/{venId} HTTP request will be send.
    /// </summary>
    public delegate Task OnGetVENRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                 OpenADRClient                         Sender,
                                                 VirtualEndNode_Id                     VirtualEndNodeId,
                                                 EventTracking_Id                      EventTrackingId,
                                                 TimeSpan                              RequestTimeout,
                                                 CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/vens/{venId} HTTP request had been received.
    /// </summary>
    public delegate Task OnGetVENResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                  OpenADRClient                         Sender,
                                                  VirtualEndNode_Id                     VirtualEndNodeId,
                                                  EventTracking_Id                      EventTrackingId,
                                                  TimeSpan                              RequestTimeout,
                                                  VirtualEndNode                        Response,
                                                  TimeSpan                              Runtime,
                                                  CancellationToken?                    CancellationToken);

    #endregion

    #region OnPutVENRequest/-Response

    /// <summary>
    /// A delegate called whenever a PUT ~/vens/{venId} HTTP request will be send.
    /// </summary>
    public delegate Task OnPutVENRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                 OpenADRClient                         Sender,
                                                 EventTracking_Id                      EventTrackingId,
                                                 TimeSpan                              RequestTimeout,
                                                 CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a PUT ~/vens/{venId} HTTP request had been received.
    /// </summary>
    public delegate Task OnPutVENResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                  OpenADRClient                         Sender,
                                                  EventTracking_Id                      EventTrackingId,
                                                  TimeSpan                              RequestTimeout,
                                                  //IEnumerable<VEN>                      Response,
                                                  TimeSpan                              Runtime,
                                                  CancellationToken?                    CancellationToken);

    #endregion

    #region OnDeleteVENRequest/-Response

    /// <summary>
    /// A delegate called whenever a DELETE ~/vens/{venId} HTTP request will be send.
    /// </summary>
    public delegate Task OnDeleteVENRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                    OpenADRClient                         Sender,
                                                    VirtualEndNode_Id                     VirtualEndNodeId,
                                                    EventTracking_Id                      EventTrackingId,
                                                    TimeSpan                              RequestTimeout,
                                                    CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a DELETE ~/vens/{venId} HTTP request had been received.
    /// </summary>
    public delegate Task OnDeleteVENResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                     OpenADRClient                         Sender,
                                                     VirtualEndNode_Id                     VirtualEndNodeId,
                                                     EventTracking_Id                      EventTrackingId,
                                                     TimeSpan                              RequestTimeout,
                                                     //IEnumerable<VEN>                  Response,
                                                     TimeSpan                              Runtime,
                                                     CancellationToken?                    CancellationToken);

    #endregion


    // ~/vens/{venId}/resources

    #region OnGetVENResourcesRequest/-Response

    /// <summary>
    /// A delegate called whenever a GET ~/vens/{venId}/resources HTTP request will be send.
    /// </summary>
    public delegate Task OnGetVENResourcesRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                          OpenADRClient                         Sender,
                                                          EventTracking_Id                      EventTrackingId,
                                                          TimeSpan                              RequestTimeout,
                                                          CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/vens/{venId}/resources HTTP request had been received.
    /// </summary>
    public delegate Task OnGetVENResourcesResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                           OpenADRClient                         Sender,
                                                           EventTracking_Id                      EventTrackingId,
                                                           TimeSpan                              RequestTimeout,
                                                           IEnumerable<VirtualEndNode>           Response,
                                                           TimeSpan                              Runtime,
                                                           CancellationToken?                    CancellationToken);

    #endregion

    #region OnPostVENResourcesRequest/-Response

    /// <summary>
    /// A delegate called whenever a POST ~/vens/{venId}/resources HTTP request will be send.
    /// </summary>
    public delegate Task OnPostVENResourcesRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                           OpenADRClient                         Sender,
                                                           EventTracking_Id                      EventTrackingId,
                                                           TimeSpan                              RequestTimeout,
                                                           CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a POST ~/vens/{venId}/resources HTTP request had been received.
    /// </summary>
    public delegate Task OnPostVENResourcesResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                            OpenADRClient                         Sender,
                                                            VirtualEndNode                        VirtualEndNode,
                                                            EventTracking_Id                      EventTrackingId,
                                                            TimeSpan                              RequestTimeout,
                                                            IEnumerable<Resource>                 Response,
                                                            TimeSpan                              Runtime,
                                                            CancellationToken?                    CancellationToken);

    #endregion


    #region OnGetVENResourceRequest/-Response

    /// <summary>
    /// A delegate called whenever a GET ~/vens/{venId}/resources/{resourceId} HTTP request will be send.
    /// </summary>
    public delegate Task OnGetVENResourceRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                         OpenADRClient                         Sender,
                                                         VirtualEndNode_Id                     VirtualEndNodeId,
                                                         EventTracking_Id                      EventTrackingId,
                                                         TimeSpan                              RequestTimeout,
                                                         CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/vens/{venId}/resources/{resourceId} HTTP request had been received.
    /// </summary>
    public delegate Task OnGetVENResourceResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                          OpenADRClient                         Sender,
                                                          VirtualEndNode_Id                     VirtualEndNodeId,
                                                          EventTracking_Id                      EventTrackingId,
                                                          TimeSpan                              RequestTimeout,
                                                          VirtualEndNode                        Response,
                                                          TimeSpan                              Runtime,
                                                          CancellationToken?                    CancellationToken);

    #endregion

    #region OnPutVENResourceRequest/-Response

    /// <summary>
    /// A delegate called whenever a PUT ~/vens/{venId}/resources/{resourceId} HTTP request will be send.
    /// </summary>
    public delegate Task OnPutVENResourceRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                         OpenADRClient                         Sender,
                                                         EventTracking_Id                      EventTrackingId,
                                                         TimeSpan                              RequestTimeout,
                                                         CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a PUT ~/vens/{venId}/resources/{resourceId} HTTP request had been received.
    /// </summary>
    public delegate Task OnPutVENResourceResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                          OpenADRClient                         Sender,
                                                          EventTracking_Id                      EventTrackingId,
                                                          TimeSpan                              RequestTimeout,
                                                          //IEnumerable<VENResource>                      Response,
                                                          TimeSpan                              Runtime,
                                                          CancellationToken?                    CancellationToken);

    #endregion

    #region OnDeleteVENResourceRequest/-Response

    /// <summary>
    /// A delegate called whenever a DELETE ~/vens/{venId}/resources/{resourceId} HTTP request will be send.
    /// </summary>
    public delegate Task OnDeleteVENResourceRequestDelegate(DateTimeOffset                        LogTimestamp,
                                                            OpenADRClient                         Sender,
                                                            VirtualEndNode_Id                     VirtualEndNodeId,
                                                            EventTracking_Id                      EventTrackingId,
                                                            TimeSpan                              RequestTimeout,
                                                            CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a DELETE ~/vens/{venId}/resources/{resourceId} HTTP request had been received.
    /// </summary>
    public delegate Task OnDeleteVENResourceResponseDelegate(DateTimeOffset                        LogTimestamp,
                                                             OpenADRClient                         Sender,
                                                             VirtualEndNode_Id                     VirtualEndNodeId,
                                                             EventTracking_Id                      EventTrackingId,
                                                             TimeSpan                              RequestTimeout,
                                                             //IEnumerable<VENResource>                  Response,
                                                             TimeSpan                              Runtime,
                                                             CancellationToken?                    CancellationToken);

    #endregion


}
