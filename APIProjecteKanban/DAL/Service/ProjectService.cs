using APIProjecteKanban.DAL.Model;
using APIProjecteKanban.DAL.Persistance;
using MySql.Data.MySqlClient;

namespace APIProjecteKanban.DAL.Service
{
    public class ProjectService
    {
        /// <summary>
        /// Obté tots els projectes d'un usuari a partir de la seva ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>LLista de projectes d'un usuari</returns>
        public List<Project> GetAllProjectsFromUserId(int Id)
        {
            var result = new List<Project>();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * Project p JOIN User_Project pu ON p.Id = pu.IdProject JOIN User u ON u.Id = pu.IdUser WHERE u.Id = @Id";

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
        public Project GetProjectFromId(int Id)
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
                        StatesList = GetStatesList(reader.GetString("StatesList"))
                    };
                }
            }
            return result;
        }
        /// <summary>
        /// Separa els estats de les tasques, originalment en una string separats per comes, i els posa dins una llista
        /// </summary>
        /// <param name="states"></param>
        /// <returns>LLista dels estats</returns>
        private static List<string> GetStatesList(string states)
        {

            string[] statesArray = states.Split(',');

            List<string> result = statesArray
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();

            return result;
        }
    }
}
