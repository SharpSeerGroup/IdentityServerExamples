using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpSeerGroup.Logging
{
    public class LoggerOptions
    {
        public static readonly string SectionKeyName = "Logger";

        public string ProcessName { get; set; }
        public string ElasticsearchUri { get; set; }
        public string Environment { get; set; }

        public LoggerOptions()
        {
            ElasticsearchUri = "http://localhost:9200";

            ProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            if (ProcessName.Equals("dotnet", StringComparison.OrdinalIgnoreCase))
            {
                ProcessName = System.IO.Path.GetFileName(System.IO.Directory.GetCurrentDirectory());
            }
        }
    }

    public static class IConfigurationLoggerOptionsExtensions
    {
        public static LoggerOptions GetCommonOptions(this IConfiguration configuration)
        {
            return configuration.GetSection(LoggerOptions.SectionKeyName).Get<LoggerOptions>() ?? new LoggerOptions();
        }
    }
}
