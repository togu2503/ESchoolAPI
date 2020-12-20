using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace restApi.Models
{
    [Table("users")]
    public class User
    {
        private int _id;
        private string _login;
        private string _password;
        private int _accessLevel;

        [Key]
        [Column("id")]
        public int Id
        {
            get { return _id; }
            set
            {
                if (value >= 0)
                {
                    _id = value;
                }
            }
        }
        [Required]
        [MaxLength(64)]
        [Column("login")]
        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                if (!String.IsNullOrEmpty(value) || !String.IsNullOrWhiteSpace(value))
                {
                    _login= value;
                }
            }
        }
        [Required]
        [MaxLength(256)]
        [Column("password")]
        public string Password
        {
            get { return _password; }
            set
            {
                if (!String.IsNullOrEmpty(value) || !String.IsNullOrWhiteSpace(value))
                {
                    _password = value;
                }
            }
        }
        [Required]
        [MaxLength(128)]
        [Column("access_level")]
        public int AccessLevel
        {
            get { return _accessLevel;}
            set {_accessLevel = value;}
  
        }

        public User(int id=0, string login="", string password="", int accessLevel = 0)
        {
            Id = id;
            Login = login;
            Password = password;
            AccessLevel = accessLevel;
        }

        public override bool Equals(object obj)
        {
            if(obj is User user)
            {
                if (Id == user.Id && Login == user.Login && Password == user.Password && AccessLevel == user.AccessLevel)
                    return true;
            }
            return false;
        }
    }
}
