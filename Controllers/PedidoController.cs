using Microsoft.AspNetCore.Mvc;
using AcmePedidosApi.Models.Rest;
using AcmePedidosApi.Services;

namespace AcmePedidosApi.Controllers;

[ApiController]
[Route("api/pedidos")]
public class PedidoController(ISoapService soap, SoapMockService mock) : ControllerBase
{
    private readonly ISoapService _soap = soap;
    private readonly SoapMockService _mock = mock;

    [HttpPost]
    public async Task<IActionResult> EnviarPedido(EnviarPedidoRequest request)
    {
        try
        {
            var response = await _soap.EnviarPedidoAsync(request);
            return Ok(response);
        }
        catch (HttpRequestException)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new { mensaje = "No fue posible comunicarse con el servicio SOAP." });
        }
        catch (InvalidOperationException)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, new { mensaje = "El servicio SOAP devolvió una respuesta inválida." });
        }
    }

    [HttpPost("mock")]
    public async Task<IActionResult> EnviarPedidoMock(EnviarPedidoRequest request)
    {
        var response = await _mock.EnviarPedidoAsync(request);
        return Ok(response);
    }
}
