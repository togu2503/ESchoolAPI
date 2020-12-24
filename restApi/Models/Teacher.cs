using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace restApi.Models
{
    [Table("teachers")]
    public class Teacher : IPerson
    {
        private int _id;
        private string _name;
        private string _surname;
        private string _patronymic;
        private string _phone;
        private int _accountId;

        public Teacher(int id = 0, string name = "", string surname = "", string patronymic = "", string phone = "", int accountId = 0)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Phone = phone;
            AccountId = accountId;
        }

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
        [MaxLength(256)]
        [Column("name")]
        public string Name
        {
            get { return _name; }
            set
            {
                if(!String.IsNullOrEmpty(value) || !String.IsNullOrWhiteSpace(value))
                {
                    _name = value;
                }
            }
        }
        [Required]
        [MaxLength(256)]
        [Column("surname")]
        public string Surname
        {
            get { return _surname; }
            set
            {
                if(!String.IsNullOrWhiteSpace(value) || !String.IsNullOrEmpty(value))
                {
                    _surname = value;
                }
            }
        }
        [Required]
        [MaxLength(256)]
        [Column("patronymic")]
        public string Patronymic
        {
            get { return _patronymic; }
            set
            {
                if (!String.IsNullOrWhiteSpace(value) || !String.IsNullOrEmpty(value))
                {
                    _patronymic = value;
                }
            }
        }
        [MaxLength(256)]
        [Column("phone")]
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        
        [Column("account_id")]
        public int AccountId
        {
            get { return _accountId; }
            set { _accountId = value; }
        }
    }
}
