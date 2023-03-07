namespace VTask.Model
{
    public class UserTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = null;
        public TaskState State { get; set; } = TaskState.New;

        public User User { get; set; }
    }
}
