using System;
using System.ComponentModel.DataAnnotations;

namespace Iraqi.Heros.Models
{
    public class Users
    {
        [Key]
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}