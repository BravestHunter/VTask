namespace VTask.Model.DTO.Task
{
    public class TaskUpdateRequestDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = Constants.Task.DefaultTitle;
        public string? Description { get; set; } = null;
        public TaskState State { get; set; } = TaskState.Inactive;
    }
}
