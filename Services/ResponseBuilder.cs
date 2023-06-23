using Microsoft.Azure.Functions.Worker.Http;
using MockHttp.Interfaces;

namespace MockHttp.Services;

internal class ResponseBuilder : IResponseBuilder
{
    private readonly IResponseConfig _responseConfig;

    public ResponseBuilder(IResponseConfig responseConfig)
    {
        _responseConfig = responseConfig;
    }

    public async Task<HttpResponseData> CreateResponseAsync(HttpRequestData request, CancellationToken cancellationToken = default)
    {
        var statusCode = await _responseConfig.GetStatusCode(cancellationToken);
        var response = request.CreateResponse(statusCode);
        var content = await _responseConfig.GetContent(cancellationToken);
        if (content is not null)
        {
            await response.WriteStringAsync(content);
        }

        return response;
    }
}