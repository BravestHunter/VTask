using System.ComponentModel.DataAnnotations;

namespace VTask.Model.MVC
{
    public class RegisterModel
    {
        [Required]
        [StringLength(Constants.User.MaxUsernameLength, MinimumLength = Constants.User.MinUsernameLength, ErrorMessage = Constants.Format.StringPropertyInvalidLengthMessageFormat)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(Constants.User.MaxPasswordLength, MinimumLength = Constants.User.MinPasswordLength, ErrorMessage = Constants.Format.StringPropertyInvalidLengthMessageFormat)]
        public string Password { get; set; } = string.Empty;
    }
}
