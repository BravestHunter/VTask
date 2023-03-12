using System.ComponentModel.DataAnnotations;

namespace VTask.Model.DTO.Auth
{
    public class RegisterRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
