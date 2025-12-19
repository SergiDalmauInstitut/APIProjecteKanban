namespace APIProjecteKanban.DAL.Model
{
    public class Project
    {
        public long Id { get; set; }
        public long IdOwner { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public List<string> StatesList { get; set; } = ["To do", "Doing", "Revising", "Done"];
        public List<User> UsersList { get; set; } = [];
        public List<Model.Task> TaskList { get; set; } = [];
    }
}
