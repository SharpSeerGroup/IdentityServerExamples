using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpSeerGroup.Examples.LocalIdentity.Data;

[assembly: HostingStartup(typeof(SharpSeerGroup.Examples.LocalIdentity.Areas.Identity.IdentityHostingStartup))]
namespace SharpSeerGroup.Examples.LocalIdentity.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}