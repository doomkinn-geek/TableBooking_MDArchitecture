using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Restaurant.Kitchen.Consumers;

namespace Restaurant.Kitchen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<KitchenBookingRequestedConsumer>(
                            configurator =>
                            {
                                
                            })
                            .Endpoint(e =>
                            {
                                e.Temporary = true;
                            }); ;

                        x.AddConsumer<KitchenBookingRequestFaultConsumer>()
                            .Endpoint(e =>
                            {
                                e.Temporary = true;
                            });
                        x.AddDelayedMessageScheduler();

                        x.UsingRabbitMq((context, cfg) =>
                        {
                            cfg.UseDelayedMessageScheduler();
                            cfg.UseInMemoryOutbox();
                            cfg.ConfigureEndpoints(context);
                        });
                    });

                    services.AddSingleton<Manager>();

                    services.AddMassTransitHostedService(true);
                });
    }
}