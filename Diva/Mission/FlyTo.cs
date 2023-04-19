using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Diva.Controls;
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
        public const int ModeChangeDelayTolerance = 3000;

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
                if (f.Drone == drone &&
                    f.Drone.Status.Firmware == Firmwares.ArduPlane ||
                    f.State == FlyToState.Flying)
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
        public bool ValidTarget => State == FlyToState.Setting && marker.BeyondLoiterRadius;

        public event EventHandler DestinationReached;

        public bool Reached
        {
            get
            {
                if (State == FlyToState.Reached ||
                    Drone.Status.Location.DistanceTo(To) < DistanceTolerance)
                    return true;
                if (marker.BeyondLoiterRadius)
                    loiterStart = DateTime.Now;
                return DateTime.Now - loiterStart > TimeTolerance;
            }
        }

        private bool DisableNotify;
        private DestinationMarker marker;
        private uint previousMode;
        private DateTime modeChangeDue;
        private DateTime loiterStart;

        public FlyTo(MavDrone drone, bool isTracker = false)
        {
            Drone = drone;
            int radius = (int?)drone.Status.Params["WP_LOITER_RAD"]?.GetValue() ?? 0;
            marker = new DestinationMarker(Drone.Status.Location, isTracker)
            {
                LoiterRadius = radius,
                CloseAreaAlert = radius > 0 && !isTracker
            };
            State = FlyToState.Setting;
        }

        public bool SetDestination(PointLatLng dest)
        { 
            if (!TrackMode && State == FlyToState.Setting)
            {
                marker.From = Drone.Status.Location;
                marker.To = dest;
                marker.LoiterCirclePen = marker.BeyondLoiterRadius
                    ? DestinationMarker.NormalLoiterCirclePen
                    : DestinationMarker.AlertLoiterCircleColorPen;
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

        private static void GMapControl_OnMarkerClick(GMap.NET.WindowsForms.GMapMarker item, MouseEventArgs e)
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

        private float GetWPAltitude(float prefAlt = 0)
            => AltitudeControl.GetWPAltitude(Drone, prefAlt);

        private bool FlyToWithAltitude(float alt, bool setMode = true)
        {
            float currentAlt = AltitudeControl.TargetAltitudes[Drone];
            bool done = Drone.SetGuidedModeWP(new WayPoint
            {
                Id = (ushort)MAVLink.MAV_CMD.WAYPOINT,
                Altitude = alt,
                Latitude = To.Lat,
                Longitude = To.Lng,
                Frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT
            }, setMode);

            int mode = ConfigData.GetIntOption("GuidedWPAltMode");
            if (done && mode != 0 && (mode != 1 || Math.Abs(alt - currentAlt) > 0.05f))
                AltitudeControl.UpdateDroneTargetAltitude(Drone, alt);

            return done;
        }

        public bool Start()
        {
            marker.CloseAreaAlert = false;
            if ((State != FlyToState.Setting && State != FlyToState.Canceled)
                || !Drone.Status.IsArmed
                || (Drone.Status.State != MAVLink.MAV_STATE.ACTIVE))
            {
                return false;
            }
                    //&& Drone.Status.Firmware == Firmwares.ArduCopter2))
                
            if (!FlyToWithAltitude(GetWPAltitude()))
            {

                Drone.SetMode(Drone.Status.FlightModeType.PauseMode);
                MessageBox.Show(Properties.Strings.MsgFlyToTargetNotProperlySet);
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
            return FlyToWithAltitude(GetWPAltitude(AltitudeControl.GetActualTarget(Drone)), false);
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
            bool tooClose = !marker.ValidDestination(TrackTarget.Status.Location);
            marker.LoiterCirclePen = tooClose
                ? DestinationMarker.AlertLoiterCircleColorPen
                : DestinationMarker.NormalLoiterCirclePen;
            if (tooClose) return false;
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
            if (!FlyToWithAltitude(GetWPAltitude()))
            {
                Drone.SetMode(Drone.Status.FlightModeType.PauseMode);
                MessageBox.Show(Properties.Strings.MsgFlyToTargetNotProperlySet);
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
            if (Drone.Status.Firmware == Firmwares.ArduPlane)
                Drone.SetGuidedModeWP(new WayPoint
                {
                    Id = (ushort)MAVLink.MAV_CMD.WAYPOINT,
                    Altitude = GetWPAltitude(Drone.Status.Altitude),
                    Latitude = Drone.Status.Latitude,
                    Longitude = Drone.Status.Longitude,
                    Frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT
                });
            else
                Drone.SetMode(Drone.Status.FlightModeType.PauseMode);
            State = FlyToState.Canceled;
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

                // Mode change may be delayed for planes
                if (!Drone.IsMode("GUIDED"))
                    flying &= DateTime.Now < modeChangeDue
                        && previousMode == Drone.Status.FlightMode;

                marker.From = Drone.Status.Location;
                if (active && flying)
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
                                Altitude = GetWPAltitude(),
                                Latitude = pos.Lat,
                                Longitude = pos.Lng,
                                Frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT
                            }, false);
                        }
                        TrackUpdate?.Invoke(this, Reached);
                        return;
                    }
                    else if (!Reached)
                    {
                        if (lastPos != PointLatLng.Empty && State == FlyToState.Flying)
                        {
                            if (Drone.Status.Location.DistanceTo(lastPos) < DistanceTolerance)
                            {
                                if (DateTime.Now - lastPosTime > TimeTolerance)
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
                            else
                                lastPos = PointLatLng.Empty;
                        }
                        return;
                    }
                    State = FlyToState.Reached;
                    marker.SetReached();
                    //marker.To = Drone.Status.Location;
                    if (ShowReachedMessage)
                        FloatMessage.NewMessage(
                            Drone.Name,
                            (int)MAVLink.MAV_SEVERITY.INFO,
                            "FlyTo destination reached.");
                    // postpone copter's timer removal to have maker updated
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
                else if (marker.LoiterRadius == 0)
                {
                    p.BackgroundTimer -= DetectDroneStatus;
                    DestinationReached?.Invoke(this, null);
                }
                // do not dispose on reached
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
