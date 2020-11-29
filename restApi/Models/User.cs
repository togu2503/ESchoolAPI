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
        private string _permissions;

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
        public string Permissions
        {
            get { return _permissions; }
            set
            {
                if (!String.IsNullOrEmpty(value) || !String.IsNullOrWhiteSpace(value))
                {
                    _permissions = value;
                }
            }
        }

        public User(int id=0, string login="", string password="", string permissions="")
        {
            Id = id;
            Login = login;
            Password = password;
            Permissions = permissions;
        }
    }
}
