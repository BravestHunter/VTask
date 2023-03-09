﻿using System.ComponentModel.DataAnnotations;

namespace VTask.Model.DTO.User
{
    public class RegisterRequestDto
    {
        [Required]
        [StringLength(32)]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(32)]
        [MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}