namespace VTask.Model.DTO.User
{
    public class UserUpdateRequestDto
    {
        public int Id { get; set; }
        public string NickName { get; set; } = string.Empty;
        public string? Email { get; set; } = null;
    }
}
