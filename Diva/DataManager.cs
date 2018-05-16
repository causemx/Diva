using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;

namespace Diva
{
    class DataManager
    {
        private static readonly DataManager instance = new DataManager();
        public static DataManager Instance { get { return instance;  } }
        private static Configuration config;

        internal struct Options
        {
            public string version;
            public int salt;
            public string ToUnquotedJson()
            {
                return JsonConvert.SerializeObject(this).Replace("\"", "");
            }
            public void FromUnquotedJson(string jstr)
            {
                this = JsonConvert.DeserializeObject<Options>(new System.Text.RegularExpressions.Regex("([\\w\\.]+)")
                .Replace(jstr, "\"$1\""));
            }
        };
        bool ready = false;
        public static bool Ready { get { return instance.ready; } }
        private Dictionary<string, object> typeLists = new Dictionary<string, object>();
        Options options;

        private DataManager()
        {
            options.version = "0.0.0.0";
            options.salt = 0;
            try
            {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            } catch (Exception e)
            {
                Console.WriteLine("Error opening config file: " + e.Message);
            }
            ready = (config != null) && Load();
        }

        public List<T> GetTypeList<T>()
        {
            return typeLists[typeof(T).AssemblyQualifiedName] as List<T>;
        }

        public void UpdateList<T>(List<T> list)
        {
            typeLists[typeof(T).AssemblyQualifiedName] = list;
        }

        public void AddItem<T>(T item)
        {
            List<T> list = typeLists[typeof(T).AssemblyQualifiedName] as List<T>;
            if (list == null)
                list = new List<T>();
            list.Add(item);
        }

        public void RemoveItem<T>(T item)
        {
            List<T> list = typeLists[typeof(T).AssemblyQualifiedName] as List<T>;
            if (list != null)
            {
                list.Remove(item);
            }
        }

        public bool Load()
        {
            if (config.AppSettings.Settings["divaOptions"] != null)
                options.FromUnquotedJson(config.AppSettings.Settings["divaOptions"].Value);

            var asms = AppDomain.CurrentDomain.GetAssemblies();
            var settings = from k in config.AppSettings.Settings.AllKeys
                            where k.StartsWith("dv") select k.Substring(2);
            foreach (var s in settings)
            {
                Type t = Type.GetType(s);
                foreach (var a in asms)
                {
                    Console.WriteLine(a.GetName().FullName);
                    t = a.GetTypes().FirstOrDefault(tt => tt.FullName == s);
                    if (t != null)
                        break;
                }
                if (t != null)
                {
                    var typeOfList = typeof(List<>).MakeGenericType(t);
                    var deserializeMethod = typeof(JsonConvert).GetMethods()
                        .Single(m => m.Name == "DeserializeObject"
                            && m.IsStatic
                            && m.IsGenericMethodDefinition
                            && m.GetParameters().Length == 1
                            && m.GetParameters()[0].ParameterType == typeof(string)).MakeGenericMethod(typeOfList);
                    var list = deserializeMethod.Invoke(null, new object[] { config.AppSettings.Settings["dv" + s].Value });
                    var updateListMethod = typeof(DataManager).GetMethod("UpdateList").MakeGenericMethod(t);
                    updateListMethod.Invoke(this, new object[] { list });
                }
                else
                    Console.WriteLine($"Unable to resolve type '{s}'.");
            }
            return true;
        }

        public void New()
        {
            /*foreach (var kv in typeLists)
            {
                var typeList = Activator.CreateInstance(typeof(List<>)
                    .MakeGenericType(new Type[] { Type.GetType(kv.Key) }));
                var list = Convert.ChangeType(kv.Value, typeOfList);
                list.GetType().GetMethod("Clear").Invoke(list, null);
            }*/
            typeLists.Clear();
        }

        private static void writeSetting(string key, string value)
        {
            if (config.AppSettings.Settings[key] == null)
                config.AppSettings.Settings.Add(key, value);
            else
                config.AppSettings.Settings[key].Value = value;
        }

        public void Save()
        {
            writeSetting("divaOptions", options.ToUnquotedJson());
            foreach (var kv in typeLists)
            {
                writeSetting("dv" + Type.GetType(kv.Key).FullName,
                    JsonConvert.SerializeObject(kv.Value));
            }
            config.Save();
        }
    }
}
