namespace VTask.Model.DTO.Auth
{
    public class PasswordResetGetTokenResponseDto
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
