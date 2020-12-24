using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace restApi.Models
{
    [Table("pupil_form_joint")]
    public class PupilFormJoint
    {
        private int _id;
        private int _pupilId;
        private int _formId;

        public PupilFormJoint(int id = 0, int pupilId = 0, int formId = 0)
        {
            Id = id;
            PupilId = pupilId;
            FormId = formId;
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
        [Column("pupil_id")]
        public int PupilId
        {
            get { return _pupilId; }
            set { _pupilId = value; }
        }

        [Required]
        [Column("form_id")]
        public int FormId
        {
            get { return _formId; }
            set { _formId = value; }
        }

        public virtual Form Form { get; set; }
        public virtual Pupil Pupil { get; set; }


    }
}
