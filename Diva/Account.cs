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

            private static byte[] generateSalt()
            {
                byte[] salt = new byte[SaltSize];
                new RNGCryptoServiceProvider().GetBytes(salt);
                return salt;
            }

            private byte[] generateHash(string password)
            {
                return new Rfc2898DeriveBytes(password, Salt, HashIterations).GetBytes(HashSize);
            }

            public Account(string name, string password, Privilege type = Privilege.Administrator)
            {
                Name = name;
                Type = type;
                Salt = generateSalt();
                Hash = generateHash(password);
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
                Salt = generateSalt();
                Hash = generateHash(password);
            }

            public void SetType(Privilege type) { Type = type; }

            public bool Authenticate(string password)
            {
                bool ret = false;
                if (!AuthenticationLocked())
                {
                    ret = Hash.SequenceEqual(generateHash(password));
                    if (!ret) AuthenticationFailed();
                }
                return ret;
            }
        }

        private const int MAX_RETRIES = 15;
        private const int RETRY_TIMEOUT = 60 * 1000;
        private const int RETRY_LOCK_TIMEOUT = 30 * RETRY_TIMEOUT;
        private const string UNLOCKTIME_FORMAT = "yyyy/MM/dd/HH/mm/ss";
        private static readonly Lazy<AccountManager> lazy = new Lazy<AccountManager>(() => new AccountManager());
        private static Timer timer;
        private static int retryCount;
        public static DateTime RetryUnlockTime { get; private set; }
        private static List<Account> accounts { get { return lazy.Value.accountList; } }
        private List<Account> accountList;
        private Account current;

        private AccountManager()
        {
            accountList = DataManager.GetTypeList<Account>();
            string unlockTimeStr = (string)DataManager.GetOption("Unlock");
            if (unlockTimeStr == null)
            {
                RetryUnlockTime = DateTime.Now;
                DataManager.SetOption("Unlock",
                    RetryUnlockTime.ToString(UNLOCKTIME_FORMAT));
            }
            else
                RetryUnlockTime = DateTime.ParseExact(unlockTimeStr,
                                    UNLOCKTIME_FORMAT, null);
            timer = new Timer((o) => { retryCount = 0; }, null, 0, Timeout.Infinite);
        }

        private static Account GetAccount(string name)
        {
            try
            {
                return accounts.Single(a => a.Name == name);
            }
            catch (Exception e)
            {
                ;
            }
            return null;
        }

        public static IEnumerable<string> GetAccounts()
        {
            return from acc in accounts select acc.Name;
        }

        public static bool CreateAccount(string name, string password)
        {
            bool ret = name.Length > 0 && !accounts.Exists(a => a.Name == name)
                && new System.Text.RegularExpressions.Regex(
                    "[A-Za-z][A-Za-z0-9_]*").Match(name).Length != name.Length;
            if (ret) accounts.Add(new Account(name, password));
            return ret;
        }

        public static bool DeleteAccount(string name)
        {
            bool ret = false;
            Account acc = GetAccount(name);
            if (acc != null && acc != lazy.Value.current)
            {
                accounts.Remove(acc);
                ret = true;
            }
            return ret;
        }

        public static bool RenameAccount(string oldName, string newName)
        {
            Account acc = GetAccount(oldName);
            bool ret = acc != null && !accounts.Exists(a => a.Name == newName);
            if (ret) acc.Rename(newName);
            return ret;
        }

        public static bool ChangePassword(string name, string oldPassword, string newPassword)
        {
            Account acc = GetAccount(name);
            bool ret = acc != null && acc.Authenticate(oldPassword);
            if (ret) acc.SetPassword(newPassword);
            return ret;
        }

        public static string GetLoginAccount() { return lazy.Value.current.Name; }

        private static bool AuthenticationLocked()
        {
            return retryCount >= MAX_RETRIES;
        }

        private static void AuthenticationFailed()
        {
            if (++retryCount >= MAX_RETRIES)
            {
                timer.Change(RETRY_LOCK_TIMEOUT, Timeout.Infinite);
                RetryUnlockTime = DateTime.Now + TimeSpan.FromMilliseconds(RETRY_LOCK_TIMEOUT);
                DataManager.SetOption("Unlock", RetryUnlockTime);
                throw new TimeoutException("Too many tries, try again later.");
            }
            else
                timer.Change(RETRY_TIMEOUT, Timeout.Infinite);
        }

        public static bool VerifyAccount(string name, string password)
        {
            Account acc = GetAccount(name);
            if (acc == null || !acc.Authenticate(password)) return false;
            timer.Change(0, Timeout.Infinite);
            return true;
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
