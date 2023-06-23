using System.Net;
using System.Web;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using MockHttp.Interfaces;

namespace MockHttp.Functions;

internal sealed class Configure
{
    private readonly IResponseConfig _responseConfig;

    public Configure(IResponseConfig responseConfig)
    {
        _responseConfig = responseConfig;
    }

    [Function(nameof(Configure))]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "configure")]
        HttpRequestData request,
        FunctionContext executionContext)
    {
        var query = HttpUtility.ParseQueryString(request.Url.Query);
        var statusCodeString = query["statusCode"];
        if (statusCodeString is not null)
        {
            if (!int.TryParse(statusCodeString, out var statusCode) || !Enum.IsDefined((HttpStatusCode)statusCode))
            {
                var response = request.CreateResponse(HttpStatusCode.BadRequest);
                await response.WriteStringAsync($"The status code '{statusCodeString}' is not valid.");
                return response;
            }

            await _responseConfig.SetStatusCode((HttpStatusCode)statusCode);
        }

        await _responseConfig.SetContent(request.Body);

        return request.CreateResponse(HttpStatusCode.NoContent);
    }
}