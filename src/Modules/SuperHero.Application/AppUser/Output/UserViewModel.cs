using SuperHero.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuperHero.Application.AppUser.Output
{
    public class UserViewModel
    {
        public UserViewModel(int id,
                                string login,
                                string name,
                                Profile profile,
                                DateTime created)
        {
            Id = id;
            Login = login;
            Name = name;
            Profile = profile;
            Created = created;
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public Profile Profile { get; set; }
        public DateTime Created { get; set; }
    }
}
