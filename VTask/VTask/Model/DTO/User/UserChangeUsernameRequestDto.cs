namespace VTask.Model.DTO.User
{
    public class UserChangeUsernameRequestDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
