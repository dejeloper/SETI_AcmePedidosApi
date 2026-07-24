using AcmePedidosApi.Models.Rest;
using System.Text;
using System.Xml;

namespace AcmePedidosApi.Services
{
    public class SoapService(HttpClient http, XmlService xml) : ISoapService
    {
        private readonly HttpClient _http = http;
        private readonly XmlService _xml = xml;

        public async Task<EnviarPedidoResponse> EnviarPedidoAsync(EnviarPedidoRequest request)
        {
            var requestXml = _xml.CrearSoapRequest(request);
            var content = new StringContent(requestXml, Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "http://WSDLs/EnvioPedidos/EnvioPedidosAcme/EnvioPedidoAcme");

            var response = await _http.PostAsync("", content);
            response.EnsureSuccessStatusCode();

            var responseXml = await response.Content.ReadAsStringAsync();

            try
            {
                return _xml.LeerSoapResponse(responseXml);
            }
            catch (XmlException ex)
            {
                throw new InvalidOperationException("La respuesta del servicio SOAP contiene un XML inválido.", ex);
            }
        }
    }
}
