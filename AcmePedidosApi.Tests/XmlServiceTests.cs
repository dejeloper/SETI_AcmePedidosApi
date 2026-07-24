using AcmePedidosApi.Services;
using FluentAssertions;
using Xunit;

namespace AcmePedidosApi.Tests;

public class XmlServiceTests
{
    private readonly XmlService _xml = new();

    [Fact]
    public void CrearSoapRequest_HappyPath_MapsFieldsAccordingToTheContract()
    {
        var request = PedidoTestDataFactory.ValidRequest();

        var xml = _xml.CrearSoapRequest(request);

        xml.Should().Contain("<pedido>75630275</pedido>");
        xml.Should().Contain("<Cantidad>1</Cantidad>");
        xml.Should().Contain("<EAN>00110000765191002104587</EAN>");
        xml.Should().Contain("<Producto>Armario INVAL</Producto>");
        xml.Should().Contain("<Cedula>1113987400</Cedula>");
        xml.Should().Contain("<Direccion>CR 72B 45 12 APT 301</Direccion>");
    }

    [Fact]
    public void CrearSoapRequest_WithSpecialCharacters_EscapesThemInsteadOfBreakingTheXml()
    {
        var request = PedidoTestDataFactory.ValidRequest(dto => dto.Direccion = "CL 1 <2> & \"3\"");

        var xml = _xml.CrearSoapRequest(request);

        xml.Should().Contain("<Direccion>CL 1 &lt;2&gt; &amp; \"3\"</Direccion>");
    }

    [Fact]
    public void LeerSoapResponse_HappyPath_MapsCodigoAndMensaje()
    {
        var response = _xml.LeerSoapResponse(PedidoTestDataFactory.ValidResponseXml);

        response.Should().BeEquivalentTo(PedidoTestDataFactory.ValidResponse());
    }

    [Fact]
    public void LeerSoapResponse_WithoutCodigoOrMensaje_ReturnsEmptyFields()
    {
        const string xmlWithoutData = """
            <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/">
              <soapenv:Body>
                <EnvioPedidoAcmeResponse />
              </soapenv:Body>
            </soapenv:Envelope>
            """;

        var response = _xml.LeerSoapResponse(xmlWithoutData);

        response.EnviarPedidoRespuesta.CodigoEnvio.Should().BeEmpty();
        response.EnviarPedidoRespuesta.Estado.Should().BeEmpty();
    }

    [Fact]
    public void LeerSoapResponse_InvalidXml_ThrowsXmlException()
    {
        const string invalidXml = "<soapenv:Envelope";

        var action = () => _xml.LeerSoapResponse(invalidXml);

        action.Should().Throw<System.Xml.XmlException>();
    }
}
