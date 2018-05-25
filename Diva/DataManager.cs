using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Diva
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    class UnquoteJson : Attribute { }

    class ConfigData
    {
        class DataCryptor
        {
            private static int AES_KEYSIZE = 128;
            private static int ITERATIONS = 1024;
            private static readonly byte[] cryptKey = Encoding.UTF8.GetBytes("Diva.GCS/ITRI");
            private static Aes aes = null;
            private static Rfc2898DeriveBytes key;
            public static UInt64 Salt { set {
                    if (value == 0)
                    {
                        key = null;
                        if (aes != null) aes.Dispose();
                        aes = null;
                    } else
                    {
                        key = new Rfc2898DeriveBytes(cryptKey, BitConverter.GetBytes(value), ITERATIONS);
                        if (aes != null) aes.Dispose();
                        aes = new AesManaged { KeySize = AES_KEYSIZE };
                        aes.Key = key.GetBytes(aes.KeySize / 8);
                        aes.IV = key.GetBytes(aes.BlockSize / 8);
                    }
                } }

            private static byte[] Cryptor(byte[] src, ICryptoTransform transform)
            {
                byte[] res = null;
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                {
                    cs.Write(src, 0, src.Length);
                    cs.Close();
                    res = ms.ToArray();
                }
                return res;
            }
            public static byte[] Encrypt(byte[] clear)
            {
                return Cryptor(clear, aes.CreateEncryptor());
            }

            public static byte[] Decrypt(byte[] crypt)
            {
                return Cryptor(crypt, aes.CreateDecryptor());
            }

            public static string Encrypt(string clearText)
            {
                return Convert.ToBase64String(Encrypt(Encoding.UTF8.GetBytes(clearText)));
            }

            public static string Decrypt(string cryptText)
            {
                return Encoding.UTF8.GetString(Decrypt(Convert.FromBase64String(cryptText)));
            }
        }

        private static readonly Lazy<ConfigData> lazy = new Lazy<ConfigData>(() => new ConfigData());
        private static Configuration config;

        private bool ready = false;
        public static bool Ready { get { return lazy.Value.ready || Load(); } }
        private ConcurrentDictionary<string, object> typeLists = new ConcurrentDictionary<string, object>();
        private static ConcurrentDictionary<string, object> Lists { get { return lazy.Value.typeLists; } }
        private ConcurrentDictionary<string, string> options;
        public static ConcurrentDictionary<string, string> Options { get { return lazy.Value.options; } }

        public const string NO_ACCOUNT_ALERT = "NoAccountAlert";
        private static ConcurrentDictionary<string, string> GetInitOptions()
        {
            const string VERSION_STRING = "1.0.0.0";
            const string NO_ENCRYPT_SALT = "0";

            return new ConcurrentDictionary<string, string>
            {
                ["Version"] = VERSION_STRING,
                ["Salt"] = NO_ENCRYPT_SALT
            };
        }

        private ConfigData()
        {
            options = GetInitOptions();
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
            return Lists.GetOrAdd(typeof(T).AssemblyQualifiedName, (k) => new List<T>()) as List<T>;
        }

        public static void UpdateList<T>(List<T> list)
        {
            Lists[typeof(T).AssemblyQualifiedName] = list;
            Save();
        }

        public static string GetOption(string name)
        {
            return Options.ContainsKey(name) ? Options[name] : "";
        }

        public static void SetOption(string name, string value)
        {
            if (GetOption(name) != value)
            {
                Options[name] = value;
                Save();
            }
        }

        public static void DeleteOption(string name)
        {
            var dic = Options as IDictionary<string, object>;
            if (dic.ContainsKey(name))
            {
                dic.Remove(name);
                Save();
            }
        }

        private static string RequoteJson(string jstr)
        {
            return new System.Text.RegularExpressions.
                Regex("([\\w\\._\\+\\-\\!%=/]+)").Replace(jstr, "\"$1\"");
        }

        public static bool Load()
        {
            if (config == null) return false;
            lock (config)  try
                {
                    lazy.Value.options = config.AppSettings.Settings["divaOptions"] == null ?
                            GetInitOptions() :
                            JsonConvert.DeserializeObject<ConcurrentDictionary<string, string>>
                                (RequoteJson(config.AppSettings.Settings["divaOptions"].Value));
                    string[] args = Environment.GetCommandLineArgs();
                    foreach (string arg in args)
                    {
                        if (arg.StartsWith("--dv", StringComparison.InvariantCultureIgnoreCase))
                        {
                            try
                            {
                                int splitter = arg.IndexOf('=');
                                Options[arg.Substring(4, splitter - 4)] = arg.Substring(splitter + 1);
                            }
                            catch { }
                        }
                    }
                    bool decrypt = Options["Salt"] != "0";
                    if (decrypt)
                        DataCryptor.Salt = UInt64.Parse(Options["Salt"]) +
                                    Convert.ToUInt64(config.AppSettings.Settings["divaOptions"].Value.Length);
                    var asms = AppDomain.CurrentDomain.GetAssemblies();
                    var settings = from k in config.AppSettings.Settings.AllKeys
                                   where k.StartsWith("dv")
                                   select k.Substring(2);
                    foreach (var s in settings)
                    {
                        string tname = (s.IndexOf(".") < 0) ? "Diva." + s : s;
                        Type t = Type.GetType(tname);
                        if (t == null)
                            foreach (var a in asms)
                            {
                                Console.WriteLine(a.GetName().FullName);
                                t = a.GetTypes().FirstOrDefault(tt => tt.FullName == tname);
                                if (t != null)
                                    break;
                            }
                        if (t != null)
                        {
                            string jstr = config.AppSettings.Settings["dv" + s].Value;
                            if (decrypt)
                                jstr = DataCryptor.Decrypt(jstr);
                            var typeOfList = typeof(List<>).MakeGenericType(t);
                            var deserializeMethod = typeof(JsonConvert).GetMethods()
                                .Single(m => m.Name == "DeserializeObject"
                                    && m.IsStatic
                                    && m.IsGenericMethodDefinition
                                    && m.GetParameters().Length == 1
                                    && m.GetParameters()[0].ParameterType == typeof(string)).MakeGenericMethod(typeOfList);
                            if (t.GetCustomAttribute<UnquoteJson>() != null) jstr = RequoteJson(jstr);
                            var list = deserializeMethod.Invoke(null, new object[] { jstr });
                            var updateListMethod = typeof(ConfigData).GetMethod("UpdateList").MakeGenericMethod(t);
                            updateListMethod.Invoke(lazy.Value, new object[] { list });
                        }
                        else
                            Console.WriteLine($"Unable to resolve type '{s}'.");
                    }
                } catch (Exception e)
                {
                    Console.WriteLine("Error loading configuration: " + e.ToString());
                    if (System.Windows.Forms.MessageBox.Show("Configuration file may be corrupted, start with new configuration?", "Loading error", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                        return lazy.Value.ready = false;
                    Reset(false);
                }
            DataCryptor.Salt = 0;
            return lazy.Value.ready = true;
        }

        public static void Reset(bool writeback = true)
        {
            lazy.Value.options = GetInitOptions();
            /*foreach (var kv in typeLists)
            {
                var list = kv.Value as IDisposable;
                if (list != null)
                    list.Dispose();
            }*/
            lazy.Value.typeLists.Clear();
            if (writeback) Save();
        }

        private static int WriteSetting(string key, object obj, bool encrypt)
        {
            string jstr = JsonConvert.SerializeObject(obj);
            bool unquote = key == "divaOptions";
            if (!unquote)
            {
                Type type = obj.GetType();
                if (type.IsGenericType)
                    type = type.GetGenericArguments()[0];
                unquote = type.GetCustomAttribute<UnquoteJson>() != null;
            }
            if (unquote)
                jstr = jstr.Replace("\"", "");
            if (encrypt)
                jstr = DataCryptor.Encrypt(jstr);
            if (config.AppSettings.Settings[key] == null)
                config.AppSettings.Settings.Add(key, jstr);
            else
                config.AppSettings.Settings[key].Value = jstr;
            return jstr.Length;
        }

        public static void Save()
        {
            lock (config)
            {
                bool encrypt = AccountManager.GetAccounts().Count() > 0;
                if (encrypt)
                {
                    UInt64 salt;
                    using (var rng = RNGCryptoServiceProvider.Create())
                    {
                        byte[] buffer = new byte[sizeof(UInt64)];
                        rng.GetBytes(buffer);
                        salt = BitConverter.ToUInt64(buffer, 0);
                        Options["Salt"] = salt.ToString();
                    }
                    salt += Convert.ToUInt64(WriteSetting("divaOptions", Options, false));
                    DataCryptor.Salt = salt;
                } else
                {
                    Options["Salt"] = "0";
                    WriteSetting("divaOptions", Options, false);
                }
                foreach (var kv in Lists) try
                    {
                        int count = (int)typeof(List<>).MakeGenericType(
                            Type.GetType(kv.Key)).GetProperty("Count").
                                GetGetMethod().Invoke(kv.Value, null);
                        string tname = Type.GetType(kv.Key).FullName;
                        string keyName = "dv" + (tname.StartsWith("Diva.") &&
                            tname.IndexOf(".", 5) < 0 ? tname.Substring(5) : tname);
                        if (count > 0)
                            WriteSetting(keyName, kv.Value, encrypt);
                        else if (config.AppSettings.Settings[keyName] != null)
                            config.AppSettings.Settings.Remove(keyName);
                    }
                    catch { }
                config.Save();
                if (encrypt) DataCryptor.Salt = 0;
            }
        }

        // actions modifying list content should be avoided be performed during load/save
        public static void DoAction(Action action) { lock (config) { action(); } }
    }
}
