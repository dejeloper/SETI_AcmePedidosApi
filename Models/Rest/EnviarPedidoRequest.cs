using System.Text.Json.Serialization;

namespace AcmePedidosApi.Models.Rest;

public class EnviarPedidoRequest
{
    [JsonPropertyName("enviarPedido")]
    public PedidoDto EnviarPedido { get; set; } = new();
}