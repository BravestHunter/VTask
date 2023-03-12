using System.ComponentModel.DataAnnotations;

namespace VTask.Model.MVC
{
    public class UsernameChangeModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(Constants.User.MaxUsernameLength, MinimumLength = Constants.User.MinUsernameLength)]
        public string Username { get; set; } = string.Empty;
    }
}
