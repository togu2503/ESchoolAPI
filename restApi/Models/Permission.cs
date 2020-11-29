using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace restApi.Models
{
    [Table("permissions")]
    public class Permission
    {
        private int _id;
        private string _shortDescription;
        private string _description;

        [Key]
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
        [Required]
        [MaxLength(128)]
        public string ShortDescription
        {
            get
            {
                return _shortDescription;
            }
            set
            {
                if (!String.IsNullOrEmpty(value) || !String.IsNullOrWhiteSpace(value))
                {
                    _shortDescription = value;
                }
            }
        }

        [Required]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (!String.IsNullOrEmpty(value) || !String.IsNullOrWhiteSpace(value))
                {
                    _description = value;
                }
            }
        }
    }
}
