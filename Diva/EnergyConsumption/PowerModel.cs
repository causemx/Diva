using Diva.Utilities;
using Diva.Mavlink;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Diva.EnergyConsumption
{
    public class PowerModel
    {
        public static readonly string PowerModelRootPath =
            AppDomain.CurrentDomain.BaseDirectory + "Power Models\\";
        private static Dictionary<string, PowerModel> pmAvailable =
            new Dictionary<string, PowerModel>();
        private static readonly PowerModel pmNone = new PowerModel(null);

        public static bool IsValidModelName(string name)
        {
            return name.Length > 2 && char.IsLetterOrDigit(name, 0) &&
                !new System.Text.RegularExpressions.Regex("[^A-Za-z0-9\\._ ]").IsMatch(name);
        }

        public static void RefreshPowerModelsList()
        {
            var newdict = new Dictionary<string, PowerModel>();
            try
            {
                var di = new DirectoryInfo(PowerModelRootPath);
                if (!di.Exists) di.Create();
                di.GetDirectories().ToList().ForEach(d => LoadModel(d, newdict));
                pmAvailable = newdict;
                powerModelNames = new List<string>(pmAvailable.Keys)
                    { Properties.Strings.MsgDroneSettingNoPowerModel };
            }
            catch (Exception e)
            {
                Planner.log.Error("Failed to find power models: " + e);
            }
        }

        static PowerModel()
        {
            RefreshPowerModelsList();
            //GetPowerModelNames().ForEach(pm => Console.WriteLine(pm));
        }

        public static PowerModel PowerModelNone => pmNone;
        public static PowerModel GetModel(string name) =>
            name != null && pmAvailable.ContainsKey(name) ? pmAvailable[name] : pmNone;

        private static List<string> powerModelNames;
        public static List<string> GetPowerModelNames() => powerModelNames;

        private static bool LoadModel(DirectoryInfo dir, Dictionary<string, PowerModel> dict)
        {
            bool ok = false;
            // reuse old object in case already referenced
            if (pmAvailable.ContainsKey(dir.Name))
            {
                dict.Add(dir.Name, pmAvailable[dir.Name]);
                ok = true;
            } else if (IsValidModelName(dir.Name))
            {
                // more checks here, or throw exception if dir does not contains valid power model
                dict.Add(dir.Name, new PowerModel(dir.Name));
                ok = true;
            }
            return ok;
        }

        public static List<Locationwp> GenerateTrainingMission(Locationwp home) =>
            QGCWaypointFileUtlity.ImportWaypoints(
                (string)AlexModelTools.MissionGenerator.Start(
                    home.lat.ToString() + "," + home.lng.ToString(), null),
                out var newhome);

        public static PowerModel TrainNewModel(string file, string name)
        {
            AlexModelTools.Trainer.Start(file, name);
            RefreshPowerModelsList();
            return GetModel(name);
        }

        public string ModelName { get; private set; }
        public  string ModelPath => PowerModelRootPath + ModelName;
        public string ModelType { get; private set; }
        public object ModelObject { get; private set; }

        private PowerModel(string name)
        {
            if (name == null)
            {
                // PowerModelNone
                ModelName = Properties.Strings.MsgDroneSettingNoPowerModel;
                return;
            }
            ModelName = name;
            // do model type check here
            {
                ModelType = "Alex";
            }
        }

        public double CalculateEnergyConsumption(MavDrone drone, List<Locationwp> wps, Locationwp home)
        {
            if (this == PowerModelNone)
                throw new NotImplementedException();
            AlexModelTools.Predictor.Start(ModelName, null);
            return (double)AlexModelTools.Predictor.Start(ModelName, null);
        }

        public Task CalculateEnergyConsumptionBackground(MavDrone drone, List<Locationwp> wps, Locationwp home, Action<double> cb)
        {
            if (this == PowerModelNone)
                throw new NotImplementedException();
            Task task = Task.Run(() => cb(CalculateEnergyConsumption(drone, wps, home)));
            return task;
        }
    }
}
