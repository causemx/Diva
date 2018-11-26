using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diva.EnergyConsumption
{
    public class PowerModel
    {
        private static readonly string PowerModelRootPath =
            AppDomain.CurrentDomain.BaseDirectory + "Power Models\\";
        private static Dictionary<string, PowerModel> pmAvailable =
            new Dictionary<string, PowerModel>();
        public static readonly PowerModel pmNone =
            new PowerModel(Properties.Strings.MsgDroneSettingNoPowerModel);

        public static bool IsValidModelName(string name) =>
            !new System.Text.RegularExpressions.Regex("[^A-Za-z0-9_ ]").IsMatch(name);

        public static void RefreshPowerModelsList()
        {
            try
            {
                var di = new DirectoryInfo(PowerModelRootPath);
                if (!di.Exists) di.Create();
                di.GetDirectories().ToList().ForEach(d => CreateModel(d));
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
            pmAvailable.ContainsKey(name) ? pmAvailable[name] : pmNone;

        public static List<string> GetPowerModelNames() =>
            new List<string>(pmAvailable.Keys) { Properties.Strings.MsgDroneSettingNoPowerModel };

        private static bool CreateModel(DirectoryInfo dir)
        {
            bool ok = false;
            if (IsValidModelName(dir.Name))
            {
                // more checks here, or throw exception if dir does not contains valid power model
                pmAvailable.Add(dir.Name, new PowerModel(dir.Name));
                ok = true;
            }
            return ok;
        }

        public string ModelName { get; private set; }
        private string ModelPath => PowerModelRootPath + ModelName;

        private PowerModel(string name) { ModelName = name; }

        public double CalculateEnergyConsumption(List<Utilities.Locationwp> wps) =>
            throw new NotImplementedException();
    }
}
