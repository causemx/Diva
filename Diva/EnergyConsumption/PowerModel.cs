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
        static readonly string PowerModelRootPath =
            AppDomain.CurrentDomain.BaseDirectory + "Power Models\\";
        private static Dictionary<string, PowerModel> pmAvailable =
            new Dictionary<string, PowerModel>();
        private static readonly PowerModel pmNone =
            new PowerModel(Properties.Strings.MsgDroneSettingNoPowerModel);

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
                di.GetDirectories().ToList().ForEach(d => CreateModel(d, newdict));
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
            pmAvailable.ContainsKey(name) ? pmAvailable[name] : pmNone;

        private static List<string> powerModelNames;
        public static List<string> GetPowerModelNames() => powerModelNames;

        private static bool CreateModel(DirectoryInfo dir, Dictionary<string, PowerModel> dict)
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

        public string ModelName { get; private set; }
        private string ModelPath => PowerModelRootPath + ModelName;

        private PowerModel(string name) { ModelName = name; }

        public double CalculateEnergyConsumption(List<Utilities.Locationwp> wps) =>
            throw new NotImplementedException();
    }
}
