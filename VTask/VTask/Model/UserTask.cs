namespace VTask.Model
{
    public class UserTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskState State { get; set; } = TaskState.New;
    }
}
