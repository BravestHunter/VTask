namespace VTask.Model.DTO.User
{
    public class UserChangePasswordRequestDto
    {
        public int Id { get; set; }
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
