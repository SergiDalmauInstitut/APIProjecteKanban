using DotNetEnv;

namespace APIProjecteKanban
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // busquem l'arxiu .env i el carreguem a l'app.
            string root = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));
            Env.Load(Path.Combine(root, ".env"));

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
