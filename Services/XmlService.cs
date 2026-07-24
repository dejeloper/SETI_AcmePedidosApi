using System.Xml.Linq;
using AcmePedidosApi.Models.Rest;
namespace AcmePedidosApi.Services;
public class XmlService
{
    public string CrearSoapRequest(EnviarPedidoRequest request)
    {
        XNamespace soap = "http://schemas.xmlsoap.org/soap/envelope/";
        XNamespace env = "http://WSDLs/EnvioPedidos/EnvioPedidosAcme";
        var xml = new XDocument(
            new XElement(soap + "Envelope",
                new XAttribute(XNamespace.Xmlns + "soapenv", soap),
                new XAttribute(XNamespace.Xmlns + "env", env),
                new XElement(soap + "Header"),
                new XElement(soap + "Body",
                    new XElement(env + "EnvioPedidoAcme",
                        new XElement("EnvioPedidoRequest",
                            new XElement("pedido", request.EnviarPedido.NumPedido),
                            new XElement("Cantidad", request.EnviarPedido.CantidadPedido),
                            new XElement("EAN", request.EnviarPedido.CodigoEAN),
                            new XElement("Producto", request.EnviarPedido.NombreProducto),
                            new XElement("Cedula", request.EnviarPedido.NumDocumento),
                            new XElement("Direccion", request.EnviarPedido.Direccion)
                        )
                        )
                    )
                )
            );

        return xml.ToString();
    }

    public EnviarPedidoResponse LeerSoapResponse(string xml)
    {
        var document = XDocument.Parse(xml);
        var codigo = document.Descendants().FirstOrDefault(x => x.Name.LocalName == "Codigo")?.Value;
        var mensaje = document.Descendants().FirstOrDefault(x => x.Name.LocalName == "Mensaje")?.Value;

        return new EnviarPedidoResponse
        {
            EnviarPedidoRespuesta = new PedidoRespuesta
            {
                CodigoEnvio = codigo ?? "",
                Estado = mensaje ?? ""
            }
        };
    }
}