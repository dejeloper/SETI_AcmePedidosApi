using AcmePedidosApi.Models.Rest;

public interface ISoapService
{
    Task<EnviarPedidoResponse> EnviarPedidoAsync(EnviarPedidoRequest request);
}