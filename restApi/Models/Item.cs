using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace restApi.Models
{
    [Table("Item")]
    public class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        [Required]
        [MaxLength(256)]
        public bool IsComplete { get; set; }

        Item(int id, string name, bool isComplete)
        {
            Id = id;
            Name = name;
            IsComplete = isComplete;
        }
    }
}
