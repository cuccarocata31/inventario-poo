using Inventario.Core.Entities;

namespace Inventario.Core.Interfaces;

public interface IProductoRepository
{
    Task<Producto?> ObtenerPorIdAsync(int id);
    Task<IReadOnlyList<Producto>> ObtenerTodosAsync();
    Task<IReadOnlyList<Producto>> ObtenerConStockBajoAsync(int? limite);
    void Agregar(Producto producto);
    void Eliminar(Producto producto);
}