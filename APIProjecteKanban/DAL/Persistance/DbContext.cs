using Microsoft.Data.Sqlite;
namespace APIProjecteKanban.DAL.Persistance
{
    public class DbContext
    {
        public static SqliteConnection GetInstance()
        {
            IConfiguration configuration = GetConfiguration();

            //obtenim la cadena de connexió del fitxer de configuració
            string? connectionString = configuration.GetSection("ConnectionStrings").GetSection("SQLite").Value;

            var db = new SqliteConnection(
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
