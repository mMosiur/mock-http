using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace MockHttp.Functions;

internal sealed class SetContent
{
    public SetContent()
    {
    }

    [Function(nameof(SetContent))]
    public async Task<HttpResponseData> Run(
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