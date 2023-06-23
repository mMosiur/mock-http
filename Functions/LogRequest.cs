using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace MockHttp.Functions;

public static class LogRequest
{
    [Function("LogRequest")]
    public static async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "GET", "PUT", "POST", Route = "log-request/{*path}")]
        HttpRequestData request,
        string? path,
        FunctionContext executionContext)
    {
        var response = request.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

        await response.WriteStringAsync($"Path was '{path ?? "null"}'");

        return response;
    }
}