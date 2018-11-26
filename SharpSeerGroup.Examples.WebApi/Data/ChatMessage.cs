using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpSeerGroup.Examples.WebApi.Data
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public DateTime Sent { get; set; } = DateTime.UtcNow;
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }

    }
}
