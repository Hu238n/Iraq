using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Iraqi.Heros.Models
{
    public class PersonForm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DoB { get; set; }
        [Required]
        public int Gov { get; set; }
        [Required]
        public DateTime DoK { get; set; }
        [Required]
        public string PoK { get; set; }
        [Required]
        public string Story { get; set; }
        [Required]
        public Type Type { get; set; }
    }
}
