using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MockHttp.Interfaces;
using MockHttp.Services;

var host = new HostBuilder()
    .ConfigureAppConfiguration(builder =>
    {
    })
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddScoped<IResponseConfig, ResponseConfig>();
        services.AddScoped<IResponseBuilder, ResponseBuilder>();
    })
    .Build();

host.Run();
