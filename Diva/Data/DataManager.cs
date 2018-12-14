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
using NDesk.Options;

namespace Diva
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    class UnquoteJson : Attribute { }

    class ConfigData
    {
        #region Data Encryption Utility Class
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
        #endregion

        private static readonly Lazy<ConfigData> lazy = new Lazy<ConfigData>(() => new ConfigData());
        private static Configuration config;

        private bool ready = false;
        public static bool Ready => lazy.Value.ready || Load();
        private ConcurrentDictionary<string, object> typeLists = new ConcurrentDictionary<string, object>();
        private static ConcurrentDictionary<string, object> Lists => lazy.Value.typeLists;

        #region Config options
        public class OptionName
        {
            public const string ForceAccountLogin = "ForceAccountLogin";
            public const string ImageMapSource = "ImageMapSource";
            public const string Language = "Language";
            public const string MapProxy = "MapProxy";
            public const string MapCacheLocation = "MapCacheLocation";
            public const string MapInitialLocation = "MapInitialLocation";
            public const string Salt = "Salt";
            public const string SkipNoAccountAlert = "NoAccountAlert";
            public const string UseImageMap = "UseImageMap";
            public const string Version = "Version";
        }

        private ConcurrentDictionary<string, string> options;
        public static ConcurrentDictionary<string, string> Options => lazy.Value.options;
        private static ConcurrentDictionary<string, string> GetInitOptions()
        {
            const string VERSION_STRING = "1.0.0.0";
            const string NO_ENCRYPT_SALT = "0";

            return new ConcurrentDictionary<string, string>
            {
                [OptionName.Version] = VERSION_STRING,
                [OptionName.Salt] = NO_ENCRYPT_SALT
            };
        }
        #endregion

        #region Command line options
        private ConcurrentDictionary<string, string> clopts;
        public static ConcurrentDictionary<string, string> CLOptions => lazy.Value.clopts;
        public static void ParseCommandLine(string[] args)
        {
            try
            {
                void setOpt(string o, string v, string defval = null) => CLOptions[o] = defval ?? v;
                void setBoolean(string o, string v) => CLOptions[o] = (v != null).ToString();
                new OptionSet()
                {
                    { "a|NoAccountAlert", v => setBoolean(OptionName.SkipNoAccountAlert, v) },
                    { "c|MapCacheLocation=", v => setOpt(OptionName.MapCacheLocation, v) },
                    { "f|ForceAccountLogin", v => setBoolean(OptionName.ForceAccountLogin, v) },
                    { "i|ImageMapSource=", v => {
                        if (v != null)
                        {
                            CLOptions[OptionName.UseImageMap] = true.ToString();
                            if (v.Substring(1) != "i+")
                                CLOptions[OptionName.ImageMapSource] = v;
                        } else
                            CLOptions[OptionName.UseImageMap] = false.ToString();
                    }  },
                    { "l|MapInitiailLocation=", v => setOpt(OptionName.MapInitialLocation, v) },
                    { "lang|Language=", v => setOpt(OptionName.Language, v) },
                    { "p|proxy=", v => setOpt(OptionName.Language, v) },
                    { "s|Salt=", v => setOpt(OptionName.Salt, v) },
                    { "u|UseImageMap=", v => setOpt(OptionName.UseImageMap, v) }
                }.Parse(args);
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        private ConfigData()
        {
            clopts = new ConcurrentDictionary<string, string>();
            options = GetInitOptions();
            try
            {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            } catch (Exception e)
            {
                Console.WriteLine("Error opening config file: " + e.Message);
            }
        }

        public static List<T> GetTypeList<T>() =>
            Lists.GetOrAdd(typeof(T).AssemblyQualifiedName,
                (k) => new List<T>()) as List<T>;

        public static void UpdateList<T>(List<T> list, bool writeback = true)
        {
            Lists[typeof(T).AssemblyQualifiedName] = list;
            if (writeback) Save();
        }

        public static string GetOption(string name)
        {
            return CLOptions.ContainsKey(name) ? CLOptions[name] :
                    Options.ContainsKey(name) ? Options[name] : "";
        }

        public static bool GetBoolOption(string name)
        {
            string opt = GetOption(name);
            if (!bool.TryParse(opt, out var res))
                res = false;
            return res;
        }

        public static int GetIntOption(string name)
        {
            string opt = GetOption(name);
            if (!int.TryParse(opt, out var res))
                res = 0;
            return res;
        }

        public static void SetOption(string name, string value)
        {
            if (GetOption(name) != value)
            {
                CLOptions.TryRemove(name, out var output);
                Options[name] = value;
                Save();
            }
        }

        public static void SetIntOption(string name, int value)
        {
            if (GetIntOption(name) != value)
            {
                CLOptions.TryRemove(name, out var output);
                Options[name] = value.ToString();
                Save();
            }
        }

        public static void SetBoolOption(string name, bool value)
        {
            if (GetBoolOption(name) != value)
            {
                CLOptions.TryRemove(name, out var output);
                Options[name] = value.ToString();
                Save();
            }
        }

        public static void SetCommandLineOption(string name, string value)
        {
            CLOptions[name] = value;
        }

        public static void DeleteOption(string name)
        {
            CLOptions.TryRemove(name, out var value);
            if (Options.TryRemove(name, out value))
                Save();
        }

        #region Config Load/Store
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
                                (config.AppSettings.Settings["divaOptions"].Value);
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
                    bool decrypt = GetOption(OptionName.Salt) != "0";
                    if (decrypt)
                    {
                        if (!config.AppSettings.Settings.AllKeys.Contains(AccountManager.AccountConfigEntryName))
                            throw new InvalidDataException("No account data found.");
                        DataCryptor.Salt = UInt64.Parse(Options[OptionName.Salt]) +
                                    Convert.ToUInt64(config.AppSettings.Settings["divaOptions"].Value.Length);
                    }
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
                            var list = deserializeMethod.Invoke(null, new [] { jstr });
                            var updateListMethod = typeof(ConfigData).GetMethod("UpdateList").MakeGenericMethod(t);
                            updateListMethod.Invoke(lazy.Value, new [] { list, false });
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
            if (key != "divaOptions")
            {
                Type type = obj.GetType();
                if (type.IsGenericType)
                    type = type.GetGenericArguments()[0];
            }
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
                        Options[OptionName.Salt] = salt.ToString();
                    }
                    salt += Convert.ToUInt64(WriteSetting("divaOptions", Options, false));
                    DataCryptor.Salt = salt;
                } else
                {
                    Options[OptionName.Salt] = "0";
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
        #endregion

        // actions modifying list content should be avoided be performed during load/save
        public static void DoAction(Action action) { lock (config) { action(); } }
    }
}
