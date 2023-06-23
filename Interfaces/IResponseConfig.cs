using System.Net;

namespace MockHttp.Interfaces;

internal interface IResponseConfig
{
    Task<string?> GetContent(CancellationToken cancellationToken = default);
    Task SetContent(string? content, CancellationToken cancellationToken = default);
    Task SetContent(Stream content, CancellationToken cancellationToken = default);
    
    Task<HttpStatusCode> GetStatusCode(CancellationToken cancellationToken = default);
    Task SetStatusCode(HttpStatusCode statusCode, CancellationToken cancellationToken = default);
}