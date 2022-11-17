
namespace ProjektAPI
{
    static class ConfigurationManager
    {
        public static IConfiguration AppSetting { get; }
        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("db.json")
                .Build();
        }
    }
}