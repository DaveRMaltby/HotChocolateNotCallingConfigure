using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using System.Threading.Tasks;

namespace HotChocolateNotCallingConfigure;

public class Program
{
    static async Task<int> Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
        return 0;
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureLogging(configureLogging => configureLogging.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Information))
            .ConfigureServices((hostContext, services) =>
            {
            })
            .UseWindowsService()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

