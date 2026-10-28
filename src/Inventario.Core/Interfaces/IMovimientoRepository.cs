using Inventario.Core.Entities;

namespace Inventario.Core.Interfaces;

public interface IMovimientoRepository
{
    Task<IReadOnlyList<Movimiento>> ObtenerPorProductoAsync(int productoId);
    Task<bool> ExistenParaProductoAsync(int productoId);
    void Agregar(Movimiento movimiento);
}