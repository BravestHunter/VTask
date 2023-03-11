using System.ComponentModel.DataAnnotations;

namespace VTask.Model.DTO.User
{
    public class UserGetResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = Constants.User.DefaultUsername;
        public string Nickname { get; set; } = Constants.User.DefaultUsername;
        public string? Email { get; set; } = null;
    }
}
