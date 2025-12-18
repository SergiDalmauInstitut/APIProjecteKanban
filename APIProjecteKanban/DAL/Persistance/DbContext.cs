using MySql.Data.MySqlClient;
namespace APIProjecteKanban.DAL.Persistance
{
    public class DbContext
    {
        public static MySqlConnection GetInstance()
        {
            IConfiguration configuration = GetConfiguration();

            //obtenim la contrasenya de la base de dades de l'arxiu .env
            //(l'arxiu .env no es publica a GitHub per raons de seguretat, però es pot afegir després de descarregar el projecte)
            //la contrasenya es la default.
            string? dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

            //obtenim la cadena de connexió del fitxer de configuració
            string? connectionString = configuration.GetSection("ConnectionStrings").GetSection("MySQL").Value + $"Password={dbPassword};";

            var db = new MySqlConnection(
               string.Format(connectionString)
            );

            db.Open();

            return db;
        }

        /// <summary>
        /// Per agafar les dades del fitxer de configuració appsettings.json
        /// </summary>
        /// <returns></returns>
        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
    }
}
