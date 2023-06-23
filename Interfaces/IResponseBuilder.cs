using Microsoft.Azure.Functions.Worker.Http;

namespace MockHttp.Interfaces;

internal interface IResponseBuilder
{
    Task<HttpResponseData> CreateResponseAsync(HttpRequestData request, CancellationToken cancellationToken = default);
}