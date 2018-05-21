using System;
using System.Collections.Concurrent;
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
        private static readonly Lazy<DataManager> lazy = new Lazy<DataManager>(() => new DataManager());
        private static Configuration config;

        internal struct Options
        {
            public string version;
            public int salt;
            public string ToUnquotedJson()
            {
                return JsonConvert.SerializeObject(this).Replace("\"", "");
            }
            public static Options FromUnquotedJson(string jstr)
            {
                return JsonConvert.DeserializeObject<Options>(new System.Text.RegularExpressions.Regex("([\\w\\.]+)")
                        .Replace(jstr, "\"$1\""));
            }
        };
        private bool ready = false;
        public static bool Ready { get { return lazy.Value.ready || Load(); } }
        private ConcurrentDictionary<string, object> typeLists = new ConcurrentDictionary<string, object>();
        private Options options;

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
        }

        public static List<T> GetTypeList<T>()
        {
            return lazy.Value.typeLists.GetOrAdd(typeof(T).AssemblyQualifiedName, (k) => new List<T>()) as List<T>;
        }

        public static void UpdateList<T>(List<T> list)
        {
            lazy.Value.typeLists[typeof(T).AssemblyQualifiedName] = list;
        }

        public static void AddItem<T>(T item)
        {
            GetTypeList<T>().Add(item);
        }

        public static bool RemoveItem<T>(T item)
        {
            return GetTypeList<T>().Remove(item);
        }

        public static bool Load()
        {
            if (config == null) return false;
            if (config.AppSettings.Settings["divaOptions"] != null)
                lazy.Value.options = Options.FromUnquotedJson(config.AppSettings.Settings["divaOptions"].Value);

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
                    updateListMethod.Invoke(lazy.Value, new object[] { list });
                }
                else
                    Console.WriteLine($"Unable to resolve type '{s}'.");
            }
            return lazy.Value.ready = true;
        }

        public static void Reset()
        {
            /*foreach (var kv in typeLists)
            {
                var list = kv.Value as IDisposable;
                if (list != null)
                    list.Dispose();
            }*/
            lazy.Value.typeLists.Clear();
        }

        private static void writeSetting(string key, string value)
        {
            if (key == "divaOptions" && lazy.Value.options.salt != 0)
            {

            }
            if (config.AppSettings.Settings[key] == null)
                config.AppSettings.Settings.Add(key, value);
            else
                config.AppSettings.Settings[key].Value = value;
        }

        public static void Save()
        {
            writeSetting("divaOptions", lazy.Value.options.ToUnquotedJson());
            foreach (var kv in lazy.Value.typeLists)
            {
                writeSetting("dv" + Type.GetType(kv.Key).FullName,
                    JsonConvert.SerializeObject(kv.Value));
            }
            config.Save();
        }
    }
}
