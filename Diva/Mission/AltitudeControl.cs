using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diva.Mavlink;

namespace Diva.Mission
{
    class AltitudeControl
    {
        private static List<AltitudeControl> instances = new List<AltitudeControl>();
        private static bool initialized = false;

        private static void AltitudeControlMonitor(object o, EventArgs e)
        {
            lock (instances)
                Parallel.ForEach(instances, ac =>
                {
                    if (ac.IsTargeting)
                    {
                        var d = ac.Drone;
                        if (d == null || !d.IsMode("GUIDED")
                                || d.Status.State != MAVLink.MAV_STATE.ACTIVE)
                            ac.IsTargeting = false;
                    }
                });
        }

        public static float MaxAltitude = 120.0f;
        public static float MinAltitude = 10.0f;
        public static float AltitudeTolerance = 1.5f;

        public class AltitudeHelper
        {
            public float this[MavDrone drone]
            {
                get
                {
                    var ac = instances.Find(a => a.Drone == drone);
                    return ac == null ? drone?.Status.Altitude ?? 0 : ac.TargetAltitude;
                }
                set
                {
                    if (!initialized)
                    {
                        initialized = true;
                        Planner.GetPlannerInstance().BackgroundTimer += AltitudeControlMonitor;
                    }

                    var ac = instances.Find(a => a.Drone == drone);
                    if (ac == null)
                    {
                        lock (instances)
                            instances.Add(ac = new AltitudeControl(drone));
                    }
                    ac.TargetAltitude = value;
                }
            }
        }
        public static AltitudeHelper TargetAltitudes = new AltitudeHelper();
        public static float GetActualTarget(MavDrone drone)
        {
            var ac = instances.Find(a => a.Drone == drone);
            return ac == null ? drone.Status.Altitude : ac.targetAltitude;
        }

        public static void UpdateDroneTargetAltitude(MavDrone drone, float alt)
        {
            try
            {
                var ac = instances.FirstOrDefault(i => i.Drone == drone);
                if (ac == null)
                {
                    lock (instances)
                        instances.Add(ac = new AltitudeControl(drone));
                    ac.targetAltitude = alt;
                }
                else if (Math.Abs(ac.targetAltitude - alt) > 0.05f)
                    ac.targetAltitude = alt;
                ac.targetSet = true;
                if (drone == Planner.GetActiveDrone())
                {
                    Planner.GetPlannerInstance().AltitudeControlPanel.SetSource(drone);
                }
            }
            catch { }
        }

        public static bool Has(MavDrone drone)
            => instances.Any(a => a.Drone == drone);

        public static void Remove(MavDrone drone)
        {
            lock (instances) instances.RemoveAll(ac => ac.Drone == drone);
        }

        public static bool VerifyDroneStates(MavDrone active)
        {
            bool activeRemoved = false;
            lock (instances)
            {
                instances.ForEach(ac =>
                {
                    try
                    {
                        if (ac.Drone.IsMode("GUIDED")) return;
                        if (ac.Drone == active) activeRemoved = true;
                    }
                    catch { }
                    instances.Remove(ac);
                });
            }
            return activeRemoved;
        }

        public MavDrone Drone { get; }

        private float targetAltitude = 0;
        private bool targetSet = false;
        public float TargetAltitude
        {
            get => targetSet ? targetAltitude : Drone.Status.Altitude;
            set
            {
                targetSet = false;
                if (value > MaxAltitude)
                    targetAltitude = MaxAltitude;
                else if (value < MinAltitude)
                    targetAltitude = MinAltitude;
                else
                    targetAltitude = value;
                if (!FlyTo.UpdateAltitude(Drone))
                    Drone.SetGuidedModeWP(new WayPoint
                    {
                        Id = (ushort)MAVLink.MAV_CMD.WAYPOINT,
                        Altitude = targetAltitude,
                        Latitude = Drone.Status.Latitude,
                        Longitude = Drone.Status.Longitude,
                        Frame = MAVLink.MAV_FRAME.GLOBAL_RELATIVE_ALT
                    });
                targetSet = true;
            }
        }
        private bool IsTargeting
        {
            get => targetSet && targetAltitude != 0;
            set
            {
                if (value)
                    targetAltitude = TargetAltitude;
                targetSet = value;
            }
        }

        private AltitudeControl(MavDrone drone)
        {
            Drone = drone;
        }
    }
}
