

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Iraqi.Heros.Models
{
    public class Report
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [JsonIgnore]
        public Guid PersonId { get; set; }
        [JsonIgnore]
        public DateTime CreateDate { get; set; }
        [Required]
        public string Note { get; set; }
        [ForeignKey(nameof(PersonId))]
        [JsonIgnore]
        public Person Person { get; set; }
        public int Status { get; set; }
    }
}