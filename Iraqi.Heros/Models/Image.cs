using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Iraqi.Heros.Models
{
    public class Image
    {
        [Key]
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public Guid PersonId { get; set; }
        [JsonIgnore]
        [NotMapped]
        [ForeignKey(nameof(PersonId))]
        public Person Person { get; set; }
    }
}