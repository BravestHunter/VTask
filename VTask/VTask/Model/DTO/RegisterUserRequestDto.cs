using System.ComponentModel.DataAnnotations;

namespace VTask.Model.DTO
{
    public class RegisterUserRequestDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
