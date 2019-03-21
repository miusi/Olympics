using Microsoft.Extensions.Logging;
using Olympics.Interfaces;
using Orleans;
using Orleans.Configuration;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace Olympics.Client
{
    class Program
    {
        private const int initializeAttemptsBeforeFailing = 5;
        private static int attempt = 0;
        static void Main(string[] args)
        {
            //获取client
            //client 干活
            using (var client = StartClient().Result)
            {
                var userBiz = client.GetGrain<IUserBiz>("godric");
                userBiz.Login("godric", "1111");
                Console.ReadKey();
            }

        }

        private static async Task<IClusterClient> StartClient()
        {
            IClientBuilder builder = new ClientBuilder().UseLocalhostClustering()
                .Configure<ClusterOptions>(options=> {
                    options.ClusterId = "dev";
                    options.ServiceId = "Server";
                }).ConfigureLogging(_=>_.AddConsole());
            var client = builder.Build();
            await client.Connect(RetryFilter);
            return client;
        }

        private static async Task<bool> RetryFilter(Exception exception)
        {
            if (exception.GetType() != typeof(SiloUnavailableException))
            {
                Console.WriteLine($"Cluster client failed to connect to cluster with unexpected error.  Exception: {exception}");
                return false;
            }
            attempt++;
            Console.WriteLine($"Cluster client attempt {attempt} of {initializeAttemptsBeforeFailing} failed to connect to cluster.  Exception: {exception}");
            if (attempt > initializeAttemptsBeforeFailing)
            {
                return false;
            }
            await Task.Delay(TimeSpan.FromSeconds(4));
            return true;
        }
    }
}
