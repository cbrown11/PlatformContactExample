using Contact.Messages.Commands;
using CrossCutting.NServiceBus;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NServiceBus;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Serilog;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Platform.GatewayAPI
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            var endpoint = "Platformm.GatewayAPI";
            Console.Title = endpoint;
            var configuration = BuildConfigurationBuilder();
            var logger = LoggerConfiguration.CreateLogger(configuration, endpoint);
            LogManager.Use<SerilogFactory>().WithLogger(logger);
            return Host
                        .CreateDefaultBuilder(args)
                        .UseNServiceBus(context =>
                        {
                            var endpointConfiguration = new EndpointConfiguration(endpoint);
                            var serilogTracing = endpointConfiguration.EnableSerilogTracing(logger);
                            serilogTracing.EnableMessageTracing();
                            endpointConfiguration.ApplyCustomConventions();
                            var transport = endpointConfiguration.UseTransport<LearningTransport>();
                            transport.Routing().RouteToEndpoint(typeof(CreateContact), "Contact.Service");

                            return endpointConfiguration;
                        })
                        .ConfigureWebHostDefaults(builder =>
                        builder.UseStartup<Startup>()
                        )
                        .Build()
                        .RunAsync();
        }

        private static IConfigurationRoot BuildConfigurationBuilder() =>
                    new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();
    }
}
