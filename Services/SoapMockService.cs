using AcmePedidosApi.Models.Rest;

namespace AcmePedidosApi.Services;

public class SoapMockService(XmlService xml) : ISoapService
{
    private readonly XmlService _xml = xml;

    private const string MockResponseXml = """
        <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/">
          <soapenv:Body>
            <EnvioPedidoAcmeResponse>
              <EnvioPedidoResponse>
                <Codigo>80375472</Codigo>
                <Mensaje>Entregado exitosamente al cliente</Mensaje>
              </EnvioPedidoResponse>
            </EnvioPedidoAcmeResponse>
          </soapenv:Body>
        </soapenv:Envelope>
        """;

    public Task<EnviarPedidoResponse> EnviarPedidoAsync(EnviarPedidoRequest request)
    {
        return Task.FromResult(_xml.LeerSoapResponse(MockResponseXml));
    }
}
