using Common;
using Repository;
using DataBase_Generator;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace DI
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var app = serviceProvider.GetService<Application>();
            Task.Run(() => app.Run()).Wait();

            static void ConfigureServices(IServiceCollection serviceCollection)
            {
                serviceCollection
                   .AddSingleton<IRepository<Person>, PersonRepository>()
                   .AddSingleton<IRepository<Payment>, PaymentRepository>()
                   .AddSingleton<ISerialize<Person>, XMLSerializator<Person>>()
                   .AddSingleton<ISerialize<Payment>, XMLSerializator<Payment>>()
                   .AddSingleton<IDeserialize<Person>, XMLDeserializator<Person>>()
                   .AddSingleton<IDeserialize<Payment>, XMLDeserializator<Payment>>()
                   .AddLogging(configure => configure.AddConsole())
                   .AddTransient<Application>();
            }

        }
    }
    public class Application
    {
        private readonly ILogger<Application> logger;

        public Application(ILogger<Application> logger)
        {
            this.logger = logger;
            this.logger.LogInformation("In Application::ctor");
        }

        public async Task Run()
        {
            logger.LogInformation("Info: In Application::Run");
            logger.LogWarning("Warn: In Application::Run");
            logger.LogError("Error: In Application::Run");
            logger.LogCritical("Critical: In Application::Run");
        }

    }
}
