using System.ComponentModel.DataAnnotations;

namespace VTask.Model.MVC
{
    public class PasswordResetModel
    {
        [Required]
        public string Token { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(
            Constants.User.MaxPasswordLength,
            MinimumLength = Constants.User.MinPasswordLength,
            ErrorMessage = Constants.Format.StringPropertyInvalidLengthMessageFormat
        )]
        public string NewPassword { get; set; } = string.Empty;
    }
}
