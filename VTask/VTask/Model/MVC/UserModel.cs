using System.ComponentModel.DataAnnotations;
using System;

namespace VTask.Model.MVC
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = Constants.User.DefaultUsername;

        public string? Email { get; set; } = null;

        public string? Nickname { get; set; } = null;
    }
}
