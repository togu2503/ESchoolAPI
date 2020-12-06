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
        private int _teacherId;

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
        [Required]
        [Column("teacher_id")]
        public int TeacherId
        {
            get { return _teacherId; }
            set
            {
                if(value != null)
                {
                    _teacherId = value;                    
                }
            }
        }

        public virtual Teacher Teacher { get; set; }

    }
}
