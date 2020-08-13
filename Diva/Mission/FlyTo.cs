using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    class FlyTo
    {
        public const double AngleTolerance = 0.5;
        public const double DistanceTolerance = 5.0;

        public PointLatLng To { get; private set; }
        public PointLatLng From { get; private set; }
        public MavDrone Drone { get; private set; }
        public FlyToState State { get; private set; }
        public bool ShowReachedMessage { get; set; } = true;

        public event EventHandler DestinationReached;

        public bool Reached => (State == FlyToState.Reached) ||
            (new PointLatLngAlt(Drone.Status.Latitude, Drone.Status.Longitude)
                .GetDistance(To) < DistanceTolerance);

        public FlyTo(MavDrone drone)
        {
            Drone = drone;
            From = To = new PointLatLng(Drone.Status.Latitude, Drone.Status.Longitude);
            State = FlyToState.Setting;
        }

        public bool SetDestination(PointLatLng dest)
        {
            if (State == FlyToState.Setting)
            {
                To = dest;
                return true;
            }
            return false;
        }

        public bool Start()
        {
            if (State != FlyToState.Setting && State != FlyToState.Canceled
                || !Drone.Status.IsArmed
                || Drone.Status.State != MAVLink.MAV_STATE.ACTIVE)
                return false;
            State = FlyToState.Flying;
            Drone.SetGuidedModeWP(new WayPoint
            {
                Id = (ushort)MAVLink.MAV_CMD.WAYPOINT,
                Altitude = Drone.Status.Altitude, // back to m
                Latitude = To.Lat,
                Longitude = To.Lng,
                Frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT
            });
            Planner.GetPlannerInstance().BackgroundTimer += DetectDroneStatus;
            return true;
        }

        public bool Stop()
        {
            if (State != FlyToState.Flying) return false;
            Drone.SetMode("BRAKE");
            return true;
        }

        private void DetectDroneStatus(object o, EventArgs e)
        {
            var p = o as Planner;
            if (State == FlyToState.Flying &&
                Drone.Status.FlightMode == FlightMode.GUIDED &&
                Drone.Status.State == MAVLink.MAV_STATE.ACTIVE)
            {
                if (!Reached)
                    return;
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
        }
    }
}
