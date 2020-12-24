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
        private int _teacherId;

        public Form(int id = 0, string formTitle = "", int teacherId = 0)
        {
            Id = id;
            FormTitle = formTitle;
            TeacherId = teacherId;
        }

        [Key]
        [Column("id")]
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
        [Column("form_title")]
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
        [Column("class_teacher")]
        
        public int TeacherId
        {
            get { return _teacherId; }
            set { _teacherId = value; }
        }
        public virtual Teacher Teacher { get; set; }
    }
}
