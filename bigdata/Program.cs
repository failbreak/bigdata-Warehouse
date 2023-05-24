using Dal;
using bigdata;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddDbContext<BigdataContext>(x => x.UseSqlServer(hostContext.Configuration.GetConnectionString("BigData")));
    })
    .Build();

host.Run();
