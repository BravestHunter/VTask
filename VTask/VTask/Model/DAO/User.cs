using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VTask.Model.DAO
{
    [Index(nameof(Username))]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(Constants.User.MinUsernameLength), MaxLength(Constants.User.MaxUsernameLength)]
        public string Username { get; set; } = Constants.User.DefaultUsername;

        [Required]
        public byte[] PasswordHash { get; set; } = new byte[0];

        [Required]
        public byte[] PasswordSalt { get; set; } = new byte[0];

        [MinLength(Constants.User.MinEmailLength), MaxLength(Constants.User.MaxEmailLength)]
        public string? Email { get; set; } = null;

        [MinLength(Constants.User.MinNicknameLength), MaxLength(Constants.User.MaxNicknameLength)]
        public string? Nickname { get; set; } = null;
    }
}
