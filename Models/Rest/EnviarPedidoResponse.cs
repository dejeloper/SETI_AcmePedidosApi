namespace AcmePedidosApi.Models.Rest;

public class EnviarPedidoResponse
{
    public PedidoRespuesta EnviarPedidoRespuesta { get; set; } = new();
}

public class PedidoRespuesta
{
    public string CodigoEnvio { get; set; } = string.Empty;

    public string Estado { get; set; } = string.Empty;
}