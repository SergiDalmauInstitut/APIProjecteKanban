using APIProjecteKanban.DAL.Persistance;
using MySql.Data.MySqlClient;
using System.Data;

namespace APIProjecteKanban.DAL.Service
{
    public class TaskService
    {
        /// <summary>
        /// Obté totes les tasques d'un projecte
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Retorna una llista de tasques</returns>
        public List<Model.Task> GetAllTasksFromProjectId(int IdProject)
        {
            var result = new List<Model.Task>();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM Task WHERE IdProject = @Id";

                using var command = new MySqlCommand(query, ctx);
                command.Parameters.Add(new MySqlParameter("Id", IdProject));
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(new Model.Task
                    {
                        Id = reader.GetInt64("Id"),
                        Name = reader.GetString("Name"),
                        Description = reader.GetString("Description"),
                        State = reader.GetInt64("State"),
                        Priority = reader.GetString("Priority"),
                        EndDate = reader.IsDBNull("EndDate") ? null : reader.GetDateTime("EndDate"),
                        StartDate = reader.GetDateTime("StartDate"),
                        IdResponsible = reader.GetInt64("IdResponsible"),
                        IdProject = IdProject
                    });
                }
            }
            return result;
        }
        /// <summary>
        /// Afageix una tasca nova dins un projecte
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="task"></param>
        /// <returns>La tasca creada</returns>
        public Model.Task Add(int IdProject, Model.Task task)
        {
            var result = task;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "INSERT INTO Task (Name, Description, Priority, IdResponsible, StartDate, EndDate, IdProject, State) VALUES (@Name, @Description, @Priority, @IdResponsible, @StartDate, @EndDate, @IdProject, @State)";
                using var command = new MySqlCommand(query, ctx);

                command.Parameters.Add(new MySqlParameter("name", result.Name));
                command.Parameters.Add(new MySqlParameter("description", result.Description));
                command.Parameters.Add(new MySqlParameter("Priority", result.Priority));
                command.Parameters.Add(new MySqlParameter("IdResponsible", result.IdResponsible));
                command.Parameters.Add(new MySqlParameter("StartDate", result.StartDate));
                command.Parameters.Add(new MySqlParameter("EndDate", result.EndDate));
                command.Parameters.Add(new MySqlParameter("IdProject", IdProject));
                command.Parameters.Add(new MySqlParameter("State", result.State));

                command.ExecuteNonQuery();

                command.CommandText = "SELECT LAST_INSERT_ID()";
                result.IdProject = IdProject;
                result.Id = (long)(ulong)command.ExecuteScalar();
            }
            return result;
        }
        /// <summary>
        /// Actualitza les dades d'una tasca
        /// </summary>
        /// <param name="id"></param>
        /// <param name="task"></param>
        /// <returns>1: la tasca s'ha modificat correctament
        ///          0: la tasca no s'ha modificat</returns>
        public int Update(int id, Model.Task task)
        {
            int rows_affected = 0;
            if (id == task.IdProject)
            {
                using var ctx = DbContext.GetInstance();

                string query = "UPDATE Task SET name = @name, description = @description, Priority = @Priority, IdResponsible = @IdResponsible, StartDate = @StartDate, EndDate = @EndDate, State = @State  WHERE Id = @Id";
                using var command = new MySqlCommand(query, ctx);

                command.Parameters.Add(new MySqlParameter("name", task.Name));
                command.Parameters.Add(new MySqlParameter("description", task.Description));
                command.Parameters.Add(new MySqlParameter("Priority", task.Priority));
                command.Parameters.Add(new MySqlParameter("IdResponsible", task.IdResponsible));
                command.Parameters.Add(new MySqlParameter("EndDate", task.EndDate));
                command.Parameters.Add(new MySqlParameter("StartDate", task.StartDate));
                command.Parameters.Add(new MySqlParameter("State", task.State));
                command.Parameters.Add(new MySqlParameter("Id", task.Id));

                rows_affected = command.ExecuteNonQuery();
            }

            return rows_affected;
        }

        public int Delete(int Id)
        {
            int rows_affected = 0;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "DELETE FROM Task WHERE Id = @Id";
                using var command = new MySqlCommand(query, ctx);

                command.Parameters.Add(new MySqlParameter("Id", Id));
                rows_affected = command.ExecuteNonQuery();
            }

            return rows_affected;
        }
    }
}
