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
        private Pupil _pupil;
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
        public Pupil Pupil
        {
            get { return _pupil; }
            set
            {
                if(value != null)
                {
                    _pupil = value;
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
