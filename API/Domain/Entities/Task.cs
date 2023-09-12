using System.ComponentModel.DataAnnotations.Schema;

namespace API.Domain.Entities
{
    [Table("T_TASKS")]
    public class Task
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime Deadline { get; set; }

    }
}