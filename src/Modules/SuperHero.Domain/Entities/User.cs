using SuperHero.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperHero.Domain.Entities
{
    public class User
    {
        public User(string name,
                    string login,
                    string password,
                    Profile profile)
        {
            Name = name;
            Login = login;
            CriptografyPassword(password);
            Profile = profile;
            Created = DateTime.Now;
        }

        public User(int id,
                string name,
                Profile profile)
        {
            Id = id;
            Name = name;
            Profile = profile;
        }

        public int Id { get; private set; }
        public string Login { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }
        public Profile Profile { get; private set; }
        public DateTime Created { get; set; }

        public bool IsValid()
        {
            var valid = true;

            if (string.IsNullOrEmpty(Name) ||
                string.IsNullOrEmpty(Login) ||
                string.IsNullOrEmpty(Password) ||
                    Profile.Id <= 0)
            {
                valid = false;
            }

            return valid;
        }

        public void CriptografyPassword(string password)
        {
            Password = PasswordHasher.Hash(password);
        }

        public bool IsEqualPassword(string password)
        {
            return PasswordHasher.Verify(password, Password);
        }

        public void InformationLoginUser(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void UpdateInfo(string name,
                    string password,
                    Profile profile)
        {
            Name = name;
            Profile = profile;

            if (password != Password)
                CriptografyPassword(password);
        }
    }
}
