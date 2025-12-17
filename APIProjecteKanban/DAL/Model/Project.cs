namespace APIProjecteKanban.DAL.Model
{
    public class Project
    {
        public int Id { get; set; }
        public int IdOwner { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public List<string> StatesList { get; set; } = new List<string>();
        public List<User> UsersList { get; set; } = new List<User>();
        public List<Model.Task> TaskList { get; set; } = new List<Model.Task>();
    }
}
