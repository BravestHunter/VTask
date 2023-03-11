namespace VTask.Model.DTO.Task
{
    public class TaskRemoveResponseDto
    {
        public string Title { get; set; } = Constants.Task.DefaultTitle;
        public string? Description { get; set; } = null;
        public TaskState State { get; set; } = TaskState.Inactive;
    }
}
