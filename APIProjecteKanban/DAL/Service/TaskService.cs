using APIProjecteKanban.DAL.Persistance;
using MySql.Data.MySqlClient;

namespace APIProjecteKanban.DAL.Service
{
    public class TaskService
    {
        /// <summary>
        /// Obté tots els usuaris
        /// </summary>
        /// <returns></returns>
        public List<Model.Task> GetAllTasksFromProjectId(int Id)
        {
            var result = new List<Model.Task>();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM Task WHERE IdProject = @Id";

                using (var command = new MySqlCommand(query, ctx))
                {
                    command.Parameters.Add(new MySqlParameter("Id", Id));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new Model.Task
                            {
                                Id = Convert.ToInt32(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                                Description = reader["Description"].ToString(),
                                State = Convert.ToInt32(reader["State"].ToString()),
                                LastUpdate = Convert.ToDateTime(reader["LastUpdate"]),
                                CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                                IdResponsible = Convert.ToInt32(reader["IdResponsible"].ToString())
                            });
                        }
                    }
                }
            }
            return result;
        }
    }
}
