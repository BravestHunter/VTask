using Microsoft.Build.Framework;

namespace VTask.Model.MVC
{
    public class DeleteTaskModel
    {
        [Required]
        public int Id { get; set; }
    }
}
