using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Iraqi.Heros.Models
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DoB { get; set; }
        public int Gov { get; set; }
        public DateTime DoK { get; set; }
        public string PoK { get; set; }
        public string Story { get; set; }
        public Type Type { get; set; }
        public ICollection<Image> Images { get; set; }
        [JsonIgnore]
        public int Status { get; set; }
        [JsonIgnore]
        public ICollection<Comments> Comments { get; set; }
    }
}

public enum Type{
    Martyr=1,
    kidnapped=2,
    wounded=3
}
