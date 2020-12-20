using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace restApi.Models
{
    [Table("homework")]
    public class Homework
    {
        private int _id;
        private string _tasks;
        private int _lessonInCuriculumId;
        private DateTime _date;
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
        [MaxLength(256)]
        [Column("tasks")]
        public string Tasks
        {
            get => _tasks;
            set => _tasks = value;
        }
        [Required]        
        [Column("lesson")]
        public int LessonId
        {
            get { return _lessonInCuriculumId; }
            set {_lessonInCuriculumId = value;}
        }

        [Required]
        [Column("date")]
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if(value != null)
                {
                    _date = value;
                }
                else
                {
                    _date = new DateTime(0, 0, 0, 0, 0, 0);
                }
            }
        }

        public virtual Curiculum Lesson{ get; set; }
    }
}
