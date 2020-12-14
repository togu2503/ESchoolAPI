using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace restApi.Models
{
    [Table("curiculum")]
    public class Curiculum
    {
        private int _id;
        private int _day;
        private int _lessonNum;
        private int _lessonId;
        private int _formId;
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
        [Column("day")]
        public int Day
        {
            get { return _day; }
            set {_day = value;}
        }
        
        [Required]
        [Column("lesson_num")]
        public int LessonNum
        {
            get { return _lessonNum; }
            set { _lessonNum = value; }
        }
        
        [Required]
        [Column("lesson_id")]
        public int LessonId
        {
            get { return _lessonId; }
            set {_lessonId = value;}
        }
        [Required]
        [Column("form_id")]
        public int FormId
        {
            get { return _formId; }
            set {_formId = value;}
        }

        [Column("teacher_id")]
        public int TeacherId
        {
            get { return _teacherId; }
            set
            {
                    _teacherId = value;
            }
        }
        public virtual Form Form { get; set; }
        public virtual Lesson Lesson { get; set; }
        public virtual Teacher Teacher { get; set; }

    }
}
