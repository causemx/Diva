﻿using System;
using System.Collections.Generic;
using System.Linq;
using Diva.Mavlink;
using Diva.Utilities;
using PointLatLng = GMap.NET.PointLatLng;

namespace Diva.Mission
{
    enum FlyToState
    {
        Setting,
        Flying,
        Reached,
        Canceled,
    }

    class FlyTo : IDisposable
    {
        public const double AngleTolerance = 0.5;
        public const double DistanceTolerance = 5.0;
        public static readonly TimeSpan TimeTolerance = new TimeSpan(0, 0, 30);
        public const int TrackUpdatePeriodMS = 5000;

        private static List<FlyTo> flyingTargets = new List<FlyTo>();
        public static void DropFlight(MavDrone drone)
            => flyingTargets.FindAll(f => f.Drone == drone || f.TrackTarget == drone).ForEach(f => f.Dispose());
        public static void DisposeAll()
        {
            var flytos = flyingTargets;
            flyingTargets = null;
            flytos.ForEach(f => f.Dispose());
        }
        public static bool UpdateAltitude(MavDrone drone)
        {
            bool found = false;
            flyingTargets.Any(f =>
            {
                if (f.Drone == drone)
                {
                    f.UpdateAltitude();
                    found = true;
                }
                return found;
            });
            return found;
        }

        public PointLatLng To => marker.To;
        public PointLatLng From => marker.From;
        public MavDrone Drone { get; private set; }
        public FlyToState State { get; private set; }
        public bool ShowReachedMessage { get; set; } = true;
        public bool TrackMode { get; private set; }

        public event EventHandler DestinationReached;

        public bool Reached => (State == FlyToState.Reached) ||
            (Drone.Status.Location.DistanceTo(To) < DistanceTolerance);

        private DestinationMarker marker;

        public FlyTo(MavDrone drone, bool isTracker = false)
        {
            Drone = drone;
            marker = new DestinationMarker(Drone.Status.Location, isTracker);
            State = FlyToState.Setting;
        }

        public bool SetDestination(PointLatLng dest)
        {
            if (!TrackMode && State == FlyToState.Setting)
            {
                marker.To = dest;
                return true;
            }
            return false;
        }

        private void RegisterDroneFlight()
        {
            flyingTargets.FirstOrDefault(f => f.Drone == Drone)?.Dispose();
            flyingTargets.Add(this);
            State = FlyToState.Flying;
            Planner.GetPlannerInstance().BackgroundTimer += DetectDroneStatus;
        }

        public bool Start()
        {
            if (State != FlyToState.Setting && State != FlyToState.Canceled
                || !Drone.Status.IsArmed
                || Drone.Status.State != MAVLink.MAV_STATE.ACTIVE)
                return false;
            if (!Drone.SetGuidedModeWP(new WayPoint
            {
                Id = (ushort)MAVLink.MAV_CMD.WAYPOINT,
                Altitude = AltitudeControl.TargetAltitudes[Drone],
                Latitude = To.Lat,
                Longitude = To.Lng,
                Frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT
            }))
            {
                Drone.SetMode("BRAKE");
                System.Windows.Forms.MessageBox.Show(Properties.Strings.MsgFlyToTargetNotProperlySet);
                Dispose();
                return false;
            }
            lastPosTime = DateTime.Now;
            lastPos = Drone.Status.Location;
            RegisterDroneFlight();
            return true;
        }

        public bool UpdateAltitude()
        {
            if (State != FlyToState.Flying || TrackMode ||
                    Drone.Status.FlightMode != FlightMode.GUIDED)
                return false;
            return Drone.SetGuidedModeWP(new WayPoint
            {
                Id = (ushort)MAVLink.MAV_CMD.WAYPOINT,
                Altitude = AltitudeControl.GetActualTarget(Drone),
                Latitude = To.Lat,
                Longitude = To.Lng,
                Frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT
            }, false);
        }

        public MavDrone TrackTarget { get; private set; }
        public double TrackDistance { get; private set; }
        public double TrackBearing { get; private set; }
        private DateTime trackUpdateTime;
        private PointLatLng TrackLocation =>
            TrackDistance > 0.01 ? TrackTarget.Status.Location
                .OffsetAngleDistance(TrackBearing, TrackDistance) :
            TrackTarget.Status.Location;
        public event EventHandler<bool> TrackUpdate;
        public event EventHandler TrackStopped;

        private bool CheckTrackUpdate()
        {
            var now = DateTime.Now;
            if (now < trackUpdateTime)
                return false;
            trackUpdateTime = now.AddMilliseconds(TrackUpdatePeriodMS);
            return true;
        }

        public bool StartTracking(MavDrone target, double distance, double bearing)
        {
            TrackMode = true;
            TrackTarget = target;
            TrackDistance = distance;
            TrackBearing = bearing;
            trackUpdateTime = DateTime.Now.AddMilliseconds(TrackUpdatePeriodMS);
            marker.To = TrackLocation;
            if (!Drone.SetGuidedModeWP(new WayPoint
            {
                Id = (ushort)MAVLink.MAV_CMD.WAYPOINT,
                Altitude = AltitudeControl.TargetAltitudes[Drone],
                Latitude = To.Lat,
                Longitude = To.Lng,
                Frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT
            }))
            {
                Drone.SetMode("BRAKE");
                System.Windows.Forms.MessageBox.Show(Properties.Strings.MsgFlyToTargetNotProperlySet);
                Dispose();
                return false;
            }
            RegisterDroneFlight();
            TrackUpdate?.Invoke(this, false);
            return true;
        }

        public bool Stop()
        {
            if (State != FlyToState.Flying) return false;
            State = FlyToState.Canceled;
            Drone.SetMode("BRAKE");
            marker.SetBrakeMode(true);
            return true;
        }

        public bool Resume()
        {
            if (State != FlyToState.Canceled) return false;
            Drone.SetMode("GUIDED");
            lastPosTime = DateTime.Now;
            lastPos = Drone.Status.Location;
            State = FlyToState.Flying;
            marker.SetBrakeMode(false);
            return true;
        }

        private DateTime lastPosTime;
        private PointLatLng lastPos;

        private void DetectDroneStatus(object o, EventArgs e)
        {
            try
            {
                var p = o as Planner;
                if (State == FlyToState.Flying &&
                        Drone.Status.FlightMode == FlightMode.GUIDED &&
                        Drone.Status.State == MAVLink.MAV_STATE.ACTIVE
                    || State == FlyToState.Canceled &&
                        Drone.Status.FlightMode == FlightMode.BRAKE &&
                        Drone.Status.State == MAVLink.MAV_STATE.ACTIVE)
                {
                    if (TrackMode)
                    {
                        if (CheckTrackUpdate())
                        {
                            var pos = TrackLocation;
                            marker.To = pos;
                            Drone.SetGuidedModeWP(new WayPoint
                            {
                                Id = (ushort)MAVLink.MAV_CMD.WAYPOINT,
                                Altitude = AltitudeControl.TargetAltitudes[Drone],
                                Latitude = pos.Lat,
                                Longitude = pos.Lng,
                                Frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT
                            }, false);
                        }
                        marker.From = Drone.Status.Location;
                        TrackUpdate?.Invoke(this, Reached);
                        return;
                    }
                    else if (!Reached)
                    {
                        var pos = Drone.Status.Location;
                        if (State == FlyToState.Flying &&
                            pos.DistanceTo(lastPos) < DistanceTolerance)
                        {
                            var now = DateTime.Now;
                            if (now - lastPosTime > TimeTolerance)
                            {
                                p.BackgroundTimer -= DetectDroneStatus;
                                FloatMessage.NewMessage(
                                    Drone.Name,
                                    (int)MAVLink.MAV_SEVERITY.WARNING,
                                    "FlyTo command applied but drone is not moving.");
                                Dispose();
                                return;
                            }
                        }
                        marker.From = pos;
                        return;
                    }
                    State = FlyToState.Reached;
                    p.BackgroundTimer -= DetectDroneStatus;
                    if (ShowReachedMessage)
                        FloatMessage.NewMessage(
                            Drone.Name,
                            (int)MAVLink.MAV_SEVERITY.INFO,
                            "FlyTo destination reached.");
                    DestinationReached?.Invoke(this, null);
                }
                else
                {
                    p.BackgroundTimer -= DetectDroneStatus;
                    if (ShowReachedMessage)
                        FloatMessage.NewMessage(
                            Drone.Name,
                            (int)MAVLink.MAV_SEVERITY.NOTICE,
                            "FlyTo canceled.");
                }
                Dispose();
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }

        public void Dispose()
        {
            flyingTargets?.Remove(this);
            Planner.GetPlannerInstance().BackgroundTimer -= DetectDroneStatus;
            marker?.Dispose();
            marker = null;
            if (TrackMode)
                TrackStopped?.Invoke(this, null);
        }
    }
}
