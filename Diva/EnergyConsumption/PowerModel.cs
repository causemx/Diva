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

        public string ModelName { get; protected set; }
        public string ModelPath => PowerModelRootPath + ModelName;

        public PowerModel()
        {
            ModelName = Properties.Strings.MsgDroneSettingNoPowerModel;
        }

        public Task CalculateEnergyConsumptionBackground(MavDrone drone, List<WayPoint> wps, WayPoint home, Action<double, object> cb, object token)
        {
            if (GetType() == typeof(PowerModel))
                throw new NotImplementedException();
            Task task = Task.Run(() => cb(CalculateEnergyConsumption(drone, wps, home), token));
            return task;
        }

        public static bool ContainsModel(string path) => false;

        public virtual double CalculateEnergyConsumption(MavDrone drone, List<WayPoint> wps, WayPoint home) => double.NaN;
    }

    public static class PowerModelManager
    {
        public static readonly Type[] AvailableModelTypes = { typeof(AlexModel) };
        private static Dictionary<string, PowerModel> pmAvailable =
            new Dictionary<string, PowerModel>();
        private static List<string> powerModelNames;
        public static List<string> GetPowerModelNames() => powerModelNames;
        public static PowerModel GetModel(string name) =>
            name != null && pmAvailable.ContainsKey(name) ? pmAvailable[name] : pmNone;

        private static readonly PowerModel pmNone = new PowerModel();
        public static PowerModel PowerModelNone => pmNone;

        public static bool IsValidModelName(string name)
        {
            return name.Length > 2 && char.IsLetterOrDigit(name, 0) &&
                !new System.Text.RegularExpressions.Regex("[^A-Za-z0-9\\._ ]").IsMatch(name);
        }

        private static bool LoadModel(DirectoryInfo dir, Dictionary<string, PowerModel> dict)
        {
            bool ok = false;
            string name = dir.Name;
            // reuse old object in case already referenced
            if (pmAvailable.ContainsKey(name))
            {
                dict.Add(name, pmAvailable[name]);
                ok = true;
            }
            else if (IsValidModelName(name))
            {
                // more checks here, or throw exception if dir does not contains valid power model
                var model = AvailableModelTypes.Where(t =>
                        (bool)t.GetMethod("ContainsModel").Invoke(null, new [] { dir.FullName }))
                    .FirstOrDefault();
                if (model != null)
                    dict.Add(dir.Name, model.GetConstructor(new Type[] { typeof(string) })
                        .Invoke(new [] { name }) as PowerModel);
                ok = true;
            }
            return ok;
        }

        static PowerModelManager() => RefreshPowerModelsList();

        public static void RefreshPowerModelsList()
        {
            var newdict = new Dictionary<string, PowerModel>();
            try
            {
                var di = new DirectoryInfo(PowerModel.PowerModelRootPath);
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

        public static List<WayPoint> GenerateTrainingMission<T>(double homelat, double homelng, double angle) =>
            (List<WayPoint>)typeof(T).GetMethod("GenerateTrainingMission")?.Invoke(null, new object[] { homelat, homelng, angle });

        public static PowerModel TrainNewModel<T>(string file, string name)
        {
            typeof(T).GetMethod("TrainNewModel")?.Invoke(null, new [] { file, name });
            RefreshPowerModelsList();
            return GetModel(name);
        }
    }
}
