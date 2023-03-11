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

        [MinLength(Constants.User.MinUsernameLength), MaxLength(Constants.User.MaxUsernameLength)]
        public string Username { get; set; } = Constants.User.DefaultUsername;

        public byte[] PasswordHash { get; set; } = new byte[0];

        public byte[] PasswordSalt { get; set; } = new byte[0];

        [MinLength(Constants.User.MinNicknameLength), MaxLength(Constants.User.MaxNicknameLength)]
        public string Nickname { get; set; } = string.Empty;

        [MinLength(Constants.User.MinEmailLength), MaxLength(Constants.User.MaxEmailLength)]
        public string? Email { get; set; } = null;

        public bool EmailConfirmed { get; set; } = false;
    }
}
