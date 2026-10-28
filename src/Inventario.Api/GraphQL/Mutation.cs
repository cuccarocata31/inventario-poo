using HotChocolate;
using Inventario.Core.DTOs;
using Inventario.Core.Interfaces;

namespace Inventario.Api.GraphQL;

public class Mutation
{
    public Task<ProductoDto> CrearProducto(string nombre, int stockMinimo, [Service] IProductoService service) =>
        service.CrearAsync(new CrearProductoDto { Nombre = nombre, StockMinimo = stockMinimo });

    public Task<MovimientoDto> RegistrarEntrada(int productoId, int cantidad, string? observacion, [Service] IMovimientoService service) =>
        service.RegistrarEntradaAsync(productoId, new RegistrarMovimientoDto { Cantidad = cantidad, Observacion = observacion });

    public Task<MovimientoDto> RegistrarSalida(int productoId, int cantidad, string? observacion, [Service] IMovimientoService service) =>
        service.RegistrarSalidaAsync(productoId, new RegistrarMovimientoDto { Cantidad = cantidad, Observacion = observacion });
}