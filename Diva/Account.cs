using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Diva
{
    class AccountManager
    {
        public enum Privilege
        {
            None,
            Guest,
            Operator,
            Administrator
        }

        [UnquoteJson]
        class Account
        {
            private const int SaltSize = 16;
            private const int HashSize = 20;
            private const int HashIterations = 1000;

            public string Name { get; private set; }
            public byte[] Salt { get; private set; }
            public byte[] Hash { get; private set; }
            public Privilege Type { get; private set; }

            private static byte[] GenerateSalt()
            {
                byte[] salt = new byte[SaltSize];
                new RNGCryptoServiceProvider().GetBytes(salt);
                return salt;
            }

            private byte[] GenerateHash(string password)
                => new Rfc2898DeriveBytes(password, Salt, HashIterations)
                    .GetBytes(HashSize);

            public Account(string name, string password, Privilege type = Privilege.Administrator)
            {
                Name = name;
                Type = type;
                Salt = GenerateSalt();
                Hash = GenerateHash(password);
            }

            [Newtonsoft.Json.JsonConstructor]
            public Account(string name, byte[] salt, byte[] hash, Privilege type)
            {
                Name = name;
                Type = type;
                Salt = salt;
                Hash = hash;
            }

            public void Rename(string name) { Name = name; }

            public void SetPassword(string password)
            {
                Salt = GenerateSalt();
                Hash = GenerateHash(password);
            }

            public void SetType(Privilege type) { Type = type; }

            public bool Authenticate(string password)
            {
                bool ret = !IsAuthenticationLocked;
                if (ret)
                {
                    ret = Hash.SequenceEqual(GenerateHash(password));
                    if (ret)
                        AuthenticationSuceeded();
                    else
                        AuthenticationFailed();
                }
                return ret;

            }

            public static bool Authenticate(Account acc, string password)
                => acc != null && acc.Authenticate(password);

            public void UpdateLockInfo()
            {
                if (Name != LOCK_ACCOUNT_NAME)
                    throw new InvalidOperationException("Setting lock account with invalid name.");
                Salt = BitConverter.GetBytes(retryCount);
                Hash = BitConverter.GetBytes(RetryUnlockTime.Ticks);
            }
        }

        #region AccountManager Members and Properties
        private const int MAX_RETRIES = 15;
        private const int RETRY_TIMEOUT = 60 * 1000;
        private const int RETRY_LOCK_TIMEOUT = 30 * RETRY_TIMEOUT;
        private const string LOCK_ACCOUNT_NAME = "_lock";
        private static readonly Lazy<AccountManager> lazy = new Lazy<AccountManager>(() => new AccountManager());
        private Timer timer;
        private static Timer RetryTimer => lazy.Value.timer;
        private static int retryCount;
        public static DateTime RetryUnlockTime { get; private set; }
        private static List<Account> Accounts => lazy.Value.accountList;
        private List<Account> accountList;
        private Account current;
        public static bool IsAuthenticated
            => Accounts.Count == 0 || lazy.Value.current != null;
        public static bool IsAuthenticationLocked => retryCount >= MAX_RETRIES;
        public static string AccountConfigEntryName
            => "dv" + typeof(Account).FullName.Substring(5);
        #endregion

        private AccountManager()
        {
            timer = new Timer((o) => {
                retryCount = 0;
                // it's not a good practice, but modifying account list during config save causes error
                // maybe that should be done by copying whole list
                ConfigData.DoAction(() => DeleteAccount(LOCK_ACCOUNT_NAME));
            }, null, 0, Timeout.Infinite);
            accountList = ConfigData.GetTypeList<Account>();
            Account _lock = null;
            try
            {
                _lock = accountList.Single(a => a.Name == LOCK_ACCOUNT_NAME);
            }
            catch { };
            if (_lock != null)
            {
                retryCount = BitConverter.ToInt32(_lock.Salt, 0);
                Int64 ticks = BitConverter.ToInt64(_lock.Hash, 0);
                RetryUnlockTime = new DateTime(ticks);
                int due = (RetryUnlockTime - DateTime.Now).Milliseconds;
                if (due > 0)
                    timer.Change(due, Timeout.Infinite);
                else
                    accountList.Remove(_lock);
            }
        }

        private static Account GetAccount(string name)
        {
            try
            {
                return Accounts.Single(a => a.Name == name);
            }
            catch  { }
            return null;
        }

        public static IEnumerable<string> GetAccounts()
            => from acc in Accounts select acc.Name;

        public static bool AccountExist(string name)
            => GetAccount(name) != null;

        public static bool VerifyAccount(string name, string password)
            => GetAccount(name).Authenticate(password);

        public static bool IsValidAccountName(string name)
        {
            return new System.Text.RegularExpressions.Regex(
                    "[A-Za-z][A-Za-z0-9_]*").Match(name).Length == name.Length;
        }

        public static bool IsValidPassword(string password)
        {
            return true;
        }

        public static bool CreateAccount(string name, string password)
        {
            // assume already checked
            bool ret = false;
            try
            {
                Accounts.Add(new Account(name, password));
                if (ConfigData.GetOption(ConfigData.NO_ACCOUNT_ALERT) == "true")
                    ConfigData.DeleteOption(ConfigData.NO_ACCOUNT_ALERT); // auto saved
                else
                    ConfigData.Save();
                ret = true;
            } catch
            { }
            return ret;
        }

        public static bool DeleteAccount(string name)
        {
            bool ret = false;
            int n = Accounts.Count;
            Account acc = GetAccount(name);
            if (acc != null && acc != lazy.Value.current)
            {
                Accounts.Remove(acc);
                ret = true;
            }
            if (Accounts.Count == 1 && Accounts[0].Name == LOCK_ACCOUNT_NAME)
                Accounts.Clear();
            if (n != Accounts.Count)
                ConfigData.Save();
            return ret;
        }

        public static bool RenameAccount(string oldName, string newName)
        {
            Account acc = GetAccount(oldName);
            bool ret = acc != null && !Accounts.Exists(a => a.Name == newName);
            if (ret)
            {
                acc.Rename(newName);
                ConfigData.Save();
            }
            return ret;
        }

        public static bool ChangePassword(string name, string newPassword)
        {
            Account acc = GetAccount(name);
            if (acc != null)
            {
                acc.SetPassword(newPassword);
                ConfigData.Save();
            }
            return acc != null;
        }

        public static string GetLoginAccount()
        {
            if (lazy.Value.current != null)
                return lazy.Value.current.Name;
            return "";
        }

        private static void AuthenticationSuceeded()
        {
            RetryTimer.Change(0, Timeout.Infinite);
        }

        public static void AuthenticationFailed()
        {
            if (++retryCount >= MAX_RETRIES)
            {
                RetryTimer.Change(RETRY_LOCK_TIMEOUT, Timeout.Infinite);
                RetryUnlockTime = DateTime.Now + TimeSpan.FromMilliseconds(RETRY_LOCK_TIMEOUT);
            }
            else
            {
                RetryTimer.Change(RETRY_TIMEOUT, Timeout.Infinite);
                RetryUnlockTime = DateTime.Now + TimeSpan.FromMilliseconds(RETRY_TIMEOUT);
            }
            Account _lock = GetAccount(LOCK_ACCOUNT_NAME);
            if (_lock == null)
                Accounts.Add(_lock = new Account(LOCK_ACCOUNT_NAME, null, null, Privilege.None));
            _lock.UpdateLockInfo();
            ConfigData.Save();
            //if (retryCount >= MAX_RETRIES) throw new TimeoutException("Too many tries, try again later.");
        }

        public static bool Login(string name, string password)
        {
            if (!VerifyAccount(name, password)) return false;
            return (lazy.Value.current = GetAccount(name)) != null;
        }

        public static bool Logout()
        {
            bool ret = lazy.Value.current != null;
            if (ret) lazy.Value.current = null;
            return ret;
        }
    }
}
