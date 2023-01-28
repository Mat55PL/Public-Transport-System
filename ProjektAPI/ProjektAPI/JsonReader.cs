
namespace ProjektAPI
{
    static class DbManager
    {
        public static IConfiguration AppSetting { get; }
        static DbManager()
        {
            AppSetting = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("db.json")
                .Build();
        }
    }
}