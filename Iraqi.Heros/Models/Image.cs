﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Iraqi.Heros.Models
{
    public class Image
    {
        [Key]
        [JsonIgnore]
        public Guid Id { get; set; }
        [JsonIgnore]
        public string Path { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        [JsonIgnore]
        public Guid PersonId { get; set; }
        [JsonIgnore]
        [NotMapped]
        [ForeignKey(nameof(PersonId))]
        public Person Person { get; set; }
    }
}