using System.Net;
using MockHttp.Interfaces;

namespace MockHttp.Services;

internal class ResponseConfig : IResponseConfig
{
    private const string ContentFilename = "./content.txt";
    private const string StatusCodeFilename = "./status-code.txt";
    private const HttpStatusCode DefaultStatusCode = HttpStatusCode.OK;

    public async Task<string?> GetContent(CancellationToken cancellationToken = default)
    {
        if (!File.Exists(ContentFilename))
        {
            return null;
        }

        return await File.ReadAllTextAsync(ContentFilename, cancellationToken);
    }

    public async Task SetContent(string? content, CancellationToken cancellationToken = default)
    {
        if (content is null)
        {
            File.Delete(ContentFilename);
            return;
        }

        await File.WriteAllTextAsync(ContentFilename, content, cancellationToken);
    }

    public async Task SetContent(Stream content, CancellationToken cancellationToken = default)
    {
        using var reader = new StreamReader(content, leaveOpen: true);
        var contentText = await reader.ReadToEndAsync(cancellationToken);
        await File.WriteAllTextAsync(ContentFilename, contentText, cancellationToken);
    }

    public async Task<HttpStatusCode> GetStatusCode(CancellationToken cancellationToken = default)
    {
        if (!File.Exists(StatusCodeFilename))
        {
            return DefaultStatusCode;
        }

        var statusCodeText = await File.ReadAllTextAsync(StatusCodeFilename, cancellationToken);
        if (!Enum.TryParse<HttpStatusCode>(statusCodeText, out var statusCode) || !Enum.IsDefined(statusCode))
        {
            return DefaultStatusCode;
        }

        return statusCode;
    }

    public async Task SetStatusCode(HttpStatusCode statusCode, CancellationToken cancellationToken = default)
    {
        if (!Enum.IsDefined(statusCode))
        {
            throw new ArgumentException($"The status code '{statusCode}' is not defined.", nameof(statusCode));
        }

        await File.WriteAllTextAsync(StatusCodeFilename, $"{(int)statusCode}", cancellationToken);
    }
}