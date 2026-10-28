using Inventario.Core.Entities;
using Inventario.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Data.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly InventarioDbContext _context;

    public ProductoRepository(InventarioDbContext context) => _context = context;

    public async Task<Producto?> ObtenerPorIdAsync(int id) =>
        await _context.Productos.FindAsync(id);

    public async Task<IReadOnlyList<Producto>> ObtenerTodosAsync() =>
        await _context.Productos.AsNoTracking().OrderBy(p => p.Nombre).ToListAsync();

    // Con límite: "menos de X unidades". Sin límite: usa el StockMinimo de cada producto.
    public async Task<IReadOnlyList<Producto>> ObtenerConStockBajoAsync(int? limite)
    {
        var consulta = _context.Productos.AsNoTracking();

        consulta = limite.HasValue
            ? consulta.Where(p => p.StockActual < limite.Value)
            : consulta.Where(p => p.StockActual < p.StockMinimo);

        return await consulta.OrderBy(p => p.StockActual).ToListAsync();
    }

    public void Agregar(Producto producto) => _context.Productos.Add(producto);

    public void Eliminar(Producto producto) => _context.Productos.Remove(producto);
}