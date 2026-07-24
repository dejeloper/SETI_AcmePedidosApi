using AcmePedidosApi.Controllers;
using AcmePedidosApi.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AcmePedidosApi.Tests;

public class PedidoControllerTests
{
    private readonly Mock<ISoapService> _soapMock = new();
    private readonly SoapMockService _mockService = new(new XmlService());

    private PedidoController CreateController() => new(_soapMock.Object, _mockService);

    [Fact]
    public async Task EnviarPedido_HappyPath_ReturnsOkWithTheResponse()
    {
        var request = PedidoTestDataFactory.ValidRequest();
        var expectedResponse = PedidoTestDataFactory.ValidResponse();
        _soapMock.Setup(s => s.EnviarPedidoAsync(request)).ReturnsAsync(expectedResponse);

        var result = await CreateController().EnviarPedido(request);

        var ok = result.Should().BeOfType<OkObjectResult>().Subject;
        ok.Value.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task EnviarPedido_SoapUnavailable_Returns503()
    {
        var request = PedidoTestDataFactory.ValidRequest();
        _soapMock.Setup(s => s.EnviarPedidoAsync(request))
            .ThrowsAsync(new HttpRequestException("no connection"));

        var result = await CreateController().EnviarPedido(request);

        result.Should().BeOfType<ObjectResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status503ServiceUnavailable);
    }

    [Fact]
    public async Task EnviarPedido_InvalidSoapResponse_Returns503()
    {
        var request = PedidoTestDataFactory.ValidRequest();
        _soapMock.Setup(s => s.EnviarPedidoAsync(request))
            .ThrowsAsync(new InvalidOperationException("invalid xml"));

        var result = await CreateController().EnviarPedido(request);

        result.Should().BeOfType<ObjectResult>()
            .Which.StatusCode.Should().Be(StatusCodes.Status503ServiceUnavailable);
    }

    [Fact]
    public async Task EnviarPedidoMock_HappyPath_AlwaysReturnsTheFixedResponse()
    {
        var request = PedidoTestDataFactory.ValidRequest(dto => dto.NumPedido = "another-order");

        var result = await CreateController().EnviarPedidoMock(request);

        var ok = result.Should().BeOfType<OkObjectResult>().Subject;
        ok.Value.Should().BeEquivalentTo(PedidoTestDataFactory.ValidResponse());
    }
}
