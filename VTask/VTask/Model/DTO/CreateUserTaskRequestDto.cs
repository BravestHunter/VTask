using System.ComponentModel.DataAnnotations;

namespace VTask.Model.DTO
{
    public class CreateUserTaskRequestDto
    {
        [Required]
        [StringLength(128)]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; } = null;
    }
}
