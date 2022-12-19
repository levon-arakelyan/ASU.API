using NLog.Web;

namespace ASU.API
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .UseSystemd()
               .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
               .ConfigureLogging(logging =>
               {
                   logging.ClearProviders();
                   logging.SetMinimumLevel(LogLevel.Trace);
               })
               .UseNLog();
    }
}