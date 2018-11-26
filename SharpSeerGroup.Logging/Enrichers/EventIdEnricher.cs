using Serilog.Core;
using System;
using System.Text;
using Serilog.Events;
using Murmur;
using System.Collections.Generic;

namespace Serilog.Enrichers
{
    public class EventIdEnricher : ILogEventEnricher
    {
        private static readonly object _locker = new object();

        private Dictionary<string, LogEventProperty> _propertyCache = new Dictionary<string, LogEventProperty>();

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var key = logEvent.MessageTemplate.Text;

            if (!_propertyCache.ContainsKey(key))
            {
                lock (_locker)
                {
                    if (!_propertyCache.ContainsKey(key))
                    {
                        var eventId = CreateEventId(key);
                        var newEventProperty = propertyFactory.CreateProperty("EventId", eventId);
                        _propertyCache.Add(key, newEventProperty);
                    }
                }
            }
            
            logEvent.AddPropertyIfAbsent(_propertyCache[key]);
            //logEvent.RemovePropertyIfPresent("MessageTemplate");
        }

        private uint CreateEventId(string messageTemplateText)
        {
            var murmur = MurmurHash.Create32();
            var bytes = Encoding.UTF8.GetBytes(messageTemplateText);
            var hash = murmur.ComputeHash(bytes);
            var numericHash = BitConverter.ToUInt32(hash, 0);
            return numericHash;
        }
    }
}
