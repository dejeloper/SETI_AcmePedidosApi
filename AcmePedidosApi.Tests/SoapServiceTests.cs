using System.Net;
using AcmePedidosApi.Services;
using FluentAssertions;
using Xunit;

namespace AcmePedidosApi.Tests;

public class SoapServiceTests
{
    private static SoapService CreateService(HttpMessageHandler handler)
    {
        var httpClient = new HttpClient(handler) { BaseAddress = new Uri("https://fake-soap.test") };
        return new SoapService(httpClient, new XmlService());
    }

    [Fact]
    public async Task EnviarPedidoAsync_HappyPath_ReturnsParsedResponse()
    {
        var service = CreateService(new StubHttpMessageHandler(HttpStatusCode.OK, PedidoTestDataFactory.ValidResponseXml));
        var request = PedidoTestDataFactory.ValidRequest();

        var response = await service.EnviarPedidoAsync(request);

        response.Should().BeEquivalentTo(PedidoTestDataFactory.ValidResponse());
    }

    [Fact]
    public async Task EnviarPedidoAsync_HttpError_ThrowsHttpRequestException()
    {
        var service = CreateService(new StubHttpMessageHandler(HttpStatusCode.InternalServerError, string.Empty));
        var request = PedidoTestDataFactory.ValidRequest();

        var action = () => service.EnviarPedidoAsync(request);

        await action.Should().ThrowAsync<HttpRequestException>();
    }

    [Fact]
    public async Task EnviarPedidoAsync_InvalidXmlResponse_ThrowsInvalidOperationException()
    {
        var service = CreateService(new StubHttpMessageHandler(HttpStatusCode.OK, "<not-closed"));
        var request = PedidoTestDataFactory.ValidRequest();

        var action = () => service.EnviarPedidoAsync(request);

        await action.Should().ThrowAsync<InvalidOperationException>();
    }
}
