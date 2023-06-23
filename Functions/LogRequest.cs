using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace MockHttp.Functions;

public static class LogRequest
{
    [Function(nameof(LogRequest))]
    public static async Task<HttpResponseData> Run(
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

public static class SetContent
{
    [Function(nameof(SetContent))]
    public static async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "set-content")] HttpRequestData request,
        FunctionContext executionContext)
    {
        const string contentFilename = "./content.txt";

        await using (var fileStream = File.OpenWrite(contentFilename))
        {
            await request.Body.CopyToAsync(fileStream);
        }
        
        return request.CreateResponse(HttpStatusCode.NoContent);
    }
}