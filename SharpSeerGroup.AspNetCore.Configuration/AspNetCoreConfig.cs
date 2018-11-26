using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using SharpSeerGroup.Logging;
using System;

namespace SharpSeerGroup.AspNetCore.Configuration
{
    public class AspNetCoreConfig
    {
        public static int Run<TStartup>(string[] args)
            where TStartup : class
        {
            var startupLogger = new LoggerConfiguration()
                .UseHourlyRollingFile()
                .CreateLogger();
            try
            {
                startupLogger.Debug("Configuring Web Host");
                var webHostBuilder = CreateWebHostBuilder<TStartup>(args);
                var webHost = webHostBuilder.Build();

                startupLogger.Information("Starting Web Host");
                webHost.Run();

                startupLogger.Information("Web Host Terminating Normally");

                return 0;
            }
            catch (Exception ex)
            {
                startupLogger.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                startupLogger.Dispose();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder<TStartup>(string[] args)
            where TStartup : class
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<TStartup>()
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    var options = hostingContext.Configuration.GetCommonOptions();
                    options.Environment = env.IsProduction() ? "Production" : env.IsStaging() ? "Staging" : "Development";

                    loggerConfiguration.UseCentralizedStructuredLog(options);
                });
        }
    }
}
