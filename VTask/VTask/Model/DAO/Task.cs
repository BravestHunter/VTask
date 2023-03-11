using System.ComponentModel.DataAnnotations;

namespace VTask.Model.DAO
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [MinLength(Constants.Task.MinTitleLength), MaxLength(Constants.Task.MaxTitleLength)]
        public string Title { get; set; } = Constants.Task.DefaultTitle;

        [MinLength(Constants.Task.MinDescriptionLength), MaxLength(Constants.Task.MaxDescriptionLength)]
        public string? Description { get; set; } = null;

        public TaskState State { get; set; } = TaskState.Inactive;
    }
}
