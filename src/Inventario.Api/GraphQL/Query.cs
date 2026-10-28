using HotChocolate;
using Inventario.Core.DTOs;
using Inventario.Core.Interfaces;

namespace Inventario.Api.GraphQL;

public class Query
{
    public Task<IReadOnlyList<ProductoDto>> GetProductos([Service] IProductoService service) =>
        service.ObtenerTodosAsync();

    public Task<ProductoDto> GetProducto(int id, [Service] IProductoService service) =>
        service.ObtenerPorIdAsync(id);

    public Task<IReadOnlyList<ProductoDto>> GetProductosConStockBajo(int? limite, [Service] IProductoService service) =>
        service.ObtenerConStockBajoAsync(limite);

    public Task<IReadOnlyList<MovimientoDto>> GetHistorialDeMovimientos(int productoId, [Service] IMovimientoService service) =>
        service.ObtenerHistorialAsync(productoId);
}