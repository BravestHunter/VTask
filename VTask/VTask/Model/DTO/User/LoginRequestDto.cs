﻿using System.ComponentModel.DataAnnotations;

namespace VTask.Model.DTO.User
{
    public class LoginRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
