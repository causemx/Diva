﻿using System;
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
                if (!AccountManager.AuthenticationLocked())
                {
                    ret = Hash.SequenceEqual(generateHash(password));
                    if (!ret) AccountManager.AuthenticationFailed();
                }
                return ret;
            }
        }

        private const int TOO_MANY_TRIES = 15;
        private const int AUTH_TRY_PERIOD = 60 * 1000;
        private static readonly Lazy<AccountManager> lazy = new Lazy<AccountManager>(() => new AccountManager());
        private static Timer timer;
        private static int authCount;
        private static List<Account> accounts { get { return lazy.Value.accountList; } }
        private List<Account> accountList;
        private Account current;

        private AccountManager()
        {
            accountList = DataManager.GetTypeList<Account>();
            timer = new Timer((o) => { authCount = 0; }, null, 0, Timeout.Infinite);
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
            return authCount >= TOO_MANY_TRIES;
        }

        private static void AuthenticationFailed()
        {
            if (++authCount >= TOO_MANY_TRIES)
            {
                timer.Change(AUTH_TRY_PERIOD * 30, Timeout.Infinite);
                throw new TimeoutException("Too many tries, try again later.");
            }
            else
                timer.Change(AUTH_TRY_PERIOD, Timeout.Infinite);
        }

        public static bool VerifyAccount(string name, string password)
        {
            Account acc = GetAccount(name);
            return acc != null && acc.Authenticate(password);
        }

        public static bool Login(string name, string password)
        {
            Account acc = GetAccount(name);
            bool ret = acc != null && acc.Authenticate(password);
            if (ret) lazy.Value.current = acc;
            return ret;
        }

        public static bool Logout()
        {
            bool ret = lazy.Value.current != null;
            if (ret) lazy.Value.current = null;
            return ret;
        }
    }
}
