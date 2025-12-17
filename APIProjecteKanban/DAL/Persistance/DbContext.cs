using MySql.Data.MySqlClient;
namespace APIProjecteKanban.DAL.Persistance
{
    public class DbContext
    {
        public static MySqlConnection GetInstance()
        {
            IConfiguration configuration = GetConfiguration();

            //obtenim la cadena de connexió del fitxer de configuració

            string? dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

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
