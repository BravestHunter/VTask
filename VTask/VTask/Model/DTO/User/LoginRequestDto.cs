using System.ComponentModel.DataAnnotations;

namespace VTask.Model.DTO.User
{
    public class LoginRequestDto
    {
        [Required]
        [StringLength(32)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(32)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
