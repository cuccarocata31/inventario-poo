using Inventario.Core.DTOs;
using Inventario.Core.Entities;
using Inventario.Core.Exceptions;
using Inventario.Core.Interfaces;

namespace Inventario.Core.Services;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _productos;
    private readonly IMovimientoRepository _movimientos;
    private readonly IUnitOfWork _unitOfWork;

    public ProductoService(IProductoRepository productos, IMovimientoRepository movimientos, IUnitOfWork unitOfWork)
    {
        _productos = productos;
        _movimientos = movimientos;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ProductoDto>> ObtenerTodosAsync()
    {
        var productos = await _productos.ObtenerTodosAsync();
        return productos.Select(p => p.ADto()).ToList();
    }

    public async Task<ProductoDto> ObtenerPorIdAsync(int id) =>
        (await ObtenerEntidadAsync(id)).ADto();

    public async Task<ProductoDto> CrearAsync(CrearProductoDto dto)
    {
        var producto = new Producto(dto.Nombre, dto.StockMinimo);
        _productos.Agregar(producto);
        await _unitOfWork.GuardarCambiosAsync();
        return producto.ADto();
    }

    public async Task<ProductoDto> ActualizarAsync(int id, ActualizarProductoDto dto)
    {
        var producto = await ObtenerEntidadAsync(id);
        producto.Actualizar(dto.Nombre, dto.StockMinimo);
        await _unitOfWork.GuardarCambiosAsync();
        return producto.ADto();
    }

    public async Task EliminarAsync(int id)
    {
        var producto = await ObtenerEntidadAsync(id);

        if (await _movimientos.ExistenParaProductoAsync(id))
            throw new ReglaDeNegocioException("No se puede eliminar un producto que tiene historial de movimientos.");

        _productos.Eliminar(producto);
        await _unitOfWork.GuardarCambiosAsync();
    }

    public async Task<StockDto> ObtenerStockAsync(int id)
    {
        var producto = await ObtenerEntidadAsync(id);
        return new StockDto(producto.Id, producto.Nombre, producto.StockActual);
    }

    public async Task<IReadOnlyList<ProductoDto>> ObtenerConStockBajoAsync(int? limite)
    {
        var productos = await _productos.ObtenerConStockBajoAsync(limite);
        return productos.Select(p => p.ADto()).ToList();
    }

    private async Task<Producto> ObtenerEntidadAsync(int id) =>
        await _productos.ObtenerPorIdAsync(id) ?? throw new ProductoNoEncontradoException(id);
}