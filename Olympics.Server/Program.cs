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
            string clientId = args[0];
            string serverId = args[1];
            RunMainAsync(clientId, serverId);
        }

        private static void RunMainAsync(string clientId,string serverId)
        {
            try
            {
                var host = StartSilo(clientId, serverId);
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


        private static async Task<ISiloHost> StartSilo(string clientId, string serverId)
        {
            // define the cluster configuration
            var builder = new SiloHostBuilder()
                //silo集群设置
                .UseLocalhostClustering()
                //集群属性配置
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = clientId;
                    options.ServiceId = serverId;
                })
                //端口配置
                .Configure<EndpointOptions>(
                options =>
                {
                    // Port to use for Silo-to-Silo
                    //options.SiloPort = 11111;
                    // Port to use for the gateway
                    //options.GatewayPort = 30000;
                    // IP Address to advertise in the cluster
                    options.AdvertisedIPAddress = IPAddress.Loopback;//IPAddress.Parse("172.16.0.42");
                    // The socket used for silo-to-silo will bind to this endpoint
                    //options.GatewayListeningEndpoint = new IPEndPoint(IPAddress.Any, 40000);
                    // The socket used by the gateway will bind to this endpoint
                    //options.SiloListeningEndpoint = new IPEndPoint(IPAddress.Any, 50000);

                }
                //options => options.AdvertisedIPAddress = IPAddress.Loopback
                )
                //配置端口
               //.ConfigureEndpoints(siloPort: 11111, gatewayPort: 30000)
                //应用设置
                .ConfigureApplicationParts(parts =>
                {
                    parts.AddApplicationPart(typeof(UserBiz).Assembly).WithReferences();
                    parts.AddApplicationPart(typeof(AccountGrain).Assembly).WithReferences();
                }).
                //增加内存的持久化
                AddMemoryGrainStorageAsDefault()
                //配置日志
                .ConfigureLogging(logging => logging.AddConsole())
                //启用事务
                .UseTransactions();

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
