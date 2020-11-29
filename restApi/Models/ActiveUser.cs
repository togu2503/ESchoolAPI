using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace restApi.Models
{
    [Table("active_users")]
    public class ActiveUser
    {
        private int _id;
        private int _userId;
        private string _token;

        [Key]
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
        [Column("user_id")]
        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                if (value >= 0)
                {
                    _userId = value;
                }
            }
        }
        [Required]
        public string Token
        {
            get { return _token; }
            set
            {
                if (!String.IsNullOrEmpty(value) || !String.IsNullOrWhiteSpace(value))
                {
                    _token = value;
                }
            }
        }

        public ActiveUser(int id, int userId, string token)
        {
            Id = id;
            UserId = userId;
            this.Token = token;
        }
    }
}
