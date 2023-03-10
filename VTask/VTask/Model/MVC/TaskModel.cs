using System.ComponentModel.DataAnnotations;

namespace VTask.Model.MVC
{
    public class TaskModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(Constants.Task.MaxTitleLength, MinimumLength = Constants.Task.MinTitleLength)]
        public string Title { get; set; } = string.Empty;


        [StringLength(Constants.Task.MaxDescriptionLength, MinimumLength = Constants.Task.MinDescriptionLength)]
        public string? Description { get; set; } = null;

        public TaskState State { get; set; } = TaskState.Inactive;
    }
}
