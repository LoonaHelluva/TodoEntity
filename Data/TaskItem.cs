using System.ComponentModel.DataAnnotations;

namespace ToDOEntity
{
    public class TaskItem
    {
        public int Id { get; set; }
        [Required] public string Title { get; set; } = string.Empty;
        public bool IsDone { get; set; }
    }
}