using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Orleans;
using Microsoft.Extensions.DependencyInjection;

namespace Olympics.Base
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var client = new OrleansClient.Client();
            client.Connect(3).Wait();
            CreateWebHostBuilder(args,client).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, OrleansClient.Client client) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().ConfigureServices(services=>services.AddSingleton<IClusterClient>(client.ClusterClient));
    }
}
