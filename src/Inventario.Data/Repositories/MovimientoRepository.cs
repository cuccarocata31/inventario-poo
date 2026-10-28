using Inventario.Core.Entities;
using Inventario.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Data.Repositories;

public class MovimientoRepository : IMovimientoRepository
{
    private readonly InventarioDbContext _context;

    public MovimientoRepository(InventarioDbContext context) => _context = context;

    public async Task<IReadOnlyList<Movimiento>> ObtenerPorProductoAsync(int productoId) =>
        await _context.Movimientos.AsNoTracking()
            .Where(m => m.ProductoId == productoId)
            .OrderByDescending(m => m.Fecha).ThenByDescending(m => m.Id)
            .ToListAsync();

    public async Task<bool> ExistenParaProductoAsync(int productoId) =>
        await _context.Movimientos.AnyAsync(m => m.ProductoId == productoId);

    public void Agregar(Movimiento movimiento) => _context.Movimientos.Add(movimiento);
}