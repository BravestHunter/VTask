using System;

namespace VTask.Model.DTO.User
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; } = DateTime.Now;
    }
}
