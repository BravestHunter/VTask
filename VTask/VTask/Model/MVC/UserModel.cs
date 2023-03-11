using System.ComponentModel.DataAnnotations;
using System;

namespace VTask.Model.MVC
{
    public class UserModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(Constants.User.MaxUsernameLength, MinimumLength = Constants.User.MinUsernameLength)]
        public string Username { get; set; } = Constants.User.DefaultUsername;

        [Required]
        [StringLength(Constants.User.MaxNicknameLength, MinimumLength = Constants.User.MinNicknameLength)]
        public string Nickname { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        [StringLength(Constants.User.MaxEmailLength, MinimumLength = Constants.User.MinEmailLength)]
        public string? Email { get; set; } = null;
    }
}
