namespace APIProjecteKanban.DAL.Model
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
