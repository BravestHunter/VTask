﻿using System.Collections.Generic;

namespace VTask.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];

        public List<UserTask> Tasks { get; set; } = new();
    }
}