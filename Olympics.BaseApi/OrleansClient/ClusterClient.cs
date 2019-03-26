using Microsoft.Extensions.Logging;
using Olympics.Grains;
using Orleans;
using Orleans.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Olympics.BaseApi.OrleansClient
{
    public class Client
    {
        public Client()
        {
            ClusterClient = BuildClient();
        }

        public IClusterClient ClusterClient { get; set; }


        private IClusterClient BuildClient()
        {
            return new ClientBuilder() 
                //集群设置
                  .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "Server";
                })
            //配置刷新简仓的时间 一般来说不会这么短
            //.Configure<GatewayOptions>(d => d.GatewayListRefreshPeriod = TimeSpan.FromSeconds(5))
            .ConfigureLogging(logging => logging.AddConsole()).Build();

        }

        public async Task Connect(int retries = 0, TimeSpan? delay = null)
        {
            if (ClusterClient.IsInitialized)
            {
                Console.WriteLine("OrleansClient is already initialized!");
                return;
            }
            if (delay == null)
            {
                delay = TimeSpan.FromSeconds(0);
            }

            while (true)
            {
                try
                {
                    await ClusterClient.Connect();
                    return;
                }
                catch (Exception e)
                {
                    if (retries-- == 0)
                    {
                        Console.WriteLine($"\nException while trying to connect to silo: {e.Message}");
                        Console.WriteLine("Make sure the silo the client is trying to connect to is running.");
                        Console.WriteLine("\nPress any key to exit.");
                        Console.ReadKey();
                        throw;
                    }

                    ClusterClient = BuildClient();
                    Console.WriteLine($"\nClient couldn't connect to silo.. retrying in {delay.Value.Seconds} seconds.");
                    await Task.Delay(delay.Value);
                }
            }
        }

    }
}
