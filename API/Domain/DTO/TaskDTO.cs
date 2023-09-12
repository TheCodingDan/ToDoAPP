namespace API.Domain.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name{ get; set; }
        public string Category { get; set; }
        public string Priority { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime Deadline { get; set; } 

    }
}