namespace Thinksoft.crudTutorial.Helper
{
    public class DbHelper
    {
        public static string GetConnectionString(string DbConnetionString)
        {
            var Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            return Configuration["ConnectionStrings:" + DbConnetionString];
        }
    }
}
