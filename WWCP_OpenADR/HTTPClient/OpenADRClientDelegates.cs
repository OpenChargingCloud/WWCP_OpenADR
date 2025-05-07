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
    public delegate Task OnGetProgramsRequestDelegate(DateTime                              LogTimestamp,
                                                      OpenADRClient                         Sender,
                                                      EventTracking_Id                      EventTrackingId,
                                                      TimeSpan                              RequestTimeout,
                                                      CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/programs HTTP request had been received.
    /// </summary>
    public delegate Task OnGetProgramsResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnPostProgramsRequestDelegate(DateTime                              LogTimestamp,
                                                       OpenADRClient                         Sender,
                                                       EventTracking_Id                      EventTrackingId,
                                                       TimeSpan                              RequestTimeout,
                                                       CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a POST ~/programs HTTP request had been received.
    /// </summary>
    public delegate Task OnPostProgramsResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnGetProgramRequestDelegate(DateTime                              LogTimestamp,
                                                     OpenADRClient                         Sender,
                                                     Program_Id                            ProgramId,
                                                     EventTracking_Id                      EventTrackingId,
                                                     TimeSpan                              RequestTimeout,
                                                     CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/programs/{programId} HTTP request had been received.
    /// </summary>
    public delegate Task OnGetProgramResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnPutProgramRequestDelegate(DateTime                              LogTimestamp,
                                                     OpenADRClient                         Sender,
                                                     EventTracking_Id                      EventTrackingId,
                                                     TimeSpan                              RequestTimeout,
                                                     CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a PUT ~/programs/{programId} HTTP request had been received.
    /// </summary>
    public delegate Task OnPutProgramResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnDeleteProgramRequestDelegate(DateTime                              LogTimestamp,
                                                        OpenADRClient                         Sender,
                                                        Program_Id                            ProgramId,
                                                        EventTracking_Id                      EventTrackingId,
                                                        TimeSpan                              RequestTimeout,
                                                        CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a DELETE ~/programs/{programId} HTTP request had been received.
    /// </summary>
    public delegate Task OnDeleteProgramResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnGetReportsRequestDelegate(DateTime                              LogTimestamp,
                                                     OpenADRClient                         Sender,
                                                     EventTracking_Id                      EventTrackingId,
                                                     TimeSpan                              RequestTimeout,
                                                     CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/reports HTTP request had been received.
    /// </summary>
    public delegate Task OnGetReportsResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnPostReportsRequestDelegate(DateTime                              LogTimestamp,
                                                      OpenADRClient                         Sender,
                                                      EventTracking_Id                      EventTrackingId,
                                                      TimeSpan                              RequestTimeout,
                                                      CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a POST ~/reports HTTP request had been received.
    /// </summary>
    public delegate Task OnPostReportsResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnGetReportRequestDelegate(DateTime                              LogTimestamp,
                                                    OpenADRClient                         Sender,
                                                    Report_Id                             ReportId,
                                                    EventTracking_Id                      EventTrackingId,
                                                    TimeSpan                              RequestTimeout,
                                                    CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/reports/{reportId} HTTP request had been received.
    /// </summary>
    public delegate Task OnGetReportResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnPutReportRequestDelegate(DateTime                              LogTimestamp,
                                                    OpenADRClient                         Sender,
                                                    EventTracking_Id                      EventTrackingId,
                                                    TimeSpan                              RequestTimeout,
                                                    CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a PUT ~/reports/{reportId} HTTP request had been received.
    /// </summary>
    public delegate Task OnPutReportResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnDeleteReportRequestDelegate(DateTime                              LogTimestamp,
                                                       OpenADRClient                         Sender,
                                                       Report_Id                             ReportId,
                                                       EventTracking_Id                      EventTrackingId,
                                                       TimeSpan                              RequestTimeout,
                                                       CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a DELETE ~/reports/{reportId} HTTP request had been received.
    /// </summary>
    public delegate Task OnDeleteReportResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnGetEventsRequestDelegate(DateTime                              LogTimestamp,
                                                    OpenADRClient                         Sender,
                                                    EventTracking_Id                      EventTrackingId,
                                                    TimeSpan                              RequestTimeout,
                                                    CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/events HTTP request had been received.
    /// </summary>
    public delegate Task OnGetEventsResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnPostEventsRequestDelegate(DateTime                              LogTimestamp,
                                                     OpenADRClient                         Sender,
                                                     EventTracking_Id                      EventTrackingId,
                                                     TimeSpan                              RequestTimeout,
                                                     CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a POST ~/events HTTP request had been received.
    /// </summary>
    public delegate Task OnPostEventsResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnGetEventRequestDelegate(DateTime                              LogTimestamp,
                                                   OpenADRClient                         Sender,
                                                   Event_Id                              EventId,
                                                   EventTracking_Id                      EventTrackingId,
                                                   TimeSpan                              RequestTimeout,
                                                   CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/events/{eventId} HTTP request had been received.
    /// </summary>
    public delegate Task OnGetEventResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnPutEventRequestDelegate(DateTime                              LogTimestamp,
                                                   OpenADRClient                         Sender,
                                                   EventTracking_Id                      EventTrackingId,
                                                   TimeSpan                              RequestTimeout,
                                                   CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a PUT ~/events/{eventId} HTTP request had been received.
    /// </summary>
    public delegate Task OnPutEventResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnDeleteEventRequestDelegate(DateTime                              LogTimestamp,
                                                      OpenADRClient                         Sender,
                                                      Event_Id                              EventId,
                                                      EventTracking_Id                      EventTrackingId,
                                                      TimeSpan                              RequestTimeout,
                                                      CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a DELETE ~/events/{eventId} HTTP request had been received.
    /// </summary>
    public delegate Task OnDeleteEventResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnGetSubscriptionsRequestDelegate(DateTime                              LogTimestamp,
                                                           OpenADRClient                         Sender,
                                                           EventTracking_Id                      EventTrackingId,
                                                           TimeSpan                              RequestTimeout,
                                                           CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/subscriptions HTTP request had been received.
    /// </summary>
    public delegate Task OnGetSubscriptionsResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnPostSubscriptionsRequestDelegate(DateTime                              LogTimestamp,
                                                            OpenADRClient                         Sender,
                                                            EventTracking_Id                      EventTrackingId,
                                                            TimeSpan                              RequestTimeout,
                                                            CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a POST ~/subscriptions HTTP request had been received.
    /// </summary>
    public delegate Task OnPostSubscriptionsResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnGetSubscriptionRequestDelegate(DateTime                              LogTimestamp,
                                                          OpenADRClient                         Sender,
                                                          Subscription_Id                       SubscriptionId,
                                                          EventTracking_Id                      EventTrackingId,
                                                          TimeSpan                              RequestTimeout,
                                                          CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/subscriptions/{subscriptionId} HTTP request had been received.
    /// </summary>
    public delegate Task OnGetSubscriptionResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnPutSubscriptionRequestDelegate(DateTime                              LogTimestamp,
                                                          OpenADRClient                         Sender,
                                                          EventTracking_Id                      EventTrackingId,
                                                          TimeSpan                              RequestTimeout,
                                                          CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a PUT ~/subscriptions/{subscriptionId} HTTP request had been received.
    /// </summary>
    public delegate Task OnPutSubscriptionResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnDeleteSubscriptionRequestDelegate(DateTime                              LogTimestamp,
                                                             OpenADRClient                         Sender,
                                                             Subscription_Id                       SubscriptionId,
                                                             EventTracking_Id                      EventTrackingId,
                                                             TimeSpan                              RequestTimeout,
                                                             CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a DELETE ~/subscriptions/{subscriptionId} HTTP request had been received.
    /// </summary>
    public delegate Task OnDeleteSubscriptionResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnGetVENsRequestDelegate(DateTime                              LogTimestamp,
                                                  OpenADRClient                         Sender,
                                                  EventTracking_Id                      EventTrackingId,
                                                  TimeSpan                              RequestTimeout,
                                                  CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/vens HTTP request had been received.
    /// </summary>
    public delegate Task OnGetVENsResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnPostVENsRequestDelegate(DateTime                              LogTimestamp,
                                                   OpenADRClient                         Sender,
                                                   EventTracking_Id                      EventTrackingId,
                                                   TimeSpan                              RequestTimeout,
                                                   CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a POST ~/vens HTTP request had been received.
    /// </summary>
    public delegate Task OnPostVENsResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnGetVENRequestDelegate(DateTime                              LogTimestamp,
                                                 OpenADRClient                         Sender,
                                                 VirtualEndNode_Id                     VirtualEndNodeId,
                                                 EventTracking_Id                      EventTrackingId,
                                                 TimeSpan                              RequestTimeout,
                                                 CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/vens/{venId} HTTP request had been received.
    /// </summary>
    public delegate Task OnGetVENResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnPutVENRequestDelegate(DateTime                              LogTimestamp,
                                                 OpenADRClient                         Sender,
                                                 EventTracking_Id                      EventTrackingId,
                                                 TimeSpan                              RequestTimeout,
                                                 CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a PUT ~/vens/{venId} HTTP request had been received.
    /// </summary>
    public delegate Task OnPutVENResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnDeleteVENRequestDelegate(DateTime                              LogTimestamp,
                                                    OpenADRClient                         Sender,
                                                    VirtualEndNode_Id                     VirtualEndNodeId,
                                                    EventTracking_Id                      EventTrackingId,
                                                    TimeSpan                              RequestTimeout,
                                                    CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a DELETE ~/vens/{venId} HTTP request had been received.
    /// </summary>
    public delegate Task OnDeleteVENResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnGetVENResourcesRequestDelegate(DateTime                              LogTimestamp,
                                                          OpenADRClient                         Sender,
                                                          EventTracking_Id                      EventTrackingId,
                                                          TimeSpan                              RequestTimeout,
                                                          CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/vens/{venId}/resources HTTP request had been received.
    /// </summary>
    public delegate Task OnGetVENResourcesResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnPostVENResourcesRequestDelegate(DateTime                              LogTimestamp,
                                                           OpenADRClient                         Sender,
                                                           EventTracking_Id                      EventTrackingId,
                                                           TimeSpan                              RequestTimeout,
                                                           CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a POST ~/vens/{venId}/resources HTTP request had been received.
    /// </summary>
    public delegate Task OnPostVENResourcesResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnGetVENResourceRequestDelegate(DateTime                              LogTimestamp,
                                                         OpenADRClient                         Sender,
                                                         VirtualEndNode_Id                     VirtualEndNodeId,
                                                         EventTracking_Id                      EventTrackingId,
                                                         TimeSpan                              RequestTimeout,
                                                         CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/vens/{venId}/resources/{resourceId} HTTP request had been received.
    /// </summary>
    public delegate Task OnGetVENResourceResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnPutVENResourceRequestDelegate(DateTime                              LogTimestamp,
                                                         OpenADRClient                         Sender,
                                                         EventTracking_Id                      EventTrackingId,
                                                         TimeSpan                              RequestTimeout,
                                                         CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a PUT ~/vens/{venId}/resources/{resourceId} HTTP request had been received.
    /// </summary>
    public delegate Task OnPutVENResourceResponseDelegate(DateTime                              LogTimestamp,
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
    public delegate Task OnDeleteVENResourceRequestDelegate(DateTime                              LogTimestamp,
                                                            OpenADRClient                         Sender,
                                                            VirtualEndNode_Id                     VirtualEndNodeId,
                                                            EventTracking_Id                      EventTrackingId,
                                                            TimeSpan                              RequestTimeout,
                                                            CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a DELETE ~/vens/{venId}/resources/{resourceId} HTTP request had been received.
    /// </summary>
    public delegate Task OnDeleteVENResourceResponseDelegate(DateTime                              LogTimestamp,
                                                             OpenADRClient                         Sender,
                                                             VirtualEndNode_Id                     VirtualEndNodeId,
                                                             EventTracking_Id                      EventTrackingId,
                                                             TimeSpan                              RequestTimeout,
                                                             //IEnumerable<VENResource>                  Response,
                                                             TimeSpan                              Runtime,
                                                             CancellationToken?                    CancellationToken);

    #endregion


}
