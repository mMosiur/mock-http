using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureAppConfiguration(builder =>
    {
    })
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
    })
    .Build();

host.Run();
