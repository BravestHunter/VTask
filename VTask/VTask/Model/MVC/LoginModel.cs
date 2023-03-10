using System.ComponentModel.DataAnnotations;

namespace VTask.Model.MVC
{
    public class LoginModel
    {
        [Required]
        [MinLength(Constants.User.MinUsernameLength), MaxLength(Constants.User.MaxUsernameLength)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(Constants.User.MinPasswordLength), MaxLength(Constants.User.MaxPasswordLength)]
        public string Password { get; set; } = string.Empty;
    }
}
