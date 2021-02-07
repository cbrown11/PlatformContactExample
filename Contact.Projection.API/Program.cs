using CrossCutting.NServiceBus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Serilog;
using System;
using System.IO;

namespace Contact.Projection.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var endpoint = "Contact.Projection.API";
            Console.Title = endpoint;


            CreateHostBuilder(args, endpoint).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, string endpoint)
        {
            var configuration = BuildConfigurationBuilder();
            var logger = LoggerConfiguration.CreateLogger(configuration, endpoint);
            LogManager.Use<SerilogFactory>().WithLogger(logger);
            return Host.CreateDefaultBuilder(args)
            .UseNServiceBus(context =>
            {
                    var endpointConfiguration = new EndpointConfiguration(endpoint);
                    var serilogTracing = endpointConfiguration.EnableSerilogTracing(logger);
                    serilogTracing.EnableMessageTracing();
                    endpointConfiguration.ApplyCustomConventions();
                    var transport = endpointConfiguration.UseTransport<LearningTransport>();
                //    transport.Routing().RouteToEndpoint(typeof(CreateContact), endpoint);

                    return endpointConfiguration;
            })
            .ConfigureWebHostDefaults(webBuilder =>
                    {
                    webBuilder.UseStartup<Startup>();
                    });
        }

        private static IConfigurationRoot BuildConfigurationBuilder() =>
                     new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile("appsettings.json")
                         .Build();

    }
}
