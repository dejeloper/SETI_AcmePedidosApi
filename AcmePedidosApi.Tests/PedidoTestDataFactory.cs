using AcmePedidosApi.Models.Rest;

namespace AcmePedidosApi.Tests;

public static class PedidoTestDataFactory
{
    public const string ValidResponseXml = """
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

    public static EnviarPedidoRequest ValidRequest(Action<PedidoDto>? customize = null)
    {
        var dto = new PedidoDto
        {
            NumPedido = "75630275",
            CantidadPedido = 1,
            CodigoEAN = "00110000765191002104587",
            NombreProducto = "Armario INVAL",
            NumDocumento = "1113987400",
            Direccion = "CR 72B 45 12 APT 301"
        };

        customize?.Invoke(dto);

        return new EnviarPedidoRequest { EnviarPedido = dto };
    }

    public static EnviarPedidoResponse ValidResponse()
    {
        return new EnviarPedidoResponse
        {
            EnviarPedidoRespuesta = new PedidoRespuesta
            {
                CodigoEnvio = "80375472",
                Estado = "Entregado exitosamente al cliente"
            }
        };
    }
}
