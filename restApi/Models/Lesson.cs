using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace restApi.Models
{
    [Table("lessons")]
    public class Lesson
    {
        private int _id;
        private string _title;

        public Lesson(int id, string title)
        {
            Id = id;
            Title = title;
        }

        [Key]
        [Column("id")]
        public int Id
        {
            get { return _id; }
            set
            {
                if(value >= 0)
                {
                    _id = value;
                }
            }
        }
        [Required]
        [MaxLength(256)]
        [Column("title")]
        public string Title
        {
            get { return _title; }
            set
            {
                if(!String.IsNullOrEmpty(value) || !String.IsNullOrWhiteSpace(value))
                {
                    _title = value;
                }
                else
                {
                    _title = "Default Title";
                }
            }
        }

    }
}
