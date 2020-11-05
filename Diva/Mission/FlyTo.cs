using System;
using System.Collections.Generic;
using System.Linq;
using Diva.Mavlink;
using Diva.Utilities;
using PointLatLng = GMap.NET.PointLatLng;
using TTMode = GMap.NET.WindowsForms.MarkerTooltipMode;

namespace Diva.Mission
{
    internal enum FlyToState
    {
        Setting,
        Flying,
        Reached,
        Canceled,
    }

    internal class FlyTo : IDisposable
    {
        public const double AngleTolerance = 0.5;
        public const double DistanceTolerance = 5.0;
        public static readonly TimeSpan TimeTolerance = new TimeSpan(0, 0, 30);
        public const int TrackUpdatePeriodMS = 5000;
        public const int ModeChangeDelayTolerance = 1000;

        private static List<FlyTo> flyingTargets = new List<FlyTo>();
        private static bool markerHandlerSet;

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
        public static FlyTo GetFlyToFrom(MavDrone drone)
            => flyingTargets.FirstOrDefault(f => f.Drone == drone);

        public PointLatLng To => marker.To;
        public PointLatLng From => marker.From;
        public MavDrone Drone { get; }
        public FlyToState State { get; private set; }
        public bool ShowReachedMessage { get; set; } = true;
        public bool TrackMode { get; private set; }

        public event EventHandler DestinationReached;

        public bool Reached => (State == FlyToState.Reached) ||
            (Drone.Status.Location.DistanceTo(To) < DistanceTolerance);

        private bool DisableNotify;
        private DestinationMarker marker;
        private uint previousMode;
        private DateTime modeChangeDue;

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
            var prev = flyingTargets.Find(f => f.Drone == Drone);
            if (prev != null)
            {
                prev.DisableNotify = true;
                prev.Dispose();
            }
            flyingTargets.Add(this);
            State = FlyToState.Flying;
            // Mode change may be delayed for planes
            previousMode = Drone.Status.FlightMode;
            modeChangeDue = DateTime.Now.AddMilliseconds(ModeChangeDelayTolerance);
            Planner.GetPlannerInstance().BackgroundTimer += DetectDroneStatus;
            if (!markerHandlerSet)
            {
                markerHandlerSet = true;
                Planner.GetPlannerInstance().GMapControl.OnMarkerClick += GMapControl_OnMarkerClick;
            }
        }

        private static void GMapControl_OnMarkerClick(GMap.NET.WindowsForms.GMapMarker item, System.Windows.Forms.MouseEventArgs e)
        {
            if (item is DestinationMarker m)
            {
                var f = flyingTargets.Find(t => t.marker == m);
                if (f != null)
                    if (f.State != FlyToState.Reached)
                        f.marker.ToolTipMode =
                            f.marker.ToolTipMode == TTMode.Always ?
                                TTMode.Never : TTMode.Always;
                    else
                        f.Dispose();
            }
        }

        public bool Start()
        {
            if ((State != FlyToState.Setting && State != FlyToState.Canceled)
                || !Drone.Status.IsArmed
                || (Drone.Status.State != MAVLink.MAV_STATE.ACTIVE
                    && Drone.Status.Firmware == Firmwares.ArduCopter2))
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
                Drone.SetMode(Drone.Status.FlightModeType.PauseMode);
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
            if (State != FlyToState.Flying || TrackMode || !Drone.IsMode("GUIDED"))
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
        public event EventHandler Destroyed;

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
                Drone.SetMode(Drone.Status.FlightModeType.PauseMode);
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
            if (Drone.Status.Firmware == Firmwares.ArduPlane)
                Drone.SetGuidedModeWP(new WayPoint
                {
                    Id = (ushort)MAVLink.MAV_CMD.WAYPOINT,
                    Altitude = AltitudeControl.TargetAltitudes[Drone],
                    Latitude = Drone.Status.Latitude,
                    Longitude = Drone.Status.Longitude,
                    Frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT
                });
            else
                Drone.SetMode(Drone.Status.FlightModeType.PauseMode);
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
                bool active = Drone.Status.State == MAVLink.MAV_STATE.ACTIVE;
                bool flying = State == FlyToState.Flying;
                bool canceled = State == FlyToState.Canceled &&
                    Drone.IsMode(Drone.Status.FlightModeType.PauseMode);

                // Mode change may be delayed for planes
                if (!Drone.IsMode("GUIDED"))
                    flying &= DateTime.Now < modeChangeDue
                        && previousMode == Drone.Status.FlightMode;

                if (active && (flying || canceled))
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
                    marker.SetReached();
                    marker.To = Drone.Status.Location;
                    p.BackgroundTimer -= DetectDroneStatus;
                    if (ShowReachedMessage)
                        FloatMessage.NewMessage(
                            Drone.Name,
                            (int)MAVLink.MAV_SEVERITY.INFO,
                            "FlyTo destination reached.");
                    DestinationReached?.Invoke(this, null);
                    // do not dispose on reached
                }
                else if (State != FlyToState.Reached)
                {
                    p.BackgroundTimer -= DetectDroneStatus;
                    if (ShowReachedMessage)
                        FloatMessage.NewMessage(
                            Drone.Name,
                            (int)MAVLink.MAV_SEVERITY.NOTICE,
                            "FlyTo canceled.");
                    Dispose();
                }
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
            if (!DisableNotify) Destroyed?.Invoke(this, null);
        }
    }
}
