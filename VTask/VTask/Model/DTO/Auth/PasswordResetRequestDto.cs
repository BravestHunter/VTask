namespace VTask.Model.DTO.Auth
{
    public class PasswordResetRequestDto
    {
        public string Token { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
