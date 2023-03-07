using System.ComponentModel.DataAnnotations;

namespace VTask.Model.DTO
{
    public class DeleteUserTaskRequestDto
    {
        [Required]
        public int Id { get; set; }
    }
}
