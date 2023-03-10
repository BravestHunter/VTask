using Microsoft.Build.Framework;
using Microsoft.Identity.Client;

namespace VTask.Model.DTO.User
{
    public class RegisterResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
    }
}
