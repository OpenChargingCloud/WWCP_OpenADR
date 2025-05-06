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
    /// A delegate called whenever a GET ~/program HTTP request will be send.
    /// </summary>
    public delegate Task OnGetProgramRequestDelegate(DateTime                              LogTimestamp,
                                                     OpenADRClient                         Sender,
                                                     Program_Id                            ProgramId,
                                                     EventTracking_Id                      EventTrackingId,
                                                     TimeSpan                              RequestTimeout,
                                                     CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a GET ~/program HTTP request had been received.
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
    /// A delegate called whenever a PUT ~/program HTTP request will be send.
    /// </summary>
    public delegate Task OnPutProgramRequestDelegate(DateTime                              LogTimestamp,
                                                     OpenADRClient                         Sender,
                                                     EventTracking_Id                      EventTrackingId,
                                                     TimeSpan                              RequestTimeout,
                                                     CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a PUT ~/program HTTP request had been received.
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
    /// A delegate called whenever a DELETE ~/program HTTP request will be send.
    /// </summary>
    public delegate Task OnDeleteProgramRequestDelegate(DateTime                              LogTimestamp,
                                                        OpenADRClient                         Sender,
                                                        Program_Id                            ProgramId,
                                                        EventTracking_Id                      EventTrackingId,
                                                        TimeSpan                              RequestTimeout,
                                                        CancellationToken?                    CancellationToken);

    /// <summary>
    /// A delegate called whenever a response to a DELETE ~/program HTTP request had been received.
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



}
