using System.ComponentModel.DataAnnotations;

namespace ToDOEntity
{
    public class Task
    {
        public int Id { get; set; }
        [Required] public string Title { get; set; }
        public bool IsDone { get; set; }
    }
}