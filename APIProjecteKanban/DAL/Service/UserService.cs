using APIProjecteKanban.DAL.Model;
using APIProjecteKanban.DAL.Persistance;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace APIProjecteKanban.DAL.Service
{
    public class UserService
    {
        /// <summary>
        /// Obté tots els usuaris
        /// </summary>
        /// <returns></returns>
        public List<User> GetAll()
        {
            var result = new List<User>();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM User";

                using (var command = new MySqlCommand(query, ctx))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new User
                            {
                                Id = Convert.ToInt32(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                                LastName = reader["Lastname"].ToString(),
                                Birthday = Convert.ToDateTime(reader["Birthday"])
                            });
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Obté les dades de l'usuari indicat
        /// </summary>
        /// <param name="Id">Identificador d'usuari</param>
        /// <returns>Dades de l'Usuari</returns>
        public User GetById(int Id)
        {
            User user = new User();

            using (var ctx = DbContext.GetInstance())
            {
                var query = "SELECT * FROM User WHERE Id = @Id";
                using (var command = new MySqlCommand(query, ctx))
                {
                    command.Parameters.Add(new MySqlParameter("Id", Id));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new User()
                            {
                                Id = Convert.ToInt32(reader["id"].ToString()),
                                Name = reader["Name"].ToString(),
                                LastName = reader["Lastname"].ToString(),
                                Birthday = Convert.ToDateTime(reader["Birthday"])
                            };
                        }
                    }
                }
            }
            return user;
        }

        /// <summary>
        /// Afegeix un nou usuari a la base de dades
        /// </summary>
        /// <param name="user">Entitat usuari</param>
        /// <returns>Id de l'usuari afegit</returns>
        public User Add(User user)
        {
            using (var ctx = DbContext.GetInstance())
            {
                string query = "INSERT INTO User (name, lastname, birthday, role, email, password) VALUES (@name, @lastname, @birthday, @role, @email, @password)";
                using (var command = new MySqlCommand(query, ctx))
                {
                    command.Parameters.Add(new MySqlParameter("name", user.Name));
                    command.Parameters.Add(new MySqlParameter("lastname", user.LastName));
                    command.Parameters.Add(new MySqlParameter("birthday", user.Birthday));
                    command.Parameters.Add(new MySqlParameter("role", user.Role));
                    command.Parameters.Add(new MySqlParameter("email", user.Email));

                    command.Parameters.Add(new MySqlParameter("password", HashPassword(user.Password)));

                    command.ExecuteNonQuery();

                    command.CommandText = "SELECT last_insert_rowid()";

                    user.Id = (Int64)command.ExecuteScalar();
                }
            }

            return user;
        }

        /// <summary>
        /// Actualitza un usuari
        /// </summary>
        /// <param name="user">Entitat usuari que es vol modificar</param>
        /// <returns>Files afectades</returns>
        public int Update(User user)
        {
            int rows_affected = 0;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "UPDATE User SET name = @name, lastname = @lastname, birthday = @birthday WHERE Id = @Id";
                using (var command = new MySqlCommand(query, ctx))
                {
                    command.Parameters.Add(new MySqlParameter("name", user.Name));
                    command.Parameters.Add(new MySqlParameter("lastname", user.LastName));
                    command.Parameters.Add(new MySqlParameter("birthday", user.Birthday));
                    command.Parameters.Add(new MySqlParameter("role", user.Role));
                    command.Parameters.Add(new MySqlParameter("email", user.Email));

                    command.Parameters.Add(new MySqlParameter("password", HashPassword(user.Password)));

                    rows_affected = command.ExecuteNonQuery();
                }
            }

            return rows_affected;
        }

        /// <summary>
        /// Elimina un usuari
        /// </summary>
        /// <param name="Id">Codi d'usuari que es vol eliminar</param>
        /// <returns>Files afectades</returns>
        public int Delete(int Id)
        {
            int rows_affected = 0;
            using (var ctx = DbContext.GetInstance())
            {
                string query = "DELETE FROM User WHERE Id = @Id";
                using (var command = new MySqlCommand(query, ctx))
                {
                    command.Parameters.Add(new MySqlParameter("Id", Id));
                    rows_affected = command.ExecuteNonQuery();
                }
            }

            return rows_affected;
        }

        private string HashPassword(string pass)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                StringBuilder hash = new StringBuilder();
                byte[] hashArray = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
                foreach (byte b in hashArray)
                {
                    hash.Append(b.ToString("x2"));
                }
                return hash.ToString();
            }
        }
    }
}
