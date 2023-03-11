using System.ComponentModel.DataAnnotations;

namespace VTask.Model.MVC
{
    public class LoginModel
    {
        [Required]
        [StringLength(Constants.User.MaxUsernameLength, MinimumLength = Constants.User.MinUsernameLength)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(Constants.User.MaxPasswordLength, MinimumLength = Constants.User.MinPasswordLength)]
        public string Password { get; set; } = string.Empty;
    }
}
