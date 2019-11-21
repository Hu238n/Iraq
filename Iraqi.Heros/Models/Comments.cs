using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Iraqi.Heros.Models
{
    public class Comments

    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Comment { get; set; }
        [JsonIgnore]
        public Guid PersonId { get; set; }
        [JsonIgnore]
        public DateTime CommentDate { get; set; }
        [ForeignKey(nameof(PersonId))]
        [JsonIgnore]
        public Person Person { get; set; }
    }
}
