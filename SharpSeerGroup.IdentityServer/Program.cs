using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SharpSeerGroup.AspNetCore.Configuration;

namespace SharpSeerGroup.IdentityServer
{
    public class Program
    {
        public static int Main(string[] args)
        {
            return AspNetCoreConfig.Run<Startup>(args);
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }
    }
}
