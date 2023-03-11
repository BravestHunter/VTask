namespace VTask.Model.DTO.Task
{
    public class TaskAddRequestDto
    {
        public string Title = Constants.Task.DefaultTitle;
        public string? Description = null;
    }
}
