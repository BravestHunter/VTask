using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VTask.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public byte[] PasswordHash { get; set; } = new byte[0];
        [Required]
        public byte[] PasswordSalt { get; set; } = new byte[0];
    }
}
