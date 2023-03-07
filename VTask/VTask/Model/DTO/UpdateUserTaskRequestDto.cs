namespace VTask.Model.DTO
{
    public class UpdateUserTaskRequestDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskState State { get; set; }
    }
}
