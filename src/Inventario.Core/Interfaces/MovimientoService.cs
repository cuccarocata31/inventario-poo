using Inventario.Core.DTOs;
using Inventario.Core.Entities;
using Inventario.Core.Exceptions;
using Inventario.Core.Interfaces;

namespace Inventario.Core.Services;

public class MovimientoService : IMovimientoService
{
    private readonly IProductoRepository _productos;
    private readonly IMovimientoRepository _movimientos;
    private readonly IUnitOfWork _unitOfWork;

    public MovimientoService(IProductoRepository productos, IMovimientoRepository movimientos, IUnitOfWork unitOfWork)
    {
        _productos = productos;
        _movimientos = movimientos;
        _unitOfWork = unitOfWork;
    }

    public Task<MovimientoDto> RegistrarEntradaAsync(int productoId, RegistrarMovimientoDto dto) =>
        RegistrarAsync(productoId, p => p.RegistrarEntrada(dto.Cantidad, dto.Observacion));

    public Task<MovimientoDto> RegistrarSalidaAsync(int productoId, RegistrarMovimientoDto dto) =>
        RegistrarAsync(productoId, p => p.RegistrarSalida(dto.Cantidad, dto.Observacion));

    public async Task<IReadOnlyList<MovimientoDto>> ObtenerHistorialAsync(int productoId)
    {
        _ = await _productos.ObtenerPorIdAsync(productoId)
            ?? throw new ProductoNoEncontradoException(productoId);

        var movimientos = await _movimientos.ObtenerPorProductoAsync(productoId);
        return movimientos.Select(m => m.ADto()).ToList();
    }

    // Entrada y salida comparten el flujo: no hay código duplicado
    private async Task<MovimientoDto> RegistrarAsync(int productoId, Func<Producto, Movimiento> operacion)
    {
        var producto = await _productos.ObtenerPorIdAsync(productoId)
            ?? throw new ProductoNoEncontradoException(productoId);

        var movimiento = operacion(producto);       // acá se valida el stock
        _movimientos.Agregar(movimiento);
        await _unitOfWork.GuardarCambiosAsync();    // stock + movimiento, juntos

        return movimiento.ADto();
    }
}