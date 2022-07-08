using WorldIndexesComparer.Application.Extensions;
using WorldIndexesComparer.BackgroundServices.Configurations;
using WorldIndexesComparer.Infrastructure.Data.Modules;
using WorldIndexesComparer.Infrastructure.Data.Seeders;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services
            .AddAppServices()
            .AddClientsConfiguration(context.Configuration)
            .AddDataInfrastructure(context.Configuration)
            .AddWorkersConfiguration(context.Configuration);

        WorldDataInitializer.WorldDataContextInitializeAndSeed(services);
    })
    .ConfigureLogging((hostingContext, logging) =>
    {
        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
        logging.AddDebug();
    })
    .Build();

await host.RunAsync();