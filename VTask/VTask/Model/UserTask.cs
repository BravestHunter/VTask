using System.ComponentModel.DataAnnotations;

namespace VTask.Model
{
    public class UserTask
    {
        [Key]
        public int Id { get; set; } = 0;

        [Required]
        [StringLength(128)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1024)]
        public string? Description { get; set; } = null;

        [Required]
        public TaskState State { get; set; } = TaskState.Inactive;

        public User? User { get; set; }
    }
}
