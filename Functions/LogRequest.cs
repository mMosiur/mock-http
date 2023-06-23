using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using MockHttp.Interfaces;

namespace MockHttp.Functions;

internal sealed class LogRequest
{
    private readonly IResponseBuilder _responseBuilder;

    public LogRequest(IResponseBuilder responseBuilder)
    {
        _responseBuilder = responseBuilder;
    }

    [Function(nameof(LogRequest))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "GET", "POST", "PUT", "PATCH", "DELETE", "HEAD", "OPTIONS", Route = "log-request/{*path}")]
        HttpRequestData request,
        string? path,
        FunctionContext executionContext)
    {
        return await _responseBuilder.CreateResponseAsync(request);
    }
}