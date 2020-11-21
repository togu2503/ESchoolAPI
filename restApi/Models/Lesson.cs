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
        private Teacher _teacher;

        [Key]
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
        [Required]
        public Teacher Teacher
        {
            get { return _teacher; }
            set
            {
                if(value != null)
                {
                    _teacher = value;                    
                }
            }
        }

    }
}
