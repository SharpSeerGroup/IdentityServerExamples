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

namespace SharpSeerGroup.Examples.WebApi
{
    public class Program
    {
        /// <summary>
        /// AspNetCore app startup functionality exists in a separate project
        /// to make it easier to reuse the common startup all Web API projects
        /// will utilize.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Exitcode, 0 for success</returns>
        public static int Main(string[] args)
        {
            return AspNetCoreConfig.Run<Startup>(args);
        }
    }
}
