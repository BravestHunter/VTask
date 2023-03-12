using Microsoft.Build.Framework;
using Microsoft.Identity.Client;

namespace VTask.Model.DTO.Auth
{
    public class RegisterResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}
