using Inventario.Core.DTOs;

namespace Inventario.Core.Interfaces;

public interface IMovimientoService
{
    Task<MovimientoDto> RegistrarEntradaAsync(int productoId, RegistrarMovimientoDto dto);
    Task<MovimientoDto> RegistrarSalidaAsync(int productoId, RegistrarMovimientoDto dto);
    Task<IReadOnlyList<MovimientoDto>> ObtenerHistorialAsync(int productoId);
}