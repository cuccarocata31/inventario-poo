using Inventario.Core.Enums;

namespace Inventario.Core.Entities;

public class Movimiento
{
    public int Id { get; private set; }
    public int ProductoId { get; private set; }
    public TipoMovimiento Tipo { get; private set; }
    public int Cantidad { get; private set; }
    public DateTime Fecha { get; private set; }
    public string? Observacion { get; private set; }

    private Movimiento() { }   // lo usa EF Core

    // internal: solo Producto puede crear movimientos
    internal Movimiento(int productoId, TipoMovimiento tipo, int cantidad, string? observacion)
    {
        ProductoId = productoId;
        Tipo = tipo;
        Cantidad = cantidad;
        Fecha = DateTime.UtcNow;
        Observacion = observacion?.Trim();
    }
}