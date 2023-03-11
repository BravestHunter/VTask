namespace VTask.Model.DTO.User
{
    public class UserUpdateRequestDto
    {
        public int Id { get; set; }
        public string? Email { get; set; } = null;
        public string NickName { get; set; } = string.Empty;
    }
}
