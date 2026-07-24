using System.Net;
using System.Text;

namespace AcmePedidosApi.Tests;

internal sealed class StubHttpMessageHandler(HttpStatusCode statusCode, string content, string mediaType = "text/xml") : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(content, Encoding.UTF8, mediaType)
        };

        return Task.FromResult(response);
    }
}
