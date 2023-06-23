using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureAppConfiguration(builder =>
    {
        var cs = Environment.GetEnvironmentVariable("ConnectionString")!;
        builder.AddAzureAppConfiguration(cs);
    })
    .ConfigureFunctionsWorkerDefaults()
    .Build();

host.Run();
