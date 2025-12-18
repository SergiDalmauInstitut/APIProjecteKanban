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

                using var command = new MySqlCommand(query, ctx);
                command.Parameters.Add(new MySqlParameter("Id", Id));
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Model.Task
                    {
                        Id = reader.GetInt64("Id"),
                        Name = reader.GetString("Name"),
                        Description = reader.GetString("Description"),
                        State = reader.GetInt64("State"),
                        LastUpdate = reader.GetDateTime("LastUpdate"),
                        CreationDate = reader.GetDateTime("CreationDate"),
                        IdResponsible = reader.GetInt64("IdResponsible")
                    });
                }
            }
            return result;
        }

        public Model.Task Add(int Id, Model.Task task)
        {
            var result = task;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "INSERT INTO Task (Name, Description, Priority, IdResponsible, CreationDate, LastUpdate, IdProject, State) VALUES (@Name, @Description, @Priority, @IdResponsible, @CreationDate, @LastUpdate, @IdProject, @State)";
                using (var command = new MySqlCommand(query, ctx))
                {
                    command.Parameters.Add(new MySqlParameter("name", result.Name));
                    command.Parameters.Add(new MySqlParameter("description", result.Description));
                    command.Parameters.Add(new MySqlParameter("priority", result.Priority));
                    command.Parameters.Add(new MySqlParameter("IdResponsible", result.IdResponsible));
                    command.Parameters.Add(new MySqlParameter("CreationDate", result.CreationDate));
                    command.Parameters.Add(new MySqlParameter("LastUpdate", result.LastUpdate));
                    command.Parameters.Add(new MySqlParameter("IdProject", Id));
                    command.Parameters.Add(new MySqlParameter("State", result.State));

                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT LAST_INSERT_ID()";
                    result.IdProject = Id;
                    result.Id = (long)(ulong)command.ExecuteScalar();
                }
            }
            return result;
        }

        public int Update(int id, Model.Task task)
        {
            int rows_affected = 0;
            if (id == task.IdProject)
            {
                task.LastUpdate = DateTime.Now;
                using var ctx = DbContext.GetInstance();

                string query = "UPDATE Task SET name = @name, description = @description, priority = @priority, IdResponsible = @IdResponsible, LastUpdate = @LastUpdate, State = @State  WHERE Id = @Id";
                using var command = new MySqlCommand(query, ctx);
                command.Parameters.Add(new MySqlParameter("name", task.Name));
                command.Parameters.Add(new MySqlParameter("description", task.Description));
                command.Parameters.Add(new MySqlParameter("priority", task.Priority));
                command.Parameters.Add(new MySqlParameter("IdResponsible", task.IdResponsible));
                command.Parameters.Add(new MySqlParameter("LastUpdate", task.LastUpdate));
                command.Parameters.Add(new MySqlParameter("State", task.State));
                command.Parameters.Add(new MySqlParameter("Id", task.Id));

                rows_affected = command.ExecuteNonQuery();
            }

            return rows_affected;
        }
    }
}
