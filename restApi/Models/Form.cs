using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace restApi.Models
{
    [Table("forms")]
    public class Form
    {
        private int _id;
        private string _formTitle;
        private Teacher _teacher;

        [Key]
        public int Id
        { 
            get { return _id; }
            set
            {
                if(value >=0)
                {
                    _id = value;
                }
            }
        }
        [Required]
        [MaxLength(256)]
        public string FormTitle
        {
            get { return _formTitle; }
            set
            {
                if(!String.IsNullOrEmpty(value) || !String.IsNullOrWhiteSpace(value))
                {
                    _formTitle = value;
                }
            }
        }
        [Required]
        
        public Teacher Teacher
        {
            get { return _teacher; }
            set
            {
                if(value!=null)
                {
                    _teacher = value;
                }
            }
        }
    }
}
