using System.ComponentModel.DataAnnotations;

namespace Inventario.Core.DTOs;

public class CrearProductoDto
{
    [Required, StringLength(100, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [Range(0, int.MaxValue)]
    public int StockMinimo { get; set; }
}

public class ActualizarProductoDto
{
    [Required, StringLength(100, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [Range(0, int.MaxValue)]
    public int StockMinimo { get; set; }
}

public class RegistrarMovimientoDto
{
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero.")]
    public int Cantidad { get; set; }

    [StringLength(250)]
    public string? Observacion { get; set; }
}

public record ProductoDto(int Id, string Nombre, int StockActual, int StockMinimo, bool StockBajo);

public record StockDto(int ProductoId, string Nombre, int StockActual);

public record MovimientoDto(int Id, int ProductoId, string Tipo, int Cantidad, DateTime Fecha, string? Observacion);