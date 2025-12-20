using APIProjecteKanban.DAL.Model;
using APIProjecteKanban.DAL.Persistance;
using MySql.Data.MySqlClient;
using System.Text.Json;

namespace APIProjecteKanban.DAL.Service
{
    public class ProjectService
    {
        /// <summary>
        /// Obté tots els projectes d'un usuari a partir de la seva ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>LLista de projectes d'un usuari</returns>
        public List<Project> GetAllProjectsFromUserId(long Id)
        {
            var result = new List<Project>();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM Project p JOIN User_Project pu ON p.Id = pu.IdProject JOIN User u ON u.Id = pu.IdUser WHERE u.Id = @Id";

                using var command = new MySqlCommand(query, ctx);
                command.Parameters.Add(new MySqlParameter("Id", Id));
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Project
                    {
                        Id = reader.GetInt64("Id"),
                        Name = reader.GetString("Name"),
                    });
                }
            }
            return result;
        }
        /// <summary>
        /// Retorna un projecte a partir de la seva ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Projecte</returns>
        public Project GetProjectFromId(long Id)
        {
            var result = new Project();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM Project WHERE Id = @Id";

                using var command = new MySqlCommand(query, ctx);

                command.Parameters.Add(new MySqlParameter("Id", Id));
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result = new Project
                    {
                        Id = reader.GetInt64("Id"),
                        Name = reader.GetString("Name"),
                        IdOwner = reader.GetInt64("Id"),
                        LastUpdate = reader.GetDateTime("LastUpdate"),
                        CreationDate = reader.GetDateTime("CreationDate"),
                        StatesList = JsonSerializer.Deserialize<List<string>>(reader.GetString("StatesList")) ?? [ "To do", "Doing", "Revising", "Done" ]
                    };
                }
            }
            return result;
        }

        public List<User> GetUsersFromProjectId(long IdProject)
        {
            var result = new List<User>();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT u.Id, u.Name, u.Lastname, u.Birthday FROM User_Project up JOIN User u ON u.Id = up.IdUser WHERE IdProject = @IdProject";

                using var command = new MySqlCommand(query, ctx);

                command.Parameters.Add(new MySqlParameter("IdProject", IdProject));
                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add( new()
                    {
                        Id = reader.GetInt64("Id"),
                        Name = reader.GetString("Name"),
                        LastName = reader.GetString("Lastname"),
                        Birthday = reader.GetDateTime("Birthday")
                    });
                }
            }
            return result;
        }

        public Project Add(Project project)
        {
            var result = project;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "INSERT INTO Project (Name, Description, IdOwner, StatesList, CreationDate, LastUpdate) VALUES (@Name, @Description, @IdOwner, @StatesList, @CreationDate, @LastUpdate)";
                using var command = new MySqlCommand(query, ctx);

                command.Parameters.Add(new MySqlParameter("name", result.Name));
                command.Parameters.Add(new MySqlParameter("description", result.Description));
                command.Parameters.Add(new MySqlParameter("IdOwner", result.IdOwner));
                command.Parameters.Add(new MySqlParameter("StatesList", JsonSerializer.Serialize(result.StatesList)));
                command.Parameters.Add(new MySqlParameter("CreationDate", result.CreationDate));
                command.Parameters.Add(new MySqlParameter("LastUpdate", result.LastUpdate));

                command.ExecuteNonQuery();

                command.CommandText = "SELECT LAST_INSERT_ID()";
                result.Id = (long)(ulong)command.ExecuteScalar();
            }
            return result;
        }

        public int AddUserToProject(long IdUser, long IdProject)
        {
            int rows_affected = 0;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "INSERT INTO User_Project (IdProject, IdUser) VALUES (@IdProject, @IdUser)";
                using var command = new MySqlCommand(query, ctx);

                command.Parameters.Add(new MySqlParameter("IdProject", IdProject));
                command.Parameters.Add(new MySqlParameter("IdUser", IdUser));

                rows_affected = command.ExecuteNonQuery();
            }
            return rows_affected;
        }

        public int Delete(int Id)
        {
            int rows_affected = 0;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "DELETE FROM Project WHERE Id = @Id";
                using var command = new MySqlCommand(query, ctx);

                command.Parameters.Add(new MySqlParameter("Id", Id));
                rows_affected = command.ExecuteNonQuery();
            }

            return rows_affected;
        }
    }
}
