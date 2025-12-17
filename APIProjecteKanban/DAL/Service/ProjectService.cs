using APIProjecteKanban.DAL.Model;
using APIProjecteKanban.DAL.Persistance;
using Microsoft.Data.Sqlite;

namespace APIProjecteKanban.DAL.Service
{
    public class ProjectService
    {
        /// <summary>
        /// Obté tots els usuaris
        /// </summary>
        /// <returns></returns>
        public List<Project> GetAllFromUserId(int Id)
        {
            var result = new List<Project>();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT Id, Name FROM Project p JOIN User_Project pu ON p.Id = pu.IdProject JOIN User u ON u.Id = pu.IdUser WHERE u.Id = @Id";

                using (var command = new SqliteCommand(query, ctx))
                {
                    command.Parameters.Add(new SqliteParameter("Id", Id));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Project
                            {
                                Id = Convert.ToInt32(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                            });
                        }
                    }
                }
            }
            return result;
        }
    }
}
