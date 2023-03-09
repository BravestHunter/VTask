using Microsoft.Build.Framework;
using Microsoft.Identity.Client;

namespace VTask.Model.DTO.User
{
    public class RegisterResponseDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
