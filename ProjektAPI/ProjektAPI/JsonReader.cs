
namespace ProjektAPI
{
    static class DBManager
    {
        public static IConfiguration AppSetting { get; }
        static DBManager()
        {
            AppSetting = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("db.json")
                .Build();
        }
    }
}