using System;
using Serilog.Configuration;
using Serilog.Enrichers;

namespace Serilog
{

    public static class EventIdLoggerConfigurationExtensions
    {
        public static LoggerConfiguration WithEventIds(
           this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<EventIdEnricher>();
        }
    }
}