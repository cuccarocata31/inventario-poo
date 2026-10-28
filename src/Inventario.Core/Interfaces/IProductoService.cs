using Inventario.Core.DTOs;

namespace Inventario.Core.Interfaces;

public interface IProductoService
{
    Task<IReadOnlyList<ProductoDto>> ObtenerTodosAsync();
    Task<ProductoDto> ObtenerPorIdAsync(int id);
    Task<ProductoDto> CrearAsync(CrearProductoDto dto);
    Task<ProductoDto> ActualizarAsync(int id, ActualizarProductoDto dto);
    Task EliminarAsync(int id);
    Task<StockDto> ObtenerStockAsync(int id);
    Task<IReadOnlyList<ProductoDto>> ObtenerConStockBajoAsync(int? limite);
}