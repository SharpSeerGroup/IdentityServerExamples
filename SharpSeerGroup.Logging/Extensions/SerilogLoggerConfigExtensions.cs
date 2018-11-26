using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Serilog.Formatting.Json;

namespace SharpSeerGroup.Logging
{
    public static class SerilogLoggerConfigExtensions
    {
        /// <summary>
        /// Configured Serilog to log to a centralized Elasticsearch server, with a fallback of a rotating file.
        /// </summary>
        /// <param name="loggerConfig"></param>
        /// <param name="options"></param>
        /// <returns>LoggerConfiguration</returns>
        public static LoggerConfiguration UseCentralizedStructuredLog(this LoggerConfiguration loggerConfig, LoggerOptions options)
        {
            loggerConfig
                .MinimumLevel.Debug()
                .Enrich.WithEventIds()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("ProcesssName", options.ProcessName)
                .WriteTo.Console();

            if (!string.IsNullOrEmpty(options.Environment))
            {
                loggerConfig.Enrich.WithProperty("Environment", options.Environment);
            }

            if (!string.IsNullOrEmpty(options.ElasticsearchUri))
            {
                var elasticOptions = new ElasticsearchSinkOptions(new Uri(options.ElasticsearchUri))
                {
                    AutoRegisterTemplate = true,
                    InlineFields = true,
                    MinimumLogEventLevel = Serilog.Events.LogEventLevel.Debug
                };
                loggerConfig.WriteTo.Elasticsearch(elasticOptions);
            }
            else
            {
                // by default write to a log file
                loggerConfig.WriteTo.RollingFile(@"logs\hourly\app-{Hour}.log");
                //loggerConfig.WriteTo.File(new JsonFormatter(), "logs\\default.log", fileSizeLimitBytes: 500000000);
            }

            return loggerConfig;
        }

        public static LoggerConfiguration UseHourlyRollingFile(this LoggerConfiguration loggerConfig)
        {
            loggerConfig
                .MinimumLevel.Debug()
                .Enrich.FromLogContext().Enrich.FromLogContext()
                .WriteTo.RollingFile("logs\\startup-{Date}.log");

            return loggerConfig;
        }
    }
}
