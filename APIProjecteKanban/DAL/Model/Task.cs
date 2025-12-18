namespace APIProjecteKanban.DAL.Model
{
    public class Task
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public long IdResponsible { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdate { get; set; }
        public long IdProject { get; set; }
        public long State { get; set; }
    }
}
