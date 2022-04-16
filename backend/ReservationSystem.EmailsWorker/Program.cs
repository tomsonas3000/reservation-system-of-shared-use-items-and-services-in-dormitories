using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReservationSystem.EmailsWorker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { 
        services.AddHostedService<EmailWorker>();
    })
    .Build();

await host.RunAsync();