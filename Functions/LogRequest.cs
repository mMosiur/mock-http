using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace MockHttp.Functions;

internal sealed class LogRequest
{
    public LogRequest()
    {
    }

    [Function(nameof(LogRequest))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "GET", "PUT", "POST", Route = "log-request/{*path}")]
        HttpRequestData request,
        string? path,
        FunctionContext executionContext)
    {
        const string contentFilename = "./content.txt";

        var content = File.Exists(contentFilename) ? await File.ReadAllTextAsync(contentFilename) : string.Empty;

        var response = request.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        await response.WriteStringAsync($"Path was '{path ?? "null"}' and content was '{content}'");
        return response;
    }
}