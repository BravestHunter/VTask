namespace VTask.Model.DTO.Task
{
    public class AddTaskRequestDto
    {
        public string Title = Constants.Task.DefaultTitle;
        public string? Description = null;
    }
}
