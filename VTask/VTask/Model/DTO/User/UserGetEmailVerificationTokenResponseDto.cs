namespace VTask.Model.DTO.User
{
    public class UserGetEmailVerificationTokenResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
