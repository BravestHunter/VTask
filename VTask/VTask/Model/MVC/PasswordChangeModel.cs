using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VTask.Model.MVC
{
    public class PasswordChangeModel
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Old")]
        public string OldPassword { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(Constants.User.MinPasswordLength), MaxLength(Constants.User.MaxPasswordLength)]
        [DisplayName("New")]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]
        [DisplayName("Confirm")]
        public string PasswordConfirm { get; set; } = string.Empty;
    }
}
