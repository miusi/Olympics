using Microsoft.Extensions.Logging;
using Olympics.Grains;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Olympics.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            RunMainAsync();
        }

        private static void RunMainAsync()
        {
            try
            {
                var host = StartSilo();
                bool isExit = true;
                while (isExit)
                {
                    string read = Console.ReadLine();
                    if (read == "Exit")
                    {
                        isExit = false;
                        host.Result.StopAsync();
                    }
                } 
            }
            catch (Exception ex)
            {
               
            }
        }


        private static async Task<ISiloHost> StartSilo()
        {
            // define the cluster configuration
            var builder = new SiloHostBuilder()
                //silo集群设置
                .UseLocalhostClustering()
                //集群属性配置
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "Server";
                })
                //端口配置
                .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                //应用设置
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(UserBiz).Assembly).WithReferences())
                //配置日志
                .ConfigureLogging(logging => logging.AddConsole());

            var host = builder.Build();
            await host.StartAsync();
            //Console.WriteLine("网关启动成功");
            //await StartAgentHost();
            return host;
        }

        private static async Task<ISiloHost> StartAgentHost()
        {
            var builder = new SiloHostBuilder()
                .UseDevelopmentClustering(new IPEndPoint(IPAddress.Loopback, 11111))
                .ConfigureEndpoints(GetInternalIp(), siloPort: 11111, gatewayPort: 30000)
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "Server";
                })
                 .ConfigureLogging(logging => logging.AddConsole());

            var host = builder.Build();
            await host.StartAsync(); 
            Console.WriteLine("代理网关启动成功");
            return host;
        }

        public static IPAddress GetInternalIp()
        {
            IPHostEntry myEntry = Dns.GetHostEntry(Dns.GetHostName());
            return myEntry.AddressList.FirstOrDefault(e => e.AddressFamily.ToString().Equals("InterNetwork"));
        }
    }
}
