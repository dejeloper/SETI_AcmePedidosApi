using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AcmePedidosApi.Models.Rest;

public class PedidoDto
{
    [JsonPropertyName("numPedido")]
    [Required]
    public string NumPedido { get; set; } = string.Empty;

    [JsonPropertyName("cantidadPedido")]
    [Range(1, int.MaxValue)]
    public int CantidadPedido { get; set; }

    [JsonPropertyName("codigoEAN")]
    [Required]
    public string CodigoEAN { get; set; } = string.Empty;

    [JsonPropertyName("nombreProducto")]
    [Required]
    public string NombreProducto { get; set; } = string.Empty;

    [JsonPropertyName("numDocumento")]
    [Required]
    public string NumDocumento { get; set; } = string.Empty;

    [JsonPropertyName("direccion")]
    [Required]
    public string Direccion { get; set; } = string.Empty;
}
