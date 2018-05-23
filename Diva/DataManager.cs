﻿using System;
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

    class DataCryptor
    {
        private static int AES_KEYSIZE = 128;
        private static int ITERATIONS = 256;
        private static readonly byte[] cryptKey = Encoding.UTF8.GetBytes("Diva.GCS/ITRI");
        private static Aes aes = null;
        private static Rfc2898DeriveBytes key;
        public static UInt64 Salt { set {
                key = new Rfc2898DeriveBytes(cryptKey, BitConverter.GetBytes(value), ITERATIONS);
                if (aes != null) aes.Dispose();
                aes = new AesManaged();
                aes.KeySize = AES_KEYSIZE;
                aes.Key = key.GetBytes(aes.KeySize / 8);
                aes.IV = key.GetBytes(aes.BlockSize / 8);
            } }

        private static byte[] cryptor(byte[] src, ICryptoTransform transform)
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
            return cryptor(clear, aes.CreateEncryptor());
        }

        public static byte[] Decrypt(byte[] crypt)
        {
            return cryptor(crypt, aes.CreateDecryptor());
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

    class DataManager
    {
        private const string VERSION_STRING = "1.0.0.0";
        private static readonly Lazy<DataManager> lazy = new Lazy<DataManager>(() => new DataManager());
        private static Configuration config;

        private bool ready = false;
        public static bool Ready { get { return lazy.Value.ready || Load(); } }
        private ConcurrentDictionary<string, object> typeLists = new ConcurrentDictionary<string, object>();
        private static ConcurrentDictionary<string, object> lists { get { return lazy.Value.typeLists; } }
        private dynamic options;
        private static dynamic divaOptions { get { return lazy.Value.options; } }

        private DataManager()
        {
            options = new System.Dynamic.ExpandoObject();
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
            return lists.GetOrAdd(typeof(T).AssemblyQualifiedName, (k) => new List<T>()) as List<T>;
        }

        public static void UpdateList<T>(List<T> list)
        {
            lists[typeof(T).AssemblyQualifiedName] = list;
        }

        public static void AddItem<T>(T item)
        {
            GetTypeList<T>().Add(item);
        }

        public static bool RemoveItem<T>(T item)
        {
            return GetTypeList<T>().Remove(item);
        }

        public static object GetOption(string name)
        {
            var dic = divaOptions as IDictionary<string, object>;
            return dic.ContainsKey(name) ? dic[name] : null;
        }

        public static void SetOption(string name, object value)
        {
            var dic = divaOptions as IDictionary<string, object>;
            if (dic.ContainsKey(name))
                dic[name] = value;
            else
                dic.Add(name, value);
        }

        public static void DeleteOption(string name)
        {
            var dic = divaOptions as IDictionary<string, object>;
            if (dic.ContainsKey(name))
                dic.Remove(name);
        }

        private static string requoteJson(string jstr)
        {
            return new System.Text.RegularExpressions.
                Regex("([\\w\\._\\+\\-\\!%=/]+)").Replace(jstr, "\"$1\"");
        }

        public static bool Load()
        {
            if (config == null) return false;
            if (config.AppSettings.Settings["divaOptions"] != null)
            {
                lazy.Value.options = JsonConvert.DeserializeObject<System.Dynamic.ExpandoObject>(requoteJson(
                    config.AppSettings.Settings["divaOptions"].Value));
            }
            else
            {
                lazy.Value.options = new System.Dynamic.ExpandoObject();
                divaOptions.Version = VERSION_STRING;
                divaOptions.Salt = "0";
            }
            bool decrypt = divaOptions.Salt != "0";
            if (decrypt)
                DataCryptor.Salt = UInt64.Parse(divaOptions.Salt) +
                            Convert.ToUInt64(config.AppSettings.Settings["divaOptions"].Value.Length);
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
                    if (t.GetCustomAttribute<UnquoteJson>() != null) jstr = requoteJson(jstr);
                    var list = deserializeMethod.Invoke(null, new object[] {  jstr });
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

        private static int writeSetting(string key, object obj, bool encrypt)
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
            bool encrypt = AccountManager.GetAccounts().Count() > 0;
            if (encrypt)
            {
                UInt64 salt;
                using (var rng = RNGCryptoServiceProvider.Create())
                {
                    byte[] buffer = new byte[sizeof(UInt64)];
                    rng.GetBytes(buffer);
                    salt = BitConverter.ToUInt64(buffer, 0);
                    divaOptions.Salt = salt.ToString();
                }
                salt += Convert.ToUInt64(writeSetting("divaOptions", divaOptions, false));
                DataCryptor.Salt = salt;
            }
            foreach (var kv in lists)
            {
                int count = (int)typeof(List<>).MakeGenericType(
                    Type.GetType(kv.Key)).GetProperty("Count").
                        GetGetMethod().Invoke(kv.Value, null);
                string keyName = "dv" + Type.GetType(kv.Key).FullName;
                if (count > 0)
                    writeSetting(keyName, kv.Value, encrypt);
                else if (config.AppSettings.Settings[keyName] != null)
                    config.AppSettings.Settings.Remove(keyName);
            }
            config.Save();
        }
    }
}
