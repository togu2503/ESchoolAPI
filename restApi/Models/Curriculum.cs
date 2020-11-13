using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace restApi.Models
{
    [Table("curiculum")]
    public class Curriculum
    {
        private int _id;
        private int _day;
        private DateTime _timeStart;
        private DateTime _timeFinish;
        private Lesson _lesson;
        private Form _form;

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
        public int Day
        {
            get { return _day; }
            set
            {
                if(value > 0)
                {
                    _day = value;
                }
            }
        }
        [Required]
        public DateTime TimeStart
        {
            get { return _timeStart; }
            set
            {
                if (value != null)
                {
                    _timeStart = value;
                }
                else
                {
                    //in the case if something will going wrong.
                    _timeStart = new DateTime(1000, 10, 10,0,0,0);
                }
            }
        }
        [Required]
        public DateTime TimeFinish
        {
            get { return _timeFinish; }
            set
            {
                if (value != null)
                {
                    _timeFinish = value;
                }
                else
                {
                    //in the case if something will going wrong.
                    _timeFinish = new DateTime(1001, 11, 11, 0, 0, 0);
                }
            }
        }
        [Required]
        public Lesson Lesson
        {
            get { return _lesson; }
            set
            {
                if(value != null)
                {
                    _lesson = value;
                }
            }
        }
        [Required]
        public Form Form
        {
            get { return _form; }
            set
            {
                if(value != null)
                {
                    _form = value;
                }
            }
        }

    }
}
