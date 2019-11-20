using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Iraqi.Heros.Models
{
    public class PersonForm
    {
        public string Name { get; set; }
        public DateTime DoB { get; set; }
        public int Gov { get; set; }
        public DateTime DoK { get; set; }
        public string PoK { get; set; }
        public string Story { get; set; }
        public Type Type { get; set; }
    }
}
